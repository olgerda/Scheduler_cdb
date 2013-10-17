using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Scheduler
{

    /*
     * FOR TESTING PURPOSES! May not properly work with large amount of data!
     */
    public class MySqlDataSet
    {
        MySqlConnection connection;
        DataSet mainDataSet;
        DataSet viewDataSet;
        Dictionary<string, MySqlDataAdapter> tableAdapters;

        QueryCache caches;

        public DataSet MainDataSet
        {
            get { return mainDataSet; }
        }

        public DataSet ViewDataSet
        {
            get { return viewDataSet; }
        }

        #region Инициализация DataSet'ов
        public MySqlDataSet(string connectionString = "")
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                connectionString = Properties.Settings.Default.ConnectionString;
            connection = new MySqlConnection(Properties.Settings.Default.ConnectionString);
            mainDataSet = new DataSet("kvartetDataSet");
            viewDataSet = new DataSet("kvartetViewSet");

            GetTables();

            caches = new QueryCache();

        }
        /*
         * Способ Fill хорошо работает на малых данных, но, возможно, потребует больших ресурсов на больших, надо тестировать.
         * Судя по принципу работы DataSet - память зажирать будет капитально. DataSet очень удобно для разработки - к нему прям льньком можно from select и т.д. делать. 
         * Потом надо эти линьки переделать в обычные запросы на SQL и захардкодить. Таким образом, думаю, получится избежать неограниченного потребления памяти.
         */
        private void GetTables()
        {
            tableAdapters = new Dictionary<string, MySqlDataAdapter>();
            string selectTablesQuery = "select TABLE_NAME, table_type from information_schema.tables where TABLE_SCHEMA='kvartet'";// AND TABLE_TYPE='BASE TABLE'";
            //string selectViewsQuery = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA='kvartet' AND TABLE_TYPE='VIEW'";
            string selectStatement = "select * from kvartet.";
            MySqlCommand sCmd = new MySqlCommand(selectTablesQuery, connection);
            sCmd.Connection.Open();
            var reader = sCmd.ExecuteReader();
            List<string> tablesNames = new List<string>();
            List<string> viewNames = new List<string>();
            while (reader.Read())
            {
                string curTableOrView = reader.GetString(0);
                MySqlCommand curCmd = new MySqlCommand(selectStatement + curTableOrView, connection);
                var curDataAdapter = new MySqlDataAdapter(curCmd);
                curDataAdapter.AcceptChangesDuringFill = true;
                curDataAdapter.AcceptChangesDuringUpdate = true;
                MySqlCommandBuilder dummy = new MySqlCommandBuilder(curDataAdapter);
                if (reader.GetString(1) == "VIEW")
                    viewNames.Add(curTableOrView);
                //curDataAdapter.Fill(viewDataSet, curTableOrView);
                else
                    tablesNames.Add(curTableOrView);
                //curDataAdapter.Fill(mainDataSet, curTableOrView);

                tableAdapters.Add(curTableOrView, curDataAdapter);
                //dummy.Dispose();//уничтожать нельзя, каким-то раком привязывается к адаптеру
            }
            reader.Close();
            sCmd.Connection.Close();
            sCmd.Connection.Dispose();

            foreach (var s in tablesNames)
            {
                tableAdapters[s].Fill(mainDataSet, s);
            }
            foreach (var s in viewNames)
            {
                tableAdapters[s].Fill(viewDataSet, s);
            }
        }

        public void UpdateTables()
        { //аццкоя магия обновления значений в БД
            foreach (DataTable table in mainDataSet.Tables)
            {
                tableAdapters[table.TableName].Update(mainDataSet, table.TableName);
            }
        }
        #endregion

        #region Вспомогательные запросы и выборки
        /*
        `receptioncards`.`id`,
        `receptioncards`.`Client`,
        `receptioncards`.`Specialist`,
        `receptioncards`.`Specialization`,
        `receptioncards`.`Cabinet`,
        `receptioncards`.`startTime`,
        `receptioncards`.`endTime`,
        `receptioncards`.`receptionDate`
         */
        public List<Entity> SelectEntityByDate(DateTime date)
        {
            List<Entity> result = new List<Entity>();
            date = date.Date; //обрежем время

            var query = mainDataSet.Tables["receptionscards"].AsEnumerable()
                .Where(ent => ent.Field<DateTime>("receptionDate") == date);
            foreach (var row in query)
            {
                Entity currentEntity = caches.EntitiesCache.Find(x => x.id == (ulong)row["id"]);
                if (currentEntity == default(Entity))
                {
                    TimeSpan startTime = (TimeSpan)row["startTime"];
                    TimeSpan endTime = (TimeSpan)row["endTime"];
                    TimeInterval currentTimeInterval = new TimeInterval(date.Add(startTime), date.Add(endTime));

                    currentEntity = new Entity
                    (
                        (ulong)row["id"],
                        currentTimeInterval,
                        SelectClientById((uint)row["Client"]),
                        SelectSpecialistById((uint)row["Specialist"]),
                        SelectSpecializationById((byte)row["Specialization"]),
                        SelectCabinetById((uint)row["Cabinet"])
                    );
                }
                result.Add(currentEntity);
            }
            return result;
        }

        public Entity SelectEntityById(ulong id)
        {
            Entity currentEntity = caches.EntitiesCache.Find(x => x.id == id);
            if (currentEntity == default(Entity))
            {
                var row = mainDataSet.Tables["receptionscards"].AsEnumerable()
                          .Where(ent => ent.Field<ulong>("id") == id).First();
                TimeSpan startTime = (TimeSpan)row["startTime"];
                TimeSpan endTime = (TimeSpan)row["endTime"];
                DateTime date = ((DateTime)row["receptionDate"]).Date;
                TimeInterval currentTimeInterval = new TimeInterval(date.Add(startTime), date.Add(endTime));

                currentEntity = new Entity
                (
                    id,
                    currentTimeInterval,
                    SelectClientById((uint)row["Client"]),
                    SelectSpecialistById((uint)row["Specialist"]),
                    SelectSpecializationById((byte)row["Specialization"]),
                    SelectCabinetById((uint)row["Cabinet"])
                );
            }
            return currentEntity;
        }

        public ClientCard SelectClientById(uint id)
        {
            ClientCard cached = caches.ClientCache.Find(x => x.id == id);
            if (cached == default(ClientCard))
            {
                var clientRow = mainDataSet.Tables["clients"].AsEnumerable()
                                .Where(client => client.Field<uint>("id") == id)
                                .First();

                cached = new ClientCard(
                SelectFIObyId((uint)clientRow["id_FIO"]),
                (ulong)clientRow["TelNumber"],
                (string)clientRow["Comment"],
                (bool)clientRow["inRedList"],
                id
                );
                caches.ClientCache.Add(cached);
            }
            return cached;
        }

        public SpecialistCard SelectSpecialistById(uint id)
        {
            SpecialistCard cached = caches.SpecialistCache.Find(x => x.id == id);
            if (cached == default(SpecialistCard))
            {
                var specialistRow = mainDataSet.Tables["specialist"].AsEnumerable()
                                    .Where(client => client.Field<uint>("id") == id)
                                    .First();

                cached = new SpecialistCard(
                SelectFIObyId((uint)specialistRow["id_FIO"]),
                Specialization.GetSpecializationsFromULong((ulong)specialistRow["SpecializationList"], SelectAllSpecializations()),
                id
                );
                caches.SpecialistCache.Add(cached);
            }
            return cached;
        }

        public FIO SelectFIObyId(uint id)
        {
            FIO cached = caches.FioCache.Find(x => x.id == id);
            if (cached == default(FIO))
            {
                var fioRow = mainDataSet.Tables["fio"].AsEnumerable()
                            .Where(fio => fio.Field<uint>("id") == id)
                            .First();

                cached = new FIO((string)fioRow["Name"], (string)fioRow["Surname"], (string)fioRow["Patronimyc"], (uint)fioRow["id"]);
                caches.FioCache.Add(cached);
            }
            return cached;
        }

        public Specialization SelectSpecializationById(byte id)
        {
            Specialization cached = caches.SpecializationCache.Find(x => x.id == id);
            if (cached == default(Specialization))
            {
                cached = SelectAllSpecializations().Find(x => x.id == id);
            }
            return cached;
        }

        public CabinetCard SelectCabinetById(uint id)
        {
            CabinetCard cached = caches.CabinetCache.Find(x => x.id == id);
            if (cached == default(CabinetCard))
            {
                var cabinetRow = mainDataSet.Tables["cabinets"].AsEnumerable()
                            .Where(cab => cab.Field<uint>("id") == id)
                            .First();

                cached = new CabinetCard((string)cabinetRow["Name"], (uint)cabinetRow["id"]);
                caches.CabinetCache.Add(cached);
            }
            return cached;
        }

        //List<Specialization> allSpecs; //если реализуется queryCache - это удалить.
        //Кэш специализаций инициализируется только здесь - всегда полон. Т.о. первая проверка валидна.
        public List<Specialization> SelectAllSpecializations()
        {
            if (caches.SpecializationCache.Count != 0) return caches.SpecializationCache;
            //allSpecs = new List<Specialization>();
            var list = caches.SpecializationCache;
            var querySpecializations = mainDataSet.Tables["specializations"].AsEnumerable();
            foreach (var specRow in querySpecializations)
            {
                list.Add(new Specialization((string)specRow["Title"], (byte)specRow["id"]));
            }
            return list;
        }

        public List<SpecialistCard> SelectAllSpecialists()
        {
            var specialistQuery = mainDataSet.Tables["specialist"].AsEnumerable();
            var listCached = caches.SpecialistCache;
            var listFull = new List<SpecialistCard>();
            foreach (var specialistRow in specialistQuery)
            {
                listFull.Add(new SpecialistCard(
                    SelectFIObyId((uint)specialistRow["id_FIO"]),
                    Specialization.GetSpecializationsFromULong((ulong)specialistRow["SpecializationList"], SelectAllSpecializations()),
                    (uint)specialistRow["id"]
                    )
                    );
            }

            //заполняем кеш до полного.
            foreach (var inFull in listFull)
            {
                if (!listCached.Any(x => x.Equals(inFull)))
                    listCached.Add(inFull);
            }
            return listCached;
        }

        /// <summary>
        /// Класс - идея кешировать активно пользуемое в списки как более быстрый доступ нежели в таблицу. Сгодится как для dataset, так и для чистого sql.
        /// </summary>
        class QueryCache
        {
            private List<ClientCard> clientCache;
            private List<SpecialistCard> specialistCache;
            private List<Specialization> specializationCache;
            private List<CabinetCard> cabinetCache;
            private List<FIO> fioCache;
            private List<Entity> entitiesCache;

            public List<ClientCard> ClientCache
            {
                get { return clientCache; }
                set { clientCache = value; }
            }
            public List<SpecialistCard> SpecialistCache
            {
                get { return specialistCache; }
                set { specialistCache = value; }
            }
            public List<Specialization> SpecializationCache
            {
                get { return specializationCache; }
                set { specializationCache = value; }
            }
            public List<CabinetCard> CabinetCache
            {
                get { return cabinetCache; }
                set { cabinetCache = value; }
            }
            public List<FIO> FioCache
            {
                get { return fioCache; }
                set { fioCache = value; }
            }
            public List<Entity> EntitiesCache
            {
                get { return entitiesCache; }
                set { entitiesCache = value; }
            }

            public QueryCache()
            {
                clientCache = new List<ClientCard>();
                specialistCache = new List<SpecialistCard>();
                specializationCache = new List<Specialization>();
                cabinetCache = new List<CabinetCard>();
                fioCache = new List<FIO>();
                entitiesCache = new List<Entity>();
            }

            public void ClearCaches()
            {
                clientCache.Clear();
                //specialistCache.Clear();
                //specializationCache.Clear();
                //cabinetCache.Clear();
                fioCache.Clear();
                entitiesCache.Clear();
            }

        }
        #endregion
    }
}
