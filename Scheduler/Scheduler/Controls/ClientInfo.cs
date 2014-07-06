using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduler_Controls_Interfaces;
using Scheduler_Common_Interfaces;

namespace Scheduler_Controls
{
    /// <summary>
    /// Контрол для отображения свойств конкретного клиента.
    /// </summary>
    public partial class ClientInfo : UserControl
    {
        private IClient client;

        public event SaveChangesHandler<IClient> OnSaveChanges;

        private IFactory entityFactory;

        public ClientInfo()
        {
            InitializeComponent();
        }

        public ClientInfo(IClient client, IFactory entityFactory)
        {
            InitializeComponent();
            this.client = client;
            this.entityFactory = entityFactory;
            InitializeClientInfo();
        }

        /// <summary>
        /// Получить/задать текущее представление клиента.
        /// </summary>
        public IClient Client
        {
            get
            {
                if (SomethingChanged() && SaveChangesAbort())
                {
                    return null;
                }
                return client;
            }
            set
            {
                if (SomethingChanged() && SaveChangesAbort())
                {
                    return;
                }
                client = value;
                InitializeClientInfo();
            }
        }

        public IFactory EntityFactory
        {
            get { return entityFactory; }
            set { entityFactory = value; }
        }

        void InitializeClientInfo()
        {
            if (client == null)
                return;

            txtFIO.Text = client.Name;
            txtComment.Text = client.Comment;
            lstTelephones.Items.Clear();
            lstTelephones.Items.AddRange(client.Telephones.ToArray());

            chkBlackList.Checked = client.BlackListed;

            txtPrice.Text = client.Price.ToString();

        }

        bool SomethingChanged()
        {
            if (client == null)
                return false;

            return
                client.Name != txtFIO.Text ||
                client.Comment != txtComment.Text ||
                client.BlackListed != chkBlackList.Checked ||
                client.Price != Convert.ToInt32(txtPrice.Text) ||
                !client.Telephones.SequenceEqual(lstTelephones.Items.Cast<string>());
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
                if (f.ShowDialog() == DialogResult.OK &&
                    !String.IsNullOrWhiteSpace(f.number) &&
                    !lstTelephones.Items.Cast<string>().ToList().Contains(f.number))
                {
                    lstTelephones.Items.Add(f.number);
                }
            }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            if (SomethingChanged())
                SaveChanges();
            else
                if (OnSaveChanges != null)
                    OnSaveChanges(this, new SaveChangesEventArgs<IClient>(client));
        }

        /// <summary>
        /// Задать вопрос о необходимости сохранения внесённых изменений.
        /// </summary>
        /// <returns>true - если была нажата кнопка Cancel (Отмена), т.е. процесс сохранения изменений отменён и надо вернуться к редактированию.</returns>
        private bool SaveChangesAbort()
        {
            var dresult = MessageBox.Show("Сохранить изменения?", "Некоторые поля изменены.", MessageBoxButtons.YesNoCancel);
            if (dresult == DialogResult.Cancel)
                return true;
            if (dresult == DialogResult.Yes)
            {
                SaveChanges();
            }
            return false;
        }

        private void SaveChanges()
        {
            client.Name = txtFIO.Text;
            client.Comment = txtComment.Text;

            client.Telephones = new HashSet<string>(lstTelephones.Items.Cast<string>());

            client.BlackListed = chkBlackList.Checked;
            client.Price = Convert.ToInt32(txtPrice.Text);

            if (OnSaveChanges != null)
                OnSaveChanges(this, new SaveChangesEventArgs<IClient>(client));
        }

        private void btnLoadReceptions_Click(object sender, EventArgs e)
        {
            lstReceptions.Items.Clear();
            lstReceptions.Items.AddRange(client.GetReceptions().Select(r => r.DisplayString).ToArray());
        }
    }
}
