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
        private IEnumerable<ICabinet> cabinetList;
        private IEnumerable<ISpecialist> specialistList;
        private IEnumerable<string> specialisationList;

        private IClient clientOnReception = null;

        private static IReception dummyReception = null;

        public static IReception DummyReception
        {
            set
            {
                if (dummyReception == null)
                    dummyReception = value;
            }
        }

        private IReception reception;
        private ShowModes mode;
        private bool doNothing;

        public event SaveChangesHandler<IReception> OnSaveChanges;
        public event ShowClientsHandler OnShowClientsButtonClicked;
        public event CreateChildReceptionHandler OnCreateChildReceptionClicked;
        public event CancelReceptionHandler OnCancelReceptionClicked;

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

        public ReceptionInfo(IEnumerable<ICabinet> cabinetList, IEnumerable<ISpecialist> specialistList, IEnumerable<string> specialisationList, ShowModes mode = ShowModes.CreateNew)
        {
            InitializeComponent();
            this.mode = mode;
            SetMode();
            this.cabinetList = cabinetList;
            this.specialistList = specialistList;
            this.specialisationList = specialisationList;
        }

        public ReceptionInfo(IEnumerable<ICabinet> cabinetList, IEnumerable<ISpecialist> specialistList, IEnumerable<string> specialisationList, IReception reception, ShowModes mode = ShowModes.CreateNew)
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

        public void UpdateLists(IEnumerable<ICabinet> cabinetList, IEnumerable<ISpecialist> specialistList, IEnumerable<string> specialisationList)
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
            get
            {
                if (SomethingChanged())
                {
                    var dresult = MessageBox.Show("Сохранить изменения?", "Некоторые поля изменены.", MessageBoxButtons.YesNoCancel);
                    if (dresult == DialogResult.Cancel)
                        return null;
                    if (dresult == DialogResult.Yes)
                    {
                        SaveChanges();
                    }
                }

                return reception;
            }
            set
            {
                if (SomethingChanged())
                {
                    var dresult = MessageBox.Show("Сохранить изменения?", "Некоторые поля изменены.", MessageBoxButtons.YesNoCancel);
                    if (dresult == DialogResult.Cancel)
                        return;
                    if (dresult == DialogResult.Yes)
                    {
                        SaveChanges();
                    }
                }
                reception = value;
                InitializeReceptionInfo();
            }
        }

        public IClient ClientOnReception
        {
            get
            {
                //                 clientOnReception.Name = txtClientName.Text;
                //                 clientOnReception.Telephones.Add(txtTelephone.Text);
                return clientOnReception;
            }
            set
            {
                clientOnReception = value;
                if (clientOnReception == null)
                    return;
                txtClientName.Text = clientOnReception.Name;
                txtTelephone.Text = clientOnReception.Telephones.FirstOrDefault();
            }
        }

        void SetMode()
        {
            doNothing = false;

            switch (mode)
            {
                case ShowModes.CreateNew:
                    chkRent.Checked = false;
                    btnCancelReception.Enabled = false;
                    btnCreateChildReception.Enabled = false;
                    btnShowClientCard.Enabled = true;
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
                cmbCabinet.DataSource = cabinetList.ToList();
                //                 if (reception.Cabinet != null && cabinetList.Any(f => reception.Cabinet == f))
                //                     cmbCabinet.SelectedItem = reception.Cabinet.Name;

                cmbCabinet.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //                 cmbCabinet.Items.Clear();
                //                 cmbCabinet.Items.AddRange(cabinetList.ToArray());
            }

            if (specialisationList != null)
            {
                cmbSpecialisation.DataSource = specialisationList.ToList();

                //                 if (!String.IsNullOrWhiteSpace(reception.Specialization) && specialisationList.Any(s => s == reception.Specialization))
                //                     cmbSpecialisation.SelectedItem = reception.Specialization;

                cmbSpecialisation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //                 cmbSpecialisation.Items.Clear();
                //                 cmbSpecialisation.Items.AddRange(specialisationList.ToArray());
            }

            if (specialistList != null)
            {
                cmbSpecialist.DataSource = specialistList.ToList();

                //                 if (reception.Specialist != null && specialistList.Any(s => reception.Specialist == s))
                //                     cmbSpecialist.SelectedItem = reception.Specialist;

                cmbSpecialist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //                 cmbSpecialist.Items.Clear();
                //                 cmbSpecialist.Items.AddRange(specialistList.ToArray());
            }
        }

        void InitializeReceptionInfo()
        {
            if (reception == null)
                return;

            RenewLists();

            //txtClientName.Text = reception.Client.Name;
            chkRent.Checked = reception.Rent;
            dateDate.Value = reception.ReceptionTimeInterval.Date;
            dateTimeStart.Value = reception.ReceptionTimeInterval.StartDate;
            dateTimeEnd.Value = reception.ReceptionTimeInterval.EndDate;

            if (reception.Cabinet != null && cabinetList.Any(f => reception.Cabinet == f))
                cmbCabinet.SelectedItem = reception.Cabinet;
            if (reception.Specialist != null && specialistList.Any(s => reception.Specialist == s))
                cmbSpecialist.SelectedItem = reception.Specialist;
            if (!String.IsNullOrWhiteSpace(reception.Specialization) && specialisationList.Any(s => s == reception.Specialization))
                cmbSpecialisation.SelectedItem = reception.Specialization;

            //txtTelephone.Text = reception.Client.Telephones.FirstOrDefault();
            ClientOnReception = reception.Client;
        }

        //         private void button1_Click(object sender, EventArgs e)
        //         {
        //             if (SomethingChanged())
        //                 SaveChanges();
        //         }

        bool SomethingChanged()
        {
            if (reception == null || reception.Cabinet == null || reception.Specialist == null || doNothing)
                return false;

            return
                reception.Rent != chkRent.Checked ||
                reception.Cabinet != cmbCabinet.SelectedItem ||
                reception.Specialist != cmbSpecialist.SelectedItem ||

                reception.ReceptionTimeInterval.Date != dateDate.Value ||
                reception.ReceptionTimeInterval.StartDate != dateTimeStart.Value ||
                reception.ReceptionTimeInterval.EndDate != dateTimeEnd.Value ||
                (chkRent.Checked &&
                    (
                        reception.Specialization != cmbSpecialisation.SelectedText ||
                        reception.Client != clientOnReception
                    )
                );

        }

        void SaveChanges()
        {
            if (reception == null || dummyReception == null)
                return;

            dummyReception.Client = ClientOnReception;
            //             reception.Client.Name = txtClientName.Text;
            // 
            //             var tmp = reception.Client.Telephones;
            //             tmp.Add(txtTelephone.Text);
            //             reception.Client.Telephones = tmp;

            dummyReception.Rent = chkRent.Checked;
            dummyReception.Cabinet = (ICabinet)cmbCabinet.SelectedItem;
            dummyReception.Specialist = (ISpecialist)cmbSpecialist.SelectedItem;
            dummyReception.Specialization = (string)cmbSpecialisation.SelectedItem;
            dummyReception.ReceptionTimeInterval.StartDate = dateTimeStart.Value;
            dummyReception.ReceptionTimeInterval.EndDate = dateTimeEnd.Value;

            string errorMessage = dummyReception.Validate();
            if (errorMessage == null)
            {
                doNothing = true;

                reception.Client = dummyReception.Client;
                reception.Rent = dummyReception.Rent;
                reception.Cabinet = dummyReception.Cabinet;
                reception.Specialist = dummyReception.Specialist;
                reception.Specialization = dummyReception.Specialization;
                reception.ReceptionTimeInterval = dummyReception.ReceptionTimeInterval;

                if (OnSaveChanges != null)
                    OnSaveChanges(this, new SaveChangesEventArgs<IReception>(reception));
                doNothing = false;
                return;
            }

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
            btnShowClientCard.Enabled = !chkRent.Checked;
        }

        private void btnShowClientCard_Click(object sender, EventArgs e)
        {
            if (OnShowClientsButtonClicked != null)
                OnShowClientsButtonClicked(this, new ShowClientsEventsArgs(txtClientName.Text, txtTelephone.Text));
        }

        private void btnCreateChildReception_Click(object sender, EventArgs e)
        {
            if (OnCreateChildReceptionClicked != null)
                OnCreateChildReceptionClicked(this, new CreateChildReceptionEventArgs(reception));
        }

        private void btnCancelReception_Click(object sender, EventArgs e)
        {
            if (DateTime.Today.Date - reception.ReceptionTimeInterval.Date < TimeSpan.FromDays(1))
            {
                if (OnCancelReceptionClicked != null)
                    OnCancelReceptionClicked(this, new CancelReceptionEventArgs(reception));
            }
            else
                MessageBox.Show("Данное посещение уже в прошлом.","Прошедшее посещение.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cmbSpecialist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
    }
}
