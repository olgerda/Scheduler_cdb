using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduler_Forms_Interfaces;
using Scheduler_Controls_Interfaces;
using Scheduler_Common_Interfaces;

namespace Scheduler_Forms
{
    public partial class CabinetListEdit : Form
    {
        private ICabinetList cabList;
        private IFactory entityFactory;
        private ICabinet selectedCabinet;


        public CabinetListEdit()
        {
            InitializeComponent();
        }

        public CabinetListEdit(ICabinetList cabList, IFactory entityFactory)
        {
            InitializeComponent();

            this.cabList = cabList;
            this.entityFactory = entityFactory;

            Init();
        }

        void Init()
        {

            if (cabList == null)
                return;

            lstCabinets.DataSource = cabList.List;

            cabinetInfoCard.OnSaveChanges += new SaveChangesHandler<ICabinet>(cabinetInfoCard_OnSaveChanges);
        }

        void cabinetInfoCard_OnSaveChanges(object source, SaveChangesEventArgs<ICabinet> e)
        {
            DeactivateEditMode();
        }

        public ICabinetList CabinetList
        {
            get { return cabList; }
            set
            {
                cabList = value;
                Init();
            }
        }

        public IFactory EntityFactory
        {
            get { return entityFactory; }
            set { entityFactory = value; }
        }

        public ICabinet SelectedCabinet
        {
            get { return selectedCabinet; }
            private set
            {
                selectedCabinet = value;
                cabinetInfoCard.Cabinet = selectedCabinet;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ICabinet newCab = entityFactory.NewCabinet();
            ActivateEditMode(newCab);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstCabinets.SelectedIndex == -1)
                return;
            ActivateEditMode((ICabinet)lstCabinets.SelectedItem);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivateEditMode(ICabinet cabinetInfo)
        {
            cabinetInfoCard.Cabinet = cabinetInfo;

            grpCabinetList.Enabled = false;
            cabinetInfoCard.Enabled = true;
        }

        private ICabinet DeactivateEditMode()
        {
            ICabinet result = cabinetInfoCard.Cabinet;

            if (result != null) //если значение не установилось - пользователь отменил закрытие.
            {
                //grpSelectSpecialist.Visible = true;
                grpCabinetList.Enabled = true;
                //grpEditMode.Visible = false;
                cabinetInfoCard.Enabled = false;

                if (!cabList.List.Contains(result))
                {
                    cabList.Add(result);
                }
                lstCabinets.DataSource = cabList.List.Cast<INamedEntity>().ToList();
                lstCabinets.SelectedItem = result;
            }
            return result;
        }

        private void lstCabinets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCabinets.SelectedIndex == -1)
                return;
            SelectedCabinet = (ICabinet)lstCabinets.SelectedItem;
        }

        private void CabinetListEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            CabinetList.ValidateAndUpdate();
        }

        private void lstCabinets_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Delete && lstCabinets.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить кабинет\n" + ((INamedEntity)lstCabinets.SelectedItem).Name + "\nиз базы?\nОтменить удаление невозможно!",
                    "Удаление кабинета из Базы",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                    return;
                try
                {
                    CabinetList.Remove((ICabinet)lstCabinets.SelectedItem);
                    lstCabinets.DataSource = CabinetList.List.Cast<INamedEntity>().ToList();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Substring(0, 4) == "1451")
                        MessageBox.Show("Произошла ошибка удаления. Удаляемый кабинет используется.", "Удаление невозможно.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    else
                        throw ex;
                }
                
                
            }
        }
    }
}
