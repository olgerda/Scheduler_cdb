/*
 * Created by SharpDevelop.
 * Ольга Едренкина
 * Date: 09.05.2013
 * Time: 14:52
 * 
 * Модуль реализует функции доступа к БД и взаимодействия с ней. Для работы с БД
 * используется библиотека MySQL.Data (MySqlConnector).
 * Модуль написан для программы-планировщика психологического центра "Квартет".
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Scheduler
{
    /// <summary>
    /// Класс содержит методы, обеспечивающие работу с БД под управлением СУБД MySQL
    /// </summary>
    public class DbConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DbConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "dbo_kvartet";
            uid = "olga";						//Важно обсудить парольный доступ с заказчиком. Зашьем ли мы статичный пароль или при каждом подключении пароль нужно будет вводить.
            password = "student88";

            string connectionString;
//             connectionString = "SERVER=" + server + ";" + "DATABASE=" +
//                 database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connectionString = Properties.Settings.Default.ConnectionString;
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Не могу подключиться к серверу. Проверьте настройки сервера.");
                        break;
                    case 1045:
                        MessageBox.Show("Неверно введен логин или пароль.");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Реализует запуск выполнения sql-запроса INSERT
        /// </summary>
        /// <param name="query">Строка запроса</param>
        public void Insert(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        /// <summary>
        /// Реализует запуск выполнения sql-запроса UPDATE
        /// </summary>
        /// <param name="query">Строка запроса</param>
        public void Update(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        /// <summary>
        /// Реализует запуск выполнения sql-запроса DELETE
        /// </summary>
        /// <param name="query">Строка запроса</param>
        public void Delete(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        //Возможно, селекты имеет смысл писать отдельные, а не один общий метод.
        public List<string>[] Select(string query, int col)
        {
            List<string>[] result = new List<string>[col];
            for (int i = 0; i < col; ++i)
                result[col] = new List<string>();
            //    	
            //    	if (this.OpenConnection() == true)
            //    	{
            //    		MySqlCommand cmd = new MySqlCommand(query, connection);
            //    		MySqlDataReader dataReader = cmd.ExecuteReader();
            //    		
            //    		while (dataReader.Read())
            //    		{
            //    			result[0].Add(dataReader["id"] + "");
            //    			...    			              
            //    		}
            //    	}
            return result;
        }

        /// <summary>
        /// Реализует запуск выполнения sql-запроса COUNT
        /// </summary>
        /// <param name="query">Строка запроса</param>
        public int Count(string query)
        {
            int result = -1;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                result = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            return result;
        }

        /*
         * 
           SELECT CName, CSurname, CPatronimyc, cl_id, Comment, TelNumber, inRedList,
                  SName, SSurname, SPatronimyc, spec_id,
                  Specialization,
                  CabName,
                  startTime, endTime, receptionDate, id
           FROM reception_view;
         *
         */
        /// <summary>
        /// Получить список сущностей, запланированных на указанную дату.
        /// </summary>
        /// <param name="date">Требуемая дата в форме DateTime.</param>
        /// <returns>Список сущностей. Может быть пустым.</returns>
        /// 
        public List<Entity> SelectFromDate(DateTime date)
        {
            date = date.Date;
            List<Entity> result = new List<Entity>();
            
            Dictionary<int, ClientCard> clientsList = new Dictionary<int, ClientCard>();
            
            Dictionary<int, CabinetCard> cabinetList = LoadCabinetList();
            Dictionary<int, Specialization> specializationsList = LoadSpecializations();
            Dictionary<int, SpecialistCard> specialistList = LoadSpecialists(specializationsList);
            
            OpenConnection();
            using (MySqlCommand mSqlCmd = new MySqlCommand("select * from reception_view r where r.receptionDate='" + date.ToString("yyyy-MM-dd") + "'", connection))
            {
                using (MySqlDataReader dr = mSqlCmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TimeSpan startT = dr.GetTimeSpan("startTime"); //CHECK!
                        TimeSpan endT = dr.GetTimeSpan("endTime");
                        TimeInterval tInterval = new TimeInterval(date.Add(startT), date.Add(endT));

                        
                        int specId = dr.GetInt32("spec_id");
                        int clntId = dr.GetInt32("cl_id");
                        SpecialistCard spec = specialistList[specId];

                        Entity curEntity = new Entity(dr.GetUInt64("id"), tInterval,
                            new ClientCard(new FIO(dr.GetString("CName"), dr.GetString("CSurname"), dr.GetString("CPatronimyc")), dr.GetUInt64("TelNumber"), dr.GetString("Comment"), dr.GetBoolean("inRedList")),
                            spec,
                            specializationsList.FirstOrDefault(x => x.Value.Title == dr.GetString("Specialization")).Value,
                            cabinetList.FirstOrDefault(x=>x.Value.Name == dr.GetString("CabName")).Value
                            );
                        result.Add(curEntity);
                    }
                }
            }
            CloseConnection();
            return result;
        }

        public Dictionary<int, Specialization> LoadSpecializations()
        {
            Dictionary<int, Specialization> result = new Dictionary<int, Specialization>();
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("SELECT id, Specialization FROM specializations", connection))
            {
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr.GetByte("id"), new Specialization(dr.GetString("Specialization")));
                    }
                }
            }
            CloseConnection();
            return result;
        }

 
         public Dictionary<int, SpecialistCard> LoadSpecialists(Dictionary<int,Specialization> allSpecs)
         {
            Dictionary<int, SpecialistCard> result = new Dictionary<int, SpecialistCard>();
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("SELECT s.id as id, s.SpecializationList as SpecializationList, f.Name as Name, f.Surname as Surname, f.Patronimyc as Patronimyc FROM specialist s, FIO f where f.id=s.id_FIO", connection))
            {
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr.GetUInt16("id"), 
                            new SpecialistCard(
                                new FIO(dr.GetString("Name"), dr.GetString("Surname"), dr.GetString("Patronimyc")), 
                                Specialization.GetSpecializationsFromULong(dr.GetUInt64("SpecializationList"), allSpecs)
                                )
                                );
                    }
                }
            }
            CloseConnection();
            return result;
        }

        public Dictionary<int, CabinetCard> LoadCabinetList()
        {
            Dictionary<int, CabinetCard> result = new Dictionary<int, CabinetCard>();

            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("SELECT id, Name FROM cabinets", connection))
            {
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr.GetUInt16("id"), new CabinetCard(dr.GetString("Name")));
                    }
                }
            }
            CloseConnection();
            return result;
        }
