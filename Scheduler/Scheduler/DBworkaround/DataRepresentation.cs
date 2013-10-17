using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class TableOfEntities : CalendarControl3.ITable2ControlInterface
    {

        public List<CalendarControl3.IColumn2ControlInterface> columns;
        public int minValue;
        public int maxValue;
        public Dictionary<int, string> descriptions;

        public TimeInterval workTime;

        public List<DateTime> descs;

        public TableOfEntities()
        {
            columns = new List<CalendarControl3.IColumn2ControlInterface>();
            descriptions = new Dictionary<int, string>();
            descs = new List<DateTime>();
            minValue = 0;
            maxValue = 1;
        }

        public int GetColumnCount()
        {
            return columns.Count;
        }

        public int GetMinValue()
        {
            if (workTime != null)
            {
                return DateTimeConvert.TimeToInt32(workTime.StartDate);
            }
            return minValue;
        }

        public int GetMaxValue()
        {
            if (workTime != null)
                return DateTimeConvert.TimeToInt32(workTime.EndDate);

            return maxValue;
        }

        public Dictionary<int, string> GetDescripptionsToValueLevels()
        {
            if (descs.Count == 0)
                return descriptions;
            else
                return descs.ToDictionary(d => DateTimeConvert.TimeToInt32(d), d => d.TimeOfDay.ToString("hh\\:mm"));
        }

        public List<CalendarControl3.IColumn2ControlInterface> GetColumns()
        {
            return columns;
        }
    }

    public class ColumnOfEntities : CalendarControl3.IColumn2ControlInterface
    {
        String name;
        List<CalendarControl3.IEntity2ControlInterface> receptions;

        public ColumnOfEntities(string cabName)
        {
            name = cabName;
            receptions = new List<CalendarControl3.IEntity2ControlInterface>();
        }

        public void AddEntities(List<Entity> list)
        {
            foreach (var ent in list)
            {
                if (ent.cabinet.Name == name) receptions.Add(ent);
            }
        }

        public void AddEntity(Entity ent)
        {
            if (ent.cabinet.Name == name && !receptions.Contains(ent)) receptions.Add(ent);
        }

        public void RemoveEntity(Entity ent)
        {
            if (ent.cabinet.Name == name) 
            {
                //var finded = receptions.Find(e => ent.Equals((Entity)e.GetObject()));
                var finded = receptions.Find(e => e.GetID() == ent.id);
                if (finded == null) return;
                receptions.Remove(finded);
            }
        }

        public void RemoveEntity(ulong id)
        {
            var finded = receptions.Find(e => e.GetID() == id);
            if (finded == null) return;
            receptions.Remove(finded);
        }

        public string GetName()
        {
            return name;
        }

        public List<CalendarControl3.IEntity2ControlInterface> GetEntities()
        {
            return receptions;
        }
    }

    public class Entity : ReceptionCard, CalendarControl3.IEntity2ControlInterface
    {
        public Entity(ulong Id = 0, TimeInterval Date = null, ClientCard Client = null, SpecialistCard Specialist = null, Specialization Specialization = null, CabinetCard Cabinet = null)
            : base(Id, Date, Client, Specialist, Specialization, Cabinet)
        {

        }
        
        public string StringToShow()
        {
            if (this.client.comment.StartsWith("АРЕНДА"))
                return date.Interval() + "\r\n" + specialist + "\r\n" + "АРЕНДА";
            else
                return date.Interval() + "\r\n" + specialist + "\r\n" + specialization + "\r\n" + client;
        }

        public int TopLevel()
        {
            return DateTimeConvert.TimeToInt32(date.StartDate);
        }

        public int BottomLevel()
        {
            return DateTimeConvert.TimeToInt32(date.EndDate);
        }

        public object GetObject()
        {
            return this;
        }


        public ulong GetID()
        {
            return id;
        }
    }

    public static class DateTimeConvert
    {
        public static int TimeToInt32(DateTime input)
        {
            var temp = input - input.Date;
            return (int)Math.Floor(temp.TotalMinutes);
        }
    }
}
