using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Scheduler_InterfacesRealisations
{
    public abstract class CommonObjectWithNotify : Scheduler_Controls_Interfaces.IDummy
    {
        //         event System.ComponentModel.PropertyChangedEventHandler innerPropertyChanged;
        //         event System.ComponentModel.PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        //         {
        //             add { innerPropertyChanged = value; }
        //             remove { innerPropertyChanged = null; }
        //         }

        bool iAmChanged = false;



        public void RaisePropertyChanged(string caller)
        {
            iAmChanged = true;
            //             if (innerPropertyChanged == null)
            //                 return;
            // 
            //             innerPropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        public bool IAmChanged
        {
            get
            {
                bool result = iAmChanged;
                iAmChanged = false;
                return result;
            }
        }
    }

    public abstract class CommonList<T> : Scheduler_Forms_Interfaces.IEntityList<T> where T : Scheduler_Controls_Interfaces.IDummy
    {
        private List<T> list;

        public event Scheduler_Forms_Interfaces.ItemAddedHandler OnItemAdded;
        public event Scheduler_Forms_Interfaces.ItemRemovedHandler OnItemRemoved;
        public event Scheduler_Forms_Interfaces.ItemChangedHandler OnItemChanged;

        public CommonList()
        {
            list = new List<T>();
        }

        public CommonList(CommonList<T> oldlist)
        {
            list = new List<T>(oldlist.List);
            OnItemAdded += oldlist.OnItemAdded;
            OnItemRemoved += oldlist.OnItemRemoved;
            OnItemChanged += oldlist.OnItemChanged;
        }

        public List<T> List
        {
            get { return list; }
        }

        public void Add(T item)
        {
            if (!list.Contains(item))
            {
                if (OnItemAdded != null)
                    OnItemAdded(item);
                list.Add(item);
            }
        }

        public void Remove(T item)
        {
            if (list.Contains(item))
            {
                if (OnItemRemoved != null)
                    OnItemRemoved(item);
                list.Remove(item);
            }
        }

        public void ValidateAndUpdate()
        {
            if (OnItemChanged != null)
                foreach (var item in list.Where(i => i.IAmChanged))
                    OnItemChanged(item);
        }

        public abstract Scheduler_Forms_Interfaces.IEntityList<T> Copy();
    }
}
