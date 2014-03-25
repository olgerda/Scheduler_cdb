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
    public partial class SpecialistInfo : UserControl
    {

        private ISpecialist spec;
        private IEnumerable<string> specialisationsList;

        public event SaveChangesHandler<ISpecialist> OnSaveChanges;

        public SpecialistInfo()
        {
            InitializeComponent();
        }

        public SpecialistInfo(ISpecialist spec, IEnumerable<string> SpecialisationsList)
        {
            InitializeComponent();
            this.spec = spec;
            this.specialisationsList = SpecialisationsList;
            InitializeSpecialistInfo();
        }

        public void UpdateList(IEnumerable<string> SpecialisationsList)
        {
            this.specialisationsList = SpecialisationsList;
            lstSpecialisations.Items.Clear();
            lstSpecialisations.Items.AddRange(specialisationsList.ToArray());
        }

        public ISpecialist Spec
        {
            get 
            {
                if (SomethingChanged() && SaveChangesAbort())
                {
                        return null;
                }
                return spec; 
            }
            set
            {
                if (SomethingChanged() && SaveChangesAbort())
                {
                    return;
                }
                spec = value;
                InitializeSpecialistInfo();
            }
        }

        private void InitializeSpecialistInfo()
        {
            if (spec == null)
                return;
            txtName.Text = spec.Name;
            chkNotWorking.Checked = spec.NotWorking;
            lstSpecialisations.Items.Clear();
            lstSpecialisations.Items.AddRange(specialisationsList.ToArray());
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            if (SomethingChanged())
                SaveChanges();
        }

        bool SomethingChanged()
        {
            if (spec == null)
                return false;
            return spec.Name != txtName.Text ||
                spec.Specialisations.SequenceEqual(lstSpecialisations.CheckedItems.Cast<string>()) ||
                spec.NotWorking != chkNotWorking.Checked;
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
            if (dresult == DialogResult.OK)
            {
                SaveChanges();
            }
            return false;
        }

        void SaveChanges()
        {
            if (spec == null)
                return;
            spec.Name = txtName.Text;
            spec.NotWorking = chkNotWorking.Checked;
            spec.Specialisations = new HashSet<string>(lstSpecialisations.CheckedItems.Cast<string>());

            if (OnSaveChanges != null)
                OnSaveChanges(this, new SaveChangesEventArgs<ISpecialist>(spec));
        }
    }
}
