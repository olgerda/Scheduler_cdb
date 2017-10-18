using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    class Administrator : CommonObjectWithNotify, IAdministrator
    {
        private string _name;
        private bool _notWorking;

        public string Name
        {
            get { return _name; }
            set
            {
                RaisePropertyChanged("Name"); _name = value;
            }
        }

        public bool NotWorking
        {
            get { return _notWorking; }
            set
            {
                RaisePropertyChanged("NotWorking"); _notWorking = value;
            }
        }

        public object Clone()
        {
            return new Administrator() { Name = Name, NotWorking = NotWorking, Coloring = (ColorPalette)Coloring.Clone() };
        }
    }

    public class AdministratorList : CommonList<Scheduler_Controls_Interfaces.IAdministrator>,
        Scheduler_Forms_Interfaces.IAdministratorList
    {
        public override IEntityList<IAdministrator> Copy()
        {
            var a = new AdministratorList();
            a.List.AddRange(List);
            return a;
        }
    }

    public class AdministratorDuty : CommonDuty, IAdministratorDuty
    {

    }

    public class AdministratorDutyList : CommonList<IAdministratorDuty>, IAdministratorDutyList
    {
        public override IEntityList<IAdministratorDuty> Copy()
        {
            var a = new AdministratorDutyList();
            a.List.AddRange(List);
            return a;
        }
    }
}
