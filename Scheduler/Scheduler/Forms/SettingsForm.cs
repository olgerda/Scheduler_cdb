using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalendarControl3_Interfaces;

namespace Scheduler.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            InitTestData();
            columnsControl.Table = testTable;
        }

        private TestTable testTable;
        void InitTestData()
        {
            testTable = new TestTable();
            testTable.Columns.Add(new TestTable.TestColumn());
            testTable.Columns.Add(new TestTable.TestColumn());
            testTable.Columns[0].Entities.Add(new TestTable.TestColumn.TestEntity());
            testTable.Columns[0].Entities.Add(new TestTable.TestColumn.TestEntity());
            testTable.Columns[1].Entities.Add(new TestTable.TestColumn.TestEntity());
            testTable.Columns[1].Entities.Add(new TestTable.TestColumn.TestEntity());

            testTable.Columns[0].Name = "Кабинет1";
            testTable.Columns[1].Name = "Кабинет2";
            (testTable.Columns[0].Entities[0] as TestTable.TestColumn.TestEntity).StringToShow = $"Информация1{Environment.NewLine}Время 10:00";
            (testTable.Columns[0].Entities[0] as TestTable.TestColumn.TestEntity).TopLevel = 50;
            (testTable.Columns[0].Entities[0] as TestTable.TestColumn.TestEntity).BottomLevel = 150;
            (testTable.Columns[0].Entities[1] as TestTable.TestColumn.TestEntity).StringToShow = $"Информация2{Environment.NewLine}Время 13:00";
            (testTable.Columns[0].Entities[1] as TestTable.TestColumn.TestEntity).TopLevel = 250;
            (testTable.Columns[0].Entities[1] as TestTable.TestColumn.TestEntity).BottomLevel = 350;

            (testTable.Columns[1].Entities[0] as TestTable.TestColumn.TestEntity).StringToShow = $"Информация3{Environment.NewLine}Время 11:00";
            (testTable.Columns[1].Entities[0] as TestTable.TestColumn.TestEntity).TopLevel = 120;
            (testTable.Columns[1].Entities[0] as TestTable.TestColumn.TestEntity).BottomLevel = 140;
            (testTable.Columns[1].Entities[1] as TestTable.TestColumn.TestEntity).StringToShow = $"Информация4{Environment.NewLine}Время 12:00";
            (testTable.Columns[1].Entities[1] as TestTable.TestColumn.TestEntity).TopLevel = 150;
            (testTable.Columns[1].Entities[1] as TestTable.TestColumn.TestEntity).BottomLevel = 250;

        }
    }

    class TestTable : CalendarControl3_Interfaces.ITable2ControlInterface
    {
        List<IColumn2ControlInterface> columns = new List<IColumn2ControlInterface>();
        public Color ColorBackground { get; set; }

        public Color ColorBorder { get; set; }

        public Color ColorMain { get; set; }

        public int ColumnCount => columns.Count;

        public List<IColumn2ControlInterface> Columns => columns;

        public Font Font { get; set; }

        public int MaxValue => 500;

        public int MinValue { get; set; }

        public Dictionary<int, string> GetDescripptionsToValueLevels()
        {
            return new Dictionary<int, string>() { { 100, "10:00" }, { 300, "14:00" } };
        }

        public class TestColumn : IColumn2ControlInterface
        {
            private List<IEntity2ControlInterface> entities = new List<IEntity2ControlInterface>();
            public Color ColorBackground { get; set; }

            public Color ColorBorder { get; set; }

            public Color ColorMain { get; set; }

            public List<IEntity2ControlInterface> Entities => entities;

            public Font Font { get; set; }

            public string Name { get; set; }

            public class TestEntity : IEntity2ControlInterface
            {
                public int BottomLevel { get; set; }

                public Color ColorBackground { get; set; }

                public Color ColorBorder { get; set; }

                public Color ColorMain { get; set; }

                public Font Font { get; set; }

                public string StringToShow { get; set; }

                public int TopLevel { get; set; }

                public bool IsIntersectWith(IEntity2ControlInterface second)
                {
                    return false;
                }
            }
        }

    }
}
