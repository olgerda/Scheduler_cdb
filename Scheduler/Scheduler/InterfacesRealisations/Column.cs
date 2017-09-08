using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Scheduler_InterfacesRealisations
{
    public class Column : Scheduler_DBobjects_Intefraces.IColumn
    {
        string name;
        List<CalendarControl3_Interfaces.IEntity2ControlInterface> entities;

        public Column()
        {
            name = String.Empty;
            entities = new List<CalendarControl3_Interfaces.IEntity2ControlInterface>();
        }

        void Scheduler_DBobjects_Intefraces.IColumn.AddEntity(Scheduler_DBobjects_Intefraces.IEntity entity)
        {
            if (entities.Any(e => e.IsIntersectWith(entity)))
                throw new Exception("Добавленная сущность пересекается с уже существующей.");
            entities.Add(entity);
        }

        string CalendarControl3_Interfaces.IColumn2ControlInterface.Name
        {
            get { return name; }
            set { name = value; }
        }

        List<CalendarControl3_Interfaces.IEntity2ControlInterface> CalendarControl3_Interfaces.IColumn2ControlInterface.Entities
        {
            get { return entities; }
        }


        public Color ColorMain => default(Color);

        public Color ColorBorder => default(Color);

        public Color ColorBackground => default(Color);

        public Font Font => null;
    }
}
