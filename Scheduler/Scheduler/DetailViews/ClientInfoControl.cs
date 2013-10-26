using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduler.DetailViews
{
    public partial class ClientInfoControl : UserControl
    {

        private ClientCard _clientCard;

        private bool fioChanged;

        public ClientInfoControl()
        {
            InitializeComponent();

            fioChanged = false;
        }

        public ClientCard ClientCard
        {
            get
            {
                if (_clientCard == null) _clientCard = new ClientCard("НЕ ЗАДАНО");
                return _clientCard;
            }

            set
            {
                _clientCard = value;
                ReinitItems();
            }
        }

        public void ReinitItems()
        {
            txtClientName.Text = ClientCard.Name.ToString();
            txtClientComment.Text = ClientCard.comment;
            lstClientTelephones.Items.AddRange(ClientCard.TelNumbers.ToArray());
        }

        private void lstClientTelephones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClientTelephones.SelectedIndex == -1) return;
            txtClientTelephone.Text = lstClientTelephones.SelectedItem.ToString();
        }

        private void btnClientTelephoneRemove_Click(object sender, EventArgs e)
        {
            ClientCard.TelNumbers.Remove(txtClientTelephone.Text);
            ReinitItems();
        }

        private void btnClientTelephoneAdd_Click(object sender, EventArgs e)
        {
            if (!ClientCard.TelNumbers.Contains(txtClientTelephone.Text))
                ClientCard.TelNumbers.Add(txtClientTelephone.Text);
            ReinitItems();
        }

        private void btnClientCommentApply_Click(object sender, EventArgs e)
        {
            ClientCard.comment = txtClientComment.Text;
        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {
            fioChanged = true;
        }

        private void txtClientName_Leave(object sender, EventArgs e)
        {
            if (fioChanged && ClientCard.Name.ToString() != txtClientName.Text)
            {
                var msgResult = MessageBox.Show("ДА/Yes - Создать нового клиента\r\nНет/No - Изменить ФИО текущего клиента\r\nОтменить/Cancel - Отменить изменения.", "Поле ФИО Клиента было изменено", MessageBoxButtons.YesNoCancel);
                switch (msgResult)
                {
                    case DialogResult.Yes:
                        _clientCard = new ClientCard(txtClientName.Text);
                        break;
                    case DialogResult.No:
                        _clientCard.Name = txtClientName.Text;
                        break;
                    default:
                        txtClientName.Text = ClientCard.Name.ToString();
                        break;
                }                    
            }
        }

        private void txtClientTelephone_TextChanged(object sender, EventArgs e)
        {
            var objs = ClientCard.TelNumbers.FindAll(s => s.StartsWith(txtClientTelephone.Text));
            lstClientTelephones.ClearSelected();
            foreach (var o in objs)
            {
                lstClientTelephones.SetSelected(lstClientTelephones.Items.IndexOf(o), true);
            }
        }
    }
}
