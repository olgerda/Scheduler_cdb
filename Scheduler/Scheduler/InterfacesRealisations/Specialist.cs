using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesRealisations
{
    public class Specialist : Scheduler_Controls_Interfaces.ISpecialist
    {
        string name;
        HashSet<string> specialisations;
        bool notworking;

        int id;

        public Specialist()
        {
            name = String.Empty;
            notworking = false;
            specialisations = new HashSet<string>();
        }

        bool Scheduler_Controls_Interfaces.ISpecialist.NotWorking
        {
            get
            {
                return notworking;
            }
            set
            {
                notworking = value;
            }
        }

        HashSet<string> Scheduler_Controls_Interfaces.ISpecialist.Specialisations
        {
            get
            {
                return specialisations;
            }
            set
            {
                specialisations = value;
            }
        }

        string Scheduler_Controls_Interfaces.INamedEntity.Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        string Scheduler_Controls_Interfaces.INamedEntity.ToString()
        {
            return name;
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
    }

    public class SpecialistList : Scheduler_Forms_Interfaces.ISpecialistList
    {
        List<Scheduler_Controls_Interfaces.ISpecialist> list;

        public SpecialistList()
        {
            list = new List<Scheduler_Controls_Interfaces.ISpecialist>();
        }

        SpecialistList(SpecialistList old)
        {
            list = new List<Scheduler_Controls_Interfaces.ISpecialist>(old.list);
        }

        Scheduler_Controls_Interfaces.ISpecialist Scheduler_Forms_Interfaces.ISpecialistList.FindSpecialistByPartialName(string partialName)
        {
            Scheduler_Controls_Interfaces.ISpecialist result;
            result = list.FirstOrDefault(s => s.Name.StartsWith(partialName));
            return result;
        }

        List<Scheduler_Controls_Interfaces.ISpecialist> Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.ISpecialist>.List
        {
            get { return list; }
        }

        Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.ISpecialist> Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.ISpecialist>.Copy()
        {
            return new SpecialistList(this);
        }
    }
}
