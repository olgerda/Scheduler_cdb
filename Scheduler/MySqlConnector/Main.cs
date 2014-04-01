using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySqlConnector
{
    public class MySQLConnector: Scheduler_DBobjects_Intefraces.Scheduler_DBconnector
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        Scheduler_Common_Interfaces.IFactory entityFactory;

        public MySQLConnector()
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(Properties.Settings.Default.mysqlconnstring);
            Connect();
        }
        
        public MySQLConnector(Scheduler_Common_Interfaces.IFactory entityFactory)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(Properties.Settings.Default.mysqlconnstring);
            Connect();
            this.entityFactory = entityFactory;
        }

        void CheckConn()
        {
            if (conn.State == System.Data.ConnectionState.Broken)
                Connect();
            if (conn.State == System.Data.ConnectionState.Closed)
                Connect();
            try
            {
                if (String.IsNullOrWhiteSpace(conn.ServerVersion))
                    ;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception("Ошибка подключения к серверу. ", ex);
            }
        }

        void Connect()
        {
            try
            {
                conn.Open();
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
        }

        /// <summary>
        /// Выполнить запрос select на заданную таблицу по заданным полям. Никаких проверок, простая подстановка.
        /// </summary>
        /// <param name="tablename">Имя таблицы.</param>
        /// <param name="fields">Имена полей через запятую.</param>
        /// <returns>Список строк, в которых значения столбцов разделены табами.</returns>
        public List<List<string>> Select(string tablename, string fields)
        {
            List<List<string>> result = new List<List<string>>();
            string query = String.Format("select {1} from {0};", tablename, fields);
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

            string[] fields_splitted = fields.Split(',');

            while (reader.Read())
            {
                //BAD! VERY BAD!
                List<string> current = new List<string>();
                foreach (var s in fields_splitted)
                    current.Add((string)reader[s]);
                result.Add(current);
            }

            return result;
        }

        ~MySQLConnector()
        {
            conn.Close();
        }

        string[] clientColumns = { "id","name","blacklisted",""};
        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddClient(Scheduler_Controls_Interfaces.IClient client)
        {
            CheckConn();

        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveClient(Scheduler_Controls_Interfaces.IClient client)
        {
            throw new NotImplementedException();
        }

        Scheduler_Forms_Interfaces.IClientList Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AllClients()
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist)
        {
            throw new NotImplementedException();
        }

        Scheduler_Forms_Interfaces.ISpecialistList Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AllSpecialists()
        {
            throw new NotImplementedException();
        }

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

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AddCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            throw new NotImplementedException();
        }

        void Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.RemoveCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet)
        {
            throw new NotImplementedException();
        }

        Scheduler_Forms_Interfaces.ICabinetList Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.AllCabinets()
        {
            throw new NotImplementedException();
        }

        List<Scheduler_DBobjects_Intefraces.IEntity> Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.GetReceptionsFromDate(DateTime date)
        {
            throw new NotImplementedException();
        }

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
