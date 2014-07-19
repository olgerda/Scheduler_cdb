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
        List<IEntity> SelectReceptionsBetweenDates(DateTime startDate, DateTime endDate);

        void AddReception(IEntity reception);
        void RemoveReception(IEntity reception);
        void UpdateReception(IEntity reception);

        void MakeBackup(string filename);
        void RestoreBackup(string filename);

        ISpecialistList SpecialistList { get; }
        IClientList ClientList { get; }
        ISpecializationList SpecializationList { get; }
        ICabinetList CabinetList { get; }
        IFactory EntityFactory { get; }

        string ErrorString { get; }
        void ClearErrorString();

    }

    public interface ITable : ITable2ControlInterface
    {
        ITimeInterval WorkTimeInterval { get; set; }

        void SetInfoColumnDescriptions(Dictionary<DateTime, string> descriptions);

        DateTime ConvertLevelToTime(int level);
        int ConvertTimeToLevel(DateTime time);
    }

    public interface IColumn : IColumn2ControlInterface
    {
        void AddEntity(IEntity entity);
    }

    public interface IEntity : IEntity2ControlInterface, IReception
    {
        void SetDatabase(Scheduler_DBobjects_Intefraces.IMainDataBase db);
    }

    public interface Scheduler_DBconnector
    {
        Scheduler_Common_Interfaces.IFactory EntityFactory { get; set; }
        string ConnectionString { get; set; }

        void AddClient(Scheduler_Controls_Interfaces.IClient client);
        void UpdateClientData(Scheduler_Controls_Interfaces.IClient client);
        void RemoveClient(Scheduler_Controls_Interfaces.IClient client);
        IClientList AllClients();

        void AddSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist);
        void UpdateSpecialistData(Scheduler_Controls_Interfaces.ISpecialist specialist);
        void RemoveSpecialist(Scheduler_Controls_Interfaces.ISpecialist specialist);
        ISpecialistList AllSpecialists();

        void AddSpecialization(string specialization);
        void RemoveSpecialization(string specialization);
        ISpecializationList AllSpecializations();

        void AddCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet);
        void UpdateCabinetData(Scheduler_Controls_Interfaces.ICabinet cabinet);
        void RemoveCabinet(Scheduler_Controls_Interfaces.ICabinet cabinet);
        ICabinetList AllCabinets();

        List<IEntity> GetReceptionsFromDate(DateTime date);
        List<IEntity> GetReceptionsBetweenDates(DateTime startDate, DateTime endDate);

        List<IReception> GetReceptionsForClient(IClient client);

        void AddReception(IEntity reception);
        void UpdateReception(IEntity reception);
        void RemoveReception(IEntity reception);

        //int GetPriceForSpecialistClientPair(int spid, int clid);
        //void SetOrUpdatePriceForSpecialistClientPair(int spid, int clid, int price);

        void MakeBackup(string filename);
        void RestoreBackup(string filename);

        bool CheckDBConnection(out string message);
    }
}
