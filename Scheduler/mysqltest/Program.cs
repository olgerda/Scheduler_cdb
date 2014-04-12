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
            Scheduler_Common_Interfaces.IFactory entity = new Scheduler_InterfacesRealisations.EntityFactory();
            //Scheduler_DBobjects_Intefraces.Scheduler_DBconnector connector = new MySqlConnector.MySQLConnector(entity);

            var t1 = new Scheduler_InterfacesRealisations.Client();
            var list = entity.NewClientList();

             var cl = entity.NewClient();
            cl.Name = "test1";
            cl.BlackListed = false;
            cl.Comment = "testcommentForTest1";
            cl.Telephones.Add("123321123");
            cl.Telephones.Add("524352345");
            cl.Telephones.Add("684683457");

            list.List.Add(cl);

            cl = entity.NewClient();
            cl.Name = "test2";
            cl.BlackListed = true;
            cl.Comment = "testcommentForTest2";
            cl.Telephones.Add("444444444");
            cl.Telephones.Add("333333333");
            cl.Telephones.Add("343434343");

            list.List.Add(cl);

            using (var a = new Scheduler_Forms.FindClientCard(list, entity))
            {
                a.ShowDialog();
            }


//             connector.AddClient(cl);
//             cl.Name = "test1changed";
//             cl.BlackListed = true;
//             cl.Comment = "testcomment Changed";
//             cl.Telephones.Remove("123321123");
//             cl.Telephones.Add("123123");
//             connector.UpdateClientData(cl);
//             connector.RemoveClient(cl);
//             var spec = entity.NewSpecialist();
//             spec.Name = "testspec1";
//             spec.NotWorking = false;
//             spec.Specialisations.Add("specialization1");
//             connector.AddSpecialist(spec);
//             spec.Name = "testspec1changed";
//             spec.NotWorking = true;
//             spec.Specialisations.Clear();
//             spec.Specialisations.Add("specialization2");
//             connector.UpdateSpecialistData(spec);
//             connector.RemoveSpecialist(spec);
            
        }
    }
}
