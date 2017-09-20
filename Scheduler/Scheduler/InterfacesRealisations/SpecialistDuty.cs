using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    public class SpecialistDuty : ISpecialistDuty
    {
        public DateTime End { get; set; }

        public bool IAmChanged => false;

        public int ID { get; set; }

        public ISpecialist Specialist { get; set; }

        public DateTime Start { get; set; }

        public bool Supplimentary { get; set; }

        public int CompareTo(ISpecialistDuty other)
        {
            if (other == null)
                return -1; //any > null

            return Supplimentary ? //true/false
                other.Supplimentary ? 0 : -1 : //true == true, true > false
                other.Supplimentary ? 1 : 0; //false < true, false == false

        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as SpecialistDuty);
        }

        public bool Equals(IHaveID other)
        {
            if (other == null)
                return false;
            return ID == other.ID;
        }

        public override string ToString()
        {
            return Specialist?.Name ?? "DUTY: NAME NOT SPECIFIED";
        }
    }

    public class SpecialistDutyList : CommonList<ISpecialistDuty>, ISpecialistDutyList
    {
        public override IEntityList<ISpecialistDuty> Copy()
        {
            var a = new SpecialistDutyList();
            a.List.AddRange(List);
            return a;
        }
    }


}
