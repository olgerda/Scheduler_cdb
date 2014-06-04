using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_DBobjects
{
    public class MainDatabase : Scheduler_DBobjects_Intefraces.IMainDataBase
    {
        Scheduler_Common_Interfaces.IFactory entityFactory;

        Scheduler_Forms_Interfaces.ISpecialistList specialistList;
        Scheduler_Forms_Interfaces.IClientList clientList;
        Scheduler_Controls_Interfaces.ISpecializationList specializationList;
        Scheduler_Forms_Interfaces.ICabinetList cabinetList;

        Scheduler_DBobjects_Intefraces.Scheduler_DBconnector dbconnector;

        string errMsg;

        public MainDatabase()
        {
            entityFactory = new Scheduler_InterfacesRealisations.EntityFactory();

            var SqlConnectionNode = System.Xml.Linq.XDocument.Load("config.xml").Descendants("dbconnection").First();
            string connString = String.Empty;

            connString = String.Join(";",SqlConnectionNode.Descendants().Select(n => String.Format("{0}={1}", n.Name, n.Value)).ToArray());

            dbconnector = entityFactory.NewDBConnector();
            dbconnector.ConnectionString = connString;

            if (dbconnector.CheckDBConnection(out errMsg))
            {
                clientList = dbconnector.AllClients();
                specialistList = dbconnector.AllSpecialists();
                specializationList = dbconnector.AllSpecializations();
                cabinetList = dbconnector.AllCabinets();
            }

        }

        string Scheduler_DBobjects_Intefraces.IMainDataBase.ErrorString
        {
            get
            {
                return errMsg;
            }
        }

        List<Scheduler_DBobjects_Intefraces.IEntity> Scheduler_DBobjects_Intefraces.IMainDataBase.SelectReceptionsFromDate(DateTime date)
        {
            return dbconnector.GetReceptionsFromDate(date);
        }

        Scheduler_Forms_Interfaces.ISpecialistList Scheduler_DBobjects_Intefraces.IMainDataBase.SpecialistList
        {
            get
            {
                return specialistList;
            }
        }

        Scheduler_Forms_Interfaces.IClientList Scheduler_DBobjects_Intefraces.IMainDataBase.ClientList
        {
            get
            {
                return clientList;
            }
        }


        Scheduler_Controls_Interfaces.ISpecializationList Scheduler_DBobjects_Intefraces.IMainDataBase.SpecializationList
        {
            get
            {
                return specializationList;
            }
        }

        Scheduler_Forms_Interfaces.ICabinetList Scheduler_DBobjects_Intefraces.IMainDataBase.CabinetList
        {
            get
            {
                return cabinetList;
            }
        }

        Scheduler_Common_Interfaces.IFactory Scheduler_DBobjects_Intefraces.IMainDataBase.EntityFactory
        {
            get { return entityFactory; }
        }

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
            dbconnector.MakeBackup(filename);
        }

        void Scheduler_DBobjects_Intefraces.IMainDataBase.RestoreBackup(string filename)
        {
            dbconnector.RestoreBackup(filename);
        }

        void Scheduler_DBobjects_Intefraces.IMainDataBase.ClearErrorString()
        {
            errMsg = null;
        }
    }
}
