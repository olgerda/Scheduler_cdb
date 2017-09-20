using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySqlConnector
{
    public class MySQLConnector : Scheduler_DBobjects_Intefraces.Scheduler_DBconnector
    {
        static string connectionString;

        public MySQLConnector(Scheduler_Common_Interfaces.IFactory entityFactor) : base (entityFactor)
        {           
        }

        static MySql.Data.MySqlClient.MySqlConnection OpenConnection()
        {
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                    case 1042:
                        throw new Exception("Не могу подключиться к серверу. Проверьте настройки сервера.", ex);
                    case 1045:
                        throw new Exception("Неверно введен логин или пароль.", ex);
                    default:
                        throw ex;
                }
            }
            return connection;
        }

        //Close connection
        private static void CloseConnection(MySql.Data.MySqlClient.MySqlConnection connection)
        {
            try
            {
                connection.Close();
                connection.Dispose();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
        }

        #region Clients and Telephones
        public override void AddClient(Scheduler_Controls_Interfaces.IClient client)
        {
            //clients columns are: idclients, name, comment, blacklisted, administrator
            //telephones columns: idtelephones, telephonescol
            //telephones2clients columns: idtelephones2clients, telid, clid
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                string query = @"insert into telephones (telephonescol) values (@tel) on duplicate key update idtelephones=LAST_INSERT_ID(idtelephones);";
                cmd.CommandText = query;
                cmd.Parameters.Add("@tel", MySql.Data.MySqlClient.MySqlDbType.String);
                List<long> listInsertedTelephonesId = new List<long>();
                foreach (var telnum in client.Telephones)
                {
                    cmd.Parameters["@tel"].Value = telnum;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    listInsertedTelephonesId.Add(cmd.LastInsertedId);
                }

                query = @"insert into clients (name, comment, blacklisted, administrator) values (@name, @comment, @blacklisted, @administrator)";
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@comment", client.Comment);
                cmd.Parameters.AddWithValue("@blacklisted", client.BlackListed ? 1 : 0);
                cmd.Parameters.AddWithValue("@administrator", client.Administrator);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                long InsertedClientId = cmd.LastInsertedId;
                client.ID = Convert.ToInt32(InsertedClientId);

                query = @"insert into telephones2clients (telid,clid) values (@tel, @cl)";
                cmd.Parameters.Clear();
                cmd.CommandText = query;
                cmd.Parameters.Add("@tel", MySql.Data.MySqlClient.MySqlDbType.Int32);
                cmd.Parameters.Add("@cl", MySql.Data.MySqlClient.MySqlDbType.Int32);
                foreach (var telid in listInsertedTelephonesId)
                {
                    cmd.Parameters["@tel"].Value = telid;
                    cmd.Parameters["@cl"].Value = InsertedClientId;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (client.GenerallyPrice != 0 || client.GenerallyTime != default(TimeSpan))
                {
                    query = @"insert into clientGenerallyParams (clientId, generallyTime, generallyPrice) values (@clId, @gT, @gP)";
                    cmd.CommandText = query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@clId", InsertedClientId);
                    cmd.Parameters.AddWithValue("@gT", client.GenerallyTime);
                    cmd.Parameters.AddWithValue("@gP", client.GenerallyPrice);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            CloseConnection(connection);
        }

        public override void RemoveClient(Scheduler_Controls_Interfaces.IClient client)
        {
            var connection = OpenConnection();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "delete from clients where idclients = @clid";
                cmd.Parameters.AddWithValue("@clid", client.ID);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException mex)
                {
                    CloseConnection(connection);
                    throw new Exception(mex.Number.ToString() + "\r\nОшибка удаления клиента из БД: " + mex.Message, mex);
                }
                cmd.CommandText = "call CleanupTelephones()";
                cmd.Parameters.Clear();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "delete from clientGenerallyParams where clientId = @clid";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@clid", client.ID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
            CloseConnection(connection);
        }

        private Scheduler_Controls_Interfaces.IClient GetClientById(int id, MySql.Data.MySqlClient.MySqlConnection existedConnection = null)
        {
            Scheduler_Controls_Interfaces.IClient result = null;
            if (id == 0)
                return result;
            var connection = existedConnection ?? OpenConnection();

            string query = "select idclients, name, comment, blacklisted, administrator from clients where idclients = @id";

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = EntityFactory.NewClient();
                        result.Name = reader.GetString("name");
                        result.ID = reader.GetInt32("idclients");
                        result.Comment = reader.GetString("comment");
                        result.BlackListed = reader.GetInt32("blacklisted") != 0;
                        result.Administrator = reader.GetString("administrator");
                    }
                }
                
                //result may be empty here!!!
                if (result == null)
                    return result;
                
                query = "select t.telephonescol from telephones t, telephones2clients t2c where t2c.clid = @clid and t2c.telid = t.idtelephones;";
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@clid", result.ID);
                cmd.Prepare();
                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Telephones.Add(reader.GetString("telephonescol"));
                    }
                }

                query = "select generallyTime, generallyPrice from clientGenerallyParams where clientId = @clId";
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@clId", result.ID);
                cmd.Prepare();
                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.GenerallyTime = reader.GetTimeSpan("generallyTime");
                        result.GenerallyPrice = reader.GetInt32("generallyPrice");
                    }
                }
            }
            if (existedConnection == null)
                CloseConnection(connection);

            return result;
        }

        public override void UpdateClientData(Scheduler_Controls_Interfaces.IClient client)
        {
            var oldClient = GetClientById(client.ID);
            if (oldClient == null)
                return;
            bool needUpdateName = oldClient.Name != client.Name;
            bool needUpdateComment = oldClient.Comment != client.Comment;
            bool needUpdateBL = oldClient.BlackListed != client.BlackListed;
            bool needUpdateAdministrator = oldClient.Administrator != client.Administrator;
            var telsOnlyInOld = oldClient.Telephones.Except(client.Telephones).ToList();
            bool needRemoveTelephones = telsOnlyInOld.Count > 0;
            var telsOnlyInNew = client.Telephones.Except(oldClient.Telephones).ToList();
            bool needAddTelephones = telsOnlyInNew.Count > 0;

            bool needUpdateGenerallyTime = oldClient.GenerallyTime != client.GenerallyTime;
            bool needUpdateGenerallyPrice = oldClient.GenerallyPrice != client.GenerallyPrice;

            var connection = OpenConnection();

            //clients columns are: idclients, name, comment, blacklisted, administrator
            //telephones columns: idtelephones, telephonescol
            //telephones2clients columns: idtelephones2clients, telid, clid
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("", connection))
            {
                if (needUpdateName)
                {
                    cmd.CommandText = "update clients set name = @name where idclients = @clId";
                    cmd.Parameters.AddWithValue("@name", client.Name);
                    cmd.Parameters.AddWithValue("@clId", client.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                if (needUpdateComment)
                {
                    cmd.CommandText = "update clients set comment = @comment where idclients = @clId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@comment", client.Comment);
                    cmd.Parameters.AddWithValue("@clId", client.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                if (needUpdateBL)
                {
                    cmd.CommandText = "update clients set blacklisted = @bl where idclients = @clId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@bl", client.BlackListed ? 1 : 0);
                    cmd.Parameters.AddWithValue("@clId", client.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                if (needUpdateAdministrator)
                {
                    cmd.CommandText = "update clients set administrator = @administrator where idclients = @clId";
                    cmd.Parameters.AddWithValue("@administrator", client.Administrator);
                    cmd.Parameters.AddWithValue("@clId", client.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                if (needAddTelephones)
                {
                    string query = @"insert into telephones (telephonescol) values (@tel) on duplicate key update idtelephones=LAST_INSERT_ID(idtelephones);";
                    cmd.CommandText = query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@tel", MySql.Data.MySqlClient.MySqlDbType.String);
                    List<long> listInsertedTelephonesId = new List<long>();
                    foreach (var telnum in telsOnlyInNew)
                    {
                        cmd.Parameters["@tel"].Value = telnum;
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                        listInsertedTelephonesId.Add(cmd.LastInsertedId);
                    }

                    query = @"insert into telephones2clients (telid,clid) values (@tel, @cl)";
                    cmd.Parameters.Clear();
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@tel", MySql.Data.MySqlClient.MySqlDbType.Int32);
                    cmd.Parameters.AddWithValue("@cl", client.ID);
                    foreach (var telid in listInsertedTelephonesId)
                    {
                        cmd.Parameters["@tel"].Value = telid;
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
                if (needRemoveTelephones)
                {
                    cmd.CommandText = "select idtelephones from telephones where telephonescol = @telnum";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@telnum", MySql.Data.MySqlClient.MySqlDbType.String);
                    List<Int32> listOfIds = new List<Int32>();
                    foreach (var telnum in telsOnlyInOld)
                    {
                        cmd.Parameters["@telnum"].Value = telnum;
                        cmd.Prepare();
                        listOfIds.Add(Convert.ToInt32(cmd.ExecuteScalar()));
                    }
                    cmd.CommandText = "delete from telephones2clients where telid = @telid and clid = @clid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@clid", client.ID);
                    cmd.Parameters.Add("@telid", MySql.Data.MySqlClient.MySqlDbType.Int32);
                    foreach (var id in listOfIds)
                    {
                        cmd.Parameters["@telid"].Value = id;
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "call CleanupTelephones()";
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateGenerallyPrice || needUpdateGenerallyTime)
                {
                    //cmd.CommandText = "update clientGenerallyParams set generallyTime = @gT, generallyPrice= @gP where clientId = @clId";
                    cmd.CommandText = "insert into clientGenerallyParams (generallyTime, generallyPrice, clientId) values (@gT, @gP, @clId) on duplicate key update idclientGenerallyParams=LAST_INSERT_ID(idclientGenerallyParams);";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@gT", client.GenerallyTime);
                    cmd.Parameters.AddWithValue("@gP", client.GenerallyPrice);
                    cmd.Parameters.AddWithValue("@clId", client.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }

            CloseConnection(connection);
        }

        protected override Scheduler_Forms_Interfaces.IClientList AllClientsInternal()
        {
            List<Scheduler_Controls_Interfaces.IClient> clientList = new List<Scheduler_Controls_Interfaces.IClient>();
            //columns are: idclients, name, comment, blacklisted
            var connection = OpenConnection();

            string query = "select idclients, name, comment, blacklisted, administrator from clients";

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {

                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Scheduler_Controls_Interfaces.IClient current = EntityFactory.NewClient();
                        current.Name = reader.GetString("name");
                        current.ID = reader.GetInt32("idclients");
                        current.Comment = reader.GetString("comment");
                        current.BlackListed = reader.GetInt32("blacklisted") != 0;
                        current.Administrator = reader.GetString("administrator");
                        clientList.Add(current);
                    }
                }

                query = "select t.telephonescol from telephones t, telephones2clients t2c where t2c.clid = @clid and t2c.telid = t.idtelephones;";
                cmd.CommandText = query;
                cmd.Parameters.Add("@clid", MySql.Data.MySqlClient.MySqlDbType.Int32);

                foreach (var client in clientList)
                {
                    cmd.Parameters["@clid"].Value = client.ID;
                    cmd.Prepare();
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            client.Telephones.Add(reader.GetString("telephonescol"));
                        }
                    }
                }

                query = "select generallyTime, generallyPrice from clientGenerallyParams where clientId = @clId;";
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@clId", MySql.Data.MySqlClient.MySqlDbType.Int32);

                foreach (var client in clientList)
                {
                    cmd.Parameters["@clId"].Value = client.ID;
                    cmd.Prepare();
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client.GenerallyTime = reader.GetTimeSpan("generallyTime");
                            client.GenerallyPrice = reader.GetInt32("generallyPrice");
                        }
                    }
                }


            }
            CloseConnection(connection);

            //var result = EntityFactory.NewClientList();
            //result.List.AddRange(clientList);
            //result.OnItemAdded += ListItemAddHandler;
            //result.OnItemRemoved += ListItemRemoveHandler;
            //result.OnItemChanged += ListItemChangedHandler;
            //return result;
            return AllClients(clientList, ListItemAddHandler, ListItemChangedHandler, ListItemRemoveHandler);
        }
        #endregion

        #region Specialists
        public override void AddSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"insert into specialists (name, notworking) values (@name, @notworking)";
                cmd.Parameters.AddWithValue("@name", specialist.Name);
                cmd.Parameters.AddWithValue("@notworking", specialist.NotWorking);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                long specialistId = cmd.LastInsertedId;
                specialist.ID = Convert.ToInt32(specialistId);

                List<int> specIds = new List<int>();
                cmd.CommandText = @"select idspecializations from specializations where name = @specname";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@specname", MySql.Data.MySqlClient.MySqlDbType.String);
                foreach (var spec in specialist.Specialisations)
                {
                    cmd.Parameters["@specname"].Value = spec;
                    cmd.Prepare();
                    specIds.Add(Convert.ToInt32(cmd.ExecuteScalar()));
                }

                cmd.CommandText = @"insert into specializations2specialist (specialization, specialist) values (@specializationId, @specialistId)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@specialistId", specialistId);
                cmd.Parameters.Add("@specializationId", MySql.Data.MySqlClient.MySqlDbType.Int32);
                foreach (var specializationId in specIds)
                {
                    cmd.Parameters["@specializationId"].Value = specializationId;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }

            CloseConnection(connection);
        }

        public override void RemoveSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "delete from specialists where idspecialists = @id";
                cmd.Parameters.AddWithValue("@id", specialist.ID);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException mex)
                {
                    CloseConnection(connection);
                    throw new Exception(mex.Number.ToString() + "\r\nОшибка удаления специалиста из БД: " + mex.Message, mex);
                }
            }
            CloseConnection(connection);
        }

        public override void UpdateSpecialistData(Scheduler_Controls_Interfaces.ISpecialist specialist)
        {
            var oldSpec = GetSpecialistById(specialist.ID);
            if (oldSpec == null)
                return;
            bool needUpdateName = oldSpec.Name != specialist.Name;
            bool needUpdateNotworking = oldSpec.NotWorking != specialist.NotWorking;
            var specsOnlyInNew = specialist.Specialisations.Except(oldSpec.Specialisations).ToList();
            var specsOnlyInOld = oldSpec.Specialisations.Except(specialist.Specialisations).ToList();
            bool needAddSpecializations = specsOnlyInNew.Count > 0;
            bool needRemoveSpecializations = specsOnlyInOld.Count > 0;

            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;

                if (needUpdateName)
                {
                    cmd.CommandText = "update specialists set name = @newvalue where idspecialists = @id";
                    cmd.Parameters.AddWithValue("@id", specialist.ID);
                    cmd.Parameters.AddWithValue("@newvalue", specialist.Name);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateNotworking)
                {
                    cmd.CommandText = "update specialists set notworking = @newvalue where idspecialists = @id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", specialist.ID);
                    cmd.Parameters.AddWithValue("@newvalue", specialist.NotWorking ? 1 : 0);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needAddSpecializations)
                {
                    List<int> specsIds = new List<int>();
                    cmd.CommandText = "select idspecializations from specializations where name = @name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@name", MySql.Data.MySqlClient.MySqlDbType.String);
                    foreach (var spec in specsOnlyInNew)
                    {
                        cmd.Parameters["@name"].Value = spec;
                        cmd.Prepare();
                        specsIds.Add(Convert.ToInt32(cmd.ExecuteScalar()));
                    }

                    cmd.CommandText = "insert into specializations2specialist (specialization, specialist) values (@snid, @stid)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@stid", specialist.ID);
                    cmd.Parameters.Add("@snid", MySql.Data.MySqlClient.MySqlDbType.Int32);
                    foreach (var id in specsIds)
                    {
                        cmd.Parameters["@snid"].Value = id;
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }

                if (needRemoveSpecializations)
                {
                    List<int> specsIds = new List<int>();
                    cmd.CommandText = "select idspecializations from specializations where name = @name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@name", MySql.Data.MySqlClient.MySqlDbType.String);
                    foreach (var spec in specsOnlyInOld)
                    {
                        cmd.Parameters["@name"].Value = spec;
                        cmd.Prepare();
                        specsIds.Add(Convert.ToInt32(cmd.ExecuteScalar()));
                    }

                    cmd.CommandText = "delete from specializations2specialist where specialization = @snid and specialist = @stid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@stid", specialist.ID);
                    cmd.Parameters.Add("@snid", MySql.Data.MySqlClient.MySqlDbType.Int32);
                    foreach (var id in specsIds)
                    {
                        cmd.Parameters["@snid"].Value = id;
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }

            }

            CloseConnection(connection);
        }

        Scheduler_Controls_Interfaces.ISpecialist GetSpecialistById(int id, MySql.Data.MySqlClient.MySqlConnection existedConnection = null)
        {
            if (id == 0)
                return null;
            var connection = existedConnection ?? OpenConnection();

            Scheduler_Controls_Interfaces.ISpecialist result = null;

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select name, notworking from specialists where idspecialists = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = EntityFactory.NewSpecialist();
                        result.ID = id;
                        result.Name = reader.GetString("name");
                        result.NotWorking = reader.GetInt32("notworking") == 1;
                    }

                }

                cmd.CommandText = "select s.name from specializations s, specializations2specialist s2s where s2s.specialist = @id and s.idspecializations = s2s.specialization";
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Specialisations.Add(reader.GetString("name"));
                    }
                }
            }

            if (existedConnection == null)
                CloseConnection(connection);

            return result;
        }

        protected override Scheduler_Forms_Interfaces.ISpecialistList AllSpecialistsInternal()
        {
            var connection = OpenConnection();

            Scheduler_Forms_Interfaces.ISpecialistList result = EntityFactory.NewSpecialistList();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idspecialists, name, notworking from specialists";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var currentSpec = EntityFactory.NewSpecialist();
                        currentSpec.ID = reader.GetInt32("idspecialists");
                        currentSpec.Name = reader.GetString("name");
                        currentSpec.NotWorking = reader.GetInt32("notworking") == 1;
                        result.List.Add(currentSpec);
                    }
                }

                cmd.CommandText = "select s.name from specializations s, specializations2specialist s2s where s2s.specialist = @specialistId and s.idspecializations = s2s.specialization";
                cmd.Parameters.Add("@specialistId", MySql.Data.MySqlClient.MySqlDbType.Int32);
                foreach (var specialist in result.List)
                {
                    cmd.Parameters["@specialistId"].Value = specialist.ID;
                    cmd.Prepare();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            specialist.Specialisations.Add(reader.GetString("name"));
                    }
                }
            }

            CloseConnection(connection);

            result.OnItemAdded += ListItemAddHandler;
            result.OnItemRemoved += ListItemRemoveHandler;
            result.OnItemChanged += ListItemChangedHandler;
            return result;
        }

        #endregion

        #region Specializations
        public override void AddSpecialization(string specialization)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "insert ignore into specializations (name) values (@newspec);";
                cmd.Parameters.AddWithValue("@newspec", specialization);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            CloseConnection(connection);
        }

        public override void RemoveSpecialization(string specialization)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {

                cmd.Connection = connection;
                cmd.CommandText = "delete from specializations where name = @spec";
                cmd.Parameters.AddWithValue("@spec", specialization);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException mex)
                {
                    CloseConnection(connection);
                    throw new Exception(mex.Number.ToString() + "\r\nОшибка удаления специализации из БД: " + mex.Message, mex);
                }
            }

            CloseConnection(connection);
        }

        static string GetSpecializationById(int id, MySql.Data.MySqlClient.MySqlConnection existedConnection = null)
        {
            if (id == 0)
                return null;
            var connection = existedConnection ?? OpenConnection();
            string result = String.Empty;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select name from specializations where idspecializations = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                result = (string)cmd.ExecuteScalar();
            }
            if (existedConnection == null)
                CloseConnection(connection);
            return result;
        }

        public override Scheduler_Controls_Interfaces.ISpecializationList AllSpecializations()
        {
            var resultList = EntityFactory.NewSpecializationList();

            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select name from specializations";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultList.SpecializationList.Add(reader.GetString("name"));
                    }
                }
            }

            CloseConnection(connection);

            resultList.OnItemAdded += ListItemAddHandler;
            resultList.OnItemRemoved += ListItemRemoveHandler;
            return resultList;
        }
        #endregion Specializations

        #region Cabinets
        public override void AddCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "insert into cabinet (name, availability) values (@name, @avail) on duplicate key update idcabinet=LAST_INSERT_ID(idcabinet);";
                cmd.Parameters.AddWithValue("@name", cabinet.Name);
                cmd.Parameters.AddWithValue("@avail", cabinet.Availability ? 1 : 0);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                cabinet.ID = Convert.ToInt32(cmd.LastInsertedId);
            }
            CloseConnection(connection);
        }

        public override void RemoveCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {

                cmd.Connection = connection;
                cmd.CommandText = "delete from cabinet where idcabinet = @id";
                cmd.Parameters.AddWithValue("@id", cabinet.ID);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException mex)
                {
                    CloseConnection(connection);
                    throw new Exception(mex.Number.ToString() + "\r\nОшибка удаления кабинета из БД: " + mex.Message, mex);
                }
            }

            CloseConnection(connection);
        }

        public override void UpdateCabinetData(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            var oldCabinet = GetCabinetById(cabinet.ID);
            if (oldCabinet == null)
                return;

            bool needUpdateName = cabinet.Name != oldCabinet.Name;
            bool needUpdateAvailability = cabinet.Availability != oldCabinet.Availability;

            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;

                if (needUpdateName)
                {
                    cmd.CommandText = "update cabinet set name = @name where idcabinet = @id";
                    cmd.Parameters.AddWithValue("@id", cabinet.ID);
                    cmd.Parameters.AddWithValue("@name", cabinet.Name);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateAvailability)
                {
                    cmd.CommandText = "update cabinet set availability = @avail where idcabinet = @id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", cabinet.ID);
                    cmd.Parameters.AddWithValue("@avail", cabinet.Availability ? 1 : 0);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            CloseConnection(connection);
        }

        Scheduler_Controls_Interfaces.ICabinet GetCabinetById(int id, MySql.Data.MySqlClient.MySqlConnection existedConnection = null)
        {
            if (id == 0)
                return null;
            Scheduler_Controls_Interfaces.ICabinet result = null;
            var connection = existedConnection ?? OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select name, availability from cabinet where idcabinet = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = EntityFactory.NewCabinet();
                        result.ID = id;
                        result.Name = reader.GetString("name");
                        result.Availability = reader.GetInt32("availability") == 1;
                    }

                }
            }
            if (existedConnection == null)
                CloseConnection(connection);
            return result;
        }

        protected override Scheduler_Forms_Interfaces.ICabinetList AllCabinetsInternal()
        {
            var resultList = EntityFactory.NewCabinetList();

            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idcabinet, name, availability from cabinet";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var currentCabinet = EntityFactory.NewCabinet();
                        currentCabinet.ID = reader.GetInt32("idcabinet");
                        currentCabinet.Name = reader.GetString("name");
                        currentCabinet.Availability = reader.GetInt32("availability") == 1;
                        resultList.List.Add(currentCabinet);
                    }
                }
            }

            CloseConnection(connection);

            resultList.OnItemAdded += ListItemAddHandler;
            resultList.OnItemRemoved += ListItemRemoveHandler;
            resultList.OnItemChanged += ListItemChangedHandler;
            return resultList;
        }
        #endregion

        #region Receptions

        struct receptionsWithIds
        {
            public int ownid;
            public int clientid;
            public int spectid;
            public int specnid;
            public int cabid;
            public int isrented;
            public TimeSpan timestart;
            public TimeSpan timeend;
            public DateTime date;
            public string administrator;
        }

        public override List<Scheduler_DBobjects_Intefraces.IEntity> GetReceptionsFromDate(DateTime date)
        {
            List<Scheduler_DBobjects_Intefraces.IEntity> result = new List<Scheduler_DBobjects_Intefraces.IEntity>();
            var connection = OpenConnection();

            List<receptionsWithIds> tempResults = new List<receptionsWithIds>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idreceptions, clientid, specialistid, cabinetid, specializationid, isrented, timestart, timeend, timedate, administrator from receptions where timedate = @date";
                cmd.Parameters.AddWithValue("@date", date.Date);
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempResults.Add(new receptionsWithIds()
                        {
                            ownid = reader.GetInt32("idreceptions"),
                            clientid = reader.GetInt32("clientid"),
                            spectid = reader.GetInt32("specialistid"),
                            specnid = reader.GetInt32("specializationid"),
                            cabid = reader.GetInt32("cabinetid"),
                            isrented = reader.GetInt32("isrented"),
                            timestart = reader.GetTimeSpan("timestart"),
                            timeend = reader.GetTimeSpan("timeend"),
                            date = reader.GetDateTime("timedate"),
                            administrator = reader.GetString("administrator")                            
                        });
                    }
                }
            }

            foreach (var recpt in tempResults)
            {
                var current = EntityFactory.NewEntity();
                current.ID = recpt.ownid;
                current.Client = GetClientById(recpt.clientid, connection);
                current.Specialist = GetSpecialistById(recpt.spectid, connection);
                current.Specialization = GetSpecializationById(recpt.specnid, connection);
                current.Cabinet = GetCabinetById(recpt.cabid, connection);
                current.Rent = recpt.isrented == 1;
                var timeinterval = EntityFactory.NewTimeInterval();
                timeinterval.SetStartEnd(recpt.date.Date.Add(recpt.timestart), recpt.date.Date.Add(recpt.timeend));
                current.ReceptionTimeInterval = timeinterval;
                current.Price = GetPriceForSpecialistClientPair(current.Specialist, current.Client, connection);
                current.Administrator = recpt.administrator;
                result.Add(current);
            }
            CloseConnection(connection);

            return result;
        }


        public override List<Scheduler_DBobjects_Intefraces.IEntity> GetReceptionsBetweenDates(DateTime startDate, DateTime endDate)
        {
            List<Scheduler_DBobjects_Intefraces.IEntity> result = new List<Scheduler_DBobjects_Intefraces.IEntity>();
            var connection = OpenConnection();

            List<receptionsWithIds> tempResults = new List<receptionsWithIds>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idreceptions, clientid, specialistid, cabinetid, specializationid, isrented, timestart, timeend, timedate, administrator from receptions where timedate between @startdate and @enddate";
                cmd.Parameters.AddWithValue("@startdate", startDate.Date);
                cmd.Parameters.AddWithValue("@enddate", endDate.Date);
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempResults.Add(new receptionsWithIds()
                        {
                            ownid = reader.GetInt32("idreceptions"),
                            clientid = reader.GetInt32("clientid"),
                            spectid = reader.GetInt32("specialistid"),
                            specnid = reader.GetInt32("specializationid"),
                            cabid = reader.GetInt32("cabinetid"),
                            isrented = reader.GetInt32("isrented"),
                            timestart = reader.GetTimeSpan("timestart"),
                            timeend = reader.GetTimeSpan("timeend"),
                            date = reader.GetDateTime("timedate"),
                            administrator = reader.GetString("administrator")
                        });
                    }
                }
            }

            foreach (var recpt in tempResults)
            {
                var current = EntityFactory.NewEntity();
                current.ID = recpt.ownid;
                current.Client = GetClientById(recpt.clientid, connection);
                current.Specialist = GetSpecialistById(recpt.spectid, connection);
                current.Specialization = GetSpecializationById(recpt.specnid, connection);
                current.Cabinet = GetCabinetById(recpt.cabid, connection);
                current.Rent = recpt.isrented == 1;
                var timeinterval = EntityFactory.NewTimeInterval();
                timeinterval.SetStartEnd(recpt.date.Date.Add(recpt.timestart), recpt.date.Date.Add(recpt.timeend));
                current.ReceptionTimeInterval = timeinterval;
                current.Price = GetPriceForSpecialistClientPair(current.Specialist, current.Client, connection);
                current.Administrator = recpt.administrator;
                result.Add(current);
            }
            CloseConnection(connection);

            return result;
        }

        public override void AddReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                int specid = 0;
                if (!reception.Rent)
                {
                    cmd.CommandText = "select idspecializations from specializations where name = @specname";
                    cmd.Parameters.AddWithValue("@specname", reception.Specialization);
                    cmd.Prepare();
                    specid = Convert.ToInt32(cmd.ExecuteScalar());
                }

                cmd.Parameters.Clear();
                cmd.CommandText = "insert into receptions (clientid, specialistid, cabinetid, specializationid, isrented, timestart, timeend, timedate, administrator) values (@cnt, @sst, @cab, @son, @r, @ts, @te, @td, @adm)";
                cmd.Parameters.AddWithValue("@sst", reception.Specialist.ID);
                cmd.Parameters.AddWithValue("@cab", reception.Cabinet.ID);
                cmd.Parameters.AddWithValue("@r", reception.Rent ? 1 : 0);
                cmd.Parameters.AddWithValue("@ts", reception.ReceptionTimeInterval.StartDate.TimeOfDay);
                cmd.Parameters.AddWithValue("@te", reception.ReceptionTimeInterval.EndDate.TimeOfDay);
                cmd.Parameters.AddWithValue("@td", reception.ReceptionTimeInterval.Date);
                cmd.Parameters.AddWithValue("@adm", reception.Administrator);

                //                 int? tmp = null;
                //                 if (reception.Client != null)
                //                     tmp = reception.Client.ID;
                cmd.Parameters.AddWithValue("@cnt", reception.Client == null ? 0 : reception.Client.ID);
                cmd.Parameters.AddWithValue("@son", specid);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                reception.ID = Convert.ToInt32(cmd.LastInsertedId);

                
            }

            AddOrUpdatePriceForSpecialistClientPair(reception.Specialist, reception.Client, reception.Price, connection);

            CloseConnection(connection);
        }

        public override void UpdateReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            var oldReception = GetReceptionById(reception.ID);
            if (oldReception == null)
                return;

            bool needUpdateSpecialist = oldReception.Specialist.ID != reception.Specialist.ID;

            bool needUpdateCabinet = oldReception.Cabinet.ID != reception.Cabinet.ID;
            bool needUpdateRent = oldReception.Rent != reception.Rent;
            //тут есть проблема - если клиент не задан, то жопа нас встречает!

            var needUpdateClient = oldReception.Client == null && reception.Client != null ||
                                    oldReception.Client != null && reception.Client == null ||
                                    (oldReception.Client != null && reception.Client != null && oldReception.Client.ID != reception.Client.ID);

            bool needUpdateSpecialization = oldReception.Specialization != reception.Specialization;

            bool needUpdateTimeInterval =
                oldReception.ReceptionTimeInterval.Date != reception.ReceptionTimeInterval.Date ||
                oldReception.ReceptionTimeInterval.StartDate != reception.ReceptionTimeInterval.StartDate ||
                oldReception.ReceptionTimeInterval.EndDate != reception.ReceptionTimeInterval.EndDate;

            bool needUpdatePrice = oldReception.Price != reception.Price;

            bool needUpdateAdministrator = oldReception.Administrator != reception.Administrator;

            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@rcptid", reception.ID);
                cmd.Parameters.Add("@id", MySql.Data.MySqlClient.MySqlDbType.Int32);

                if (needUpdateClient)
                {
                    cmd.CommandText = "update receptions set clientid = @id where idreceptions = @rcptid";
                    cmd.Parameters["@id"].Value = reception.Client == null ? 0 : reception.Client.ID;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateCabinet)
                {
                    cmd.CommandText = "update receptions set cabinetid = @id where idreceptions = @rcptid";
                    cmd.Parameters["@id"].Value = reception.Cabinet.ID;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateSpecialist)
                {
                    cmd.CommandText = "update receptions set specialistid = @id where idreceptions = @rcptid";
                    cmd.Parameters["@id"].Value = reception.Specialist.ID;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateRent)
                {
                    cmd.CommandText = "update receptions set isrented = @id where idreceptions = @rcptid";
                    cmd.Parameters["@id"].Value = reception.Rent ? 1 : 0;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateTimeInterval)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "update receptions set timestart = @ts, timeend = @te, timedate = @td where idreceptions = @rcptid";
                    cmd.Parameters.AddWithValue("@ts", reception.ReceptionTimeInterval.StartDate);
                    cmd.Parameters.AddWithValue("@te", reception.ReceptionTimeInterval.EndDate);
                    cmd.Parameters.AddWithValue("@td", reception.ReceptionTimeInterval.Date.Date);
                    cmd.Parameters.AddWithValue("@rcptid", reception.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateAdministrator)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "update receptions set administrator = @adm where idreceptions = @rcptid";
                    cmd.Parameters.AddWithValue("@adm", reception.Administrator);
                    cmd.Parameters.AddWithValue("@rcptid", reception.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdateSpecialization)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "select idspecializations from specializations where name = @specname";
                    cmd.Parameters.AddWithValue("@specname", reception.Specialization);
                    cmd.Prepare();
                    int specnid = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            specnid = reader.GetInt32("idspecializations");
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = "update receptions set specializationid = @id where idreceptions = @rcptid";
                    cmd.Parameters.AddWithValue("@id", specnid);
                    cmd.Parameters.AddWithValue("@rcptid", reception.ID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                if (needUpdatePrice)
                {
                    AddOrUpdatePriceForSpecialistClientPair(reception.Specialist, reception.Client, reception.Price, connection);
                }
            }

            CloseConnection(connection);
        }

        public Scheduler_DBobjects_Intefraces.IEntity GetReceptionById(int id)
        {
            if (id == 0)
                return null;
            var connection = OpenConnection();

            Scheduler_DBobjects_Intefraces.IEntity result = null;
            receptionsWithIds temp = new receptionsWithIds();
            bool somethingreaded = false;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idreceptions, clientid, specialistid, cabinetid, specializationid, isrented, timestart, timeend, timedate, administrator from receptions where idreceptions = @rcptid";
                cmd.Parameters.AddWithValue("@rcptid", id);
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        somethingreaded = true;
                        temp.ownid = reader.GetInt32("idreceptions");
                        temp.clientid = reader.GetInt32("clientid");
                        temp.spectid = reader.GetInt32("specialistid");
                        temp.specnid = reader.GetInt32("specializationid");
                        temp.cabid = reader.GetInt32("cabinetid");
                        temp.isrented = reader.GetInt32("isrented");
                        temp.timestart = reader.GetTimeSpan("timestart");
                        temp.timeend = reader.GetTimeSpan("timeend");
                        temp.date = reader.GetDateTime("timedate");
                        temp.administrator = reader.GetString("administrator");
                    }
                }
            }

            if (somethingreaded)
            {
                result = EntityFactory.NewEntity();
                result.ID = temp.ownid;
                result.Client = GetClientById(temp.clientid, connection);
                result.Specialist = GetSpecialistById(temp.spectid, connection);
                result.Specialization = GetSpecializationById(temp.specnid, connection);
                result.Cabinet = GetCabinetById(temp.cabid, connection);
                result.Rent = temp.isrented == 1;
                var timeinterval = EntityFactory.NewTimeInterval();
                timeinterval.SetStartEnd(temp.date.Date.Add(temp.timestart), temp.date.Date.Add(temp.timeend));
                result.ReceptionTimeInterval = timeinterval;
                result.Price = GetPriceForSpecialistClientPair(result.Specialist, result.Client, connection);
                result.Administrator = temp.administrator;
            }
            CloseConnection(connection);

            return result;
        }

        public override void RemoveReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "delete from receptions where idreceptions = @id";
                cmd.Parameters.AddWithValue("@id", reception.ID);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException mex)
                {
                    CloseConnection(connection);
                    throw new Exception(mex.Number.ToString() + "\r\nОшибка при удалении посещения из БД: " + mex.Message, mex);
                }
            }

            CloseConnection(connection);
        }

        public override List<Scheduler_Controls_Interfaces.IReception> GetReceptionsForClient(Scheduler_Controls_Interfaces.IClient client)
        {
            List<Scheduler_DBobjects_Intefraces.IEntity> result = new List<Scheduler_DBobjects_Intefraces.IEntity>();
            var connection = OpenConnection();

            List<receptionsWithIds> tempResults = new List<receptionsWithIds>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idreceptions, clientid, specialistid, cabinetid, specializationid, isrented, timestart, timeend, timedate, administrator from receptions where clientid = @clid";
                cmd.Parameters.AddWithValue("@clid", client.ID);
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempResults.Add(new receptionsWithIds()
                        {
                            ownid = reader.GetInt32("idreceptions"),
                            clientid = reader.GetInt32("clientid"),
                            spectid = reader.GetInt32("specialistid"),
                            specnid = reader.GetInt32("specializationid"),
                            cabid = reader.GetInt32("cabinetid"),
                            isrented = reader.GetInt32("isrented"),
                            timestart = reader.GetTimeSpan("timestart"),
                            timeend = reader.GetTimeSpan("timeend"),
                            date = reader.GetDateTime("timedate"),
                            administrator = reader.GetString("administrator")
                        });
                    }
                }
            }

            foreach (var recpt in tempResults)
            {
                var current = EntityFactory.NewEntity();
                current.ID = recpt.ownid;
                current.Client = client;
                current.Specialist = GetSpecialistById(recpt.spectid, connection);
                current.Specialization = GetSpecializationById(recpt.specnid, connection);
                current.Cabinet = GetCabinetById(recpt.cabid, connection);
                current.Rent = recpt.isrented == 1;
                var timeinterval = EntityFactory.NewTimeInterval();
                timeinterval.SetStartEnd(recpt.date.Date.Add(recpt.timestart), recpt.date.Date.Add(recpt.timeend));
                current.ReceptionTimeInterval = timeinterval;
                current.Price = GetPriceForSpecialistClientPair(current.Specialist, current.Client, connection);
                current.Administrator = recpt.administrator;
                result.Add(current);
            }
            CloseConnection(connection);

            return result.Cast<Scheduler_Controls_Interfaces.IReception>().ToList();
        }

        //public List<Scheduler_Controls_Interfaces.IReception> GetReceptionsForClient(Scheduler_Controls_Interfaces.IClient client)
        //{
        //    return MySQLConnector.GetReceptionsForClient(client);
        //}

        public override Dictionary<int, int> GetCostsForSpecialist(Scheduler_Controls_Interfaces.ISpecialist spec)
        {
            return GetPriceForSpecialist(spec);
        }
        #endregion

        #region ListEventAddRemoveHandlers

        public void ListItemAddHandler(object item)// where T : Scheduler_Controls_Interfaces.IDummy
        {
            Scheduler_Controls_Interfaces.IClient client = item as Scheduler_Controls_Interfaces.IClient;
            if (client != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).AddClient(client);
                return;
            }

            Scheduler_Controls_Interfaces.ISpecialist specialist = item as Scheduler_Controls_Interfaces.ISpecialist;
            if (specialist != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).AddSpecialist(specialist);
                return;
            }

            Scheduler_Controls_Interfaces.ICabinet cabinet = item as Scheduler_Controls_Interfaces.ICabinet;
            if (cabinet != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).AddCabinet(cabinet);
                return;
            }

            string specialization = item as string;
            if (specialization != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).AddSpecialization(specialization);
                return;
            }

        }

        public void ListItemRemoveHandler(object item)// where T : Scheduler_Controls_Interfaces.IDummy
        {
            Scheduler_Controls_Interfaces.IClient client = item as Scheduler_Controls_Interfaces.IClient;
            if (client != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).RemoveClient(client);
                return;
            }

            Scheduler_Controls_Interfaces.ISpecialist specialist = item as Scheduler_Controls_Interfaces.ISpecialist;
            if (specialist != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).RemoveSpecialist(specialist);
                return;
            }

            Scheduler_Controls_Interfaces.ICabinet cabinet = item as Scheduler_Controls_Interfaces.ICabinet;
            if (cabinet != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).RemoveCabinet(cabinet);
                return;
            }

            string specialization = item as string;
            if (specialization != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).RemoveSpecialization(specialization);
                return;
            }
        }

        public void ListItemChangedHandler(object item)
        {
            Scheduler_Controls_Interfaces.IClient client = item as Scheduler_Controls_Interfaces.IClient;
            if (client != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).UpdateClientData(client);
                return;
            }

            Scheduler_Controls_Interfaces.ISpecialist specialist = item as Scheduler_Controls_Interfaces.ISpecialist;
            if (specialist != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).UpdateSpecialistData(specialist);
                return;
            }

            Scheduler_Controls_Interfaces.ICabinet cabinet = item as Scheduler_Controls_Interfaces.ICabinet;
            if (cabinet != null)
            {
                ((Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)this).UpdateCabinetData(cabinet);
                return;
            }
        }

        #endregion

        public override void MakeBackup(string filename)
        {
            if (System.IO.File.Exists(filename))
                return;
            var connection = OpenConnection();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                using (MySql.Data.MySqlClient.MySqlBackup mb = new MySql.Data.MySqlClient.MySqlBackup(cmd))
                {
                    cmd.Connection = connection;
                    mb.ExportToFile(filename);
                }
            }
            CloseConnection(connection);
        }

        public override void RestoreBackup(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return;
            var connection = OpenConnection();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                using (MySql.Data.MySqlClient.MySqlBackup mb = new MySql.Data.MySqlClient.MySqlBackup(cmd))
                {
                    cmd.Connection = connection;
                    mb.ImportInfo.DatabaseDefaultCharSet = "utf8";
                    mb.ImportFromFile(filename);
                }
            }
            CloseConnection(connection);
        }


        public override bool CheckDBConnection(out string message)
        {
            bool result = true;
            message = null;
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            try
            {
                conn = OpenConnection();
            }
            catch (ArgumentException)
            {
                message = "Неправильный формат строки подключения.";
                result = false;
            }
            catch (Exception ex)
            {
                var mysqlException = ex.InnerException as MySql.Data.MySqlClient.MySqlException;
                if (mysqlException != null)
                    switch (mysqlException.ErrorCode)
                    {
                        case 0:
                            message = "В доступе к базе данных отказано. Проверьте настройки подключения и учётной записи БД.";
                            break;
                        case 1042:
                            message = "Невозможно подключиться к базе данных. Проверьте настройки подключения (адрес сервера и порт).";
                            break;
                        case 1045:
                            message = "Неверно введен логин или пароль.";
                            break;
                        default:
                            message = "Неизвестная ошибка подключения к базе данных. " + Environment.NewLine + mysqlException.Message + Environment.NewLine + connectionString ?? String.Empty;
                            break;
                    }
                else
                    message = "Неизвестная ошибка подключения к базе данных: " + Environment.NewLine + ex.Message + Environment.NewLine + connectionString ?? String.Empty;
                result = false;
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    CloseConnection(conn);
            }

            if (result)
                UPDATEDB_IfNeeded();

            return result;
        }


        public override string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }



        #region PriceWorkaround


        static int GetPriceForSpecialistClientPair(Scheduler_Controls_Interfaces.ISpecialist spec, 
            Scheduler_Controls_Interfaces.IClient client, 
            MySql.Data.MySqlClient.MySqlConnection existedConnection = null)
        {
            if (spec == null || spec.ID == 0)
                return -1;
            var connection = existedConnection ?? OpenConnection();

            int result = 0;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select price from specialist2clientprice where specid=@spid and clid=@clid";
                cmd.Parameters.AddWithValue("@spid", spec.ID);

                int clid = client == null ? DEFAULTCLIENTID : client.ID;
                cmd.Parameters.AddWithValue("@clid", clid);

                cmd.Prepare();

                result = Convert.ToInt32(cmd.ExecuteScalar());
                
            }

            if (existedConnection == null)
                CloseConnection(connection);

            return result;
        }

        static Dictionary<int,int> GetPriceForSpecialist(Scheduler_Controls_Interfaces.ISpecialist spec)
        {
            if (spec == null)
                return new Dictionary<int, int>();
            var connection = OpenConnection();

            Dictionary<int,int> result = new Dictionary<int,int>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select clid, price from specialist2clientprice where specid=@spid";
                cmd.Parameters.AddWithValue("@spid", spec.ID);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetInt32("clid"), reader.GetInt32("price"));
                    }
                }
            }

            CloseConnection(connection);
            return result;
        }

        static void AddOrUpdatePriceForSpecialistClientPair(Scheduler_Controls_Interfaces.ISpecialist spec, 
            Scheduler_Controls_Interfaces.IClient client,
            int newPrice = 0,
            MySql.Data.MySqlClient.MySqlConnection existedConnection = null)
        {
            if (spec == null || spec.ID == 0)
                return;
            var connection = existedConnection ?? OpenConnection();

            object result = null;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idspecialist2clientcost from specialist2clientprice where specid=@spid and clid=@clid";
                cmd.Parameters.AddWithValue("@spid", spec.ID);

                int clid = client == null ? DEFAULTCLIENTID : client.ID;
                cmd.Parameters.AddWithValue("@clid", clid);

                cmd.Prepare();

                result = cmd.ExecuteScalar();

                if (result == null)
                { //insert
                    cmd.CommandText = "insert into specialist2clientprice (specid, clid, price) values (@spid, @clid, @price)";
                }
                else
                { //update
                    cmd.CommandText = "update specialist2clientprice set price=@price where specid=@spid and clid=@clid";
                }

                cmd.Parameters.AddWithValue("@price", newPrice);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

            }

            if (existedConnection == null)
                CloseConnection(connection);
        }

        #endregion

        #region DB Updates
        public void UPDATEDB_IfNeeded()
        {
            var connection = OpenConnection();
            bool need_addTable_clientGenerallyParams = false;
            bool need_alterTable_addAdministratorFieldToClientAndReception = false;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "show tables like '%clientGenerallyParams%'";
                var reader = cmd.ExecuteReader();
                need_addTable_clientGenerallyParams = !reader.HasRows;
                reader.Close();
                
                cmd.CommandText = "select `COLUMN_NAME` from `INFORMATION_SCHEMA`.`COLUMNS` WHERE `TABLE_SCHEMA`='Scheduler' AND `TABLE_NAME`='clients' AND `COLUMN_NAME` like '%administrator%'";
                reader = cmd.ExecuteReader();
                need_alterTable_addAdministratorFieldToClientAndReception = !reader.HasRows;
                reader.Close();
            }
            CloseConnection(connection);

            if (need_addTable_clientGenerallyParams)
                UPDATEDB_addTable_clientGenerallyParams();
            if (need_alterTable_addAdministratorFieldToClientAndReception)
                UPDATEDB_alterTables_addAdministratorFieldToClientAndReception();
        }

        static void UPDATEDB_addTable_clientGenerallyParams()
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "CREATE TABLE `clientGenerallyParams` (  `idclientGenerallyParams` INT NOT NULL AUTO_INCREMENT,  `clientId` INT ZEROFILL NOT NULL,  `generallyTime` TIME NOT NULL,  `generallyPrice` INT ZEROFILL NOT NULL,  PRIMARY KEY (`idclientGenerallyParams`),  UNIQUE INDEX `idclientGenerallyParams_UNIQUE` (`idclientGenerallyParams` ASC),  UNIQUE INDEX `clientId_UNIQUE` (`clientId` ASC));";
                cmd.ExecuteNonQuery();                
            }

            CloseConnection(connection);
        }

        static void UPDATEDB_alterTables_addAdministratorFieldToClientAndReception()
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "ALTER TABLE `receptions` ADD COLUMN administrator TINYTEXT NOT NULL;";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "ALTER TABLE `clients` ADD COLUMN administrator TINYTEXT NOT NULL; ";
                cmd.ExecuteNonQuery();
            }

            CloseConnection(connection);
        }
        #endregion
    }
}
