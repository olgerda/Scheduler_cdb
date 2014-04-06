using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySqlConnector
{
    public class MySQLConnector : Scheduler_DBobjects_Intefraces.Scheduler_DBconnector
    {
        Scheduler_Common_Interfaces.IFactory entityFactory;

        public MySQLConnector(Scheduler_Common_Interfaces.IFactory entityFactory)
        {
            this.entityFactory = entityFactory;
        }

        MySql.Data.MySqlClient.MySqlConnection OpenConnection()
        {
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Properties.Settings.Default.mysqlconnstring);
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
                }
            }
            return connection;
        }

        //Close connection
        private void CloseConnection(MySql.Data.MySqlClient.MySqlConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
        }

        #region Clients and Telephones
        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddClient(Scheduler_Controls_Interfaces.IClient client)
        {
            //clients columns are: idclients, name, comment, blacklisted
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

                query = @"insert into clients (name, comment, blacklisted) values (@name, @comment, @blacklisted)";
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@comment", client.Comment);
                cmd.Parameters.AddWithValue("@blacklisted", client.BlackListed ? 1 : 0);
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
            }
            CloseConnection(connection);
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveClient(Scheduler_Controls_Interfaces.IClient client)
        {
            var connection = OpenConnection();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "delete from clients where idclients = @clid";
                cmd.Parameters.AddWithValue("@clid", client.ID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "call CleanupTelephones()";
                cmd.Parameters.Clear();
                cmd.ExecuteNonQuery();
            }

            CloseConnection(connection);
        }

        private Scheduler_Controls_Interfaces.IClient GetClientById(int id)
        {
            Scheduler_Controls_Interfaces.IClient result = null;
            var connection = OpenConnection();

            string query = "select idclients, name, comment, blacklisted from clients where idclients = @id";

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = entityFactory.NewClient();
                        result.Name = reader.GetString("name");
                        result.ID = reader.GetInt32("idclients");
                        result.Comment = reader.GetString("comment");
                        result.BlackListed = reader.GetInt32("blacklisted") != 0;
                        result.ReceptionListFuncition(GetReceptionsForClient);
                    }
                }

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
            }
            CloseConnection(connection);

            return result;
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.UpdateClientData(Scheduler_Controls_Interfaces.IClient client)
        {
            var oldClient = GetClientById(client.ID);
            bool needUpdateName = oldClient.Name != client.Name;
            bool needUpdateComment = oldClient.Comment != client.Comment;
            bool needUpdateBL = oldClient.BlackListed != client.BlackListed;
            var telsOnlyInOld = oldClient.Telephones.Except(client.Telephones).ToList();
            bool needRemoveTelephones = telsOnlyInOld.Count > 0;
            var telsOnlyInNew = client.Telephones.Except(oldClient.Telephones).ToList();
            bool needAddTelephones = telsOnlyInNew.Count > 0;

            var connection = OpenConnection();

            //clients columns are: idclients, name, comment, blacklisted
            //telephones columns: idtelephones, telephonescol
            //telephones2clients columns: idtelephones2clients, telid, clid
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("", connection))
            {
                if (needUpdateName)
                {
                    cmd.CommandText = "update clients set name = @name where idclients = " + client.ID.ToString();
                    cmd.Parameters.AddWithValue("@name", client.Name);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                if (needUpdateComment)
                {
                    cmd.CommandText = "update clients set comment = @comment where idclients = " + client.ID.ToString();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@comment", client.Comment);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                if (needUpdateBL)
                {
                    cmd.CommandText = "update clients set blacklisted = @bl where idclients = " + client.ID.ToString();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@bl", client.BlackListed ? 1 : 0);
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
            }

            CloseConnection(connection);
        }

        Scheduler_Forms_Interfaces.IClientList Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AllClients()
        {
            List<Scheduler_Controls_Interfaces.IClient> clientList = new List<Scheduler_Controls_Interfaces.IClient>();
            //columns are: idclients, name, comment, blacklisted
            var connection = OpenConnection();

            string query = "select idclients, name, comment, blacklisted from clients";

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {

                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Scheduler_Controls_Interfaces.IClient current = entityFactory.NewClient();
                        current.Name = reader.GetString("name");
                        current.ID = reader.GetInt32("idclients");
                        current.Comment = reader.GetString("comment");
                        current.BlackListed = reader.GetInt32("blacklisted") != 0;
                        current.ReceptionListFuncition(GetReceptionsForClient);
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

            }
            CloseConnection(connection);

            var result = entityFactory.NewClientList();
            result.List.AddRange(clientList);

            return result;
        }
        #endregion

        #region Specialists
        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist)
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

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist)
        {
            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "delete from specialists where idspecialists = @id";
                cmd.Parameters.AddWithValue("@id", specialist.ID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            CloseConnection(connection);
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.UpdateSpecialistData(Scheduler_Controls_Interfaces.ISpecialist specialist)
        {
            var oldSpec = GetSpecialistById(specialist.ID);

            bool needUpdateName = oldSpec.Name != specialist.Name;
            bool needUpdateNotworking = oldSpec.NotWorking != specialist.NotWorking;
            var specsOnlyInNew = specialist.Specialisations.Except(oldSpec.Specialisations).ToList();
            var specsOnlyInOld = oldSpec.Specialisations.Except(specialist.Specialisations).ToList();
            bool needAddSpecializations = specsOnlyInNew.Count > 0;
            bool needRemoveSpecializations = specsOnlyInOld.Count > 0;

            var connection = OpenConnection();

            using (MySql.Data.MySqlClient.MySqlCommand cmd  = new MySql.Data.MySqlClient.MySqlCommand())
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

        Scheduler_Controls_Interfaces.ISpecialist GetSpecialistById(int id)
        {
            var connection = OpenConnection();

            Scheduler_Controls_Interfaces.ISpecialist result = entityFactory.NewSpecialist();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select name, notworking from specialists where idspecialists = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    result.Name = reader.GetString("name");
                    result.NotWorking = reader.GetInt32("notworking") == 1;
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

            CloseConnection(connection);

            return result;
        }

        Scheduler_Forms_Interfaces.ISpecialistList Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AllSpecialists()
        {
            var connection = OpenConnection();

            Scheduler_Forms_Interfaces.ISpecialistList result = entityFactory.NewSpecialistList();

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "select idspecialists, name, notworking from specialists";

                using (var reader = cmd.ExecuteReader())
                {
                    var currentSpec = entityFactory.NewSpecialist();
                    currentSpec.ID = reader.GetInt32("idspecialists");
                    currentSpec.Name = reader.GetString("name");
                    currentSpec.NotWorking = reader.GetInt32("notworking") == 1;
                    result.List.Add(currentSpec);
                }

                cmd.CommandText = "select s.name from specializations s, specializations2specialist s2s where s2s.specialist = @specialistId and s.idspecializations = s2s.specialization";
                cmd.Parameters.Add("@specialistId");
                foreach (var specialist in result.List)
                {
                    cmd.Parameters["@specialistId"].Value = specialist.ID;
                    cmd.Prepare();
                    specialist.Specialisations.Add(Convert.ToString(cmd.ExecuteScalar()));
                }
            }

            CloseConnection(connection);

            return result;
        }

        #endregion

        #region Specializations
        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddSpecialization(string specialization)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveSpecialization(string specialization)
        {
            throw new NotImplementedException();
        }

        Scheduler_Controls_Interfaces.ISpecializationList Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AllSpecializations()
        {
            throw new NotImplementedException();
        }
        #endregion Specializations

        #region Cabinets
        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.UpdateCabinetData(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            throw new NotImplementedException();
        }

        Scheduler_Forms_Interfaces.ICabinetList Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AllCabinets()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Receptions
        List<Scheduler_DBobjects_Intefraces.IEntity> Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.GetReceptionsFromDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.UpdateReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            throw new NotImplementedException();
        }

        public static List<Scheduler_Controls_Interfaces.IReception> GetReceptionsForClient(Scheduler_Controls_Interfaces.IClient client)
        {
            throw new NotImplementedException();
        }

        List<Scheduler_Controls_Interfaces.IReception> Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.GetReceptionsForClient(Scheduler_Controls_Interfaces.IClient client)
        {
            return GetReceptionsForClient(client);
        }
        #endregion

        Scheduler_Common_Interfaces.IFactory Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.EntityFactory
        {
            get
            {
                return entityFactory;
            }
            set
            {
                entityFactory = value;
            }
        }







    }
}
