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
        private ISpecialist selectedSpecialist;
        private ISpecialistDuty selectedDuty;
        private DateTime lastDate;
        private SortableList<ISpecialistDuty> currentDuty = new SortableList<ISpecialistDuty>();

        private BindingList<ISpecialist> specialists = new BindingList<ISpecialist>();
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
            Init();
        }

        void Init()
        {
            if (context == null)
                return;
            currentDuty.Clear();
            specialists.Clear();
            foreach (var duty in context.SelectSpecialistDutyFromDate(monthCalendar1.SelectionStart))
                currentDuty.Add(duty);
            foreach (var sp in context.SpecialistList.List.Where(
                x => !x.NotWorking && currentDuty.FirstOrDefault(y => y.Specialist == x) == null))
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
            if (context == null)
                return;
            var oldDuty = context.SelectSpecialistDutyFromDate(lastDate);
            var duty2Remove = oldDuty.Except(currentDuty).ToArray();
            var duty2Add = currentDuty.Except(oldDuty).ToArray();

            foreach (var duty in duty2Remove)
                context.SpecialistDutyList.Remove(duty);
            foreach (var duty in duty2Add)
                context.SpecialistDutyList.Add(duty);
        }

        private void lstSpecialists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSpecialists.SelectedIndex == -1)
            {
                selectedSpecialist = null;
                return;
            }
            selectedSpecialist = ((ISpecialist)lstSpecialists.SelectedItem);
            monthCalendar1.BoldedDates = context.SelectSpecialistDutyDates(selectedSpecialist).ToArray();
            monthCalendar1.UpdateBoldedDates();
        }

        private void lstDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDuty.SelectedIndex == -1)
            {
                selectedDuty = null;
                return;
            }
            selectedDuty = ((ISpecialistDuty)lstDuty.SelectedItem);
            monthCalendar1.BoldedDates = context.SelectSpecialistDutyDates(selectedDuty.Specialist).ToArray();
            monthCalendar1.UpdateBoldedDates();
        }

        private void btnToDuty_Click(object sender, EventArgs e)
        {
            if (selectedSpecialist == null)
                return;
            var exist = currentDuty.FirstOrDefault(x => x.Specialist == selectedSpecialist &&
                                                        x.Start == monthCalendar1.SelectionStart &&
                                                        x.End == monthCalendar1.SelectionEnd);
            if (exist != null)
                return;
            ISpecialistDuty duty = entityFactory.NewSpecialistDuty();
            duty.Start = monthCalendar1.SelectionStart;
            duty.End = monthCalendar1.SelectionEnd;
            duty.Specialist = selectedSpecialist;
            duty.Supplimentary = false;
            currentDuty.Add(duty);
            specialists.Remove(selectedSpecialist);
        }

        private void btnToSupplimentary_Click(object sender, EventArgs e)
        {
            if (selectedSpecialist == null)
                return;
            var exist = currentDuty.FirstOrDefault(x => x.Specialist == selectedSpecialist &&
                                                        x.Start == monthCalendar1.SelectionStart &&
                                                        x.End == monthCalendar1.SelectionEnd);
            if (exist != null)
                return;
            ISpecialistDuty duty = entityFactory.NewSpecialistDuty();
            duty.Start = monthCalendar1.SelectionStart;
            duty.End = monthCalendar1.SelectionEnd;
            duty.Specialist = selectedSpecialist;
            duty.Supplimentary = true;
            currentDuty.Add(duty);
            specialists.Remove(selectedSpecialist);
            selectedSpecialist = null;
        }

        private void btnFromDuty_Click(object sender, EventArgs e)
        {
            if (selectedDuty == null)
                return;
            specialists.Add(selectedDuty.Specialist);
            currentDuty.Remove(selectedDuty);
            selectedDuty = null;
        }

        private void lstDuty_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            ISpecialistDuty cd = (ISpecialistDuty)lstDuty.Items[e.Index];

            e.DrawBackground();
            var g = e.Graphics;
            if (cd.Supplimentary)
                g.FillRectangle(Brushes.Aquamarine, e.Bounds);
            g.DrawString(cd.ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }
    }
}
