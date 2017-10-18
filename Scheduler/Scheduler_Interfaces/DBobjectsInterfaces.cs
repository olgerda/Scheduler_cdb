using System;
using System.Collections.Generic;
using CalendarControl3_Interfaces;
using Scheduler_Common_Interfaces;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;
using System.ComponentModel;

namespace Scheduler_DBobjects_Intefraces
{
    public enum ColorChangers
    {
        [Description("Обычный приём")]
        ReceptionClient,
        [Description("Аренда")]
        ReceptionRent,
        [Description("Аренда Песок")]
        ReceptionSpecialRent,
        [Description("Комментарий")]
        ReceptionComment,
        [Description("Не состоявшийся приём")]
        ReceptionNotTakePlace,
        [Description("Не состоявшийся приём (аренда)")]
        ReceptionNotTakePlaceRent,
        [Description("Кабинет для комментариев")]
        ColumnCommentOnly,
        [Description("Кабинет для приёмов")]
        ColumnForReceptions,
        [Description("Основная таблица")]
        Table,
    }

    public interface IMainDataBase: ICanRequestStatistics
    {
        List<IEntity> SelectReceptionsFromDate(DateTime date);
        List<IEntity> SelectReceptionsBetweenDates(DateTime startDate, DateTime endDate);
        IEnumerable<IDuty> SelectDutyFromDate<T>(DateTime date) where T : ICanNotWork;
        IEnumerable<DateTime> SelectDutyDates<T>(T spec) where T: ICanNotWork;

        void AddReception(IEntity reception);
        void RemoveReception(IEntity reception);
        void UpdateReception(IEntity reception);

        void MakeBackup(string filename);
        void RestoreBackup(string filename);

        ISpecialistList SpecialistList { get; }
        IAdministratorList AdministratorList { get; }
        IClientList ClientList { get; }
        IClientList ArendatorList { get; }
        ISpecializationList SpecializationList { get; }
        ICabinetList CabinetList { get; }
        ISpecialistDutyList SpecialistDutyList { get; }
        IAdministratorDutyList AdministratorDutyList { get; }
        IFactory EntityFactory { get; }

        string ErrorString { get; }
        void ClearErrorString();

    }

    public interface ICanRequestStatistics
    {
        int SpecialistGetReceptionCount(ISpecialist spec);
        int SpecialistGetClientCount(ISpecialist spec);
        List<IReception> SpecialistGetReceptions(ISpecialist spec);
        List<IClient> SpecialistGetClients(ISpecialist spec);
    }


    public interface ITable : ITable2ControlInterface
    {
        ITimeInterval WorkTimeInterval { get; set; }

        void SetInfoColumnDescriptions(Dictionary<DateTime, string> descriptions);

        DateTime ConvertLevelToTime(int level);
        int ConvertTimeToLevel(DateTime time);

        void SetColors(Dictionary<ColorChangers, IColorPalette> palette);
    }

    public interface IColumn : IColumn2ControlInterface
    {
        void AddEntity(IEntity entity);
        bool OnlyComment { get; set; }
    }

    public interface IEntity : IEntity2ControlInterface, IReception
    {
        void SetDatabase(IMainDataBase db);
    }

    public interface Scheduler_DBconnectorIntefrace : ICanRequestStatistics
    {
        IFactory EntityFactory { get; set; }
        string ConnectionString { get; set; }

        void AddClient(IClient client);
        void UpdateClientData(IClient client);
        void RemoveClient(IClient client);
        IClientList AllClients();
        IClientList AllArendators();

        void AddSpecialist(ISpecialist specialist);
        void UpdateSpecialistData(ISpecialist specialist);
        void RemoveSpecialist(ISpecialist specialist);
        ISpecialistList AllSpecialists();

        void AddSpecialization(string specialization);
        void RemoveSpecialization(string specialization);
        ISpecializationList AllSpecializations();
        ISpecialistDutyList AllSpecDuty();
        IAdministratorDutyList AllAdmDuty();
        IAdministratorList AllAdministrators();

        void AddCabinet(ICabinet cabinet);
        void UpdateCabinetData(ICabinet cabinet);
        void RemoveCabinet(ICabinet cabinet);
        ICabinetList AllCabinets();

        List<IEntity> GetReceptionsFromDate(DateTime date);
        List<IEntity> GetReceptionsBetweenDates(DateTime startDate, DateTime endDate);

        List<IReception> GetReceptionsForClient(IClient client);
        Dictionary<int, int> GetCostsForSpecialist(ISpecialist spec);

        void AddReception(IEntity reception);
        void UpdateReception(IEntity reception);
        void RemoveReception(IEntity reception);

        //int GetPriceForSpecialistClientPair(int spid, int clid);
        //void SetOrUpdatePriceForSpecialistClientPair(int spid, int clid, int price);

