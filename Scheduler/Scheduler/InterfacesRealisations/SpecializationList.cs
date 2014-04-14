using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_InterfacesRealisations
{
    public class SpecializationList: Scheduler_Controls_Interfaces.ISpecializationList
    {
        private HashSet<string> list;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public event Scheduler_Forms_Interfaces.ItemAddedHandler OnItemAdded;

        public event Scheduler_Forms_Interfaces.ItemRemovedHandler OnItemRemoved;

        public SpecializationList()
        {
            list = new HashSet<string>();
            PropertyChanged += PropertyChangedHandler;
        }

        public SpecializationList(SpecializationList listToCopy)
        {
            list = new HashSet<string>(listToCopy.list);
            OnItemAdded += listToCopy.OnItemAdded;
            OnItemRemoved += listToCopy.OnItemRemoved;
            PropertyChanged += PropertyChangedHandler;
        }

        HashSet<string> Scheduler_Controls_Interfaces.ISpecializationList.SpecializationList
        {
            get
            {
                return list;
            }
            set
            {
                if (list != null && value != null)
                {
                    var listExceptValue = list.Except(value);
                    var ValueExceptList = value.Except(list);
                    if (OnItemRemoved != null)
                        foreach (var item in listExceptValue)
                            OnItemRemoved(item);
                    if (OnItemAdded != null)
                        foreach (var item in ValueExceptList)
                            OnItemAdded(item);
                }

                list = value;
            }
        }

        Scheduler_Controls_Interfaces.ISpecializationList Scheduler_Controls_Interfaces.ISpecializationList.Copy()
        {
            return new SpecializationList(this);
        }

        private void PropertyChangedHandler(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (o == null)
                return;
        }

    }
}
