using Scheduler_DBobjects_Intefraces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class ColorSettings : Form
    {
        private Dictionary<ColorChangers, CalendarControl3_Interfaces.IColorPalette> palette = new Dictionary<ColorChangers, CalendarControl3_Interfaces.IColorPalette>();

        public ColorSettings()
        {
            InitializeComponent();
            tabControl1.TabPages.Clear();
            Init();
            InitTestTable();
        }

        public void SetColors(Dictionary<ColorChangers, CalendarControl3_Interfaces.IColorPalette> colors)
        {
            palette = colors;
            Init();
        }

        void Init()
        {
            tabControl1.TabPages.Clear();
            foreach (ColorChangers en in Enum.GetValues(typeof(ColorChangers)))
            {
                var desc = en.GetAttributeOfType<DescriptionAttribute>().Description;
                TabPage tab = new TabPage(desc);
                tabControl1.TabPages.Add(tab);
                Scheduler.Controls.ColorPicker2 cp = new Scheduler.Controls.ColorPicker2();
                if (palette.ContainsKey(en))
                    cp.SelectedColors = palette[en];
                tab.Controls.Add(cp);
                cp.onColorsChanged += Cp_onColorsChanged;
                cp.Dock = DockStyle.Fill;
            }
            Cp_onColorsChanged(null);
        }


        private void Cp_onColorsChanged(Controls.ColorPicker2 obj)
        {
            (columnsView1.Table as Scheduler_DBobjects_Intefraces.ITable)?.SetColors(palette);
            columnsView1.Refresh();
        }

        //private void Cp_onColorsChanged1()
        //{
        //    (columnsView1.Table as Scheduler_DBobjects_Intefraces.ITable).SetColors(palette);
        //}

        void InitTestTable()
        {
            Scheduler_InterfacesRealisations.EntityFactory ef = new Scheduler_InterfacesRealisations.EntityFactory();

            var table = ef.NewTable();
            var col1 = ef.NewColumn();
            col1.Name = "Первый";
            var col2 = ef.NewColumn();
            col2.Name = "2";
            var col3CommentOnly = ef.NewColumn();
            col3CommentOnly.Name = "Комментарии"; col3CommentOnly.OnlyComment = true;
            table.Columns.Add(col1);
            table.Columns.Add(col2);
            table.Columns.Add(col3CommentOnly);

            var ti = ef.NewTimeInterval();
            ti.SetStartEnd(new DateTime(1, 1, 1, 9, 0, 0), new DateTime(1, 1, 1, 15, 0, 0));
            table.WorkTimeInterval = ti;
            Dictionary<DateTime, string> descriprions = new Dictionary<DateTime, string>(24);
            for (int i = 0; i <= 23; i++) //TODO: дикий хардкод, переписать на что-то вразумительное 
            {
                DateTime date = new DateTime(1, 1, 1, i, 0, 0);
                descriprions.Add(date, date.ToShortTimeString());
            }
            table.SetInfoColumnDescriptions(descriprions);

            ti = ef.NewTimeInterval();
            ti.SetStartEnd(new DateTime(1, 1, 1, 10, 0, 0), new DateTime(1, 1, 1, 13, 0, 0));
            var ent = new Scheduler_InterfacesRealisations.Reception() { CommentOnlyReception = true, Comment = "Простой комментарий", ReceptionTimeInterval = ti };
            col3CommentOnly.AddEntity(ent);

            var cli = ef.NewClient();
            cli.Name = "Клиент";
            var sp = ef.NewSpecialist();
            sp.Name = "Специалист";

            ti = ef.NewTimeInterval();
            ti.SetStartEnd(new DateTime(1, 1, 1, 11, 0, 0), new DateTime(1, 1, 1, 12, 0, 0));
            ent = new Scheduler_InterfacesRealisations.Reception() { Comment = "Простой приём", ReceptionTimeInterval = ti, Client = cli, Specialist = sp };
            col1.AddEntity(ent);

            ti = ef.NewTimeInterval();
            ti.SetStartEnd(new DateTime(1, 1, 1, 12, 0, 0), new DateTime(1, 1, 1, 14, 0, 0));
            ent = new Scheduler_InterfacesRealisations.Reception() { Comment = "Простая аренда", ReceptionTimeInterval = ti, Rent = true, Client = cli, Specialist = sp };
            col1.AddEntity(ent);

            ti = ef.NewTimeInterval();
            ti.SetStartEnd(new DateTime(1, 1, 1, 11, 0, 0), new DateTime(1, 1, 1, 14, 0, 0));
            ent = new Scheduler_InterfacesRealisations.Reception() { Comment = "Аренда Песок", ReceptionTimeInterval = ti, Rent = true, SpecialRent = true, Client = cli, Specialist = sp };
            col2.AddEntity(ent);

            ti = ef.NewTimeInterval();
            ti.SetStartEnd(new DateTime(1, 1, 1, 9, 0, 0), new DateTime(1, 1, 1, 10, 0, 0));
            ent = new Scheduler_InterfacesRealisations.Reception() { Comment = "Не состоявшийся приём", ReceptionTimeInterval = ti, ReceptionDidNotTakePlace = true, Client = cli, Specialist = sp };
            col2.AddEntity(ent);

            columnsView1.Table = table;
            columnsView1.Dock = DockStyle.Fill;
        }
    }

    public static class EnumHelper
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;</example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
