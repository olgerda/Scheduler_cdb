using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Scheduler_DBobjects_Intefraces;
using CalendarControl3_Interfaces;

namespace Scheduler_InterfacesRealisations
{
    public class Column : IColumn
    {
        string name;
        List<IEntity2ControlInterface> entities;

        public Column()
        {
            name = String.Empty;
            entities = new List<IEntity2ControlInterface>();
        }

        public void AddEntity(IEntity entity)
        {
            if (entities.Any(e => e.IsIntersectWith(entity)))
                throw new Exception("Добавленная сущность пересекается с уже существующей.");
            entities.Add(entity);
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<IEntity2ControlInterface> Entities
        {
            get { return entities; }
        }


        public Color ColorMain { get; set; }

        public Color ColorBorder { get; set; }

        public Color ColorBackground { get; set; }

        public Font Font { get; set; }

        public bool OnlyComment { get; set; }
    }
}
