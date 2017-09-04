using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scheduler_Controls_Interfaces;
using Scheduler_DBobjects_Intefraces;
using Scheduler_Forms_Interfaces;

namespace Scheduler_Common_Interfaces
{
    public interface IFactory
    {
        IClient NewClient();
        IClientList NewClientList();
        ICabinet NewCabinet();
        ICabinetList NewCabinetList();
        ISpecialist NewSpecialist();
        ISpecialistList NewSpecialistList();
        ISpecializationList NewSpecializationList();
        
        //IReception NewReception();

        ITelephone NewTelephone();
        ITimeInterval NewTimeInterval();

        IMainDataBase NewMainDataBase();
        Scheduler_DBconnectorIntefrace NewDBConnector();
        ITable NewTable();
        IColumn NewColumn();
        IEntity NewEntity();
    }
}
