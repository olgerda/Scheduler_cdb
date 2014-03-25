using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;
using Scheduler_Common_Interfaces;

namespace Scheduler_Forms
{
    public partial class ReceptionInfoEdit : Form
    {
        IReception currentReception;
        Scheduler_Controls.ReceptionInfo.ShowModes mode;

        static ISpecialistList specialistList;
        static ISpecializationList specializationsList;
        static ICabinetList cabinetList;
        static IClientList clientList;
        static IFactory entityFactory;

        public ReceptionInfoEdit()
        {
            InitializeComponent();

            receptionInfoCard.OnSaveChanges += new SaveChangesHandler<IReception>(receptionInfoCard_OnSaveChanges);
            receptionInfoCard.OnShowClientsButtonClicked += new ShowClientsHandler(receptionInfoCard_OnShowClientsButtonClicked);
            receptionInfoCard.OnCreateChildReceptionClicked += new CreateChildReceptionHandler(receptionInfoCard_OnCreateChildReceptionClicked);
            receptionInfoCard.OnCancelReceptionClicked += new CancelReceptionHandler(receptionInfoCard_OnCancelReceptionClicked);

            receptionInfoCard.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.ReadExist;
        }

        void receptionInfoCard_OnCancelReceptionClicked(object source, CancelReceptionEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить запись?", "Удаление записи.", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;
            currentReception.Dispose();
            this.Close();
        }

        void receptionInfoCard_OnCreateChildReceptionClicked(object source, CreateChildReceptionEventArgs e)
        {
            ReceptionInfoEdit childForm = new ReceptionInfoEdit();

            childForm.Reception = currentReception;

            childForm.Reception.ReceptionTimeInterval.StartDate = DateTime.Now;
            childForm.Reception.ReceptionTimeInterval.EndDate = DateTime.Now + new TimeSpan(1, 0, 0);
            childForm.receptionInfoCard.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.CloneExist;
            childForm.Show();
        }

        void receptionInfoCard_OnSaveChanges(object source, SaveChangesEventArgs<IReception> e)
        {
            Reception = receptionInfoCard.Reception;
            this.Close();
        }

        void receptionInfoCard_OnShowClientsButtonClicked(object source, ShowClientsEventsArgs e)
        {
            //тут логика показа формы с выбором клиента
            using (FindClientCard FindClientForm = new FindClientCard(clientList, entityFactory))
            {
                FindClientForm.SelectedClient = clientList.FindClientByPartialTelephone(e.Telephone) ?? clientList.FindClientByPartialName(e.Name);
                FindClientForm.ShowDialog();
                currentReception.Client = FindClientForm.SelectedClient;
                if (currentReception.Client != null)
                {
                    receptionInfoCard.ClientOnReception = currentReception.Client;
                }
            }

        }



        public IReception Reception
        {
            get { return currentReception; }
            set 
            { 
                currentReception = value; 
                Init(); 
            }
        }

        public ISpecialistList SpecialistList
        {
            set { specialistList = value; Init(); }
        }

        public ISpecializationList SpecializationList
        {
            set { specializationsList = value; Init(); }
        }

        public ICabinetList CabinetList
        {
            set { cabinetList = value; Init(); }
        }

        public IClientList ClientList
        {
            set { clientList = value; }
        }

        public IFactory EntityFactory
        {
            set { entityFactory = value; }
        }

        public Scheduler_Controls.ReceptionInfo.ShowModes Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                receptionInfoCard.Mode = mode;
            }
        }

        public static void SetLists(ICabinetList cabinetList, ISpecialistList specialistList, ISpecializationList specializationList, 
            IClientList clientList, IFactory entityFactory)
        {
            ReceptionInfoEdit.cabinetList = cabinetList;
            ReceptionInfoEdit.specialistList = specialistList;
            ReceptionInfoEdit.specializationsList = specializationList;
            ReceptionInfoEdit.clientList = clientList;
            ReceptionInfoEdit.entityFactory = entityFactory;
        }

        void Init()
        {
            if (currentReception == null)
                return;

            receptionInfoCard.Reception = currentReception;

            if (cabinetList == null || specialistList == null || specializationsList == null)
                return;

            var cabinetListActualised = cabinetList.List.Where(c => c.Availability);
            var specialistListActualised = specialistList.List.Where(s => !s.NotWorking);

            receptionInfoCard.UpdateLists(cabinetListActualised, specialistListActualised, specializationsList.SpecializationList);
        }

        private void ReceptionInfoEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reception = receptionInfoCard.Reception;
        }

        private void receptionInfoCard_Load(object sender, EventArgs e)
        {

        }


    }
}