        void MakeBackup(string filename);
        void RestoreBackup(string filename);

        bool CheckDBConnection(out string message);
    }



    public enum ColumnType
    {
        Cabinet,
        Remarks
    }

    public enum EntityType
    {
        Client,
        ClientSMS,
        Rent,
        RentSpecial,
        ReceptionNotTakePlace
    }

    public abstract class Scheduler_DBconnector : Scheduler_DBconnectorIntefrace
    {
        public const int DEFAULTCLIENTID = 0;
        protected IClientList clientList;
        protected IClientList arendatorList;
        protected ISpecialistList specialistList;
        protected IAdministratorList administratorList;
        protected ICabinetList cabinetList;
        protected ISpecialistDutyList dutySpecList;
        protected IAdministratorDutyList dutyAdmList;

        protected Scheduler_DBconnector(IFactory factory)
        {
            EntityFactory = factory;
        }
        public IFactory EntityFactory { get; set; }

        public abstract string ConnectionString { get; set; }
        public abstract void AddClient(IClient client);
        public abstract void UpdateClientData(IClient client);
        public abstract void RemoveClient(IClient client);

        public IClientList AllClients()
        {
            return (clientList ?? (clientList = AllClientsInternal()));
        }

        public IClientList AllArendators()
        {
            return (arendatorList ?? (arendatorList = AllClientsInternal(1)));
        }

        protected abstract IClientList AllClientsInternal(int clientType = 0);
        protected IClientList AllClients(IEnumerable<IClient> list, ItemAddedHandler added, ItemChangedHandler changed, ItemRemovedHandler removed)
        {
            var result = EntityFactory.NewClientList();
            result.List.AddRange(list);

            result.OnItemAdded += added;
            result.OnItemRemoved += removed;
            result.OnItemChanged += changed;
            return result;
        }

        public abstract void AddSpecialist(ISpecialist specialist);
        public abstract void UpdateSpecialistData(ISpecialist specialist);
        public abstract void RemoveSpecialist(ISpecialist specialist);

        public ISpecialistList AllSpecialists()
        {
            return (specialistList ?? (specialistList = AllSpecialistsInternal()));
        }

        protected abstract ISpecialistList AllSpecialistsInternal();
        public abstract void AddSpecialization(string specialization);
        public abstract void RemoveSpecialization(string specialization);
        public abstract ISpecializationList AllSpecializations();
        public abstract void AddCabinet(ICabinet cabinet);
        public abstract void UpdateCabinetData(ICabinet cabinet);
        public abstract void RemoveCabinet(ICabinet cabinet);

        public ICabinetList AllCabinets()
        {
            return cabinetList ?? (cabinetList = AllCabinetsInternal());
        }
        protected abstract ICabinetList AllCabinetsInternal();
        public abstract List<IEntity> GetReceptionsFromDate(DateTime date);
        public abstract List<IEntity> GetReceptionsBetweenDates(DateTime startDate, DateTime endDate);
        public abstract List<IReception> GetReceptionsForClient(IClient client);
        public abstract Dictionary<int, int> GetCostsForSpecialist(ISpecialist spec);
        public abstract void AddReception(IEntity reception);
        public abstract void UpdateReception(IEntity reception);
        public abstract void RemoveReception(IEntity reception);
        public abstract void MakeBackup(string filename);
        public abstract void RestoreBackup(string filename);
        public abstract bool CheckDBConnection(out string message);

        public ISpecialistDutyList AllSpecDuty()
        {
            return dutySpecList ?? (dutySpecList = AllSpecDutyInternal());
        }

        public IAdministratorDutyList AllAdmDuty()
        {
            return dutyAdmList ?? (dutyAdmList = AllAdmDutyInternal());
        }

        public abstract void AddSpecialistDuty(ISpecialistDuty dt);
        public abstract void RemoveSpecialistDuty(ISpecialistDuty dt);
        public abstract void UpdateSpecialistDuty(ISpecialistDuty dt);

        protected abstract ISpecialistDutyList AllSpecDutyInternal();

        public IAdministratorList AllAdministrators()
        {
            return (administratorList ?? (administratorList = AllAdministratorsInternal()));
        }


        protected abstract IAdministratorList AllAdministratorsInternal();
        public abstract void AddAdministratorDuty(IAdministratorDuty dt);
        public abstract void RemoveAdministratorDuty(IAdministratorDuty dt);
        public abstract void UpdateAdministratorDuty(IAdministratorDuty dt);

        protected abstract IAdministratorDutyList AllAdmDutyInternal();
        public abstract int SpecialistGetReceptionCount(ISpecialist spec);
        public abstract int SpecialistGetClientCount(ISpecialist spec);
        public abstract List<IReception> SpecialistGetReceptions(ISpecialist spec);
        public abstract List<IClient> SpecialistGetClients(ISpecialist spec);
    }
}
