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

        Scheduler_DBobjects_Intefraces.IMainDataBase NewMainDataBase();
        Scheduler_DBobjects_Intefraces.Scheduler_DBconnector NewDBConnector();
        Scheduler_DBobjects_Intefraces.ITable NewTable();
        Scheduler_DBobjects_Intefraces.IColumn NewColumn();
        Scheduler_DBobjects_Intefraces.IEntity NewEntity();
    }
}
