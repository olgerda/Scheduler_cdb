using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CalendarControl3_Interfaces;
using Scheduler_DBobjects_Intefraces;
using Scheduler_Controls_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    public class Table : ControlsColors, ITable
    {
        ITimeInterval workInterval;
        TimeSpan duration;

        List<IColumn2ControlInterface> columns;
        Dictionary<int, string> descriptions;

        int minValue;
        int maxValue;

        public Table()
        {
            columns = new List<IColumn2ControlInterface>();
            workInterval = null;
            descriptions = new Dictionary<int, string>();

            minValue = 0;
            maxValue = 0;
        }

        public ITimeInterval WorkTimeInterval
        {
            get
            {
                return workInterval;
            }
            set
            {
                workInterval = value;
                duration = workInterval.EndDate - workInterval.StartDate;
                minValue = ConvertTimespanToLevel(workInterval.StartDate.TimeOfDay);
                maxValue = ConvertTimespanToLevel(workInterval.EndDate.TimeOfDay);
            }
        }

        public DateTime ConvertLevelToTime(int level)
        {
            if (workInterval == null || level > maxValue || level < minValue)
                return DateTime.Now;
            return workInterval.StartDate.Date.AddMinutes(level);
        }

        public int ConvertTimeToLevel(DateTime time)
        {
            if (workInterval == null ||
                workInterval.StartDate.TimeOfDay > time.TimeOfDay
                || workInterval.EndDate.TimeOfDay < time.TimeOfDay)
                return -1;
            return minValue + ConvertTimespanToLevel(time.TimeOfDay);
        }

        public int ColumnCount
        {
            get { return columns.Count; }
        }

        public int MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                maxValue = minValue + ConvertTimespanToLevel(duration);
            }
        }

        public int MaxValue
        {
            get { return maxValue; }
        }

        public Dictionary<int, string> GetDescripptionsToValueLevels()
        {
            return descriptions;
        }

        public List<IColumn2ControlInterface> Columns
        {
            get { return columns; }
        }


        public void SetInfoColumnDescriptions(Dictionary<DateTime, string> descriptions)
        {
            foreach (var pair in descriptions)
            {
                int level = ConvertTimespanToLevel(pair.Key.TimeOfDay);
                if (!this.descriptions.ContainsKey(level))
                    this.descriptions.Add(level, pair.Value);
            }
        }
        private int ConvertTimespanToLevel(TimeSpan input)
        {
            return Convert.ToInt32(Math.Truncate(input.TotalMinutes));
        }

        //private void SetColors(IColorPalette source, IColorPalette target)
        //{
        //    target.ColorBackground = source.ColorBackground;
        //    target.ColorBorder = source.ColorBorder;
        //    target.ColorMain = source.ColorMain;
        //    target.Font = source.Font;
        //}

        //public void SetColumnColors(ColumnType columnType, IColorPalette colors)
        //{
        //    switch (columnType)
        //    {
        //        case ColumnType.Cabinet:
        //            foreach (var col in columns.Cast<IColumn>().Where(x => !x.OnlyComment).Select(x => x.Coloring))
        //                SetColors(colors, col);
        //            break;
        //        case ColumnType.Remarks:
        //            foreach (var col in columns.Cast<IColumn>().Where(x => x.OnlyComment).Select(x => x.Coloring))
        //                SetColors(colors, col);
        //            break;
        //    }
        //}

        //public void SetEntityColors(EntityType entityType, IColorPalette colors)
        //{
        //    switch (entityType)
        //    {
        //        case EntityType.Client:
        //            foreach (var ent in columns.SelectMany(x => x.Entities).Cast<IEntity>().Where(x => !x.Rent).Select(x => x.Coloring))
        //                SetColors(colors, ent);
        //            break;
        //        case EntityType.Rent:
        //            foreach (var ent in columns.SelectMany(x => x.Entities).Cast<IEntity>().Where(x => x.Rent).Select(x => x.Coloring))
        //                SetColors(colors, ent);
        //            break;
        //    }
        //}

        public void SetColors(Dictionary<ColorChangers, IColorPalette> palette)
        {
            var table = this;
            table.Coloring = palette[ColorChangers.Table];
            foreach (Scheduler_DBobjects_Intefraces.IColumn column in table.Columns)
            {
                if (column.OnlyComment)
                    column.Coloring = palette[ColorChangers.ColumnCommentOnly];
                else
                    column.Coloring = palette[ColorChangers.ColumnForReceptions];

                foreach (Scheduler_DBobjects_Intefraces.IEntity ent in column.Entities)
                {
                    if (ent.CommentOnlyReception)
                        ent.Coloring = palette[ColorChangers.ReceptionComment];
                    else if (ent.Rent)
                    {
                        ent.Coloring = palette[ColorChangers.ReceptionRent];
                        if (ent.SpecialRent)
                            ent.Coloring = palette[ColorChangers.ReceptionSpecialRent];
                    }
                    else if (ent.ReceptionDidNotTakePlace)
                    {
                        if (ent.Rent)
                            ent.Coloring = palette[ColorChangers.ReceptionNotTakePlaceRent];
                        else
                            ent.Coloring = palette[ColorChangers.ReceptionNotTakePlace];
                    }
                    else
                        ent.Coloring = palette[ColorChangers.ReceptionClient];
                }
            }
        }
    }
}
