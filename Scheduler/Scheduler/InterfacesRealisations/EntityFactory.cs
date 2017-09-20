using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    public class EntityFactory : Scheduler_Common_Interfaces.IFactory
    {
        //         T Create<T>() where T: IDummy, new()
        //         {

        //http://stackoverflow.com/questions/1144835/factory-creating-objects-according-to-a-generic-type-c-sharp
        //             var type = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
        //                 .Where(t => typeof(T).IsAssignableFrom(t)).Single();
        //             return (T)(type.GetConstructor(Type.EmptyTypes).Invoke(null));

        //}

//         public object Create<T>() where T: IDummy
//         {
//             Type currentType = typeof(T);
//             if (currentType == typeof(IClient))
//                 return new Client();
//             if (currentType == typeof(IClientList))
//                 return new ClientList();
//             if (currentType == typeof(ICabinet))
//                 return new Cabinet();
//             if (currentType == typeof(ICabinetList))
//                 return new CabinetList();
//             if (currentType == typeof(ISpecialist))
//                 return new Specialist();
//             if (currentType == typeof(ISpecialistList))
//                 return new SpecialistList();
//             if (currentType == typeof(ISpecializationList))
//                 return new SpecializationList();
//             if (currentType == typeof(IReception))
//                 return new Reception();
//             if (currentType == typeof(ITelephone))
//                 return new Telephone();
//             return null;
//         }

        public IClient NewClient()
        {
            return (IClient)(new Client());
        }

        public ISpecialist NewSpecialist()
        {
            return new Specialist();
        }

        public ISpecialistList NewSpecialistList()
        {
            return new SpecialistList();
        }

        public ISpecializationList NewSpecializationList()
        {
            return new SpecializationList();
        }

        public ICabinet NewCabinet()
        {
            return new Cabinet();
        }

        public ICabinetList NewCabinetList()
        {
            return new CabinetList();
        }

        public ITelephone NewTelephone()
        {
            return new Telephone();
        }
        
        public IClientList NewClientList()
        {
            return new ClientList();
        }

        public ITimeInterval NewTimeInterval()
        {
            return new TimeInterval();
        }

        public Scheduler_DBobjects_Intefraces.IMainDataBase NewMainDataBase()
        {
            return new Scheduler_DBobjects.MainDatabase();
        }

        public Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace NewDBConnector()
        {
            //TODO: implement code to switch connectors
            var connectorFileName = Scheduler.Properties.Settings.Default.DbConnectorFile;
            var file = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,
                connectorFileName, System.IO.SearchOption.TopDirectoryOnly);
            if (file.Length != 1)
                throw new OverflowException("There " + (file.Length == 0 ? "none" : "more than one") + " DbConnectorFile found in directory " + AppDomain.CurrentDomain.BaseDirectory);
            var t = System.Reflection.Assembly.LoadFile(file[0])
                .GetExportedTypes()
                .First(x => typeof(Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace).IsAssignableFrom(x));
            
            return (Scheduler_DBobjects_Intefraces.Scheduler_DBconnectorIntefrace)(Activator.CreateInstance(t, this));
            //return new MySqlConnector.MySQLConnector(this);
        }

        public Scheduler_DBobjects_Intefraces.ITable NewTable()
        {
            return new Table();
        }

        public Scheduler_DBobjects_Intefraces.IColumn NewColumn()
        {
            return new Column();
        }

        public Scheduler_DBobjects_Intefraces.IEntity NewEntity()
        {
            return new Reception();
        }

        public ISpecialistDuty NewSpecialistDuty()
        {
            return new SpecialistDuty();
        }

        public ISpecialistDutyList NewSpecialistDutyList()
        {
            return new SpecialistDutyList();
        }
    }
}
