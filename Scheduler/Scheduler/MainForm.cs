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
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;
using Scheduler_Forms;
using Scheduler_DBobjects_Intefraces;

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
		private DateTime schedule_date = DateTime.Today;

        public DateTime ScheduleDate
        {
            get { return schedule_date; }
            set 
            { 
                schedule_date = value;
                FirstLoad();
                calendarControl.Table = receptionEntitiesTable;
            }
        }
		/// <summary>
		/// Флаг, указывающий, была ли дата изменена путем ввода строки в dataPicker (true) или выбором даты в календаре (false)
		/// </summary>
		bool changedByHand = false;

        /// <summary>
        /// Таблица, хранящая в себе посещения на текущую дату
        /// </summary>
        ITable receptionEntitiesTable;

        IMainDataBase database;

        CalendarControl3.ColumnsView calendarControl;

        /// <summary>
        /// Рабочее время. Используется для масштабного отображения и проверок границ.
        /// </summary>
//        public ITimeInterval workDay;// = new TimeInterval(new DateTime(1, 1, 1, 8, 0, 0), new DateTime(1, 1, 1, 20, 0, 0));
//         /// <summary>
//         /// Период, который будет отображаться на сетке расписания. Влияет только на отображение.
//         /// </summary>
//         public static TimeSpan workDelta = new TimeSpan(2, 0, 0);
// 
//         private List<DateTime> GetWorkTimeList
//         {
//             get 
//             {
//                 List<DateTime> result = new List<DateTime>();
//                 DateTime curTime = workDay.StartDate;
//                 while (curTime < workDay.EndDate)
//                 {
//                     result.Add(curTime);
//                     curTime.Add(workDelta);
//                 }
//                 return result;
//             }
//         }

		public MainForm()
		{

			InitializeComponent();

            Init();
		}

        public MainForm(IMainDataBase database)
        {
            InitializeComponent();

            this.database = database;

            Init();
        }

        void Init()
        {
            FirstLoad();
            calendarControl = new CalendarControl3.ColumnsView();
            calendarControl.Dock = DockStyle.Fill;
            mainView.Panel1.Controls.Add(calendarControl);
            calendarControl.Table = receptionEntitiesTable;
            calendarControl.MouseClick += new MouseEventHandler(calendarControl_MouseClick);

            datePicker.Text = schedule_date.Day.ToString() + "." +
                schedule_date.Month.ToString() + "." + schedule_date.Year.ToString();
            ReceptionInfoEdit.SetLists(database.CabinetList, database.SpecialistList, database.SpecializationList, database.ClientList, database.EntityFactory);
        }

        public IMainDataBase Database
        {
            get { return database; }
            set { database = value; }
        }

        void calendarControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            IEntity ent = ((CalendarControl3.ColumnsView)sender).GetEntityOnClick(e.Location) as IEntity;
            ReceptionInfoEdit receptionEditForm = new ReceptionInfoEdit();
            
            if (ent == null)
            {
                ent = database.EntityFactory.Create<IEntity>();
                ent.Cabinet = database.CabinetList.List.Find(x => x.Name == calendarControl.GetColumnNameOnClick(e.Location));

                IEntity nearestTopEntity = (calendarControl.GetNearestTopEntity(e.Location) as IEntity);
                var clickLevel = calendarControl.GetVerticalValueOfClick(e.Location);
                var nearestLevel = receptionEntitiesTable.GetDescripptionsToValueLevels().Keys.Select(i => (int)i).Where(i => i <= clickLevel).Max();

                var maximum = Math.Max(nearestTopEntity.BottomLevel, nearestLevel);
                ent.ReceptionTimeInterval.StartDate = schedule_date + receptionEntitiesTable.ConvertLevelToTime(maximum).TimeOfDay;
                ent.ReceptionTimeInterval.EndDate = ent.ReceptionTimeInterval.StartDate + new TimeSpan(1,0,0);

                receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.CreateNew;
                receptionEntitiesTable.Columns.Find(c => c.Name == ent.Cabinet.Name).Entities.Add(ent);
            }
            else
            {
                receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.ReadExist;
            }
            receptionEditForm.Reception = ent;
            receptionEditForm.ShowDialog();
        }

        void FirstLoad()
        {
            receptionEntitiesTable = database.EntityFactory.Create<ITable>();
            ITimeInterval workday = database.EntityFactory.Create<ITimeInterval>();
            workday.SetStartEnd(new DateTime(1, 1, 1, 8, 0, 0), new DateTime(1, 1, 1, 18, 0, 0));
            receptionEntitiesTable.WorkTimeInterval = workday;

            var listOfEntities = database.SelectReceptionsFromDate(schedule_date);

            List<IColumn> listOfColumns = new List<IColumn>();
            foreach (var ent in listOfEntities)
            {
                var column = listOfColumns.Find(x => x.Name == ent.Cabinet.Name);
                if (column == null) 
                {
                    column = database.EntityFactory.Create<IColumn>();
                    listOfColumns.Add(column);
                }
                column.AddEntity(ent);
            }

            foreach (var col in listOfColumns)
                receptionEntitiesTable.Columns.Add(col);//.columns.Add(col);
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

			if(date_parsed.Length == 3 && date_parsed[2].Length == 4)
				schedule_date = new DateTime(Convert.ToInt32(date_parsed[2]), Convert.ToInt32(date_parsed[1]), Convert.ToInt32(date_parsed[0]));
			if(changedByHand)
			{
				//monthCalendar.SetSelectionRange(schedule_date, schedule_date);
				monthCalendar.SetDate(schedule_date);
			}
			
		}

        private void сделатьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void развернутьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FindClientCard clientsForm = new FindClientCard(database.ClientList, database.EntityFactory))
            {
                clientsForm.Show();
            }            
        }
	}
}
