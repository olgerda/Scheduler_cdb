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

        Scheduler_Forms_Interfaces.ISpecialistList DBspecialistList;
        Scheduler_Forms_Interfaces.IClientList DBclientList;
        Scheduler_Controls_Interfaces.ISpecializationList DBspecializationList;
        Scheduler_Forms_Interfaces.ICabinetList DBcabinetList;

        Scheduler_DBobjects_Intefraces.Scheduler_DBconnector dbconnector;

        public MainDatabase()
        {
            entityFactory = new InterfacesRealisations.EntityFactory();

            dbconnector = entityFactory.NewDBConnector();

            DBspecialistList = entityFactory.NewSpecialistList();
            DBclientList = entityFactory.NewClientList();
            DBspecializationList = entityFactory.NewSpecializationList();
            DBcabinetList = entityFactory.NewCabinetList();


            UpdateDBbyList<Scheduler_Controls_Interfaces.ICabinet>(DBcabinetList);
            UpdateDBbyList<Scheduler_Controls_Interfaces.IClient>(DBclientList);
            UpdateDBbyList<Scheduler_Controls_Interfaces.ISpecialist>(DBspecialistList);
            UpdateSpecialization();

            specialistList = (Scheduler_Forms_Interfaces.ISpecialistList)DBspecialistList.Copy();
            clientList = (Scheduler_Forms_Interfaces.IClientList)DBclientList.Copy();
            specializationList = (Scheduler_Controls_Interfaces.ISpecializationList)DBspecializationList.Copy();
            cabinetList = (Scheduler_Forms_Interfaces.ICabinetList)DBcabinetList.Copy();

        }

        List<Scheduler_DBobjects_Intefraces.IEntity> Scheduler_DBobjects_Intefraces.IMainDataBase.SelectReceptionsFromDate(DateTime date)
        {
            return dbconnector.GetReceptionsFromDate(date);
        }

        Scheduler_Forms_Interfaces.ISpecialistList Scheduler_DBobjects_Intefraces.IMainDataBase.SpecialistList
        {
            get
            {
                SyncList<Scheduler_Controls_Interfaces.ISpecialist>(DBspecialistList, specialistList);

                return specialistList;
            }
        }

        Scheduler_Forms_Interfaces.IClientList Scheduler_DBobjects_Intefraces.IMainDataBase.ClientList
        {
            get
            {
                SyncList<Scheduler_Controls_Interfaces.IClient>(DBclientList, clientList);

                return clientList;
            }
        }


        Scheduler_Controls_Interfaces.ISpecializationList Scheduler_DBobjects_Intefraces.IMainDataBase.SpecializationList
        {
            get
            {
                var Added = specializationList.SpecializationList.Except(DBspecializationList.SpecializationList);
                var Removed = DBspecializationList.SpecializationList.Except(specializationList.SpecializationList);

                foreach (var sa in Added)
                    AddSpecialization(sa);
                foreach (var sr in Removed)
                    RemoveSpecialization(sr);
                UpdateSpecialization();
                specializationList.SpecializationList.Clear();
                foreach (var s in DBspecializationList.SpecializationList)
                    specializationList.SpecializationList.Add(s);

                return specializationList;
            }
        }

        Scheduler_Forms_Interfaces.ICabinetList Scheduler_DBobjects_Intefraces.IMainDataBase.CabinetList
        {
            get
            {
                SyncList<Scheduler_Controls_Interfaces.ICabinet>(DBcabinetList, cabinetList);

                return cabinetList;
            }
        }

        Scheduler_Common_Interfaces.IFactory Scheduler_DBobjects_Intefraces.IMainDataBase.EntityFactory
        {
            get { return entityFactory; }
        }

        //         delegate void AddItem<T>(T item);
        //         delegate void RemoveItem<T>(T item);
        //         delegate void UpdateDBbyList<T>(Scheduler_Forms_Interfaces.IEntityList<T> list);

        void SyncList<T>(Scheduler_Forms_Interfaces.IEntityList<T> dblist, Scheduler_Forms_Interfaces.IEntityList<T> list) where T : Scheduler_Controls_Interfaces.IDummy
        {
            var Added = list.List.Except(dblist.List);
            var Removed = dblist.List.Except(list.List);
            bool needUpdate = false;
            foreach (var sa in Added)
            {
                AddItem<T>(sa);
                needUpdate = true;
            }
            foreach (var sr in Removed)
            {
                RemoveItem<T>(sr);
                needUpdate = true;
            }
            if (needUpdate)
                UpdateDBbyList<T>(dblist);
            list.List.Clear();
            list.List.AddRange(dblist.List);
        }

        void AddItem<T>(T item) where T : Scheduler_Controls_Interfaces.IDummy
        {
            Scheduler_Controls_Interfaces.IClient cl = item as Scheduler_Controls_Interfaces.IClient;
            if (cl != null)
            {
                dbconnector.AddClient(cl);
                return;
            }
            Scheduler_Controls_Interfaces.ISpecialist sp = item as Scheduler_Controls_Interfaces.ISpecialist;
            if (sp != null)
            {
                dbconnector.AddSpecialist(sp);
                return;
            }
            Scheduler_Controls_Interfaces.ICabinet cab = item as Scheduler_Controls_Interfaces.ICabinet;
            if (cab != null)
            {
                dbconnector.AddCabinet(cab);
                return;
            }
        }

        void RemoveItem<T>(T item) where T : Scheduler_Controls_Interfaces.IDummy
        {
            Scheduler_Controls_Interfaces.IClient cl = item as Scheduler_Controls_Interfaces.IClient;
            if (cl != null)
            {
                dbconnector.RemoveClient(cl);
                return;
            }
            Scheduler_Controls_Interfaces.ISpecialist sp = item as Scheduler_Controls_Interfaces.ISpecialist;
            if (sp != null)
            {
                dbconnector.RemoveSpecialist(sp);
                return;
            }
            Scheduler_Controls_Interfaces.ICabinet cab = item as Scheduler_Controls_Interfaces.ICabinet;
            if (cab != null)
            {
                dbconnector.RemoveCabinet(cab);
                return;
            }
        }

        void UpdateDBbyList<T>(Scheduler_Forms_Interfaces.IEntityList<T> list) where T : Scheduler_Controls_Interfaces.IDummy
        {
            Scheduler_Forms_Interfaces.IClientList cl = list as Scheduler_Forms_Interfaces.IClientList;
            if (cl != null)
            {
                DBclientList = dbconnector.AllClients();
                return;
            }
            Scheduler_Forms_Interfaces.ISpecialistList sp = list as Scheduler_Forms_Interfaces.ISpecialistList;
            if (sp != null)
            {
                DBspecialistList = dbconnector.AllSpecialists();
                return;
            }
            Scheduler_Forms_Interfaces.ICabinetList cab = list as Scheduler_Forms_Interfaces.ICabinetList;
            if (cab != null)
            {
                DBcabinetList = dbconnector.AllCabinets();
                return;
            }
        }

        void AddSpecialization(string specialization)
        {
            dbconnector.AddSpecialization(specialization);
        }

        void RemoveSpecialization(string specialization)
        {
            dbconnector.RemoveSpecialization(specialization);
        }

        void UpdateSpecialization()
        {
            DBspecializationList.SpecializationList.Clear();
            DBspecializationList = dbconnector.AllSpecializations();
        }



    }
}
