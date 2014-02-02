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
    public partial class SpecializationsInfo : UserControl
    {
        private ISpecializationList specialisationsList;

        public SpecializationsInfo()
        {
            InitializeComponent();
        }

        public SpecializationsInfo(ISpecializationList SpecialisationsList)
        {
            InitializeComponent();
            UpdateList(SpecialisationsList);
        }

        public void UpdateList(ISpecializationList SpecialisationsList)
        {
            this.specialisationsList = SpecialisationsList;
            InitializeSpecializationList();
        }

        void InitializeSpecializationList()
        {
            if (specialisationsList == null)
                return;
            lstSpecializations.Items.Clear();
            lstSpecializations.Items.AddRange(specialisationsList.SpecializationList.ToArray());
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSpecializations.SelectedIndex == -1)
                return;

            if (lstSpecializations.SelectedIndices.Count < 2)
            {
                lstSpecializations.Items.RemoveAt(lstSpecializations.SelectedIndex);
            }
            else
                foreach (var item in new List<string>(lstSpecializations.SelectedItems.Cast<string>()))
                    lstSpecializations.Items.Remove(item);
            
        }

        bool SomethingChanged()
        {
            if (specialisationsList == null)
                return false;
            return !specialisationsList.SpecializationList.SequenceEqual(lstSpecializations.Items.Cast<string>());
        }

        void SaveChanges()
        {
            if (specialisationsList == null)
                return;
            specialisationsList.SpecializationList = new HashSet<string>(lstSpecializations.Items.Cast<string>());
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            if (SomethingChanged())
                SaveChanges();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            using (AddString f = new AddString())
            {
                Point p = btnAdd.PointToScreen(btnAdd.Location);
                p.Y -= this.Height;
                f.Location = p;
                if (f.ShowDialog() == DialogResult.OK && 
                    !lstSpecializations.Items.Cast<string>().ToList().Contains(f.TextInputed))
                    lstSpecializations.Items.Add(f.TextInputed);
                lstSpecializations.SelectedIndex = -1;
                lstSpecializations.SelectedItem = f.TextInputed;
            }
        }
    }
}
