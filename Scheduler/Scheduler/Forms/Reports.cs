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
            cmbReportKindSelect.SelectedIndex = 0;
            date2.Value = DateTime.Now.Date;
            date1.Value = date2.Value.Subtract(TimeSpan.FromDays(30));
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

        string reportDelimiter = ";";

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            date1_ValueChanged(null, null);

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            //return;

            var list = GetEntityesFromDateRange();

            var listNotRent = list.Where(ent => !ent.Rent);

            List<string> report = new List<string>();
            report.Add(String.Format("Временной интервал: {0} - {1}", date1.Value.ToShortDateString(), date2.Value.ToShortDateString()));
            report.Add(String.Format("Наименование{0}Всего{0}Первичные{0}Повторные", reportDelimiter));
            report.AddRange(GenerateReport(listNotRent, cmbReportKindSelect.SelectedIndex));

            System.IO.File.WriteAllLines(saveFileDialog1.FileName, report, Encoding.UTF8);
            
        }

        List<string> GenerateReport(IEnumerable<Scheduler_DBobjects_Intefraces.IEntity> list, int type = -1)
        {
            /*
             * Отчёт по специалистам
             * Отчёт по кабинетам
             * Отчёт по специализациям
             */
            var result = new List<string>();

            var clients = list.Select(ent => ent.Client).Distinct();

            Dictionary<Scheduler_Controls_Interfaces.IClient, List<Scheduler_Controls_Interfaces.IReception>> receptionsByClient = new Dictionary<Scheduler_Controls_Interfaces.IClient, List<Scheduler_Controls_Interfaces.IReception>>(clients.Count());
            foreach (var client in clients)
                receptionsByClient.Add(client, client.GetReceptions().Where(ent => ent.ReceptionTimeInterval.EndDate <= date2.Value).ToList());

            int first = 0;
            int repeat = 0;
            int overall = 0;

            switch (type)
            {
                case 0:
                    var specialists = list.Select(ent => ent.Specialist).Distinct();
                    foreach (var spec in specialists)
                    {
                        var receptionsByThisSpec = list.Where(ent => ent.Specialist.Equals(spec));

                        first = 0;
                        repeat = 0;
                        overall = 0;

                        foreach (var reception in receptionsByThisSpec)
                        {
                            if (receptionsByClient[reception.Client].Where(ent =>
                                ent.ReceptionTimeInterval.EndDate < reception.ReceptionTimeInterval.EndDate &&
                                ent.Specialist.Equals(spec)).FirstOrDefault() == null)
                                first++;
                            else
                                repeat++;
                        }

                        overall = receptionsByThisSpec.Count();

                        result.Add(String.Join(reportDelimiter, spec.Name, overall, first, repeat));
                    }
                    break;
                case 1:
                    var cabinets = list.Select(ent => ent.Cabinet).Distinct();
                    foreach (var cab in cabinets)
                    {
                        var receptionsByThisCab = list.Where(ent => ent.Cabinet.Equals(cab));

                        first = 0;
                        repeat = 0;
                        overall = 0;

                        foreach (var reception in receptionsByThisCab)
                        {
                            if (receptionsByClient[reception.Client].Where(ent =>
                                ent.ReceptionTimeInterval.EndDate < reception.ReceptionTimeInterval.EndDate &&
                                ent.Cabinet.Equals(cab)).FirstOrDefault() == null)
                                first++;
                            else
                                repeat++;
                        }

                        overall = receptionsByThisCab.Count();

                        result.Add(String.Join(reportDelimiter, cab.Name, overall, first, repeat));
                    }
                    break;
                case 2:
                    var specializations = list.Select(ent => ent.Specialization).Distinct();
                    foreach (var specn in specializations)
                    {
                        var receptionsByThisSpecn = list.Where(ent => ent.Specialization == specn);

                        first = 0;
                        repeat = 0;
                        overall = 0;

                        foreach (var reception in receptionsByThisSpecn)
                        {
                            if (receptionsByClient[reception.Client].Where(ent =>
                                ent.ReceptionTimeInterval.EndDate < reception.ReceptionTimeInterval.EndDate &&
                                ent.Specialization == specn).FirstOrDefault() == null)
                                first++;
                            else
                                repeat++;
                        }

                        overall = receptionsByThisSpecn.Count();

                        result.Add(String.Join(reportDelimiter, specn, overall, first, repeat));
                    }
                    break;
                default:
                    return result;
            }

            return result;

        }



        private List<Scheduler_DBobjects_Intefraces.IEntity> GetEntityesFromDateRange()
        {
            return db.SelectReceptionsBetweenDates(date1.Value, date2.Value);
            //List<Scheduler_DBobjects_Intefraces.IEntity> result = new List<Scheduler_DBobjects_Intefraces.IEntity>();
            //for (var day = date1.Value; day.Date <= date2.Value; day = day.AddDays(1))
            //{
            //    result.AddRange(db.SelectReceptionsFromDate(day));
            //}
            //return result;
        }

        #region Helper funcs
        class EntityComparerByClient : IEqualityComparer<Scheduler_DBobjects_Intefraces.IEntity>
        {

            public bool Equals(Scheduler_DBobjects_Intefraces.IEntity x, Scheduler_DBobjects_Intefraces.IEntity y)
            {
                if (x == null || y == null)
                    return false;

                if (Object.ReferenceEquals(x.Client, y.Client))
                    return true;

                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                return x.Client.ID == y.Client.ID;
            }

            public int GetHashCode(Scheduler_DBobjects_Intefraces.IEntity obj)
            {
                if (Object.ReferenceEquals(obj, null) || Object.ReferenceEquals(obj.Client, null))
                    return 0;
                return obj.Client.Name.GetHashCode();
            }
        }
        #endregion
    }
}
