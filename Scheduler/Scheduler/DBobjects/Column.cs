using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_DBobjects
{
    public class Column : Scheduler_DBobjects_Intefraces.IColumn
    {
        void Scheduler_DBobjects_Intefraces.IColumn.AddEntity(Scheduler_DBobjects_Intefraces.IEntity entity)
        {
            throw new NotImplementedException();
        }

        string CalendarControl3_Interfaces.IColumn2ControlInterface.Name
        {
            get { throw new NotImplementedException(); }
        }

        List<CalendarControl3_Interfaces.IEntity2ControlInterface> CalendarControl3_Interfaces.IColumn2ControlInterface.Entities
        {
            get { throw new NotImplementedException(); }
        }
    }
}
