using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Scheduler_Controls_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    public class Client : CommonObjectWithNotify, IClient
    {
        struct GeneralParams
        {
            public TimeSpan GenerallyTime;
            public int GenerallyPrice;
        }

        string comment;
        bool blacklisted;
        HashSet<string> telephones;
        string name;
        string administrator;

        GeneralParams generalParams;
        private string _eMail;
        private bool _needSms;
        private int _balance;
        private int _clientType;
        private bool _active;

        static GetClientReceptionsList getreceptions;


        public Client()
        {
            name = String.Empty;
            comment = String.Empty;
            telephones = new HashSet<string>();
            blacklisted = false;
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
                RaisePropertyChanged("Comment");
            }
        }

        public bool BlackListed
        {
            get
            {
                return blacklisted;
            }
            set
            {
                blacklisted = value;
                RaisePropertyChanged("Blacklisted");
            }
        }

        public HashSet<string> Telephones
        {
            get
            {
                return telephones;
            }
            set
            {
                telephones = value;
                RaisePropertyChanged("Telephones");
            }
        }

        public bool CheckTelephone(string telNumber)
        {
            return telephones.Contains(telNumber);
        }


        public List<IReception> GetReceptions()
        {
            if (getreceptions != null)
                return getreceptions(this);
            else
                return new List<IReception>();
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public void ReceptionListFuncition(GetClientReceptionsList func)
        {
            if (getreceptions == null)
                getreceptions = func;
        }

        public object Clone()
        {
            return new Client()
            {
                administrator = this.administrator,
                blacklisted = blacklisted,
                comment = comment,
                generalParams = generalParams,
                name = name,
                telephones = new HashSet<string>(telephones),
                Balance = Balance,
                NeedSMS = NeedSMS,
                EMail = EMail
            };
        }

        public TimeSpan GenerallyTime
        {
            get
            {
                return generalParams.GenerallyTime;
            }
            set
            {
                generalParams = new GeneralParams() { GenerallyTime = value, GenerallyPrice = generalParams.GenerallyPrice };
            }
        }

        public int GenerallyPrice
        {
            get
            {
                return generalParams.GenerallyPrice;
            }
            set
            {
                generalParams = new GeneralParams() { GenerallyTime = generalParams.GenerallyTime, GenerallyPrice = value };
            }
        }

        public string Administrator
        {
            get
            {
                return administrator ?? String.Empty;
            }

            set
            {
                administrator = value;
            }
        }

        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public bool NeedSMS
        {
            get { return _needSms; }
            set { _needSms = value; }
        }

        public string EMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }

        public int ClientType
        {
            get { return _clientType; }
            set { _clientType = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public override string ToString()
        {
            return Name + "*" + BlackListed + "*" + Active;
        }
    }

    public class ClientList : CommonList<IClient>, Scheduler_Forms_Interfaces.IClientList
    {

        public ClientList()
            : base()
        {
        }

        ClientList(ClientList old)
            : base(old)
        {
        }

        IClient Scheduler_Forms_Interfaces.IClientList.FindClientByPartialName(string partialName)
        {
            IClient result;
            result = this.List.FirstOrDefault(c => c.Name.StartsWith(partialName));
            return result;
        }

        IClient Scheduler_Forms_Interfaces.IClientList.FindClientByTelephone(string Tel, bool partial)
        {
            IClient result;
            result = partial ? this.List.FirstOrDefault(c => c.Telephones.FirstOrDefault(t => t.StartsWith(Tel)) != default(string)) :
                this.List.FirstOrDefault(c => c.Telephones.FirstOrDefault(t => t == Tel) != default(string));
            return result;
        }

        public override Scheduler_Forms_Interfaces.IEntityList<IClient> Copy()
        {
            return new ClientList(this);
        }
    }
}
