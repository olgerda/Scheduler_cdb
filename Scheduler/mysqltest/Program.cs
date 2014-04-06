using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqltest
{
    class Program
    {
        static void Main(string[] args)
        {
            Scheduler_Common_Interfaces.IFactory entity = new InterfacesRealisations.EntityFactory();
            Scheduler_DBobjects_Intefraces.Scheduler_DBconnector connector = new MySqlConnector.MySQLConnector(entity);
//             var cl = entity.NewClient();
//             cl.Name = "test1";
//             cl.BlackListed = false;
//             cl.Comment = "testcommentForTest1";
//             cl.Telephones.Add("123321123");
//             cl.Telephones.Add("524352345");
//             cl.Telephones.Add("684683457");
// 
//             connector.AddClient(cl);
//             cl.Name = "test1changed";
//             cl.BlackListed = true;
//             cl.Comment = "testcomment Changed";
//             cl.Telephones.Remove("123321123");
//             cl.Telephones.Add("123123");
//             connector.UpdateClientData(cl);
//             connector.RemoveClient(cl);
            var spec = entity.NewSpecialist();
            spec.Name = "testspec1";
            spec.NotWorking = false;
            spec.Specialisations.Add("specialization1");
            connector.AddSpecialist(spec);
            spec.Name = "testspec1changed";
            spec.NotWorking = true;
            spec.Specialisations.Clear();
            spec.Specialisations.Add("specialization2");
            connector.UpdateSpecialistData(spec);
            connector.RemoveSpecialist(spec);
            
        }
    }
}
