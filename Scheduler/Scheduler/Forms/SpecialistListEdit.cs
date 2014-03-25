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
    public partial class SpecialistListEdit : Form
    {
        private ISpecialistList specList;
        private IFactory entityFactory;
        private ISpecialist selectedSpecialist;

        public SpecialistListEdit()
        {
            InitializeComponent();
        }

        public SpecialistListEdit(ISpecialistList specList, IFactory entityFactory)
        {
            InitializeComponent();

            this.specList = specList;
            this.entityFactory = entityFactory;

            Init();
        }

        private void Init()
        {
            if (specList == null)
                return;

            lstSpecialistList.DataSource = specList.List;

            var customAutoComplete = new AutoCompleteStringCollection();
            customAutoComplete.AddRange(lstSpecialistList.Items.Cast<ISpecialist>().Select(c => c.Name).ToArray());
            txtSpecialistName.AutoCompleteCustomSource = customAutoComplete;

            specialistInfoCard.OnSaveChanges += new SaveChangesHandler<ISpecialist>(specialistInfoCard_OnSaveChanges);
        }

        void specialistInfoCard_OnSaveChanges(object source, SaveChangesEventArgs<ISpecialist> e)
        {
            DeactivateEditMode();
        }

        public ISpecialistList SpecialistList
        {
            get { return specList; }
            set
            {
                specList = value;
                Init();
            }
        }


        public IFactory EntityFactory
        {
            get { return entityFactory; }
            set { entityFactory = value; }
        }

        public ISpecialist SelectedSpecialist
        {
            get { return selectedSpecialist; }
            private set
            {
                selectedSpecialist = value;
                specialistInfoCard.Spec = selectedSpecialist;
            }
        }

        private void ActivateEditMode(ISpecialist specialistInfo)
        {
            specialistInfoCard.Spec = specialistInfo;

            grpSelectSpecialist.Enabled = false;
            grpSelectSpecialist.Visible = false;

            grpEditMode.Visible = true;
            grpEditMode.Enabled = true;
            specialistInfoCard.Enabled = true;
        }

        private ISpecialist DeactivateEditMode()
        {
            ISpecialist result = specialistInfoCard.Spec;

            if (result != null) //если значение не установилось - пользователь отменил закрытие.
            {
                grpSelectSpecialist.Visible = true;
                grpSelectSpecialist.Enabled = true;
                grpEditMode.Visible = false;
                grpEditMode.Enabled = false;
                specialistInfoCard.Enabled = false;

                if (!specList.List.Contains(result))
                {
                    specList.List.Add(result);
                }
                lstSpecialistList.SelectedItem = result;
            }
            return result;
        }

        private void btnEditModeOff_Click(object sender, EventArgs e)
        {
            DeactivateEditMode();
        }

        private void txtSpecialistName_TextChanged(object sender, EventArgs e)
        {
            ISpecialist curSpec = null;
            curSpec = specList.FindSpecialistByPartialName(txtSpecialistName.Text);
            if (curSpec != null)
                lstSpecialistList.SelectedItem = curSpec;
            else
                lstSpecialistList.SelectedIndex = -1;
        }

        private void btnAddSpecialist_Click(object sender, EventArgs e)
        {
            ISpecialist newSpec = entityFactory.NewSpecialist();
            ActivateEditMode(newSpec);
        }

        private void btnEditSpecialist_Click(object sender, EventArgs e)
        {
            if (lstSpecialistList.SelectedIndex == -1)
                return;
            ActivateEditMode((ISpecialist)lstSpecialistList.SelectedItem);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstSpecialistList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSpecialistList.SelectedIndex == -1)
                return;
            SelectedSpecialist = (ISpecialist)lstSpecialistList.SelectedItem;
        }

    }
}
