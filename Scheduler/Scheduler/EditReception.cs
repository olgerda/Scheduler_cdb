using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class EditReception : Form
    {
        TableOfEntities table;
        CalendarControl3.ColumnsView ctrl;

        public DbConnect conn;

        private Entity toReturn;

        public EditReception(ref Entity edited)
        {
            InitializeComponent();

            datePicker.Value = DateTime.Now;

            Entity innerPurpose = edited;

            table = new TableOfEntities();
            ctrl = new CalendarControl3.ColumnsView();
            ctrl.Table = table;
            table.workTime = new TimeInterval(new DateTime(1, 1, 1, 8, 0, 0), new DateTime(1, 1, 1, 18, 0, 0));
            if (conn == null) return;
            List<Entity> toDayEntities = conn.SelectFromDate(datePicker.Value).FindAll(x => x.cabinet.Name == innerPurpose.cabinet.Name && x.date.StartDate.Date == innerPurpose.date.StartDate.Date);
            ColumnOfEntities column = new ColumnOfEntities(edited.cabinet.Name);

            splitContainer1.Panel1.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
        }

        private void DateChangedHandler(object sender, DateRangeEventArgs e)
        {
            datePicker.Value = e.Start;
        }

        private void BoxDateChangedHandler(object sender, EventArgs e)
        {
            calendarPicker.SetDate(datePicker.Value);
        }
    }
}
