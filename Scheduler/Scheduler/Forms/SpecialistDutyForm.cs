using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Scheduler_Controls_Interfaces;
using Scheduler_DBobjects_Intefraces;
using Scheduler_Common_Interfaces;
using Scheduler_InterfacesRealisations;

namespace Scheduler.Forms
{
    public partial class SpecialistDutyForm : Form
    {
        private IMainDataBase context;
        private IFactory entityFactory;
        private ICanNotWork selectedSpecialist;
        private IDuty selectedDuty;
        private DateTime lastDate;
        private SortableList<IDuty> currentDuty = new SortableList<IDuty>();

        private bool modeSpecialistDuty = false;
        private Func<DateTime, IEnumerable<IDuty>> funcGetDutyFromDate;
        private List<ICanNotWork> collectionOfNamed;

        private bool ModeSpecialistDuty
        {
            get { return modeSpecialistDuty; }
            set
            {
                //тут логика выбора режима - переключение списков
                if (modeSpecialistDuty != value)
                {
                    UpdateDutyes();
                    modeSpecialistDuty = value;
                    lstDuty.SelectedIndex = -1;
                    lstSpecialists.SelectedIndex = -1;
                    if (value)
                    {
                        funcGetDutyFromDate = context.SelectDutyFromDate<ISpecialist>;
                        collectionOfNamed = context.SpecialistList.List.Cast<ICanNotWork>().ToList();
                        grpSpecialists.Text = radModeSpecialist.Text;
                        grpSpecialistsOnDuty.Text = radModeSpecialist.Text + " на дежурстве";
                    }
                    else
                    {
                        funcGetDutyFromDate = context.SelectDutyFromDate<IAdministrator>;
                        collectionOfNamed = context.AdministratorList.List.Cast<ICanNotWork>().ToList();
                        grpSpecialists.Text = radModeAdministrator.Text;
                        grpSpecialistsOnDuty.Text = radModeAdministrator.Text + " на дежурстве";
                    }
                    Init();
                }
                
            }
        }

        private BindingList<INamedEntity> specialists = new BindingList<INamedEntity>();
        //продумать добавление - удаление 

        public SpecialistDutyForm()
        {
            InitializeComponent();
            monthCalendar1.SetDate(DateTime.Now);
            this.FormClosing += (o, e) => { UpdateDutyes(); };

        }

        public void SetDatabase(IMainDataBase ctx, IFactory factory)
        {
            context = ctx;
            entityFactory = factory;
            lstDuty.DataSource = currentDuty;
            //lstDuty.DisplayMember = "Specialist";
            lstSpecialists.DataSource = specialists;
            lstSpecialists.DisplayMember = "Name";
            radModeSpecialist.Checked = true;
            //Init();
        }

        void Init()
        {
            if (context == null)
                return;
            currentDuty.Clear();
            specialists.Clear();

            foreach (var duty in funcGetDutyFromDate(monthCalendar1.SelectionStart))
                currentDuty.Add(duty);
            foreach (var sp in collectionOfNamed.Where(
                x => !x.NotWorking && currentDuty.FirstOrDefault(y => y.Named == x) == null))
                specialists.Add(sp);
        }


        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            UpdateDutyes();
            Init();
            lastDate = e.Start;
        }

        void UpdateDutyes()
        {
            if (context == null || funcGetDutyFromDate == null)
                return;
            var oldDuty = funcGetDutyFromDate(lastDate);
            var duty2Remove = oldDuty.Except(currentDuty).ToArray();
            var duty2Add = currentDuty.Except(oldDuty).ToArray();


            if (ModeSpecialistDuty)
            {
                foreach (var duty in duty2Remove.Cast<ISpecialistDuty>())
                    context.SpecialistDutyList.Remove(duty);
                foreach (var duty in duty2Add.Cast<ISpecialistDuty>())
                    context.SpecialistDutyList.Add(duty);
            }
            else
            {
                foreach (var duty in duty2Remove.Cast<IAdministratorDuty>())
                    context.AdministratorDutyList.Remove(duty);
                foreach (var duty in duty2Add.Cast<IAdministratorDuty>())
                    context.AdministratorDutyList.Add(duty);
            }
        }

        private void lstSpecialists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSpecialists.SelectedIndex == -1)
            {
                selectedSpecialist = null;
                return;
            }
            if (ModeSpecialistDuty)
                selectedSpecialist = ((ISpecialist)lstSpecialists.SelectedItem);
            else
                selectedSpecialist = ((IAdministrator)lstSpecialists.SelectedItem); //TODO: INITITALIZATION EXCEPTION!!!
            monthCalendar1.BoldedDates = context.SelectDutyDates(selectedSpecialist).ToArray();
            monthCalendar1.UpdateBoldedDates();
        }

        private void lstDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDuty.SelectedIndex == -1)
            {
                selectedDuty = null;
                return;
            }
            selectedDuty = ((IDuty)lstDuty.SelectedItem);
            monthCalendar1.BoldedDates = context.SelectDutyDates(selectedDuty.Named).ToArray();
            monthCalendar1.UpdateBoldedDates();
        }

        private void btnToDuty_Click(object sender, EventArgs e)
        {
            var duty = ToDuty();
            if (duty != null)
                duty.Supplimentary = false;
        }

        private void btnToSupplimentary_Click(object sender, EventArgs e)
        {
            var duty = ToDuty();
            if (duty != null)
                duty.Supplimentary = true;
        }

        IDuty ToDuty()
        {
            if (selectedSpecialist == null && lstSpecialists.SelectedIndex != -1)
                selectedSpecialist = (ICanNotWork)lstSpecialists.SelectedItem;
            if (selectedSpecialist == null)
                return null;

            var exist = currentDuty.FirstOrDefault(x => x.Named == selectedSpecialist &&
                                                        x.Start == monthCalendar1.SelectionStart &&
                                                        x.End == monthCalendar1.SelectionEnd);
            if (exist != null)
                return null;
            IDuty duty;
            if (ModeSpecialistDuty)
                duty = entityFactory.NewSpecialistDuty();
            else
                duty = entityFactory.NewAdministratorDuty();
            
            duty.Start = monthCalendar1.SelectionStart;
            duty.End = monthCalendar1.SelectionEnd;
            duty.Named = selectedSpecialist;
            currentDuty.Add(duty);
            specialists.Remove(selectedSpecialist);
            selectedSpecialist = null;
            return duty;
        }

        private void btnFromDuty_Click(object sender, EventArgs e)
        {
            if (selectedDuty == null && lstDuty.SelectedIndex != -1)
                selectedDuty = (IDuty)lstDuty.SelectedItem;
            if (selectedDuty == null)
                return;
            specialists.Add(selectedDuty.Named);
            currentDuty.Remove(selectedDuty);
            selectedDuty = null;
        }

        private void lstDuty_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            IDuty cd = (IDuty)lstDuty.Items[e.Index];

            e.DrawBackground();
            var g = e.Graphics;
            if (cd.Supplimentary)
                g.FillRectangle(Brushes.Aquamarine, e.Bounds);
            g.DrawString(cd.ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }

        private void radModeAdministrator_CheckedChanged(object sender, EventArgs e)
        {
            ModeSpecialistDuty = radModeSpecialist.Checked;
        }
    }
}
