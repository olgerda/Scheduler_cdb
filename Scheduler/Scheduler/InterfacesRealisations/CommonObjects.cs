using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace Scheduler_InterfacesRealisations
{
    public abstract class CommonObjectWithNotify : Scheduler_Controls_Interfaces.IDummy, Scheduler_Controls_Interfaces.IHaveID, INotifyPropertyChanged, CalendarControl3_Interfaces.ICanCustomizeLook
    {
        //https://stackoverflow.com/questions/30141045/two-ways-databinding-in-winforms
        //https://stackoverflow.com/questions/1334815/how-to-bind-controls-two-properties-to-two-object-properties-properly
        bool iAmChanged = false;

        public void RaisePropertyChanged(string caller = null)
        {
            iAmChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
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

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }

        public Color ColorMain { get; set; }

        public Color ColorBorder { get; set; }

        public Color ColorBackground { get; set; }

        public Font Font { get; set; }

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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var test = obj as CommonObjectWithNotify;
            return test != null && this == test;
        }

        protected bool Equals(CommonObjectWithNotify other)
        {
            return id == other.id;
        }

        public override int GetHashCode()
        {
            return id;
        }

        //int Scheduler_Controls_Interfaces.IHaveID.ID
        //{
        //    get
        //    {
        //        return id;
        //    }
        //    set
        //    {
        //        id = value;
        //    }
        //}

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

    public abstract class CommonList<T> : Scheduler_Forms_Interfaces.IEntityList<T> where T : Scheduler_Controls_Interfaces.IDummy, Scheduler_Controls_Interfaces.IHaveID
    {
        private List<T> list;

        public event Scheduler_Forms_Interfaces.ItemAddedHandler OnItemAdded;
        public event Scheduler_Forms_Interfaces.ItemRemovedHandler OnItemRemoved;
        public event Scheduler_Forms_Interfaces.ItemChangedHandler OnItemChanged;

        protected CommonList()
        {
            list = new List<T>();
        }

        protected CommonList(CommonList<T> oldlist)
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
            {
                foreach (var item in list.Where(i => i.IAmChanged))
                {
                    if (item.ID == 0)
                        OnItemAdded(item);
                    else
                        OnItemChanged(item);
                }
            }
        }

        public abstract Scheduler_Forms_Interfaces.IEntityList<T> Copy();
    }

    public class ControlsColors : CalendarControl3_Interfaces.ICanCustomizeLook
    {
        public Color ColorBackground { get; set; }

        public Color ColorBorder { get; set; }

        public Color ColorMain { get; set; }

        public Font Font { get; set; }
    }

    public class SortableList<T> : BindingList<T>
    {
        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var modifier = direction == ListSortDirection.Ascending ? 1 : -1;
            if (prop.PropertyType.GetInterface("IComparable") != null)
            {
                var items = Items.ToList();
                items.Sort(new Comparison<T>((a, b) =>
                {
                    var aVal = prop.GetValue(a) as IComparable;
                    var bVal = prop.GetValue(b) as IComparable;
                    return aVal.CompareTo(bVal) * modifier;
                }));
                Items.Clear();
                foreach (var i in items)
                    Items.Add(i);
            }
        }
    }
}
