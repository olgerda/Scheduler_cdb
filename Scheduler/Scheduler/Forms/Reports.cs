using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduler_Forms
{
    public partial class Reports : Form
    {

        Scheduler_DBobjects_Intefraces.IMainDataBase db;

        public Reports(Scheduler_DBobjects_Intefraces.IMainDataBase database)
        {
            InitializeComponent();
            db = database;
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {
            if (date1.Value > date2.Value)
                date2.Value = date1.Value;
        }

        private void date2_ValueChanged(object sender, EventArgs e)
        {
            if (date2.Value < date1.Value)
                date1.Value = date2.Value;
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            date1_ValueChanged(null, null);

            return;

            var list = GetEntityesFromDateRange();

            var clients = list.Where(ent => !ent.Rent).Select(ent => ent.Client);
        }

        private void btnCreateReportSpecialist_Click(object sender, EventArgs e)
        {
            date1_ValueChanged(null, null);

            return;

            var list = GetEntityesFromDateRange();
        }


        private List<Scheduler_DBobjects_Intefraces.IEntity> GetEntityesFromDateRange()
        {
            List<Scheduler_DBobjects_Intefraces.IEntity> result = new List<Scheduler_DBobjects_Intefraces.IEntity>();
            for (var day = date1.Value; day.Date <= date2.Value; day = day.AddDays(1))
            {
                result.AddRange(db.SelectReceptionsFromDate(day));
            }
            return result;
        }

    }
}
