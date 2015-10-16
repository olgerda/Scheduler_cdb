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
        private DateTime schedule_date = DateTime.Today.Date;

        public DateTime ScheduleDate
        {
            get { return schedule_date; }
            set
            {
                schedule_date = value;
                ReloadEntities();
            }
        }

        /// <summary>
        /// глушилка действий, чтоб события друг-друга не закольцовывали
        /// </summary>
        private bool doNothing = false;

        /// <summary>
        /// Таблица, хранящая в себе посещения на текущую дату
        /// </summary>
        ITable receptionEntitiesTable;

        IMainDataBase database;

        CalendarControl3.ColumnsView calendarControl;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IMainDataBase database)
        {
            InitializeComponent();

            if (database.ErrorString != null)
            {
                MessageBox.Show(database.ErrorString, "Ошибка подключения в базе данных.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                database.ClearErrorString();
            }
            else
            {
                Database = database;
            }
        }

        void Init()
        {
            FirstLoad();
            dateTimePicker1.Value = schedule_date;
            calendarControl = new CalendarControl3.ColumnsView();
            calendarControl.Dock = DockStyle.Fill;
            mainView.Panel1.Controls.Add(calendarControl);
            calendarControl.Table = receptionEntitiesTable;
            calendarControl.MouseClick += new MouseEventHandler(calendarControl_MouseClick);

            Rectangle screenRectangle=RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;
            this.MinimumSize = new System.Drawing.Size(calendarControl.MinimumSize.Width + mainView.Panel2MinSize, calendarControl.MinimumSize.Height + mainView.Location.Y + titleHeight + 20);

            ReceptionInfoEdit.SetLists(database.CabinetList, database.SpecialistList, database.SpecializationList, database.ClientList, database.EntityFactory);
        }

        public IMainDataBase Database
        {
            get { return database; }
            set
            {
                database = value;
                Init();
            }
        }

        void calendarControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            IEntity ent = calendarControl.GetEntityOnClick() as IEntity;
            using (ReceptionInfoEdit receptionEditForm = new ReceptionInfoEdit())
            {
                if (ent == null)
                {
                    ent = database.EntityFactory.NewEntity();
                    ent.Cabinet = database.CabinetList.List.Find(x => x.Name == calendarControl.GetColumnNameOnClick());

                    IEntity nearestTopEntity = (calendarControl.GetNearestTopEntity() as IEntity);
                    int nearestTopEntityBottomLevel = nearestTopEntity == null ? -1 : nearestTopEntity.BottomLevel;
                    int clickLevel = calendarControl.GetVerticalValueOfClick(e.Location);
                    int nearestLevel = clickLevel;
                    try
                    {
                        nearestLevel = receptionEntitiesTable.GetDescripptionsToValueLevels().Keys.Where(i => i <= clickLevel).Max();
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    var maximum = Math.Max(Math.Max(nearestTopEntityBottomLevel, nearestLevel), receptionEntitiesTable.MinValue);
                    var timeInterval = database.EntityFactory.NewTimeInterval();
                    timeInterval.StartDate = schedule_date + receptionEntitiesTable.ConvertLevelToTime(maximum).TimeOfDay;
                    timeInterval.EndDate = timeInterval.StartDate + new TimeSpan(1, 0, 0);
                    ent.ReceptionTimeInterval = timeInterval;

                    receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.CreateNew;
                    receptionEditForm.Reception = ent;
                    if (receptionEditForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        database.AddReception(ent);
                    }
                }
                else
                {
                    receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.ReadExist;
                    receptionEditForm.Reception = ent;
                    var dresult = receptionEditForm.ShowDialog();

                    switch (dresult)
                    {
                        case System.Windows.Forms.DialogResult.Abort:
                            database.RemoveReception(ent);
                            break;
                        case System.Windows.Forms.DialogResult.OK:
                            database.UpdateReception(ent);
                            break;
                        //case System.Windows.Forms.DialogResult.Yes:
                        //    database.AddReception((IEntity)receptionEditForm.Reception);
                        //    break;
                    }
                }

                ReloadEntities();
            }

        }

        void FirstLoad()
        {
            receptionEntitiesTable = database.EntityFactory.NewTable();
            ITimeInterval workday = database.EntityFactory.NewTimeInterval();
            workday.SetStartEnd(new DateTime(1, 1, 1, 10, 0, 0), new DateTime(1, 1, 1, 22, 0, 0));
            receptionEntitiesTable.WorkTimeInterval = workday;

            Dictionary<DateTime, string> descriprions = new Dictionary<DateTime, string>(13);
            for (int i = 10; i <= 22; i++) //дикий хардкод, переписать на что-то вразумительное 
            {
                DateTime date = new DateTime(1, 1, 1, i, 0, 0);
                descriprions.Add(date, date.ToShortTimeString());
            }

            receptionEntitiesTable.SetInfoColumnDescriptions(descriprions);

            database.EntityFactory.NewEntity().SetDatabase(database);
            
            ReloadColumns();
            ReloadEntities();
        }

        void ReloadEntities()
        {
            receptionEntitiesTable.Columns.ForEach(c => c.Entities.Clear());

            var listOfEntities = database.SelectReceptionsFromDate(schedule_date);

            foreach (var ent in listOfEntities)
            {
                receptionEntitiesTable.Columns.Find(x => x.Name == ent.Cabinet.Name).Entities.Add(ent);
            }
            if (calendarControl != null)
                calendarControl.Refresh();
        }

        void ReloadColumns()
        {
            receptionEntitiesTable.Columns.Clear();
            foreach (var cabname in database.CabinetList.List.Where(c => c.Availability))
            {
                var column = database.EntityFactory.NewColumn();
                column.Name = cabname.Name;
                receptionEntitiesTable.Columns.Add(column);
            }
        }

        private void сделатьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckFileExists = false;
                dlg.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    database.MakeBackup(dlg.FileName);
            }
        }

        private void развернутьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    database.RestoreBackup(dlg.FileName);
            }
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FindClientCard clientsForm = new FindClientCard(database.ClientList, database.EntityFactory))
            {
                clientsForm.ShowDialog();
            }
        }

        private void кабинетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CabinetListEdit cabinetForm = new CabinetListEdit(database.CabinetList, database.EntityFactory))
            {
                cabinetForm.ShowDialog();
            }
            ReloadColumns();
            ReloadEntities();
        }

        private void специалистыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SpecialistListEdit specialistForm = new SpecialistListEdit(database.SpecialistList, database.SpecializationList, database.EntityFactory))
            {
                specialistForm.ShowDialog();
            }
        }

        private void специальностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SpecializationListEdit specializationsForm = new SpecializationListEdit(database.SpecializationList))
            {
                specializationsForm.ShowDialog();
            }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            doNothing = true;
            ScheduleDate = dateTimePicker1.Value.Date;
            monthCalendar1.SetDate(ScheduleDate);
            doNothing = false;
            
        }

        private void сформироватьОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Reports reportsForm = new Reports(database))
            {
                reportsForm.ShowDialog();
            }
        }

        private void btnCreteFile_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog save = new SaveFileDialog() { Filter = "jpeg (*.jpeg)|*.jpeg|bmp (*.bmp)|*.bmp", FileName = DateTime.Today.ToString("yyyy-MM-dd") })
            {
                if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (System.Drawing.Bitmap bmp = calendarControl.GenerateBitmap())
                    {
                        bmp.Save(save.FileName, save.FilterIndex == 1 ? System.Drawing.Imaging.ImageFormat.Bmp : System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }

        }

        private void mainView_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (!doNothing)
                dateTimePicker1.Value = e.Start;
        }



    }
}
