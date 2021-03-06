﻿using System;
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
using Scheduler_DBobjects_Intefraces;

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
        static IClientList arendatorList;
        static IFactory entityFactory;

        private bool doNothing;
        private static IMainDataBase _database;

        public ReceptionInfoEdit()
        {
            InitializeComponent();

            doNothing = false;

            receptionInfoCard.OnSaveChanges += new SaveChangesHandler<IReception>(receptionInfoCard_OnSaveChanges);
            receptionInfoCard.OnShowClientsButtonClicked += new ShowClientsHandler(receptionInfoCard_OnShowClientsButtonClicked);
            receptionInfoCard.OnCreateChildReceptionClicked += new CreateChildReceptionHandler(receptionInfoCard_OnCreateChildReceptionClicked);
            receptionInfoCard.OnCancelReceptionClicked += new CancelReceptionHandler(receptionInfoCard_OnCancelReceptionClicked);

            receptionInfoCard.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.ReadExist;
            receptionInfoCard.Database = _database;
        }

        void receptionInfoCard_OnCancelReceptionClicked(object source, CancelReceptionEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить запись?", "Удаление записи.", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;
            this.DialogResult = DialogResult.Abort;
            doNothing = true;
            this.Close();
            doNothing = false;
        }

        void receptionInfoCard_OnCreateChildReceptionClicked(object source, CreateChildReceptionEventArgs e)
        {
            using (ReceptionInfoEdit childForm = new ReceptionInfoEdit())
            {
                var receptionNew = entityFactory.NewEntity();

                receptionNew.Client = e.Entity.Client;
                receptionNew.Cabinet = e.Entity.Cabinet;
                receptionNew.Specialist = e.Entity.Specialist;
                receptionNew.Specialization = e.Entity.Specialization;
                receptionNew.Price = e.Entity.Price;
                receptionNew.Rent = e.Entity.Rent;
                receptionNew.ReceptionTimeInterval = entityFactory.NewTimeInterval();
                receptionNew.ReceptionTimeInterval.StartDate = e.Entity.Client == null ? e.Entity.ReceptionTimeInterval.StartDate : DateTime.Now.Date + e.Entity.Client.GenerallyTime;
                receptionNew.ReceptionTimeInterval.EndDate = receptionNew.ReceptionTimeInterval.StartDate + TimeSpan.FromHours(1);
                receptionNew.Comment = e.Entity.Comment;
                receptionNew.Administrator = e.Entity.Administrator;
                receptionNew.SpecialRent = e.Entity.SpecialRent;

                childForm.Reception = receptionNew;
                childForm.receptionInfoCard.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.CloneExist;
                if (childForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    receptionNew.CommitToDatabase();
                }
            }
        }

        void receptionInfoCard_OnSaveChanges(object source, SaveChangesEventArgs<IReception> e)
        {
            if (e.Entity != null)
            {
                doNothing = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
                doNothing = false;
            }
        }

        void receptionInfoCard_OnShowClientsButtonClicked(object source, ShowClientsEventsArgs e)
        {
            IClientList clientList = e.ArendatorList ? ReceptionInfoEdit.arendatorList : ReceptionInfoEdit.clientList;
            //тут логика показа формы с выбором клиента
            using (FindClientCard FindClientForm = new FindClientCard(clientList, entityFactory, e.ArendatorList ? 1 : 0))
            {
                FindClientForm.SelectedClient = clientList.FindClientByPartialName(e.Name) ?? clientList.FindClientByTelephone(e.Telephone);
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
                if (!doNothing)
                    Init();
            }
        }

        public static Scheduler_DBobjects_Intefraces.IMainDataBase Database
        {
            get { return _database; }
            set
            {
                _database = value;
            }
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
            IClientList clientList, IClientList arendatorList, IFactory entityFactory)
        {
            ReceptionInfoEdit.cabinetList = cabinetList;
            ReceptionInfoEdit.specialistList = specialistList;
            ReceptionInfoEdit.specializationsList = specializationList;
            ReceptionInfoEdit.clientList = clientList;
            ReceptionInfoEdit.arendatorList = arendatorList;
            ReceptionInfoEdit.entityFactory = entityFactory;
            if (entityFactory != null)
            {
                var ent = entityFactory.NewEntity();
                ent.ReceptionTimeInterval = entityFactory.NewTimeInterval();
                Scheduler_Controls.ReceptionInfo.DummyReception = ent;
            }
        }

        void Init()
        {
            if (currentReception == null)
                return;

            if (cabinetList == null || specialistList == null || specializationsList == null)
                return;

            var cabinetListActualised = cabinetList.List.Where(c => c.Availability);
            var specialistListActualised = specialistList.List.Where(s => !s.NotWorking);

            receptionInfoCard.UpdateLists(cabinetListActualised, specialistListActualised, specializationsList.SpecializationList);

            receptionInfoCard.Reception = currentReception;
        }

        private void ReceptionInfoEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!doNothing)
            {

                var temp = receptionInfoCard.Reception;
                doNothing = true;
                if (temp == null)
                    e.Cancel = true;
                else
                    Reception = temp;
                doNothing = false;
            }
        }

        private void ReceptionInfoEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }


    }
}
