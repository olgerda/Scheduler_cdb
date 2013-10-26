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
using System.Linq;


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

        /// <summary>
        /// Таблица, хранящая в себе посещения на текущую дату
        /// </summary>
        TableOfEntities receptionEntitiesTable;

        /*
         * DESIGN TIME. Переписать после отработки функционала на серию запросов к БД напрямую. Будет медлненнее, но не будет зажираться в память целая копия всех таблиц...
         * Или так оставить. Надо протестировать на большом объёме данных.
         * Сейчас нет никакой проверки на успешность соединения с БД. Если соединения нет - какой-то подвисон.
         */
        MySqlDataSet datasets;
        /*
         * 
         */

        /// <summary>
        /// Рабочее время. Используется для масштабного отображения и проверок границ.
        /// </summary>
        public static TimeInterval workDay = new TimeInterval(new DateTime(1, 1, 1, 8, 0, 0), new DateTime(1, 1, 1, 20, 0, 0));
        /// <summary>
        /// Период, который будет отображаться на сетке расписания. Влияет только на отображение.
        /// </summary>
        public static TimeSpan workDelta = new TimeSpan(2, 0, 0);

        private static List<DateTime> GetWorkTimeList
        {
            get 
            {
                List<DateTime> result = new List<DateTime>();
                DateTime curTime = workDay.StartDate;
                while (curTime < workDay.EndDate)
                {
                    result.Add(curTime);
                    curTime.Add(workDelta);
                }
                return result;
            }
        }

		public MainForm(/*DbConnect database*/)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            datasets = new MySqlDataSet();

            FirstLoad();
            CalendarControl3.ColumnsView ctrl = new CalendarControl3.ColumnsView();
            ctrl.Dock = DockStyle.Fill;
            mainView.Panel1.Controls.Add(ctrl);
            ctrl.Table = receptionEntitiesTable;
            ctrl.MouseClick += new MouseEventHandler(ctrl_MouseClick);

			//schedule.ColumnCount = 1 + database.Count("SELECT count(*) FROM cab");
			datePicker.Text = schedule_date.Day.ToString() + "." + 
				schedule_date.Month.ToString() + "." + schedule_date.Year.ToString();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}



        /*
         * WORKZONE
         */
        void ctrl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            var ent = (Entity)((CalendarControl3.ColumnsView)sender).GetEntityOnClick(e.Location);
            var column = (ColumnOfEntities)receptionEntitiesTable.GetColumns().Find(x => x.GetEntities().Contains(ent));
            if (ent == default(Entity))
                CreateNewEntity();
            else
            {
                EditEntity(ref ent, column);
            }
        }

        void EditEntity(ref Entity entToEdit, ColumnOfEntities column)
        {
            MessageBox.Show("Under construction!");
            EditReception f = new EditReception(datasets);
            
            f.EditedEntity = entToEdit;
            f.ShowDialog();
            entToEdit = f.EditedEntity;
        }

        void CreateNewEntity()
        {
            MessageBox.Show("CreateEntity form not implemented yet...");
        }

        /*
         * /WORKZONE
         */

        void FirstLoad()
        {
            receptionEntitiesTable = new TableOfEntities();
            receptionEntitiesTable.workTime = new TimeInterval(new DateTime(1, 1, 1, 8, 0, 0), new DateTime(1, 1, 1, 18, 0, 0));
            //var listOfEntities = database.SelectFromDate(schedule_date);
            var listOfEntities = datasets.SelectEntityByDate(new DateTime(2013, 10, 14));
            List<ColumnOfEntities> listOfColumns = new List<ColumnOfEntities>();
            foreach (var ent in listOfEntities)
            {
                var column = listOfColumns.Find(x => x.GetName() == ent.cabinet.Name);
                if (column == null) 
                {
                    column = new Scheduler.ColumnOfEntities(ent.cabinet.Name);
                    listOfColumns.Add(column);
                }
                column.AddEntity(ent);
            }

            foreach (var col in listOfColumns)
                receptionEntitiesTable.columns.Add(col);
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
