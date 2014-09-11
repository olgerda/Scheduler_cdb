using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Scheduler_InterfacesRealisations
{
    public abstract class CommonObjectWithNotify : Scheduler_Controls_Interfaces.IDummy, Scheduler_Controls_Interfaces.IHaveID
    {
        bool iAmChanged = false;

        public void RaisePropertyChanged(string caller)
        {
            iAmChanged = true;
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

        int id = 0;

        public int ID
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

        public static bool operator ==(CommonObjectWithNotify a, CommonObjectWithNotify b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.ID == b.ID;
        }

        public static bool operator !=(CommonObjectWithNotify a, CommonObjectWithNotify b)
        {
            return !(a == b);
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

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            CommonObjectWithNotify other = obj as CommonObjectWithNotify;

            if (other == null)
                return 1;

            return (this as Scheduler_Controls_Interfaces.IHaveID).ID.CompareTo((other as Scheduler_Controls_Interfaces.IHaveID).ID);
        }

        bool IEquatable<Scheduler_Controls_Interfaces.IHaveID>.Equals(Scheduler_Controls_Interfaces.IHaveID other)
        {
            if (other == null)
                return false;
            return (this as Scheduler_Controls_Interfaces.IHaveID).ID == (other as Scheduler_Controls_Interfaces.IHaveID).ID;
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
