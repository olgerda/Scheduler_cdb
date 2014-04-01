using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesRealisations
{
    public class SpecializationList: Scheduler_Controls_Interfaces.ISpecializationList
    {
        private HashSet<string> list;

        public SpecializationList()
        {
            list = new HashSet<string>();
        }

        HashSet<string> Scheduler_Controls_Interfaces.ISpecializationList.SpecializationList
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }
    }
}
