using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using Scheduler_Controls_Interfaces;
using CalendarControl3_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    public abstract class CommonObjectWithNotify : ControlsColors, Scheduler_Controls_Interfaces.IDummy, Scheduler_Controls_Interfaces.IHaveID, INotifyPropertyChanged
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

    public abstract class CommonDuty : Scheduler_Controls_Interfaces.IDuty
    {
        public DateTime End { get; set; }

        public bool IAmChanged => false;

        public int ID { get; set; }

        public ICanNotWork Named { get; set; }

        public DateTime Start { get; set; }

        public bool Supplimentary { get; set; }

        public int CompareTo(IDuty other)
        {
            if (other == null)
                return -1; //any > null

            return Supplimentary ? //true/false
                other.Supplimentary ? 0 : -1 : //true == true, true > false
                other.Supplimentary ? 1 : 0; //false < true, false == false
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as CommonDuty);
        }

        public bool Equals(IHaveID other)
        {
            if (other == null)
                return false;
            return ID == other.ID;
        }

        public override string ToString()
        {
            return Named?.Name ?? "DUTY: NAME NOT SPECIFIED";
        }
    }

    public class ColorPalette : IColorPalette
    {
        private Dictionary<ColorPaletteSelectables, Color> _colors;
        private Dictionary<ColorPaletteSelectables, string> _brushNames;
        private List<ColorPaletteSelectables> _activeColorChangers;

        public Font Font { get; set; }

        public Dictionary<ColorPaletteSelectables, Color> Colors
        {
            get { return _colors; }

            set { _colors = value; }
        }

        public Dictionary<ColorPaletteSelectables, string> BrushNames
        {
            get { return _brushNames; }

            set { _brushNames = value; }
        }

        public List<ColorPaletteSelectables> ActiveColorChangers
        {
            get { return _activeColorChangers; }

            set { _activeColorChangers = value; }
        }

        public ColorPalette()
        {
            _brushNames = new Dictionary<ColorPaletteSelectables, string>();
            _colors = new Dictionary<ColorPaletteSelectables, Color>();
            _activeColorChangers = new List<ColorPaletteSelectables>();
            foreach (ColorPaletteSelectables cps in Enum.GetValues(typeof(ColorPaletteSelectables)))
            {
                _brushNames.Add(cps, "Solid");
                _colors.Add(cps, Color.White);
                _activeColorChangers.Add(cps);
            }

            Font = new Font("Arial", 10);
        }

        public object Clone()
        {
            var a = new ColorPalette() { Font = Font };
            a.ActiveColorChangers = new List<ColorPaletteSelectables>(_activeColorChangers);
            a._brushNames = new Dictionary<ColorPaletteSelectables, string>(_brushNames);
            a._colors = new Dictionary<ColorPaletteSelectables, Color>(_colors);
            a.Font = Font;
            return a;
        }

        public Color GetColor(ColorPaletteSelectables selectable)
        {
            return _colors[selectable];
        }

        public string GetBrushName(ColorPaletteSelectables selectable)
        {
            return _brushNames[selectable];
        }

        public void SetColor(ColorPaletteSelectables selectable, Color color)
        {
            _colors[selectable] = color;
        }

        public void SetBrushName(ColorPaletteSelectables selectable, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;
            _brushNames[selectable] = name;
        }

        public Brush GetBrush(ColorPaletteSelectables selectable)
        {
            var name = GetBrushName(selectable);
            if (name == "Solid")
                return new SolidBrush(GetColor(selectable));
            else
                return new System.Drawing.Drawing2D.HatchBrush(
                    (System.Drawing.Drawing2D.HatchStyle)Enum.Parse(typeof(System.Drawing.Drawing2D.HatchStyle), name), GetColor(selectable));
        }
    }

    public class ControlsColors : ICanCustomizeLook
    {
        public ControlsColors()
        {
            Coloring = new ColorPalette();
        }
        public IColorPalette Coloring { get; set; }

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
