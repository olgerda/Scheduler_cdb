using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Scheduler;

namespace RawDbEdit
{
    public partial class RawDBEdit : Form
    {
        MySqlConnection connection = new MySqlConnection(RawDbEdit.Properties.Settings.Default.kvartetMySQLConnectionString);
        DataSet ds;
        //Dictionary<string, MySqlDataAdapter> tableAdapters;
        MySqlDataSet mySqlDataSet;
        public RawDBEdit()
        {
            InitializeComponent();
        }

        private void initToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mySqlDataSet = new MySqlDataSet();
            ds = mySqlDataSet.MainDataSet;
            InitializeTabs();
        }

        private void InitializeTabs()
        {
//             tableAdapters = new Dictionary<string, MySqlDataAdapter>();
//             string sQuery = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA='kvartet' AND TABLE_TYPE='BASE TABLE'";
//             MySqlCommand sCmd = new MySqlCommand(sQuery, connection);
//             sCmd.Connection.Open();
//             var reader = sCmd.ExecuteReader();
//             while (reader.Read())
//             {
//                 tableAdapters.Add(reader.GetString(0), null);
//             }
//             reader.Close();
//             sCmd.Connection.Close();
//             sCmd.Connection.Dispose();

            tabControl1.TabPages.Clear();
//             foreach (var tableName in tableAdapters.Keys)
//             {
//                 tabControl1.TabPages.Add(tableName, tableName);
//             }

            //PopulateTabs();

            foreach (DataTable table in ds.Tables)
            {
                DataGridView dgv = new DataGridView();
                dgv.AllowUserToAddRows = true;
                dgv.AllowUserToDeleteRows = true;
                dgv.AllowUserToResizeColumns = true;
                dgv.AllowUserToResizeRows = true;
                dgv.DataSource = ds;
                dgv.DataMember = table.TableName;
                dgv.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(table.TableName, table.TableName);
                tabControl1.TabPages[table.TableName].Controls.Add(dgv);
            }

            tabControl1.TabPages.Add("TABLES<-->VIEWS", "TABLES<-->VIEWS");
            tabControl1.Selecting += new TabControlCancelEventHandler(tabControl1_Selecting);

            foreach (DataTable view in mySqlDataSet.ViewDataSet.Tables)
            {
                DataGridView dgv = new DataGridView();
                dgv.AllowUserToAddRows = true;
                dgv.AllowUserToDeleteRows = true;
                dgv.AllowUserToResizeColumns = true;
                dgv.AllowUserToResizeRows = true;
                dgv.DataSource = mySqlDataSet.ViewDataSet;
                dgv.DataMember = view.TableName;
                dgv.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(view.TableName, view.TableName);
                tabControl1.TabPages[view.TableName].Controls.Add(dgv);
            }
            //PopulateTabsWithViews();
        }

        void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (sender == null || e.TabPage == null) return;
            if (e.TabPage.Name == "TABLES<-->VIEWS") e.Cancel = true;
        }

//         private void PopulateTabs()
//                 {
//                     //PopulateDataSet();
//                     foreach (var tableName in tableAdapters.Keys)
//                     {
//                         DataGridView dgv = new DataGridView();
//                         dgv.AllowUserToAddRows = true;
//                         dgv.AllowUserToDeleteRows = true;
//                         dgv.AllowUserToResizeColumns = true;
//                         dgv.AllowUserToResizeRows = true;
//                         dgv.DataSource = ds;
//                         dgv.DataMember = tableName;
//                         dgv.Dock = DockStyle.Fill;
//                         tabControl1.TabPages[tableName].Controls.Add(dgv);
//                     }
//                     
//                 }

//         private void PopulateDataSet()
//         {
//             ds = new DataSet();
//             string selectStatement = "select * from kvartet.";
// //             for (int i = 0; i < tableAdapters.Count; i++ )
// //             {
// //                 var pair = tableAdapters
// //             }
//             List<string> keys = new List<string>(tableAdapters.Keys);
//             foreach (var tableName in keys)
//             {
//                 MySqlCommand curCmd = new MySqlCommand(selectStatement + tableName, connection);
//                 var curDataAdapter = new MySqlDataAdapter(curCmd);
//                 MySqlCommandBuilder dummy = new MySqlCommandBuilder(curDataAdapter);
//                 curDataAdapter.Fill(ds, tableName);
//                 tableAdapters[tableName] = curDataAdapter;
//             }
//         }

//         void PopulateTabsWithViews()
//         {
//             List<string> views = new List<string>();
//             string sQuery = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA='kvartet' AND TABLE_TYPE='VIEW'";
//             MySqlCommand sCmd = new MySqlCommand(sQuery, connection);
//             sCmd.Connection.Open();
//             var reader = sCmd.ExecuteReader();
//             while (reader.Read())
//             {
//                 views.Add(reader.GetString(0));
//             }
//             reader.Close();
//             sCmd.Connection.Close();
//             sCmd.Connection.Dispose();
// 
//             foreach (var viewName in views)
//             {
//                 string selectStatement = "select * from kvartet.";
//                 MySqlCommand curCmd = new MySqlCommand(selectStatement + viewName, connection);
//                 var curDataAdapter = new MySqlDataAdapter(curCmd);
//                 MySqlCommandBuilder dummy = new MySqlCommandBuilder(curDataAdapter);
//                 curDataAdapter.Fill(ds, viewName);
//                 tableAdapters[viewName] = curDataAdapter;
// 
//                 DataGridView dgv = new DataGridView();
//                 dgv.AllowUserToAddRows = false;
//                 dgv.AllowUserToDeleteRows = false;
//                 dgv.AllowUserToResizeColumns = true;
//                 dgv.AllowUserToResizeRows = true;
//                 dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
//                 dgv.DataSource = ds;
//                 dgv.DataMember = viewName;
//                 dgv.Dock = DockStyle.Fill;
//                 tabControl1.TabPages.Add(viewName, viewName);
//                 tabControl1.TabPages[viewName].Controls.Add(dgv);
//             }
//         }

        private void updateToDBToolStripMenuItem_Click(object sender, EventArgs e)
        {

            mySqlDataSet.UpdateTables();
//             foreach (var pair in tableAdapters)
//             {
//                 pair.Value.Update(ds, pair.Key);
//             }
        }
    }
}
