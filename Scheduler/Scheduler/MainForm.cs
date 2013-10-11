/*
 * Created by SharpDevelop.
 * User: ANC_04
 * Date: 09.05.2013
 * Time: 14:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


//TEST
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
//

namespace Scheduler
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Дата, на которую строится расписание.
		/// </summary>
		public DateTime schedule_date = DateTime.Today;
		/// <summary>
		/// Флаг, указывающий, была ли дата изменена путем ввода строки в dataPicker (true) или выбором даты в календаре (false)
		/// </summary>
		bool changedByHand = false;
        
        /*
         * DESIGN TIME. Переписать после отработки функционала на серию запросов к БД напрямую. Будет медлненнее, но не будет зажираться в память целая копия всех таблиц...
         */
        MySqlDataSet datasets;
        /*
         * 
         */

		public MainForm(DbConnect database)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            /*
             * TEST
             */
//             datasets = new MySqlDataSet();
//             DataTable receptionsTable = datasets.ViewDataSet.Tables["reception_view"];
//             var receWptions = from receptionEntity in receptionsTable.AsEnumerable() 
//                              where receptionEntity.Field<DateTime>("receptionDate")==new DateTime(2013,10,07,0,0,0)
//                              select receptionEntity;
//             var dummy = from r in receptions
//                         select r.Field<TimeSpan>("startTime");
            TESTCASE();
            /*
             * /TEST
             */
			//schedule.ColumnCount = 1 + database.Count("SELECT count(*) FROM cab");
			datePicker.Text = schedule_date.Day.ToString() + "." + 
				schedule_date.Month.ToString() + "." + schedule_date.Year.ToString();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        void TESTCASE()
        {
            TableOfEntities testtbl = new TableOfEntities();
            testtbl.workTime = new TimeInterval(new DateTime(2013, 10, 11, 8, 0, 0), new DateTime(2013, 10, 11, 17, 30, 0));
            ColumnOfEntities testclmn = new ColumnOfEntities("101");
            Entity entity = new Entity(1, new TimeInterval(new DateTime(2013, 10, 11, 13, 0, 0), new DateTime(2013, 10, 11, 14, 30, 0)), new ClientCard(new FIO("1", "2", "3"), 1991234567), new SpecialistCard(new FIO("4", "5", "6"), new Specialization("spec1")), new Specialization("spec1"), new CabinetCard("101"));
            ColumnOfEntities testclmn2 = new ColumnOfEntities("102");

            testtbl.descs.Add(new DateTime(1, 1, 1, 8, 0, 0));
            testtbl.descs.Add(new DateTime(1, 1, 1, 9, 0, 0));
            testtbl.descs.Add(new DateTime(1, 1, 1, 10, 0, 0));
            testtbl.descs.Add(new DateTime(1, 1, 1, 11, 30, 0));
            testtbl.descs.Add(new DateTime(1, 1, 1, 13, 0, 0));
            testtbl.descs.Add(new DateTime(1, 1, 1, 14, 0, 0));
            testtbl.descs.Add(new DateTime(1, 1, 1, 16, 20, 0));

            testclmn.AddEntities(new List<Entity>(1) { entity });
            testtbl.columns.Add(testclmn);
            testtbl.columns.Add(testclmn2);
            for (int i = 1; i < 10; i++)
            {
                testtbl.columns.Add(new ColumnOfEntities(i.ToString()));
            }
            CalendarControl3.ColumnsView ctrl = new CalendarControl3.ColumnsView();
            ctrl.Dock = DockStyle.Fill;
            mainView.Panel1.Controls.Add(ctrl);
            ctrl.Table = testtbl;
        }

		void MonthCalendarDateChanged(object sender, DateRangeEventArgs e)
		{
			changedByHand = false;
			schedule_date = e.Start;
			datePicker.Text = schedule_date.Day.ToString() + "." + 
				schedule_date.Month.ToString() + "." + schedule_date.Year.ToString();
		}
		
		void DatePickerTextChanged(object sender, EventArgs e)
		{
			string[] date = datePicker.Text.Split(' ');
			string[] date_parsed = date[0].Split('.');
//			if(Convert.ToInt32(date_parsed[0]) / 100 == 0)
//				date_parsed[0] = "20" + date_parsed[0];
			if(date_parsed.Length == 3 && date_parsed[2].Length == 4)
				schedule_date = new DateTime(Convert.ToInt32(date_parsed[2]), Convert.ToInt32(date_parsed[1]), Convert.ToInt32(date_parsed[0]));
			if(changedByHand)
			{
				monthCalendar.SetSelectionRange(schedule_date, schedule_date);
				monthCalendar.SetDate(schedule_date);
				
			}
			
		}
    

//		string DateToString(DateTime date)
//		{
//			string day = date.Day.ToString();
			//if
//		}
	}
}
