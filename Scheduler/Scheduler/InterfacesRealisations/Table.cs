using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesRealisations
{
    public class Table : Scheduler_DBobjects_Intefraces.ITable
    {
        Scheduler_Controls_Interfaces.ITimeInterval workInterval;
        TimeSpan duration;

        List<CalendarControl3_Interfaces.IColumn2ControlInterface> columns;
        Dictionary<int, string> descriptions;

        int minValue;
        int maxValue;
        
        public Table()
        {
            columns = new List<CalendarControl3_Interfaces.IColumn2ControlInterface>();
            workInterval = null;
            descriptions = new Dictionary<int, string>();
            
            minValue = 0;
            maxValue = 0;
        }

        Scheduler_Controls_Interfaces.ITimeInterval Scheduler_DBobjects_Intefraces.ITable.WorkTimeInterval
        {
            get
            {
                return workInterval;
            }
            set
            {
                workInterval = value;
                duration = workInterval.EndDate - workInterval.StartDate;
                maxValue = minValue + Convert.ToInt32(Math.Truncate(duration.TotalMinutes));
            }
        }

        DateTime Scheduler_DBobjects_Intefraces.ITable.ConvertLevelToTime(int level)
        {
            if (workInterval == null || level > maxValue || level < minValue)
                return DateTime.Now;
            return workInterval.StartDate.AddMinutes(level);
        }

        int Scheduler_DBobjects_Intefraces.ITable.ConvertTimeToLevel(DateTime time)
        {
            if (workInterval == null || 
                workInterval.StartDate.TimeOfDay > time.TimeOfDay 
                || workInterval.EndDate.TimeOfDay < time.TimeOfDay)
                return -1;
            return minValue + Convert.ToInt32(Math.Truncate(time.TimeOfDay.TotalMinutes));
        }

        int CalendarControl3_Interfaces.ITable2ControlInterface.ColumnCount
        {
            get { return columns.Count; }
        }

        int CalendarControl3_Interfaces.ITable2ControlInterface.MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                maxValue = minValue + Convert.ToInt32(Math.Truncate(duration.TotalMinutes));
            }
        }

        int CalendarControl3_Interfaces.ITable2ControlInterface.MaxValue
        {
            get { return maxValue; }
        }

        Dictionary<int, string> CalendarControl3_Interfaces.ITable2ControlInterface.GetDescripptionsToValueLevels()
        {
            return descriptions;
        }

        List<CalendarControl3_Interfaces.IColumn2ControlInterface> CalendarControl3_Interfaces.ITable2ControlInterface.Columns
        {
            get { return columns; }
        }
    }
}
