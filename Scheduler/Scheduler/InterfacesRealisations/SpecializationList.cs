using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_InterfacesRealisations
{
    public class SpecializationList: Scheduler_Controls_Interfaces.ISpecializationList
    {
        private HashSet<string> list;

        public SpecializationList()
        {
            list = new HashSet<string>();
        }

        public SpecializationList(HashSet<string> listToCopy)
        {
            list = new HashSet<string>(listToCopy);
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

        Scheduler_Controls_Interfaces.ISpecializationList Scheduler_Controls_Interfaces.ISpecializationList.Copy()
        {
            return new SpecializationList(list);
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
