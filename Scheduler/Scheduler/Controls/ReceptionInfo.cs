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

        private Dictionary<int, int> currentSpecialistCosts = new Dictionary<int, int>();

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
            CloneExist,
            CommentOnly
        }

        public ReceptionInfo()
        {
            InitializeComponent();
        }

        //public ReceptionInfo(ShowModes mode = ShowModes.CreateNew)
        //{
        //    InitializeComponent();
        //    this.mode = mode;
        //    SetMode();
        //}

        //public ReceptionInfo(IEnumerable<ICabinet> cabinetList, IEnumerable<ISpecialist> specialistList, IEnumerable<string> specialisationList, ShowModes mode = ShowModes.CreateNew)
        //{
        //    InitializeComponent();
        //    this.mode = mode;
        //    SetMode();
        //    this.cabinetList = cabinetList;
        //    this.specialistList = specialistList;
        //    this.specialisationList = specialisationList;
        //}

        //public ReceptionInfo(IEnumerable<ICabinet> cabinetList, IEnumerable<ISpecialist> specialistList, IEnumerable<string> specialisationList, IReception reception, ShowModes mode = ShowModes.CreateNew)
        //{
        //    InitializeComponent();
        //    this.mode = mode;
        //    SetMode();
        //    this.cabinetList = cabinetList;
        //    this.specialistList = specialistList;
        //    this.specialisationList = specialisationList;
        //    this.reception = reception;
        //    InitializeReceptionInfo();
        //}

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
                return clientOnReception;
            }
            set
            {
                clientOnReception = value;
                if (clientOnReception == null)
                    return;
                txtClientName.Text = clientOnReception.Name;
                txtTelephone.Text = clientOnReception.Telephones.FirstOrDefault() ?? String.Empty;
                //txtAdministrator.Text = clientOnReception.Administrator;
            }
        }

        void SetMode()
        {
            doNothing = false;

            var controlsNotForCommentOnly = new Control[] { grpClient, grpOther, grpReceptionParams };
            foreach (var ctrl in controlsNotForCommentOnly)
                ctrl.Enabled = true;

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
                    btnCancelReception.Enabled = true;
                    btnCreateChildReception.Enabled = true;
                    btnShowClientCard.Enabled = true;
                    break;
                case ShowModes.CommentOnly:
                    foreach (var ctrl in controlsNotForCommentOnly)
                        ctrl.Enabled = false;
                    break;
            }
        }

        void RenewLists()
        {
            if (cabinetList != null)
            {
                cmbCabinet.DataSource = cabinetList.ToList();
                cmbCabinet.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }

            if (specialisationList != null)
            {
                cmbSpecialisation.DataSource = specialisationList.ToList();
                cmbSpecialisation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }

            if (specialistList != null)
            {
                cmbSpecialist.DataSource = specialistList.ToList();
                cmbSpecialist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
        }

        void InitializeReceptionInfo()
        {
            if (reception == null)
                return;

            RenewLists();

            chkRent.Checked = reception.Rent;
            dateDate.Value = reception.ReceptionTimeInterval.Date;
            dateTimeStart.Value = reception.ReceptionTimeInterval.StartDate;
            dateTimeEnd.Value = reception.ReceptionTimeInterval.EndDate;

            if (reception.Cabinet != null)
                cmbCabinet.SelectedItem = reception.Cabinet;
            if (reception.Specialist != null)
            {
                foreach (var spec in cmbSpecialist.Items.Cast<ISpecialist>())
                {
                    if (spec.Equals(reception.Specialist))
                    {
                        cmbSpecialist.SelectedItem = spec;
                        break;
                    }
                }
            }
            if (!String.IsNullOrWhiteSpace(reception.Specialization))
                cmbSpecialisation.SelectedItem = reception.Specialization;
            else
                if (reception.Specialist != null && reception.Specialist.Specialisations.FirstOrDefault() != null)
                cmbSpecialisation.SelectedItem = reception.Specialist.Specialisations.First();

            numericPrice.Value = reception.Price;

            txtAdministrator.Text = reception.Administrator;

            ClientOnReception = reception.Client;

            chkReceptionDidNotTakePlace.Checked = reception.ReceptionDidNotTakePlace;
            txtComment.Text = reception.Comment;
            chkSpecialRent.Checked = reception.SpecialRent;
        }

        bool SomethingChanged()
        {
            if (reception == null || reception.Cabinet == null || reception.Specialist == null || doNothing)
                return false;

            return
                reception.Rent != chkRent.Checked ||
                (Scheduler_InterfacesRealisations.CommonObjectWithNotify)reception.Cabinet != (Scheduler_InterfacesRealisations.CommonObjectWithNotify)cmbCabinet.SelectedItem ||
                (Scheduler_InterfacesRealisations.CommonObjectWithNotify)reception.Specialist != (Scheduler_InterfacesRealisations.CommonObjectWithNotify)cmbSpecialist.SelectedItem ||
                reception.Price != Convert.ToInt32(numericPrice.Value) ||
                reception.ReceptionTimeInterval.Date != dateDate.Value ||
                reception.ReceptionTimeInterval.StartDate != dateTimeStart.Value ||
                reception.ReceptionTimeInterval.EndDate != dateTimeEnd.Value ||
                (!chkRent.Checked &&
                    (
                        reception.Specialization != cmbSpecialisation.SelectedItem.ToString() ||
                        (Scheduler_InterfacesRealisations.CommonObjectWithNotify)reception.Client != (Scheduler_InterfacesRealisations.CommonObjectWithNotify)clientOnReception
                    )
                ) ||
                reception.Price != (int)numericPrice.Value ||
                reception.Administrator != txtAdministrator.Text ||
                chkReceptionDidNotTakePlace.Checked != reception.ReceptionDidNotTakePlace ||
                txtComment.Text != reception.Comment ||
                chkSpecialRent.Checked != reception.SpecialRent;

            ;

        }

        void SaveChanges()
        {
            if (reception == null || dummyReception == null)
                return;

            if (dateTimeEnd.Value - dateTimeStart.Value < TimeSpan.FromMinutes(10))
            {
                MessageBox.Show("Время между началом и концом приёма меньше 10 минут.", "Ошибка при сохранении результатов.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dummyReception.ID = reception.ID;

            dummyReception.Rent = chkRent.Checked;
            if (chkRent.Checked)
            {
                dummyReception.Client = null;
                dummyReception.Specialization = null;
            }
            else
            {
                dummyReception.Client = ClientOnReception;
                dummyReception.Specialization = (String)cmbSpecialisation.SelectedItem;
            }

            dummyReception.Cabinet = (ICabinet)cmbCabinet.SelectedItem;
            dummyReception.Specialist = (ISpecialist)cmbSpecialist.SelectedItem;

            dummyReception.ReceptionTimeInterval.StartDate = dateTimeStart.Value;
            dummyReception.ReceptionTimeInterval.EndDate = dateTimeEnd.Value;
            dummyReception.Price = Convert.ToInt32(numericPrice.Value);

            dummyReception.Administrator = txtAdministrator.Text;
            dummyReception.ReceptionDidNotTakePlace = chkReceptionDidNotTakePlace.Checked;
            dummyReception.SpecialRent = chkSpecialRent.Checked;
            dummyReception.Comment = txtComment.Text;

            string errorMessage = dummyReception.Validate();

            if (errorMessage != null)
            {
                MessageBox.Show(errorMessage, "Ошибка при сохранении результатов.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            doNothing = true;

            reception.Client = dummyReception.Client;
            reception.Rent = dummyReception.Rent;
            reception.Cabinet = dummyReception.Cabinet;
            reception.Specialist = dummyReception.Specialist;
            reception.Specialization = dummyReception.Specialization;
            reception.ReceptionTimeInterval = dummyReception.ReceptionTimeInterval;
            reception.Price = dummyReception.Price;
            reception.Administrator = dummyReception.Administrator;

            reception.ReceptionDidNotTakePlace = chkReceptionDidNotTakePlace.Checked;
            reception.Comment = txtComment.Text;
            reception.SpecialRent = chkSpecialRent.Checked;
            if (OnSaveChanges != null)
                OnSaveChanges(this, new SaveChangesEventArgs<IReception>(reception));
            doNothing = false;
            return;


        }

        private void dateDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimeStart.Value = dateDate.Value.Date + dateTimeStart.Value.TimeOfDay;
            dateTimeEnd.Value = dateDate.Value.Date + dateTimeEnd.Value.TimeOfDay;
        }

        private void chkRent_CheckedChanged(object sender, EventArgs e)
        {
            //pnlClient.Enabled = !chkRent.Checked;
            //btnShowClientCard.Enabled = !chkRent.Checked;
            ActualizePrice();
        }

        private void btnShowClientCard_Click(object sender, EventArgs e)
        {
            if (OnShowClientsButtonClicked != null)
                OnShowClientsButtonClicked(this, new ShowClientsEventsArgs(txtClientName.Text, txtTelephone.Text));

            ActualizePrice();
        }

        private void btnCreateChildReception_Click(object sender, EventArgs e)
        {
            if (OnCreateChildReceptionClicked != null)
                OnCreateChildReceptionClicked(this, new CreateChildReceptionEventArgs(reception));
        }

        private void btnCancelReception_Click(object sender, EventArgs e)
        {
            if (DateTime.Today.Date - reception.ReceptionTimeInterval.Date < TimeSpan.FromDays(1) ||
                MessageBox.Show("Данный приём уже в прошлом. Всё-равно удалить?", "Прошедший приём.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (OnCancelReceptionClicked != null)
                    OnCancelReceptionClicked(this, new CancelReceptionEventArgs(reception));
            }

        }

        private void cmbSpecialist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecialist.SelectedIndex == -1)
                return;
            ISpecialist currentSpec = (ISpecialist)cmbSpecialist.SelectedItem;
            currentSpecialistCosts = currentSpec.GetCosts();

            cmbSpecialisation.DataSource = null;
            cmbSpecialisation.Items.Clear();
            cmbSpecialisation.DataSource = currentSpec.Specialisations.ToArray();

            ActualizePrice();
        }

        void ActualizePrice()
        {
            if (chkRent.Checked)
            {
                if (currentSpecialistCosts.ContainsKey(Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.CLIENTRENTID))
                    numericPrice.Value = currentSpecialistCosts[Scheduler_DBobjects_Intefraces.Scheduler_DBconnector.CLIENTRENTID];
            }
            else
            {
                if (clientOnReception != null)
                    if (currentSpecialistCosts.ContainsKey(clientOnReception.ID))
                        numericPrice.Value = currentSpecialistCosts[clientOnReception.ID];
                    else
                        numericPrice.Value = clientOnReception.GenerallyPrice;
            }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

    }
}
