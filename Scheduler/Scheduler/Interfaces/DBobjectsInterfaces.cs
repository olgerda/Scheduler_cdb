using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalendarControl3_Interfaces;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;
using Scheduler_Common_Interfaces;

namespace Scheduler_DBobjects_Intefraces
{
    public interface IMainDataBase
    {
        List<IEntity> SelectReceptionsFromDate(DateTime date);

        ISpecialistList SpecialistList { get; }
        IClientList ClientList { get; }
        ISpecializationList SpecializationList { get; }
        ICabinetList CabinetList { get; }
        IFactory EntityFactory { get; }
        
    }

    public interface ITable : ITable2ControlInterface
    {
        ITimeInterval WorkTimeInterval { get; set; }

        DateTime ConvertLevelToTime(int level);
        int ConvertTimeToLevel(DateTime time);
    }

    public interface IColumn : IColumn2ControlInterface
    {
        void AddEntity(IEntity entity);
    }

    public interface IEntity : IEntity2ControlInterface, IReception
    {
    }

}
