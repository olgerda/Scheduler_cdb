using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesRealisations
{
    public class Client : Scheduler_Controls_Interfaces.IClient
    {
        string comment;
        bool blacklisted;
        HashSet<string> telephones;
        string name;
        //public delegate List<Scheduler_Controls_Interfaces.IReception> GetReceptions(Scheduler_Controls_Interfaces.IClient client);

        Scheduler_Controls_Interfaces.GetClientReceptionsList getreceptions;

        int id;

        public Client()
        {
            name = String.Empty;
            comment = String.Empty;
            telephones = new HashSet<string>();
            blacklisted = false;
            getreceptions = null;
        }

        string Scheduler_Controls_Interfaces.IClient.Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }

        bool Scheduler_Controls_Interfaces.IClient.BlackListed
        {
            get
            {
                return blacklisted;
            }
            set
            {
                blacklisted = value;
            }
        }

        HashSet<string> Scheduler_Controls_Interfaces.IClient.Telephones
        {
            get
            {
                return telephones;
            }
            set
            {
                telephones = value;
            }
        }

        bool Scheduler_Controls_Interfaces.IClient.CheckTelephone(string telNumber)
        {
            return telephones.Contains(telNumber);
        }

        //         GetReceptions SetReceptionListFuncition
        //         {
        //             set { getreceptions = value; }
        //         }

        List<Scheduler_Controls_Interfaces.IReception> Scheduler_Controls_Interfaces.IClient.Receptions
        {
            get
            {
                if (getreceptions != null)
                    return getreceptions(this);
                else
                    return new List<Scheduler_Controls_Interfaces.IReception>();
            }
        }

        string Scheduler_Controls_Interfaces.INamedEntity.Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        string Scheduler_Controls_Interfaces.INamedEntity.ToString()
        {
            return name;
        }

        int Scheduler_Controls_Interfaces.IHaveID.ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }


        void Scheduler_Controls_Interfaces.IClient.ReceptionListFuncition(Scheduler_Controls_Interfaces.GetClientReceptionsList func)
        {
            if (getreceptions == null)
                getreceptions = func;
        }
    }

    public class ClientList : Scheduler_Forms_Interfaces.IClientList
    {
        List<Scheduler_Controls_Interfaces.IClient> list;

        public ClientList()
        {
            list = new List<Scheduler_Controls_Interfaces.IClient>();
        }

        ClientList(ClientList old)
        {
            list = new List<Scheduler_Controls_Interfaces.IClient>(old.list);
        }

        Scheduler_Controls_Interfaces.IClient Scheduler_Forms_Interfaces.IClientList.FindClientByPartialName(string partialName)
        {
            Scheduler_Controls_Interfaces.IClient result;
            result = list.FirstOrDefault(c => c.Name.StartsWith(partialName));
            return result;
        }

        Scheduler_Controls_Interfaces.IClient Scheduler_Forms_Interfaces.IClientList.FindClientByPartialTelephone(string partialName)
        {
            Scheduler_Controls_Interfaces.IClient result;
            result = list.FirstOrDefault(c => c.Telephones.FirstOrDefault(t => t.StartsWith(partialName)) != default(string));
            return result;
        }

        List<Scheduler_Controls_Interfaces.IClient> Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.IClient>.List
        {
            get { return list; }
        }

        Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.IClient> Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.IClient>.Copy()
        {
            return new ClientList(this);
        }
    }
}
