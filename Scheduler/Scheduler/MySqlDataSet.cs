﻿using System;
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

        public DataSet MainDataSet
        {
            get { return mainDataSet;}
        }

        public DataSet ViewDataSet
        {
            get { return viewDataSet; }
        }

        public MySqlDataSet(string connectionString = "")
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                connectionString = Properties.Settings.Default.ConnectionString;
            connection = new MySqlConnection(Properties.Settings.Default.ConnectionString);
            mainDataSet = new DataSet("kvartetDataSet");
            viewDataSet = new DataSet("kvartetViewSet");


            PullTables();

        }
        /*
         * Способ Fill хорошо работает на малых данных, но, возможно, потребует больших ресурсов на больших, надо тестировать.
         * Судя по принципу работы DataSet - память зажирать будет капитально. DataSet очень удобно для разработки - к нему прям льньком можно from select и т.д. делать. 
         * Потом надо эти линьки переделать в обычные запросы на SQL и захардкодить. Таким образом, думаю, получится избежать неограниченного потребления памяти.
         */
        private void PullTables()
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
    }
}