using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Scheduler_InterfacesRealisations
{
    public abstract class CommonObjectWithNotify : Scheduler_Controls_Interfaces.IDummy
    {
        event System.ComponentModel.PropertyChangedEventHandler innerPropertyChanged;
        event System.ComponentModel.PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        {
            add { innerPropertyChanged = value; }
            remove { innerPropertyChanged = null; }
        }

        public void RaisePropertyChanged(string caller)
        {
            if (innerPropertyChanged == null)
                return;

            innerPropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }

    public abstract class CommonList<T> : Scheduler_Forms_Interfaces.IEntityList<T> where T : Scheduler_Controls_Interfaces.IDummy
    {
        private List<T> list;

        public CommonList()
        {
            list = new List<T>();
        }

        public CommonList(CommonList<T> oldlist)
        {
            list = new List<T>(oldlist.List);
        }

        public List<T> List
        {
            get { return list; }
        }

        public void Add(T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
                if (onItemAdded != null)
                    onItemAdded(item);
            }
        }

        public void Remove(T item)
        {
            if (list.Contains(item))
            {
                list.Remove(item);
                if (onItemRemoved != null)
                    onItemRemoved(item);
            }
        }

        event Scheduler_Forms_Interfaces.ItemAddedHandler onItemAdded;

        event Scheduler_Forms_Interfaces.ItemRemovedHandler onItemRemoved;

        public abstract Scheduler_Forms_Interfaces.IEntityList<T> Copy();

        event Scheduler_Forms_Interfaces.ItemAddedHandler Scheduler_Forms_Interfaces.IEntityList<T>.OnItemAdded
        {
            add { onItemAdded += value; }
            remove { onItemAdded -= value; }
        }

        event Scheduler_Forms_Interfaces.ItemRemovedHandler Scheduler_Forms_Interfaces.IEntityList<T>.OnItemRemoved
        {
            add { onItemRemoved += value; }
            remove { onItemRemoved -= value; }
        }
    }
}
