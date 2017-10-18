using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Scheduler_Common_Interfaces;
using Scheduler_Controls_Interfaces;

namespace Scheduler.Forms
{
    public partial class AdministratorListEdit : Form
    {
        private Scheduler_Common_Interfaces.IFactory factory;
        private Scheduler_DBobjects_Intefraces.IMainDataBase db;
        private IAdministrator _currentAdministrator;

        private Scheduler_Controls_Interfaces.IAdministrator CurrentAdministrator
        {
            get { return _currentAdministrator; }
            set
            {
                chkNotWork.DataBindings.Clear();
                txtName.DataBindings.Clear();
                _currentAdministrator = value;
                if (_currentAdministrator != null)
                {
                    txtName.DataBindings.Add(new Binding("Text", _currentAdministrator, "Name"));
                    chkNotWork.DataBindings.Add(new Binding("Checked", _currentAdministrator, "NotWorking"));
                }
            }
        }

        public AdministratorListEdit()
        {
            InitializeComponent();
        }

        public AdministratorListEdit(Scheduler_Common_Interfaces.IFactory factory, Scheduler_DBobjects_Intefraces.IMainDataBase db)
        {
            InitializeComponent();
            this.factory = factory;
            this.db = db;

            lstAdministrators.DataSource = db.AdministratorList.List.Cast<INamedEntity>().ToArray();
            lstAdministrators.DisplayMember = "Name";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CurrentAdministrator = factory.NewAdministrator();

            btnAccept.Enabled = btnCancel.Enabled = true;
            btnAdd.Enabled = btnRemove.Enabled = grpList.Enabled = false;
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (CurrentAdministrator == null)
                return;
            if (db.AdministratorList.List.Contains(CurrentAdministrator))
                db.AdministratorList.Remove(CurrentAdministrator);
            lstAdministrators.DataSource = null;
            lstAdministrators.DataSource = db.AdministratorList.List.Cast<INamedEntity>().ToArray();
            CurrentAdministrator = null;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (CurrentAdministrator == null)
                return;
            if (db.AdministratorList.List.Contains(CurrentAdministrator))
                return;
            db.AdministratorList.Add(CurrentAdministrator);
            lstAdministrators.DataSource = null;
            lstAdministrators.DataSource = db.AdministratorList.List.Cast<INamedEntity>().ToArray();
            btnAccept.Enabled = btnCancel.Enabled = false;
            btnAdd.Enabled = btnRemove.Enabled = grpList.Enabled = true;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CurrentAdministrator = null;
            btnAccept.Enabled = btnCancel.Enabled = false;
            btnAdd.Enabled = btnRemove.Enabled = grpList.Enabled = true;
        }

        private void lstAdministrators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAdministrators.SelectedIndex == -1)
                return;
            CurrentAdministrator = lstAdministrators.SelectedItem as IAdministrator;
        }
    }
}
