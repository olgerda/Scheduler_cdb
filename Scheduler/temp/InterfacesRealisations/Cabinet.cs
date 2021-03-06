﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesRealisations
{

    public class CabinetList : Scheduler_Forms_Interfaces.ICabinetList
    {
        List<Scheduler_Controls_Interfaces.ICabinet> list;

        public CabinetList()
        {
            list = new List<Scheduler_Controls_Interfaces.ICabinet>();
        }

        List<Scheduler_Controls_Interfaces.ICabinet> Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.ICabinet>.List
        {
            get { return list; }
        }
    }

    public class Cabinet: Scheduler_Controls_Interfaces.ICabinet
    {
        public UInt16 id;
        private string name;
        bool availability;

        public Cabinet()
        {
            name = String.Empty;
            availability = false;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            return name;// +available.ToString();
        }

        public override bool Equals(object obj)
        {
            Cabinet cab = obj as Cabinet;
            if (cab == null) return false;
            return name == cab.name;
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public bool Availability
        {
            get
            {
                return availability;
            }
            set
            {
                availability = value;
            }
        }
    }
}
