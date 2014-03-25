using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_DBobjects
{
    public class Table : Scheduler_DBobjects_Intefraces.ITable
    {
        Scheduler_Controls_Interfaces.ITimeInterval Scheduler_DBobjects_Intefraces.ITable.WorkTimeInterval
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        DateTime Scheduler_DBobjects_Intefraces.ITable.ConvertLevelToTime(int level)
        {
            throw new NotImplementedException();
        }

        int Scheduler_DBobjects_Intefraces.ITable.ConvertTimeToLevel(DateTime time)
        {
            throw new NotImplementedException();
        }

        int CalendarControl3_Interfaces.ITable2ControlInterface.ColumnCount
        {
            get { throw new NotImplementedException(); }
        }

        int CalendarControl3_Interfaces.ITable2ControlInterface.MinValue
        {
            get { throw new NotImplementedException(); }
        }

        int CalendarControl3_Interfaces.ITable2ControlInterface.MaxValue
        {
            get { throw new NotImplementedException(); }
        }

        Dictionary<int, string> CalendarControl3_Interfaces.ITable2ControlInterface.GetDescripptionsToValueLevels()
        {
            throw new NotImplementedException();
        }

        List<CalendarControl3_Interfaces.IColumn2ControlInterface> CalendarControl3_Interfaces.ITable2ControlInterface.Columns
        {
            get { throw new NotImplementedException(); }
        }
    }
}
