using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scheduler_Controls_Interfaces;
using Scheduler_DBobjects_Intefraces;
using Scheduler_Forms_Interfaces;

namespace Scheduler_DBobjects
{
    public class MainDatabase : IMainDataBase
    {
        Scheduler_Common_Interfaces.IFactory entityFactory;

        ISpecialistList specialistList;
        private IAdministratorList administratorList;
        IClientList clientList;

        Scheduler_Controls_Interfaces.ISpecializationList specializationList;
        private ISpecialistDutyList specialistDutyList;
        private IAdministratorDutyList administratorDutyList;
        ICabinetList cabinetList;

        Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace dbconnector;

        string errMsg;

        public MainDatabase()
        {
            entityFactory = new Scheduler_InterfacesRealisations.EntityFactory();

            var SqlConnectionNode = System.Xml.Linq.XDocument.Load("config.xml").Descendants("dbconnection").First();
            string connString = String.Empty;

            connString = String.Join(";", SqlConnectionNode.Descendants().Select(n => String.Format("{0}={1}", n.Name, n.Value)).ToArray());

            dbconnector = entityFactory.NewDBConnector();
            if (String.IsNullOrWhiteSpace(dbconnector.ConnectionString))
                dbconnector.ConnectionString = connString;

            if (dbconnector.CheckDBConnection(out errMsg))
            {
                clientList = dbconnector.AllClients();
                specialistList = dbconnector.AllSpecialists();
                administratorList = dbconnector.AllAdministrators();
                specializationList = dbconnector.AllSpecializations();
                cabinetList = dbconnector.AllCabinets();
                specialistDutyList = dbconnector.AllSpecDuty();
                administratorDutyList = dbconnector.AllAdmDuty();
            }

            entityFactory.NewClient().ReceptionListFuncition(dbconnector.GetReceptionsForClient);
            entityFactory.NewSpecialist().CostsFunction(dbconnector.GetCostsForSpecialist);

        }

        public string ErrorString
        {
            get
            {
                return errMsg;
            }
        }

        public List<Scheduler_DBobjects_Intefraces.IEntity> SelectReceptionsFromDate(DateTime date)
        {
            return dbconnector.GetReceptionsFromDate(date);
        }

        public List<Scheduler_DBobjects_Intefraces.IEntity> SelectReceptionsBetweenDates(DateTime startDate, DateTime endDate)
        {
            return dbconnector.GetReceptionsBetweenDates(startDate, endDate);
        }

        public ISpecialistList SpecialistList
        {
            get
            {
                return specialistList;
            }
        }

        public IClientList ClientList
        {
            get
            {
                return clientList;
            }
        }


        public Scheduler_Controls_Interfaces.ISpecializationList SpecializationList
        {
            get
            {
                return specializationList;
            }
        }

        public ICabinetList CabinetList
        {
            get
            {
                return cabinetList;
            }
        }

        public Scheduler_Common_Interfaces.IFactory EntityFactory => entityFactory;

        public IClientList ArendatorList => dbconnector.AllArendators();

        public ISpecialistDutyList SpecialistDutyList => specialistDutyList;

        public IAdministratorList AdministratorList => administratorList;

        public IAdministratorDutyList AdministratorDutyList => administratorDutyList;


        void Scheduler_DBobjects_Intefraces.IMainDataBase.AddReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            dbconnector.AddReception(reception);
        }

        void Scheduler_DBobjects_Intefraces.IMainDataBase.RemoveReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            dbconnector.RemoveReception(reception);
        }

        void Scheduler_DBobjects_Intefraces.IMainDataBase.UpdateReception(Scheduler_DBobjects_Intefraces.IEntity reception)
        {
            dbconnector.UpdateReception(reception);
        }


        void Scheduler_DBobjects_Intefraces.IMainDataBase.MakeBackup(string filename)
        {
            try
            {
                dbconnector.MakeBackup(filename);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        void Scheduler_DBobjects_Intefraces.IMainDataBase.RestoreBackup(string filename)
        {
            try
            {
                dbconnector.RestoreBackup(filename);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        void Scheduler_DBobjects_Intefraces.IMainDataBase.ClearErrorString()
        {
            errMsg = null;
        }

        public IEnumerable<IDuty> SelectDutyFromDate<T>(DateTime date) where T : ICanNotWork
        {
            if (typeof(T) == typeof(ISpecialist))
                return specialistDutyList.List.Where(x => x.Start.Date == date.Date).Cast<IDuty>();
            else
                return administratorDutyList.List.Where(x => x.Start.Date == date.Date).Cast<IDuty>();
        }

        public IEnumerable<DateTime> SelectDutyDates<T>(T spec) where T : ICanNotWork
        {
            if (typeof(ISpecialist).IsAssignableFrom(spec.GetType()))
            {
                ISpecialist s = (ISpecialist)spec;
                return specialistDutyList.List.Where(x => x.Named == s).Select(x => x.Start.Date);
            }
            else
            {
                IAdministrator a = (IAdministrator)spec;
                return administratorDutyList.List.Where(x => x.Named == a).Select(x => x.Start.Date);
            }
        }

        public int SpecialistGetReceptionCount(ISpecialist spec)
        {
            return dbconnector.SpecialistGetReceptionCount(spec);
        }

        public int SpecialistGetClientCount(ISpecialist spec)
        {
            return dbconnector.SpecialistGetClientCount(spec);
        }

        public List<IReception> SpecialistGetReceptions(ISpecialist spec)
        {
            return dbconnector.SpecialistGetReceptions(spec);
        }

        public List<IClient> SpecialistGetClients(ISpecialist spec)
        {
            return dbconnector.SpecialistGetClients(spec);
        }
    }
}
