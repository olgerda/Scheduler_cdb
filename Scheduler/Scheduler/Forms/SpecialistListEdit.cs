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
        private ISpecializationList specializationList;

        private bool doNothing;

        public SpecialistListEdit()
        {
            InitializeComponent();
            doNothing = false;
        }

        public SpecialistListEdit(ISpecialistList specList, ISpecializationList specializationList, IFactory entityFactory)
        {
            InitializeComponent();

            this.specList = specList;
            this.entityFactory = entityFactory;
            this.SpecializationList = specializationList;
            doNothing = false;

            Init();
        }

        private void Init()
        {
            if (specList == null)
                return;

            lstSpecialistList.DataSource = specList.List.Cast<INamedEntity>().ToList();

            var customAutoComplete = new AutoCompleteStringCollection();
            customAutoComplete.AddRange(lstSpecialistList.Items.Cast<INamedEntity>().Select(c => c.Name).ToArray());
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

        public ISpecializationList SpecializationList
        {
            get { return specializationList; }
            set
            {
                specializationList = value;
                this.specialistInfoCard.UpdateList(specializationList.SpecializationList);
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
                txtSpecialistName.Text = selectedSpecialist.Name;
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
                    specList.Add(result);
                    lstSpecialistList.DataSource = specList.List.Cast<INamedEntity>().ToList();
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
            if (doNothing)
                return;
            ISpecialist curSpec = null;
            curSpec = specList.List.FirstOrDefault(s => s.Name == txtSpecialistName.Text) ?? specList.FindSpecialistByPartialName(txtSpecialistName.Text);
            doNothing = true;
            if (curSpec != null)
                lstSpecialistList.SelectedItem = curSpec;
            else
                lstSpecialistList.SelectedIndex = -1;
            doNothing = false;
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
            if (lstSpecialistList.SelectedIndex == -1 && SpecialistList == null)
                return;
            SelectedSpecialist = (ISpecialist)lstSpecialistList.SelectedItem;
        }

        private void SpecialistListEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            SpecialistList.ValidateAndUpdate();
        }

        private void lstSpecialistList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Delete && lstSpecialistList.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить специалиста\n" + ((INamedEntity)lstSpecialistList.SelectedItem).Name + "\nиз базы?\nОтменить удаление невозможно!",
                    "Удаление специалиста из Базы",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                    return;
                try
                {
                    SpecialistList.Remove((ISpecialist)lstSpecialistList.SelectedItem);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Substring(0, 4) == "1451")
                        System.Windows.Forms.MessageBox.Show("Произошла ошибка удаления. Удаляемый специалист используется.", "Удаление невозможно.",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                    else
                        throw ex;
                }
                lstSpecialistList.DataSource = SpecialistList.List.Cast<INamedEntity>().ToList();
            }
        }

    }
}
