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

namespace Scheduler_Forms
{
    public partial class FindClientCard : Form
    {
        IClientList clientList;

        IClient selectedClient;

        public FindClientCard()
        {
            InitializeComponent();

            StartInit();
        }

        public FindClientCard(IClientList clientList)
        {
            InitializeComponent();

            this.clientList = clientList;

            StartInit();
        }

        void StartInit()
        {
            grpEditMode.Location = grpSelectClient.Location;

            lstClientList.DisplayMember = "Name";
            if (clientList != null)
                lstClientList.DataSource = clientList.List;

            var customAutoComplete = new AutoCompleteStringCollection();
            customAutoComplete.AddRange(lstClientList.Items.Cast<IClient>().Select(c => c.Name).ToArray());
            txtClientName.AutoCompleteCustomSource = customAutoComplete;

            customAutoComplete = new AutoCompleteStringCollection();
            customAutoComplete.AddRange(lstClientList.Items.Cast<IClient>().SelectMany(c => c.Telephones).ToArray());
            txtTelephone.AutoCompleteCustomSource = customAutoComplete;
        }

        IClientList ClientList
        {
            get { return clientList; }
            set
            {
                clientList = value;
                StartInit();
            }
        }

        IClient SelectedClient
        {
            get { return selectedClient; }
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
            grpSelectClient.Visible = false;
            grpSelectClient.Enabled = false;

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
            }
            return result;

        }

        private void btnEditModeOff_Click(object sender, EventArgs e)
        {
            DeactivateEditMode();
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
//             IClient newClient = new IClient();//IClient.CreateBlank();
//             ActivateEditMode(newClient);
//             clientList.List.Add(newClient);
        }

        private void lstClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClientList.SelectedIndex == -1)
                return;
            selectedClient = (IClient)lstClientList.SelectedItem;

            clientInfoCard.Client = selectedClient;
            txtClientName.Text = selectedClient.Name;
            txtTelephone.Text = selectedClient.Telephones.FirstOrDefault();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lstClientList.SelectedIndex != -1)
                selectedClient = (IClient)lstClientList.SelectedItem;
            this.Close();
        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {
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
            IClient curClient = null;
            if (txtTelephone.Text.Length > 3 && !String.IsNullOrWhiteSpace(txtTelephone.Text))
                curClient = clientList.FindClientByPartialTelephone(txtTelephone.Text);
            if (curClient != null)
                lstClientList.SelectedItem = curClient;
            else
                lstClientList.SelectedIndex = -1;
        }
        


    }
}
