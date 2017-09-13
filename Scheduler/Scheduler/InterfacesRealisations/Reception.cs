using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scheduler_Controls_Interfaces;
using System.Drawing;

namespace Scheduler_InterfacesRealisations
{
    public class Reception : CommonObjectWithNotify, Scheduler_DBobjects_Intefraces.IEntity
    {
        ITimeInterval receptionTimeInterval;
        IClient client;
        ISpecialist specialist;
        ICabinet cabinet;
        string specialisation;
        bool isRented;

        int price;

        static Scheduler_DBobjects_Intefraces.IMainDataBase database;

        //int id;

        public ITimeInterval ReceptionTimeInterval
        {
            get { return receptionTimeInterval; }
            set { receptionTimeInterval = value; RaisePropertyChanged("ReceptionTimeInterval"); }
        }

        public IClient Client
        {
            get { return client; }
            set { client = value; RaisePropertyChanged("Client"); }
        }

        public ISpecialist Specialist
        {
            get { return specialist; }

            set { specialist = value; RaisePropertyChanged("Specialist"); }
        }

        public ICabinet Cabinet
        {
            get { return cabinet; }
            set
            {
                cabinet = value;
                CommentOnlyReception = cabinet.CommentOnly;
                RaisePropertyChanged("Cabinet");
            }
        }

        public string Specialization
        {
            get { return specialisation; }
            set { specialisation = value; RaisePropertyChanged("Specialization"); }
        }

        public bool Rent
        {
            get { return isRented; }
            set { isRented = value; RaisePropertyChanged("Rent"); }
        }

        public string Validate()
        {
            string result = String.Empty;

            if (receptionTimeInterval == null)
                result += "Временной интервал не задан." + Environment.NewLine;
            if (!CommentOnlyReception)
            {
                if (specialist == null)
                    result += "Специалист не задан." + Environment.NewLine;

                if (isRented)
                {

                }
                else
                {
                    if (String.IsNullOrWhiteSpace(specialisation))
                        result += "Специализация не задана." + Environment.NewLine;
                    if (client == null)
                        result += "Клиент не задан." + Environment.NewLine;
                }
            }
            if (database != null)
            {
                List<Scheduler_DBobjects_Intefraces.IEntity> receptionsIntersectedWith = new List<Scheduler_DBobjects_Intefraces.IEntity>();
                foreach (var r in database.SelectReceptionsFromDate(this.receptionTimeInterval.Date)
                    .Where(r => r.ID != this.ID &&
                        r.Cabinet.ID == this.cabinet.ID &&
                        r.IsIntersectWith(this)))
                    receptionsIntersectedWith.Add(r);
                if (receptionsIntersectedWith.Count != 0)
                    result += "Время посещения пересекается с другими: " + Environment.NewLine;
                foreach (var r in receptionsIntersectedWith.Take(3))
                    result += r.DisplayString + Environment.NewLine;
            }

            return result == String.Empty ? null : result;
        }

        string CalendarControl3_Interfaces.IEntity2ControlInterface.StringToShow
        {
            get
            {
                List<string> result = new List<string>();
                result.Add(receptionTimeInterval.Interval());
                if (CommentOnlyReception)
                    result.Add(Comment);
                else
                {
                    result.Add(specialist.Name);
                    if (!isRented)
                    {
                        result.Add(client.Name);
                        result.Add((client.NeedSMS ? "SMS " : "") + client.Telephones.FirstOrDefault());
                    }
                    result.Add(price + " руб.");
                    if (!isRented)
                    {
                        result.Add(specialisation);
                    }
                }
                return String.Join(Environment.NewLine, result);
                //if (isRented)
                //{
                //    return String.Join(Environment.NewLine,
                //        receptionTimeInterval.Interval(),
                //        specialist.Name,
                //        price + " руб.");
                //}
                //else
                //{
                //    return String.Join(Environment.NewLine,
                //        receptionTimeInterval.Interval(),
                //        specialist.Name,
                //        client.Name,
                //        (client.NeedSMS ? "SMS " : "") + client.Telephones.FirstOrDefault(),
                //        price + " руб.",
                //        specialisation);
                //}
            }
        }

        int CalendarControl3_Interfaces.IEntity2ControlInterface.TopLevel
        {
            get { return Convert.ToInt32(Math.Truncate(receptionTimeInterval.StartDate.TimeOfDay.TotalMinutes)); }
        }

        int CalendarControl3_Interfaces.IEntity2ControlInterface.BottomLevel
        {
            get { return Convert.ToInt32(Math.Truncate(receptionTimeInterval.EndDate.TimeOfDay.TotalMinutes)); }
        }

        bool CalendarControl3_Interfaces.IEntity2ControlInterface.IsIntersectWith(CalendarControl3_Interfaces.IEntity2ControlInterface second)
        {
            CalendarControl3_Interfaces.IEntity2ControlInterface theThis = this as CalendarControl3_Interfaces.IEntity2ControlInterface;
            bool topIntersect = theThis.TopLevel > second.TopLevel && theThis.TopLevel < second.BottomLevel;
            bool bottomIntersect = theThis.BottomLevel < second.BottomLevel && theThis.BottomLevel > second.TopLevel;
            return topIntersect || bottomIntersect;
        }

        public string DisplayString
        {
            get { return String.Join(" ", receptionTimeInterval.Date.ToShortDateString(), receptionTimeInterval.Interval(), specialist.Name, specialisation, cabinet.Name); }
        }

        public override string ToString()
        {
            return ((IReception)this).DisplayString;
        }

        void Scheduler_DBobjects_Intefraces.IEntity.SetDatabase(Scheduler_DBobjects_Intefraces.IMainDataBase db)
        {
            if (database == null)
                database = db;
        }



        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                RaisePropertyChanged("Price");
            }
        }


        public void CommitToDatabase()
        {
            if (IAmChanged)
            {
                if (ID == 0)
                    database.AddReception(this);
                else
                    database.UpdateReception(this);
            }

        }

        private string _administrator;
        public string Administrator
        {
            get { return _administrator; }
            set
            {
                _administrator = value;
                RaisePropertyChanged();
            }
        }

        private bool _specialRent;
        public bool SpecialRent
        {
            get { return _specialRent; }
            set
            {
                _specialRent = value;
                RaisePropertyChanged();
            }
        }

        private bool _receptionDidNotTakePlace;
        public bool ReceptionDidNotTakePlace
        {
            get { return _receptionDidNotTakePlace; }

            set
            {
                _receptionDidNotTakePlace = value;
                RaisePropertyChanged();
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }

            set
            {
                _comment = value;
                RaisePropertyChanged();
            }
        }

        private bool _commentOnlyReception;
        public bool CommentOnlyReception
        {
            get
            {
                return _commentOnlyReception;

            }

            set
            {
                _commentOnlyReception = value;
                RaisePropertyChanged();
            }
        }
    }
}
