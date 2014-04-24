using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_InterfacesRealisations
{
//     public class TimeInterval: Scheduler_Controls_Interfaces.ITimeInterval
//     {
//         private TimeIntervalInternal timeInterval;
// 
//         public TimeInterval(DateTime start, DateTime end)
//         {
//             timeInterval = new TimeIntervalInternal(start, end);
//         }
// 
//         public void Scheduler_Controls_Interfaces.ITimeInterval.SetStartEnd(DateTime startDate, DateTime endDate)
//         {
//             timeInterval.StartDate = startDate;
//             timeInterval.EndDate = endDate;
//         }
// 
//         public DateTime Scheduler_Controls_Interfaces.ITimeInterval.Date
//         {
//             get { return timeInterval.Date; }
//         }
// 
//         public DateTime Scheduler_Controls_Interfaces.ITimeInterval.StartDate
//         {
//             get
//             {
//                 return timeInterval.StartDate;
//             }
//             set
//             {
//                 timeInterval.StartDate = value;
//             }
//         }
// 
//         public DateTime Scheduler_Controls_Interfaces.ITimeInterval.EndDate
//         {
//             get
//             {
//                 return timeInterval.EndDate;
//             }
//             set
//             {
//                 timeInterval.EndDate = value;
//             }
//         }
//     }

        /// <summary>
    /// Класс представляет временной интервал между двумя датами.
    /// </summary>
    class TimeInterval: Scheduler_Controls_Interfaces.ITimeInterval
    {
        private DateTime startDate;
        private DateTime endDate;

        /// <summary>
        /// Длительность интервала времени.
        /// </summary>
        public TimeSpan Duration
        {
            get 
            {
                var nulDate = new TimeSpan(0);
                TimeSpan delta;

                if (startDate != default(DateTime) &&
                    endDate != default(DateTime) &&
                    ((delta = endDate - startDate) >= nulDate))
                    return delta;

                return nulDate;
            }
        }

        public DateTime Date
        {
            get { return startDate.Date;}
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
//                 if (value == default(DateTime)) 
//                     throw new DateSetException(this, value, "Set to default(DateTime) happen.");
//                 if (endDate != default(DateTime) && value > endDate) 
//                     throw new DateSetException(this, value, "Set StartDate after EndDate happen.");
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
//                 if (value == default(DateTime)) 
//                     throw new DateSetException(this, value, "Set to default(DateTime) happen.");
//                 if (startDate != default(DateTime) && value < startDate) 
//                     throw new DateSetException(this, value, "Set EndDate before StartDate happen.");
                endDate = value;
            }
        }

        /// <summary>
        /// Создать временной интервал между датой start и датой end. Если start > end, то start = end. Если переданное значение null - создаётся интервал по-умолчанию (0-0).
        /// </summary>
        public TimeInterval(DateTime start, DateTime end)
        {
            if (start == default(DateTime)) start = new DateTime(0);
            if (end == default(DateTime)) end = new DateTime(0);
            if (start > end) 
            {
                endDate = startDate = start;
            }
            else
            {
                startDate = start;
                endDate = end;
            }
            
        }

        public TimeInterval()
        {
            startDate = endDate = new DateTime(0);
        }

        /// <summary>
        /// Получить временной интервал в виду "ЧЧ:ММ - ЧЧ:ММ".
        /// </summary>
        public string Interval()
        {
            return startDate.TimeOfDay.ToString("hh\\:mm") + " - " + endDate.TimeOfDay.ToString("hh\\:mm");
        }

        public override string ToString()
        {
            return "Start at: " + startDate.ToString("g") + ". End at: " + endDate.ToString("g") + ". Duration is " + Duration.ToString("t");
        }

        /// <summary>
        /// Класс, описывающий ошибку задания времени в интервале.
        /// </summary>
        public class DateSetException : Exception
        {
            private TimeInterval timeInterval;
            private DateTime valueToSet;

            public TimeInterval Time_Interval
            {
                get { return timeInterval; }
            }

            public DateTime ValueToSet
            {
                get { return valueToSet; }
            }

            public DateSetException(TimeInterval obj, DateTime value, string msg)
                : base(msg)
            {
                timeInterval = obj;
                valueToSet = value;
            }

        }

        /// <summary>
        /// Пересекаются ли 2 интервала.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool IsIntersect(TimeInterval t1, TimeInterval t2)
        {
            if (t1.startDate > t2.startDate && t1.startDate < t2.endDate) return true;
            if (t1.endDate < t2.endDate && t1.endDate > t2.startDate) return true;
            return false;
        }


        public override bool Equals(object obj)
        {
            TimeInterval date = obj as TimeInterval;
            if (date == null) return false;
            return date.startDate == startDate && date.endDate == endDate;
        }

        public override int GetHashCode()
        {
            return startDate.GetHashCode() ^ endDate.GetHashCode();
        }

        public void SetStartEnd(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

}
