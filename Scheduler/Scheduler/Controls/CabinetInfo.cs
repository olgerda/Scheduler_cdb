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
    public partial class CabinetInfo : UserControl
    {
        private ICabinet cab;

        public event SaveChangesHandler<ICabinet> OnSaveChanges;

        public CabinetInfo()
        {
            InitializeComponent();
        }

        public CabinetInfo(ICabinet cab) : this()
        {
            this.cab = cab;
            InitializeCabinetInfo();
        }

        public ICabinet Cabinet
        {
            get { return cab; }
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
                cab = value;
                InitializeCabinetInfo();
            }
        }

        void InitializeCabinetInfo()
        {
            if (cab == null)
                return;
            txtName.Text = cab.Name;
            chkAvailable.Checked = cab.Availability;
            chkCommentOnly.Checked = cab.CommentOnly;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SomethingChanged())
                SaveChanges();
            else
                if (OnSaveChanges != null)
                    OnSaveChanges(this, new SaveChangesEventArgs<ICabinet>(cab));
        }

        bool SomethingChanged()
        {
            if (cab == null)
                return false;
            return cab.Availability != chkAvailable.Checked ||
                   cab.Name != txtName.Text ||
                   cab.CommentOnly != chkCommentOnly.Checked;
        }

        void SaveChanges()
        {
            if (cab == null)
                return;
            cab.Name = txtName.Text;
            cab.Availability = chkAvailable.Checked;
            cab.CommentOnly = chkCommentOnly.Checked;

            if (OnSaveChanges != null)
                OnSaveChanges(this, new SaveChangesEventArgs<ICabinet>(cab));
        }
    }
}
