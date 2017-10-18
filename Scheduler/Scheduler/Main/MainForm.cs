using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;
using Scheduler_Forms;
using Scheduler_DBobjects_Intefraces;
using System.Reflection;

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
        /// Таблица, хранящая в себе посещения на текущую дату
        /// </summary>
        ITable receptionEntitiesTable;

        IMainDataBase database;
        private Scheduler_InterfacesRealisations.SortableList<IDuty> specsOnDuty = new Scheduler_InterfacesRealisations.SortableList<IDuty>();
        Scheduler_InterfacesRealisations.SortableList<IDuty> adminsOnDuty = new Scheduler_InterfacesRealisations.SortableList<IDuty>();
        CalendarControl3.ColumnsView calendarControl;
        List<int> columnOrder = new List<int>();
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
            calendarControl = new CalendarControl3.ColumnsView();

            calendarControl.Dock = DockStyle.Fill;
            mainView.Panel1.Controls.Add(calendarControl);
            calendarControl.Table = receptionEntitiesTable;
            calendarControl.MouseClick += new MouseEventHandler(calendarControl_MouseClick);

            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;
            this.MinimumSize = new Size(calendarControl.MinimumSize.Width + mainView.Panel2MinSize, calendarControl.MinimumSize.Height + mainView.Location.Y + titleHeight + 20);

            ReceptionInfoEdit.SetLists(cabinetList: database.CabinetList, specialistList: database.SpecialistList,
                specializationList: database.SpecializationList,
                entityFactory: database.EntityFactory, clientList: database.ClientList,
                arendatorList: database.ArendatorList);
            ReceptionInfoEdit.Database = Database;
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
            string cabname = calendarControl.GetColumnNameOnClick();
            if (cabname == null)
                return;
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

                    if (ent.CommentOnlyReception)
                        receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.CommentOnly;
                    else
                        receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.CreateNew;
                    receptionEditForm.Reception = ent;
                    if (receptionEditForm.ShowDialog() == DialogResult.OK)
                    {
                        database.AddReception(ent);
                    }
                }
                else
                {
                    if (ent.CommentOnlyReception)
                        receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.CommentOnly;
                    else
                        receptionEditForm.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.ReadExist;
                    receptionEditForm.Reception = ent;
                    var dresult = receptionEditForm.ShowDialog();

                    switch (dresult)
                    {
                        case DialogResult.Abort:
                            database.RemoveReception(ent);
                            break;
                        case DialogResult.OK:
                            database.UpdateReception(ent);
                            break;
                    }
                }

                ReloadEntities();
            }
        }

        void FirstLoad()
        {
            columnOrder = Properties.Settings.Default.ColumnOrder.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            receptionEntitiesTable = database.EntityFactory.NewTable();
            ITimeInterval workday = database.EntityFactory.NewTimeInterval();
            workday.SetStartEnd(new DateTime(1, 1, 1, 9, 0, 0), new DateTime(1, 1, 1, 22, 0, 0));
            receptionEntitiesTable.WorkTimeInterval = workday;
            Main.ProgramSettings2.FromStrings(Properties.Settings.Default.LegacyColorSettings.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));

            Dictionary<DateTime, string> descriprions = new Dictionary<DateTime, string>(13);
            for (int i = 0; i <= 23; i++) //TODO: дикий хардкод, переписать на что-то вразумительное 
            {
                DateTime date = new DateTime(1, 1, 1, i, 0, 0);
                descriprions.Add(date, date.ToShortTimeString());
            }

            receptionEntitiesTable.SetInfoColumnDescriptions(descriprions);

            database.EntityFactory.NewEntity().SetDatabase(database);

            lstAdministratorsOnDuty.DataSource = adminsOnDuty;
            lstAdministratorsOnDuty.DisplayMember = "Name";
            lstSpecialistsOnDuty.DataSource = specsOnDuty;
            lstSpecialistsOnDuty.DisplayMember = "Name";
            monthCalendar1.SetDate(DateTime.Now);            
        }

        void ReloadEntities()
        {
            receptionEntitiesTable.Columns.ForEach(c => c.Entities.Clear());

            var listOfEntities = database.SelectReceptionsFromDate(schedule_date);

            foreach (var ent in listOfEntities)
            {
                receptionEntitiesTable.Columns.Find(x => x.Name == ent.Cabinet.Name).Entities.Add(ent);
            }

            ResetColors();
            calendarControl?.Refresh();
        }

        void ReloadColumns()
        {
            receptionEntitiesTable.Columns.Clear();

            var cabs = database.CabinetList.List.Where(c => c.Availability).ToDictionary(x => x.ID, x =>
            {
                var column = database.EntityFactory.NewColumn();
                column.Name = x.Name;
                column.OnlyComment = x.CommentOnly;
                return column;
            });

            foreach (var id in columnOrder)
                receptionEntitiesTable.Columns.Add(cabs[id]);
            foreach (var cab in cabs.Where(x => !columnOrder.Contains(x.Key)).Select(x => x.Value))
                receptionEntitiesTable.Columns.Add(cab);

            //foreach (var cabname in database.CabinetList.List.Where(c => c.Availability))
            //{
            //    var column = database.EntityFactory.NewColumn();
            //    column.Name = cabname.Name;
            //    column.OnlyComment = cabname.CommentOnly;
            //    receptionEntitiesTable.Columns.Add(column);
            //}
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
            if (database.ErrorString != null)
            {
                MessageBox.Show(database.ErrorString, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                database.ClearErrorString();
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
            if (database.ErrorString != null)
            {
                MessageBox.Show(database.ErrorString, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                database.ClearErrorString();
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
            using (CabinetListEdit cabinetForm = new CabinetListEdit(database.CabinetList, database.EntityFactory, columnOrder))
            {                
                cabinetForm.ShowDialog();
                columnOrder = cabinetForm.CabinetOrder;
                Properties.Settings.Default.ColumnOrder = String.Join(" ", columnOrder);
                Properties.Settings.Default.Save();
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
            specsOnDuty.Clear();
            foreach (var duty in database.SelectDutyFromDate<ISpecialist>(monthCalendar1.SelectionStart))
                specsOnDuty.Add(duty);

            adminsOnDuty.Clear();
            foreach (var duty in database.SelectDutyFromDate<IAdministrator>(monthCalendar1.SelectionStart))
                adminsOnDuty.Add(duty);

            schedule_date = monthCalendar1.SelectionStart.Date;

            ReloadColumns();
            ReloadEntities();
            calendarControl?.Refresh();
        }

        private void контактыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var settings = new Scheduler.Forms.ColorSettings())
            {
                settings.SetColors(Scheduler.Main.ProgramSettings2.ControlsColors);
                settings.ShowDialog();
            }
            ResetColors();
            Properties.Settings.Default.LegacyColorSettings = String.Join(Environment.NewLine, Main.ProgramSettings2.ToStrings());
            Properties.Settings.Default.Save();
        }

        private void ResetColors()
        {
            receptionEntitiesTable.SetColors(Main.ProgramSettings2.ControlsColors);
            calendarControl?.Refresh();
        }

        private void арендаторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FindClientCard clientsForm = new FindClientCard(database.ArendatorList, database.EntityFactory, 1))
            {
                clientsForm.ShowDialog();
            }
        }

        private void дежурстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dutyForm = new Scheduler.Forms.SpecialistDutyForm())
            {
                dutyForm.SetDatabase(database, database.EntityFactory);
                dutyForm.ShowDialog();
            }

            monthCalendar1_DateChanged(null, null);
        }

        private void администраторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Scheduler.Forms.AdministratorListEdit specialistForm = new Scheduler.Forms.AdministratorListEdit(database.EntityFactory, database))
            {
                specialistForm.ShowDialog();
            }
        }

        string GetSaveFile()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.Filter = "CSV (*.csv)|*.csv|All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                    return dialog.FileName;
            }
            return null;
        }

        void SaveFileWUTF8Preamble(string file, IEnumerable<string> lines)
        {
            using (var bw = new System.IO.BinaryWriter(System.IO.File.OpenWrite(file)))
            {
                bw.Write(System.Text.Encoding.UTF8.GetPreamble());
                foreach (var line in lines)
                {
                    var data = System.Text.Encoding.UTF8.GetBytes(line + Environment.NewLine);
                    bw.Write(data);
                }
            }
        }

        private string GenerateDelimitedString(params string[] data)
        {
            return String.Join(",", data);
        }

        private void GetClientsStatistics(object sender, EventArgs e)
        {
            var saveFile = GetSaveFile();

            if (String.IsNullOrWhiteSpace(saveFile))
                return;

            List<string> result = new List<string>();
            result.Add(GenerateDelimitedString("ФИО", "Контакты", "Последний приём", "Всего приёмов", "Специалисты"));

            foreach (var cl in Database.ClientList.List)
            {
                var name = cl.Name;
                var contacts = String.Join(" ", cl.Telephones.Concat(new string[] { cl.EMail }));
                var receptions = cl.GetReceptions();
                DateTime receptionLastDate = default(DateTime);
                int receptionsCount = receptions.Count;

                if (receptionsCount > 0)
                {
                    receptionLastDate = receptions.Max(x => x.ReceptionTimeInterval.Date).Date;
                }

                var specialists = String.Join(" ",
                    receptions.Where(x => x.Specialist != null).Select(x => x.Specialist.Name).Distinct());
                string record = GenerateDelimitedString(name, contacts, receptionLastDate.ToShortDateString(), receptionsCount.ToString(), specialists);
                result.Add(record);
            }

            SaveFileWUTF8Preamble(saveFile, result);
        }

        private void GetArendatorsStatistics(object sender, EventArgs e)
        {
            var saveFile = GetSaveFile();

            if (String.IsNullOrWhiteSpace(saveFile))
                return;

            List<string> result = new List<string>();
            result.Add(GenerateDelimitedString("ФИО", "Контакты", "Всего аренд", "Последняя аренда"));

            foreach (var ar in Database.ArendatorList.List)
            {
                var name = ar.Name;
                var contacts = String.Join(" ", ar.Telephones.Concat(new string[] { ar.EMail }));
                var receptions = ar.GetReceptions();
                result.Add(GenerateDelimitedString(name, contacts, receptions.Count.ToString(), receptions.Count > 0 ? receptions.Max(x => x.ReceptionTimeInterval.StartDate).ToShortDateString() : default(DateTime).ToShortDateString()));
            }

            SaveFileWUTF8Preamble(saveFile, result);
        }

        private void GetSpecialistsStatistics(object sender, EventArgs e)
        {
            var saveFile = GetSaveFile();

            if (String.IsNullOrWhiteSpace(saveFile))
                return;

            List<string> result = new List<string>();
            result.Add(GenerateDelimitedString("ФИО", "Всего приёмов", "Всего различных клиентов"));

            foreach (var sp in Database.SpecialistList.List)
            {
                var name = sp.Name;
                var receptionCount = Database.SpecialistGetReceptionCount(sp);
                var clientCount = Database.SpecialistGetClientCount(sp);
                result.Add(GenerateDelimitedString(name, receptionCount.ToString(), clientCount.ToString()));
            }

            SaveFileWUTF8Preamble(saveFile, result);
        }

        private void GetClientsReport(object sender, EventArgs e)
        {
            var saveFile = GetSaveFile();

            if (String.IsNullOrWhiteSpace(saveFile))
                return;

            List<string> result = new List<string>();
            result.Add(GenerateDelimitedString("ФИО", "Контакты", "Дата приёма", "Время приёма", "Специалист", "Явка", "Порядковый номер"));

            foreach (var cl in Database.ClientList.List)
            {
                var name = cl.Name;
                var contacts = String.Join(" ", cl.Telephones.Concat(new string[] { cl.EMail }));
                var receptions = cl.GetReceptions();
                for (int i = 0; i < receptions.Count; i++)
                {
                    result.Add(GenerateDelimitedString(name, contacts,
                        receptions[i].ReceptionTimeInterval.Date.ToShortDateString(),
                        receptions[i].ReceptionTimeInterval.Interval(),
                        receptions[i].Specialist.Name,
                        receptions[i].ReceptionDidNotTakePlace ? "-" : "+",
                        (i + 1).ToString()));
                }
            }

            SaveFileWUTF8Preamble(saveFile, result);
        }

        private void GetArendatorsReport(object sender, EventArgs e)
        {
            var saveFile = GetSaveFile();

            if (String.IsNullOrWhiteSpace(saveFile))
                return;

            List<string> result = new List<string>();
            result.Add(GenerateDelimitedString("ФИО", "Контакты", "Дата аренды", "Время аренды"));

            foreach (var ar in Database.ArendatorList.List)
            {
                var name = ar.Name;
                var contacts = String.Join(" ", ar.Telephones.Concat(new string[] { ar.EMail }));
                var receptions = ar.GetReceptions();
                foreach (var rcptn in receptions)
                    result.Add(GenerateDelimitedString(name, contacts, rcptn.ReceptionTimeInterval.Date.ToShortDateString(), rcptn.ReceptionTimeInterval.Interval()));
            }

            SaveFileWUTF8Preamble(saveFile, result);
        }

        private void GetSpecialistsReport(object sender, EventArgs e)
        {
            var saveFile = GetSaveFile();

            if (String.IsNullOrWhiteSpace(saveFile))
                return;

            List<string> result = new List<string>();
            result.Add(GenerateDelimitedString("ФИО", "Дата приёма", "Кабинет", "Время приёма", "Клиент"));

            foreach (var sp in Database.SpecialistList.List)
            {
                var name = sp.Name;
                foreach (var rcptn in Database.SpecialistGetReceptions(sp))
                    result.Add(GenerateDelimitedString(name, rcptn.ReceptionTimeInterval.Date.ToShortDateString(), rcptn.Cabinet.Name, rcptn.ReceptionTimeInterval.Interval(), rcptn.Client.Name));
            }

            SaveFileWUTF8Preamble(saveFile, result);
        }
    }
}
