﻿using System;
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
        public enum ColorChangers
        {
            Table,
            Column,
            Entity1,
            Entity2
        }


        public SettingsForm()
        {
            InitializeComponent();
            InitTestData();
            columnsControl.Table = testTable;

            colorPicker1.onColorsChanged += () =>
                {
                    ChangeColors(colorPicker1.SelectedColors, columnsControl.Table);
                };

            colorPicker2.onColorsChanged += () =>
                {
                    foreach (var column in columnsControl.Table.Columns) ChangeColors(colorPicker2.SelectedColors, column);
                };
            colorPicker3.onColorsChanged += () =>
                {
                    foreach (var ent in columnsControl.Table.Columns[0].Entities)
                        ChangeColors(colorPicker3.SelectedColors, ent);
                };
            colorPicker4.onColorsChanged += () =>
                {
                    foreach (var ent in columnsControl.Table.Columns[1].Entities)
                        ChangeColors(colorPicker3.SelectedColors, ent);
                };
        }

        private Action<Scheduler.Controls.ColorPicker.ControlsColors, ICanCustomizeLook> ChangeColors => (colors, control) =>
        {
            control.ColorMain = colors.ColorMain;
            control.ColorBorder = colors.ColorBorder;
            control.ColorBackground = colors.ColorBackground;
            control.Font = colors.Font;
            columnsControl.Refresh();
        };

        private void ColorPicker_onColorsChanged(object sender)
        {
            Scheduler.Controls.ColorPicker colorPicker = (Scheduler.Controls.ColorPicker)sender;
            ChangeColors(colorPicker1.SelectedColors, columnsControl.Table);

            foreach (var column in columnsControl.Table.Columns)
                ChangeColors(colorPicker2.SelectedColors, column);

            foreach (var ent in columnsControl.Table.Columns[0].Entities)
                ChangeColors(colorPicker3.SelectedColors, ent);

            foreach (var ent in columnsControl.Table.Columns[1].Entities)
                ChangeColors(colorPicker3.SelectedColors, ent);
        }

        private Dictionary<ColorChangers, Controls.ColorPicker.ControlsColors> _allColors;

        private Action<Controls.ColorPicker.ControlsColors, Controls.ColorPicker, bool> actualizeColors =
            (colors, control, fromControl) =>
            {
                if (!fromControl)
                    control.SelectedColors = colors;
                else
                    colors = control.SelectedColors;
            };
        public Dictionary<ColorChangers, Controls.ColorPicker.ControlsColors> SelectedColorsDictionary
        {
            get
            {
                actualizeColors(_allColors[ColorChangers.Table], colorPicker1, true);
                actualizeColors(_allColors[ColorChangers.Column], colorPicker2, true);
                actualizeColors(_allColors[ColorChangers.Entity1], colorPicker3, true);
                actualizeColors(_allColors[ColorChangers.Entity2], colorPicker4, true);
                return _allColors;
            }
            set
            {
                _allColors = value;
                RefreshColorPickers();
            }
        }

        public void RefreshColorPickers()
        {
            actualizeColors(_allColors[ColorChangers.Table], colorPicker1, false);
            actualizeColors(_allColors[ColorChangers.Column], colorPicker2, false);
            actualizeColors(_allColors[ColorChangers.Entity1], colorPicker3, false);
            actualizeColors(_allColors[ColorChangers.Entity2], colorPicker4, false);
        }

        private TestTable testTable;
        private TestTable.TestColumn.TestEntity[] entities;
        void InitTestData()
        {
            testTable = new TestTable();
            testTable.Columns.Add(new TestTable.TestColumn());
            testTable.Columns.Add(new TestTable.TestColumn());
            entities = new TestTable.TestColumn.TestEntity[] { new TestTable.TestColumn.TestEntity(), new TestTable.TestColumn.TestEntity(), new TestTable.TestColumn.TestEntity(), new TestTable.TestColumn.TestEntity() };
            testTable.Columns[0].Entities.Add(entities[0]);
            testTable.Columns[0].Entities.Add(entities[1]);
            testTable.Columns[1].Entities.Add(entities[2]);
            testTable.Columns[1].Entities.Add(entities[3]);

            testTable.Columns[0].Name = "Кабинет1";
            testTable.Columns[1].Name = "Кабинет2";
            entities[0].StringToShow = $"Информация1{Environment.NewLine}Время 10:00";
            entities[0].TopLevel = 50;
            entities[0].BottomLevel = 150;
            entities[1].StringToShow = $"Информация2{Environment.NewLine}Время 13:00";
            entities[1].TopLevel = 250;
            entities[1].BottomLevel = 350;

            entities[2].StringToShow = $"Информация3{Environment.NewLine}Время 11:00";
            entities[2].TopLevel = 120;
            entities[2].BottomLevel = 180;
            entities[3].StringToShow = $"Информация4{Environment.NewLine}Время 12:00";
            entities[3].TopLevel = 200;
            entities[3].BottomLevel = 250;

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