// 
//         public Dictionary<int, ClientCard> LoadClients()
//         {
//             Dictionary<int, ClientCard> result = new Dictionary<int, ClientCard>();
// 
// 
// 
//             return result;
//         }

        /// <summary>
        /// Функция реализует создание резервной копии базы данных. Файл резервной копии располагается в корне диска C:
        /// Имя файла имеет формат ClientBaseBackupГГГГ_ММ_ДД_чч_мм_сс_мс.sql, где ГГГГ - год, ММ - месяц, ДД - день,
        /// чч - часы, мм - минуты, сс - секунды, мс - миллисекунды, соответствующие моменту запуска данной функции.
        /// </summary>
        public void Backup()
        {
            try
            {												//Нужно уточнить у заказчика: бэкапить базу вручную или по расписанию. Если по расписанию, то как планируется решать вопрос с местом.
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int msec = Time.Millisecond;

                string path = "C:\\ClientBaseBackup" + year + "_" + month +
                    "_" + day + "_" + hour + "_" + minute + "_" + second + "_" +
                    msec + ".sql";
                StreamWriter file = new StreamWriter(path);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output = process.StandardOutput.ReadToEnd();

                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("При создании резервной копии базы данных произошла ошибка. Копирование прервано. " + ex.Message);

            }
        }

        /// <summary>
        /// Функция реализует развертывание базы данных из сохраненной в файл резервной копии.
        /// </summary>
        public void Restore()
        {
            try
            {
                string path;			//Потом надо заменить на выбор файла резервной копии через окно "обзор"
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("При развертывании базы из резервной копии произошла ошибка. Операция прервана. " + ex.Message);
            }
        }
    }
}
