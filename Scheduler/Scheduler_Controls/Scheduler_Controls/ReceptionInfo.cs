using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduler_Controls_Interfaces;

namespace Scheduler_Controls
{


    public partial class ReceptionInfo : UserControl
    {
        private IEnumerable<string> cabinetList;
        private IEnumerable<string> specialistList;
        private IEnumerable<string> specialisationList;

        private IReception reception;
        private ShowModes mode;

        public enum ShowModes
        {
            ReadExist,
            CreateNew,
            CloneExist
        }

        public ReceptionInfo()
        {
            InitializeComponent();
        }

        public ReceptionInfo(ShowModes mode = ShowModes.CreateNew)
        {
            InitializeComponent();
            this.mode = mode;
            SetMode();
        }

        public ReceptionInfo(IEnumerable<string> cabinetList, IEnumerable<string> specialistList, IEnumerable<string> specialisationList, ShowModes mode = ShowModes.CreateNew)
        {
            InitializeComponent();
            this.mode = mode;
            SetMode();
            this.cabinetList = cabinetList;
            this.specialistList = specialistList;
            this.specialisationList = specialisationList;
        }

        public ReceptionInfo(IEnumerable<string> cabinetList, IEnumerable<string> specialistList, IEnumerable<string> specialisationList, IReception reception, ShowModes mode = ShowModes.CreateNew)
        {
            InitializeComponent();
            this.mode = mode;
            SetMode();
            this.cabinetList = cabinetList;
            this.specialistList = specialistList;
            this.specialisationList = specialisationList;
            this.reception = reception;
            InitializeReceptionInfo();
        }

        public void UpdateLists(IEnumerable<string> cabinetList, IEnumerable<string> specialistList, IEnumerable<string> specialisationList)
        {
            this.cabinetList = cabinetList;
            this.specialistList = specialistList;
            this.specialisationList = specialisationList;
            RenewLists();
        }

        public ShowModes Mode
        {
            get { return mode; }
            set { mode = value; SetMode(); }
        }

        public IReception Reception
        {
            get { return reception; }
            set
            {
                if (SomethingChanged())
                {
                    var dresult = MessageBox.Show("Сохранить изменения?", "Некоторые поля изменены.", MessageBoxButtons.YesNoCancel);
                    if (dresult == DialogResult.Cancel)
                        return;
                    if (dresult == DialogResult.OK)
                    {
                        SaveChanges();
                    }
                }
                reception = value;
                InitializeReceptionInfo();
            }
        }

        void SetMode()
        {
            switch (mode)
            {
                case ShowModes.CreateNew:
                    chkRent.Checked = false;
                    btnCancelReception.Enabled = false;
                    btnCreateChildReception.Enabled = false;
                    btnShowClientCard.Enabled = false;
                    break;
                case ShowModes.CloneExist:
                    btnCancelReception.Enabled = false;
                    btnCreateChildReception.Enabled = false;
                    if (reception != null)
                        btnShowClientCard.Enabled = true;                    
                    else
                        btnShowClientCard.Enabled = false;                    
                    break;
                case ShowModes.ReadExist:
                    if (reception != null &&
                        reception.ReceptionTimeInterval.StartDate < DateTime.Today - new TimeSpan(1, 0, 0, 0))
                        btnCancelReception.Enabled = false;
                    else
                        btnCancelReception.Enabled = true;
                    btnCreateChildReception.Enabled = true;
                    btnShowClientCard.Enabled = true;
                    break;
            }
        }

        void RenewLists()
        {
            if (cabinetList != null)
            {
                cmbCabinet.Items.Clear();
                cmbCabinet.Items.AddRange(cabinetList.ToArray());
            }

            if (specialisationList != null)
            {
                cmbSpecialisation.Items.Clear();
                cmbSpecialisation.Items.AddRange(specialisationList.ToArray());
            }

            if (specialistList != null)
            {
                cmbSpecialist.Items.Clear();
                cmbSpecialist.Items.AddRange(specialistList.ToArray());
            }
        }

        void InitializeReceptionInfo()
        {
            if (reception == null)
                return;

            RenewLists();

            txtClientName.Text = reception.client.Name;
            chkRent.Checked = reception.rent;
            dateDate.Value = reception.ReceptionTimeInterval.Date;
            dateTimeStart.Value = reception.ReceptionTimeInterval.StartDate;
            dateTimeEnd.Value = reception.ReceptionTimeInterval.EndDate;

            txtTelephone.Text = reception.client.Telephones.First();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SomethingChanged())
                SaveChanges();
        }

        bool SomethingChanged()
        {
            if (reception == null)
                return false;

            return
                reception.client.Name != txtClientName.Text ||
                reception.client.Telephones.First() != txtTelephone.Text ||
                reception.rent != chkRent.Checked ||
                reception.cabinet.Name != cmbCabinet.SelectedText ||
                reception.specialist.Name != cmbSpecialist.SelectedText ||
                reception.specialization != cmbSpecialisation.SelectedText ||
                reception.ReceptionTimeInterval.Date != dateDate.Value ||
                reception.ReceptionTimeInterval.StartDate != dateTimeStart.Value ||
                reception.ReceptionTimeInterval.EndDate != dateTimeEnd.Value;
        }

        void SaveChanges()
        {
            if (reception == null)
                return;
            reception.client.Name = txtClientName.Text;

            var tmp = reception.client.Telephones;
            tmp.Add(txtTelephone.Text);
            reception.client.Telephones = tmp;

            reception.rent = chkRent.Checked;
            reception.cabinet.Name = cmbCabinet.SelectedText;
            reception.specialist.Name = cmbSpecialist.SelectedText;
            reception.specialization = cmbSpecialisation.SelectedText;
            reception.ReceptionTimeInterval.StartDate = dateTimeStart.Value;
            reception.ReceptionTimeInterval.EndDate = dateTimeEnd.Value;

            string errorMessage = reception.Validate();
            if (errorMessage == null)
                return;

            MessageBox.Show(errorMessage, "Ошибка при сохранении результатов.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dateDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimeStart.Value = dateDate.Value.Date + dateTimeStart.Value.TimeOfDay;
            dateTimeEnd.Value = dateDate.Value.Date + dateTimeEnd.Value.TimeOfDay;
        }

        private void chkRent_CheckedChanged(object sender, EventArgs e)
        {
            pnlClient.Enabled = !chkRent.Checked;
        }
    }
}
