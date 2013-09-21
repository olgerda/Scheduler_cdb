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
			connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
				database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
			
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
    public List <string> [] Select(string query, int col)
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
