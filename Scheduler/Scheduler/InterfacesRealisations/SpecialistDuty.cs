using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler_Controls_Interfaces;
using Scheduler_Forms_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    public class SpecialistDuty : CommonDuty, ISpecialistDuty
    {
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
