using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySqlConnector
{
    public class Main
    {
        MySql.Data.MySqlClient.MySqlConnection conn;

        public Main()
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(Properties.Settings.Default.mysqlconnstring);
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

        ~Main()
        {
            conn.Close();
        }
    }
}
