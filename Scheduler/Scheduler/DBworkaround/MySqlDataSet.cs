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
        public MySqlDataSet(string connectionString = "", bool initViews = false)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                connectionString = Properties.Settings.Default.ConnectionString;
            connection = new MySqlConnection(Properties.Settings.Default.ConnectionString);
            mainDataSet = new DataSet("kvartetDataSet");
            viewDataSet = new DataSet("kvartetViewSet");

            GetTables(initViews);

            caches = new QueryCache();

        }
        /*
         * Способ Fill хорошо работает на малых данных, но, возможно, потребует больших ресурсов на больших, надо тестировать.
         * Судя по принципу работы DataSet - память зажирать будет капитально. DataSet очень удобно для разработки - к нему прям льньком можно from select и т.д. делать. 
         * Потом надо эти линьки переделать в обычные запросы на SQL и захардкодить. Таким образом, думаю, получится избежать неограниченного потребления памяти.
         */
        private void GetTables(bool initViews = false)
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

                if (reader.GetString(1) == "VIEW")
                {
                    if (initViews)
                    {
                        tableAdapters.Add(curTableOrView, curDataAdapter);
                        viewNames.Add(curTableOrView);
                    }
                }
                else
                {
                    MySqlCommandBuilder dummy = new MySqlCommandBuilder(curDataAdapter);
                    tableAdapters.Add(curTableOrView, curDataAdapter);
                    tablesNames.Add(curTableOrView);
                }


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

        public void UpdateTable(string tablename)
        {
            tableAdapters[tablename].Update(mainDataSet, tablename);
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


            var query = mainDataSet.Tables["receptioncards"].AsEnumerable()
                .Where(ent => ent.Field<DateTime>("receptionDate") == date);


            foreach (var row in query)
            {
                Entity currentEntity = caches.EntitiesCache.Find(x => x.id == (ulong)row["id"]);
                if (currentEntity == default(Entity))
                {
                    TimeSpan startTime = (TimeSpan)row["startTime"];
                    TimeSpan endTime = (TimeSpan)row["endTime"];
                    TimeInterval currentTimeInterval = new TimeInterval(date.Add(startTime), date.Add(endTime));

                    //                     var a1 = (ulong)row["id"];
                    //                     var a2 = currentTimeInterval;
                    //var c3 = (row["Client"]).GetType().ToString();
                    //var b3 = (UInt16)row["Client"];

                    //                     var a3 = SelectClientById((UInt16)row["Client"]);
                    //                     var a4 = SelectSpecialistById((UInt16)row["Specialist"]);
                    //                     var a5 = SelectSpecializationById((byte)row["Specialization"]);
                    //                     var a6 = SelectCabinetById((UInt16)row["Cabinet"]);

                    //currentEntity = new Entity(a1,a2,a3,a4,a5,a6);
                    currentEntity = new Entity
                    (
                        (ulong)row["id"],
                        currentTimeInterval,
                        SelectClientById((UInt16)row["Client"]),
                        SelectSpecialistById((UInt16)row["Specialist"]),
                        SelectSpecializationById((byte)row["Specialization"]),
                        SelectCabinetById((UInt16)row["Cabinet"])
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
                    SelectClientById((UInt16)row["Client"]),
                    SelectSpecialistById((UInt16)row["Specialist"]),
                    SelectSpecializationById((byte)row["Specialization"]),
                    SelectCabinetById((UInt16)row["Cabinet"])
                );
            }
            return currentEntity;
        }

        public ClientCard SelectClientById(UInt16 id)
        {
            ClientCard cached = caches.ClientCache.Find(x => x.id == id);
            if (cached == default(ClientCard))
            {
                caches.clientFullfilled = false;
                cached = SelectAllClients().Find(x => x.id == id);
            }
            return cached;
        }

        public SpecialistCard SelectSpecialistById(UInt16 id)
        {
            SpecialistCard cached = caches.SpecialistCache.Find(x => x.id == id);
            if (cached == default(SpecialistCard))
            {
                caches.specialistFullfilled = false;
                cached = SelectAllSpecialists().Find(x => x.id == id);
            }
            return cached;
        }

        public FIO SelectFIObyId(UInt16 id)
        {
            FIO cached;// = caches.FioCache.Find(x => x.id == id);
            //             if (cached == default(FIO))
            //             {
            //var a3 = 

            var fioRow = mainDataSet.Tables["fio"].AsEnumerable()
                        .Where(fio => fio.Field<UInt16>("id") == id)
                        .First();

            cached = new FIO((string)fioRow["Name"], (string)fioRow["Surname"], (string)fioRow["Patronimyc"], (UInt16)fioRow["id"]);
            //  caches.FioCache.Add(cached);
            //}
            return cached;
        }

        public Specialization SelectSpecializationById(byte id)
        {
            Specialization cached = caches.SpecializationCache.Find(x => x.id == id);
            if (cached == default(Specialization))
            {
                caches.specializationFullfilled = false;
                cached = SelectAllSpecializations().Find(x => x.id == id);
            }
            return cached;
        }

        public CabinetCard SelectCabinetById(UInt16 id)
        {
            CabinetCard cached = caches.CabinetCache.Find(x => x.id == id);
            if (cached == default(CabinetCard))
            {
                caches.cabinetFullfilled = false;
                cached = SelectAllCabinets().Find(x => x.id == id);
            }
            return cached;
        }

        //List<Specialization> allSpecs; //если реализуется queryCache - это удалить.
        //Кэш специализаций инициализируется только здесь - всегда полон. Т.о. первая проверка валидна.
        public List<Specialization> SelectAllSpecializations()
        {
            if (caches.specializationFullfilled) return caches.SpecializationCache;
            //allSpecs = new List<Specialization>();
            var list = caches.SpecializationCache;
            var querySpecializations = mainDataSet.Tables["specializations"].AsEnumerable();
            foreach (var specRow in querySpecializations)
            {
                list.Add(new Specialization((string)specRow["Specialization"], (byte)specRow["id"]));
            }
            caches.specializationFullfilled = true;
            return list;
        }

        public List<SpecialistCard> SelectAllSpecialists()
        {
            if (caches.specialistFullfilled) return caches.SpecialistCache;

            var listCached = caches.SpecialistCache;
            listCached.Clear();

            var specialistQuery = mainDataSet.Tables["specialist"].AsEnumerable();
            foreach (var specialistRow in specialistQuery)
            {
                listCached.Add(new SpecialistCard(
                    SelectFIObyId((UInt16)specialistRow["id_FIO"]),
                    Specialization.GetSpecializationsFromULong((UInt64)specialistRow["SpecializationList"], SelectAllSpecializations()),
                    (UInt16)specialistRow["id"]
                    )
                    );
            }

            caches.specialistFullfilled = true;
            return listCached;
        }

        public List<CabinetCard> SelectAllCabinets()
        {
            if (caches.cabinetFullfilled) return caches.CabinetCache;
            var list = caches.CabinetCache;
            list.Clear();
            var queryCabinets = mainDataSet.Tables["cabinets"].AsEnumerable();
            foreach (var cabRow in queryCabinets)
            {
                list.Add(new CabinetCard((string)cabRow["Name"], (UInt16)cabRow["id"]));
            }
            caches.cabinetFullfilled = true;
            return list;
        }

        public List<ClientCard> SelectAllClients()
        {
            if (caches.clientFullfilled) return caches.ClientCache;
            var list = caches.ClientCache;
            list.Clear();

            var clientQuery = mainDataSet.Tables["clients"].AsEnumerable();
            foreach (var clientRow in clientQuery)
            {
                list.Add(new ClientCard(
                SelectFIObyId((UInt16)clientRow["id_FIO"]),
                (UInt64)clientRow["TelNumber"],
                (string)clientRow["Comment"],
                Convert.ToBoolean(clientRow["inRedList"]),
                (UInt16)clientRow["id"])
                );
            }
            caches.clientFullfilled = true;
            return list;
        }

        /// <summary>
        /// Класс - идея кешировать активно пользуемое в списки как более быстрый доступ нежели в таблицу. Сгодится как для dataset, так и для чистого sql.
        /// С другой стороны, если мы в бд добавляем запись, надо говорить о неактуальном кеше...
        /// </summary>
        class QueryCache
        {
            private List<ClientCard> clientCache; //при вызове формы редактирования нужен полный список
            private List<SpecialistCard> specialistCache; //при вызове формы редактирования нужен полный список
            private List<Specialization> specializationCache; //при вызове формы редактирования нужен полный список
            private List<CabinetCard> cabinetCache; //при вызове формы редактирования нужен полный список
            //private List<FIO> fioCache; //из-за полных clientCache и specialistCache надобность в этом отпадает.
            private List<Entity> entitiesCache;

            public bool clientFullfilled;
            public bool specialistFullfilled;
            public bool specializationFullfilled;
            public bool cabinetFullfilled;

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
            //             public List<FIO> FioCache
            //             {
            //                 get { return fioCache; }
            //                 set { fioCache = value; }
            //             }
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
                //fioCache = new List<FIO>();
                entitiesCache = new List<Entity>();

                clientFullfilled = false;
                specialistFullfilled = false;
                specializationFullfilled = false;
                cabinetFullfilled = false;
            }

            public void ClearCaches()
            {
                //clientCache.Clear();
                //specialistCache.Clear();
                //specializationCache.Clear();
                //cabinetCache.Clear();
                //fioCache.Clear();
                entitiesCache.Clear();
            }

        }
        #endregion
    }
}
