using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class EditReception : Form
    {
        TableOfEntities table;
        CalendarControl3.ColumnsView ctrl;

        public MySqlDataSet datasets;

        private Entity toReturn;

        private bool programmaticalyClosing;

        private List<string> errorlist;

        private List<SpecialistCard> specialistList;
        private List<Specialization> specializationList;
        private List<ClientCard> clientList;
        private List<CabinetCard> cabinetList;

        public EditReception(MySqlDataSet data)
        {
            InitializeComponent();

            datePicker.Value = DateTime.Now;
            programmaticalyClosing = false;

            errorlist = new List<string>();

            datasets = data;
            

        }

        private void LoadEntity(Entity curEntity)
        {
            if (curEntity == null) return;
            toReturn = curEntity;
            InitializeLists();
            InitializeControls();
            
            table = new TableOfEntities();
            ctrl = new CalendarControl3.ColumnsView();
            ctrl.Table = table;
            table.workTime = Scheduler.MainForm.workDay;

            List<Entity> toDayEntities = datasets.SelectEntityByDate(toReturn.date.Date).FindAll(x => x.cabinet.Name == toReturn.cabinet.Name);
            ColumnOfEntities column = new ColumnOfEntities(toReturn.cabinet.Name);
            column.AddEntities(toDayEntities);
            table.columns.Add(column);

            splitContainer1.Panel1.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;

            
        }

        private Entity UnloadEntity()
        {
            /*
             * Общая внезапная мысль. Вынести создание/редактирование клиента в другое окно. Слишком много функционала на одно окно. В это добавить кнопку создания если только и возможность редактировать комментарий.
             * Тогда надо подгружать список клиентов и в поле ввода имени клиента подставлять возможные значения. Несуществующего недопускать (выдавать запрос на создание с последующим вызовом другой формы).
             */
            if (toReturn == null) return new Entity();
            
            if (toReturn.date.Date != datePicker.Value.Date || 
                toReturn.date.StartDate == datePicker.Value.Date.Add(timePickerStart.Value.TimeOfDay) ||
                toReturn.date.EndDate == datePicker.Value.Date.Add(timePickerEnd.Value.TimeOfDay))
            {
                toReturn.date = new TimeInterval(datePicker.Value.Date.Add(timePickerStart.Value.TimeOfDay), datePicker.Value.Date.Add(timePickerEnd.Value.TimeOfDay));
            }

            if (chkRent.Checked && !toReturn.client.comment.StartsWith("АРЕНДА"))
            {
                ClientCard cli = clientList.FirstOrDefault(x => x.comment.StartsWith("АРЕНДА"));
                if (cli == default(ClientCard)) cli = new ClientCard(new FIO(""), newComment:"АРЕНДА");
                toReturn.client = cli;
            }
            else
            {
                if (toReturn.client.Name.ToString() != cmbbxClientFIO.Text)
                { //Имя другое - клиент другой.
                    ClientCard cli = clientList.FirstOrDefault(x => x.Name.ToString() == cmbbxClientFIO.Text);
                    
                    toReturn.client = cli;
                }
            }

            if (toReturn.cabinet.Name != cmbbxCabinet.Text)
                toReturn.cabinet = cabinetList.Find(x => x.Name == cmbbxCabinet.Text);
            if (toReturn.specialist.Name.ToString() != cmbbxSpecialist.Text)
            {
                toReturn.specialist = specialistList.Find(x => x.Name.ToString() == cmbbxSpecialist.Text);
            }
            /*
             * 
             */
            return new Entity();
        }

        public Entity EditedEntity
        {
            get { return toReturn; }
            set { LoadEntity(value);}
        }

        private void InitializeControls()
        {
            if (toReturn == null) return;
            
            datePicker.Value = toReturn.date.Date;
            timePickerStart.Value = toReturn.date.StartDate;
            timePickerEnd.Value = toReturn.date.EndDate;

            if (!toReturn.client.comment.StartsWith("АРЕНДА"))
            {
                chkRent.Checked = false;

                cmbbxClientFIO.Text = toReturn.client.Name.ToString();
                txtClientTelephone.Text = toReturn.client.telNumber.ToString();
                txtClientComment.Text = toReturn.client.comment;
                chkClientinRedBox.Checked = toReturn.client.inRedList;

                cmbbxSpecialization.Text = toReturn.specialization.Title;
            }
            else
                chkRent.Checked = true;
            
            
            cmbbxSpecialist.Text = toReturn.specialist.Name.ToString();

            cmbbxCabinet.Text = toReturn.cabinet.Name;

            cmbbxCabinet.DataSource = cabinetList; cmbbxCabinet.DisplayMember = "Name";
            cmbbxSpecialist.DataSource = specialistList; cmbbxSpecialist.DisplayMember = "Name";
            cmbbxSpecialization.DataSource = specializationList; cmbbxSpecialization.DisplayMember = "Title";
            cmbbxClientFIO.DataSource = clientList; cmbbxClientFIO.DisplayMember = "Name";

        }

        void InitializeLists()
        {
            specializationList = datasets.SelectAllSpecializations();
            specialistList = datasets.SelectAllSpecialists();
            clientList = datasets.SelectAllClients();
            cabinetList = datasets.SelectAllCabinets();
        }

        /// <summary>
        /// Проверить корректность введённых данных: непротиворечивость времени приёма, наложение на другие приёмы, правильность задания имени клиента, специалиста, специальности и т.д.
        /// </summary>
        /// <returns></returns>
        private bool ValidateValues()
        {
            /*
             * 
             */
            MessageBox.Show("Проверка правильности заполнения полей в процессе разработки.");
            /*
             * 
             */
            var entries = datasets.SelectEntityByDate(datePicker.Value);
            TimeInterval now = new TimeInterval(datePicker.Value.Date.Add(timePickerStart.Value.TimeOfDay), datePicker.Value.Date.Add(timePickerEnd.Value.TimeOfDay));
            foreach (var e in entries)
            {
                if (TimeInterval.IsIntersect(e.date, now))
                {
                    errorlist.Add("Пересечение с приёмом " + e.date.Interval());
                    return false;
                }
            }

            
            return true;
        }

        private void DateChangedHandler(object sender, DateRangeEventArgs e)
        {
            datePicker.Value = e.Start;
        }

        private void BoxDateChangedHandler(object sender, EventArgs e)
        {
            calendarPicker.SetDate(datePicker.Value);
        }

        private void RentCheckChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            grpClient.Enabled = !chk.Checked;
            grpSpecialization.Enabled = !chk.Checked;

        }

        private void btnAcceptChanges_Click(object sender, EventArgs e)
        {
            if (ValidateValues())
            {
                toReturn = UnloadEntity();
                programmaticalyClosing = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Обнаружены ошибки ввода. Пожалуйста исправьте.\r\n" + string.Join("\r\n", errorlist.ToArray()));
            }
        }

        private void ClosingFormHandler(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !programmaticalyClosing)
            {
                var btn = MessageBox.Show("Сохранить изменения?", "Закрытие формы", MessageBoxButtons.YesNoCancel);
                switch (btn)
                {
                    case DialogResult.Yes:
                        if (ValidateValues())
                        {
                            toReturn = UnloadEntity();
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClientFIOChangeHandler(object sender, EventArgs e)
        {
            ClientCard cli = clientList.ElementAtOrDefault(cmbbxClientFIO.SelectedIndex);
        }


        //private ulong _telNimberOld = 0;
        private void TelephoneNumberChangeHandler(object sender, EventArgs e)
        {
            List<ClientCard> cliList;

            if (txtClientTelephone.Text.Length == 0)
                cliList = clientList;
            else
            {
                cliList = clientList.Where(x => x.telNumber.ToString().StartsWith(txtClientTelephone.Text)).ToList();
            }

            cmbbxClientFIO.DataSource = cliList;
        }
    }
}
