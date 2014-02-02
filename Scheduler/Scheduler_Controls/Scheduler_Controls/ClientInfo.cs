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
    /// <summary>
    /// Контрол для отображения свойств конкретного клиента.
    /// </summary>
    public partial class ClientInfo : UserControl
    {
        private IClient client;

        public ClientInfo()
        {
            InitializeComponent();
        }

        public ClientInfo(IClient client)
        {
            InitializeComponent();
            this.client = client;
            InitializeClientInfo();
        }

        /// <summary>
        /// Получить/задать текущее представление клиента.
        /// </summary>
        public IClient Client
        {
            get { return client; }
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
                client = value;
                InitializeClientInfo();
            }
        }

        void InitializeClientInfo()
        {
            if (client == null)
                return;

            txtFIO.Text = client.Name;
            txtComment.Text = client.Comment;
            lstReceptions.Items.AddRange(client.Receptions.ToArray());
            lstTelephones.Items.AddRange(client.Telephones.ToArray());

            chkBlackList.Checked = client.BlackListed;

        }

        bool SomethingChanged()
        {
            if (client == null)
                return false;

            return 
                client.Name != txtFIO.Text ||
                client.Comment != txtComment.Text ||
                client.BlackListed != chkBlackList.Checked ||
                client.Telephones.SequenceEqual(lstTelephones.Items.Cast<string>());
        }

        private void btnRemoveTelephone_Click(object sender, EventArgs e)
        {
            if (lstTelephones.SelectedIndex == -1)
                return;

            lstTelephones.Items.RemoveAt(lstTelephones.SelectedIndex);
            if (lstTelephones.Items.Count > 0)
                lstTelephones.SelectedItem = lstTelephones.Items[lstTelephones.Items.Count - 1];
        }

        private void btnAddTelephone_Click(object sender, EventArgs e)
        {

            using (AddTelNumber f = new AddTelNumber())
            {
                f.StartPosition = FormStartPosition.Manual;
                Point p = btnAddTelephone.PointToScreen(btnAddTelephone.Location);
                p.Y -= this.Height;
                f.Location = p;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrWhiteSpace(f.number) && !lstTelephones.Items.Cast<string>().ToList().Contains(f.number))
                        lstTelephones.Items.Add(f.number);
                    lstTelephones.SelectedItem = f.number;
                }
            }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            if (SomethingChanged())
                SaveChanges();
        }

        private void SaveChanges()
        {
            client.Name = txtFIO.Text;
            client.Comment = txtComment.Text;

            var telList = new HashSet<string>(lstTelephones.Items.AsQueryable().Cast<string>());
            client.Telephones = telList;
//             var telListToRemove = client.Telephones.Except(telList);
//             var telListToAdd = telList.Except(client.Telephones);
// 
//             foreach (var s in telListToRemove.Union(telListToRemove))
//                 client.Telephone = s;

            client.BlackListed = chkBlackList.Checked;
        }

    }
}
