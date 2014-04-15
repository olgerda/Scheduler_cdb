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
    public partial class FindClientCard : Form
    {
        IClientList clientList;

        IClient selectedClient;

        IFactory entityFactory;

        bool doNothingNow;

        public FindClientCard()
        {
            InitializeComponent();

            Init();
        }

        public FindClientCard(IClientList clientList, IFactory entityFactory)
        {
            InitializeComponent();

            this.clientList = clientList;
            this.entityFactory = entityFactory;

            Init();
        }

        void Init()
        {
            //grpEditMode.Location = grpSelectClient.Location;
            doNothingNow = false;
            if (clientList == null)
                return;

            lstClientList.DataSource = clientList.List.Cast<INamedEntity>().ToList();
            lstClientList.DisplayMember = "Name";

            var customAutoComplete = new AutoCompleteStringCollection();
            customAutoComplete.AddRange(lstClientList.Items.Cast<IClient>().Select(c => c.Name).ToArray());
            txtClientName.AutoCompleteCustomSource = customAutoComplete;

            customAutoComplete = new AutoCompleteStringCollection();
            customAutoComplete.AddRange(lstClientList.Items.Cast<IClient>().SelectMany(c => c.Telephones).ToArray());
            txtTelephone.AutoCompleteCustomSource = customAutoComplete;

            clientInfoCard.OnSaveChanges += new SaveChangesHandler<IClient>(clientInfoCard_OnSaveChanges);
            clientInfoCard.EntityFactory = entityFactory;
        }

        void clientInfoCard_OnSaveChanges(object source, SaveChangesEventArgs<IClient> e)
        {
            DeactivateEditMode();
        }

        public IClientList ClientList
        {
            get { return clientList; }
            set
            {
                clientList = value;
                Init();
            }
        }

        public IFactory EntityFactory
        {
            get { return entityFactory; }
            set
            {
                entityFactory = value;
                clientInfoCard.EntityFactory = entityFactory;
            }
        }

        public IClient SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                if (selectedClient == null)
                    return;
                doNothingNow = true;
                txtClientName.Text = selectedClient.Name;
                txtTelephone.Text = String.IsNullOrEmpty(selectedClient.Telephones.FirstOrDefault(t => t.StartsWith(txtTelephone.Text))) ? selectedClient.Telephones.FirstOrDefault()
                    : selectedClient.Telephones.FirstOrDefault(t => t.StartsWith(txtTelephone.Text));
                doNothingNow = false;
                clientInfoCard.Client = selectedClient;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            if (lstClientList.SelectedIndex == -1)
                return;
            ActivateEditMode((IClient)lstClientList.SelectedItem);
        }

        private void ActivateEditMode(IClient clientInfo)
        {
            clientInfoCard.Client = clientInfo;
            /*//grpSelectClient.Visible = false;*/
            grpSelectClient.Enabled = false;
            grpSelectClient.Visible = false;

            grpEditMode.Visible = true;
            grpEditMode.Enabled = true;
            clientInfoCard.Enabled = true;
        }

        private IClient DeactivateEditMode()
        {
            IClient result = clientInfoCard.Client;

            if (result != null) //если значение не установилось - пользователь отменил закрытие.
            {
                grpSelectClient.Visible = true;
                grpSelectClient.Enabled = true;
                grpEditMode.Visible = false;
                grpEditMode.Enabled = false;
                clientInfoCard.Enabled = false;

                if (!clientList.List.Contains(result))
                {
                    //clientList.List.Add(result);
                    clientList.Add(result);
                }
                lstClientList.DataSource = clientList.List.Cast<INamedEntity>().ToList();
                lstClientList.SelectedItem = result;
            }
            return result;
            //return null;
        }

        private void btnEditModeOff_Click(object sender, EventArgs e)
        {
            DeactivateEditMode();
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            IClient newClient = entityFactory.NewClient();
            ActivateEditMode(newClient);
            //clientList.List.Add(newClient);
        }

        private void lstClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClientList.SelectedIndex == -1)
                return;
            //if (SelectedClient != (IClient)lstClientList.SelectedItem)
                SelectedClient = (IClient)lstClientList.SelectedItem;

            //            clientInfoCard.Client = selectedClient;
            //             txtClientName.Text = selectedClient.Name;
            //             txtTelephone.Text = selectedClient.Telephones.FirstOrDefault();
        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {
            if (doNothingNow)
                return;
            IClient curClient = null;
            if (txtClientName.Text.Length > 4 && !String.IsNullOrWhiteSpace(txtClientName.Text))
                curClient = clientList.FindClientByPartialName(txtClientName.Text);
            if (curClient != null)
                lstClientList.SelectedItem = curClient;
            else
                lstClientList.SelectedIndex = -1;
        }

        private void txtTelephone_TextChanged(object sender, EventArgs e)
        {
            if (doNothingNow)
                return;
            IClient curClient = null;
            if (txtTelephone.Text.Length > 3 && !String.IsNullOrWhiteSpace(txtTelephone.Text))
                curClient = clientList.FindClientByPartialTelephone(txtTelephone.Text);
            if (curClient != null)
                lstClientList.SelectedItem = curClient;
            else
                lstClientList.SelectedIndex = -1;
        }

        private void FindClientCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientList.ValidateAndUpdate();
        }

        private void lstClientList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Delete && lstClientList.SelectedIndex != -1)
            {
                ClientList.Remove((IClient)lstClientList.SelectedItem);
                lstClientList.DataSource = ClientList.List.Cast<INamedEntity>().ToList();
            }
        }




    }
}
