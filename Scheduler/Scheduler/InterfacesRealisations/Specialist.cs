using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_InterfacesRealisations
{
    public class Specialist : CommonObjectWithNotify, Scheduler_Controls_Interfaces.ISpecialist
    {
        string name;
        HashSet<string> specialisations;
        bool notworking;

        static Scheduler_Controls_Interfaces.GetCosts getCostsFunction;

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
                RaisePropertyChanged("NotWorking");
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
                RaisePropertyChanged("Specialisations");
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
                RaisePropertyChanged("Name");
            }
        }

        public override string ToString()
        {
            return name;
        }

        Dictionary<int, int> Scheduler_Controls_Interfaces.ISpecialist.GetCosts()
        {
            return getCostsFunction == null ? new Dictionary<int, int>() : getCostsFunction(this);
        }

        void Scheduler_Controls_Interfaces.ISpecialist.CostsFunction(Scheduler_Controls_Interfaces.GetCosts func)
        {
            if (getCostsFunction == null)
                getCostsFunction = func;
        }
    }

    public class SpecialistList : CommonList<Scheduler_Controls_Interfaces.ISpecialist>, Scheduler_Forms_Interfaces.ISpecialistList
    {

        public SpecialistList()
            : base()
        {
        }

        SpecialistList(SpecialistList old)
            : base(old)
        {
        }

        Scheduler_Controls_Interfaces.ISpecialist Scheduler_Forms_Interfaces.ISpecialistList.FindSpecialistByPartialName(string partialName)
        {
            Scheduler_Controls_Interfaces.ISpecialist result;
            result = this.List.FirstOrDefault(s => s.Name.StartsWith(partialName));
            return result;
        }

        public override Scheduler_Forms_Interfaces.IEntityList<Scheduler_Controls_Interfaces.ISpecialist> Copy()
        {
            return new SpecialistList(this);
        }
    }
}
