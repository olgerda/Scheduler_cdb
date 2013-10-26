using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    /// <summary>
    /// Класс реализует карточку клиента с простейшим функцилналом.
    /// </summary>
    public class ClientCard
    {
        private string name;

        public ClientCard(string newName, List<string> TelNumber = null, string newComment = "", bool inRed = false, UInt16 Id = 0)
        {
            name = newName;
            telNumber = TelNumber;
            comment = newComment;
            inRedList = inRed;
            id = Id;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public UInt16 id;
        private List<string> telNumber;

        public List<string> TelNumbers
        {
            get
            {
                if (telNumber == null) telNumber = new List<string>() { "+0(000)0000000" };
                return telNumber;
            }
            set
            {
                telNumber = value;
            }
        }

        public string comment;
        public bool inRedList;

        public override string ToString()
        {
            return name.ToString() + " Телефон: " + TelNumbers[0];
        }

        public override bool Equals(object obj)
        {
            ClientCard clnt = obj as ClientCard;
            if (clnt == null) return false;
            return name == clnt.name && TelNumbers[0] == clnt.TelNumbers[0];
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ telNumber.GetHashCode();
        }
    }

    /// <summary>
    /// Класс реализует карточку специалиста с простейшим функционалом.
    /// </summary>
    public class SpecialistCard
    {
        public UInt16 id;
        private string name;
        private List<string> specializations;

        public SpecialistCard(string name, UInt16 Id = 0)
        {
            id = Id;
            this.name = name;
            specializations = new List<string>();
        }

        public SpecialistCard(string name, string spec, UInt16 Id = 0)
        {
            id = Id;
            this.name = name;
            specializations = new List<string>() { spec };
        }

        public SpecialistCard(string name, List<string> specs, UInt16 Id = 0)
        {
            id = Id;
            this.name = name;
            specializations = new List<string>(specs);
        }

        public void AddSpecialization(string spec)
        {
            specializations.Add(spec);
        }

        public void RemoveSpecialization(string spec)
        {
            specializations.Remove(spec);
        }

        public List<string> Specializations
        {
            get { return specializations; }
            set { specializations = value; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public override string ToString()
        {
//             string result = name.ToString() + " has specialization: ";
//             foreach (var s in specialization)
//             {
//                 result += s.ToString() + " ";
//             }
            return name.ToString();//result;
        }

        public override bool Equals(object obj)
        {
            SpecialistCard spec = obj as SpecialistCard;
            if (spec == null) return false;
            foreach (var sp in specializations)
            {
                if (!spec.specializations.Contains(sp)) return false;
            }
            return spec.name == name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }

    /// <summary>
    /// Класс реализует основную сущность - один приём, который имеет свой номер, дату и другие параметры.
    /// </summary>
    public class ReceptionCard
    {
        public ulong id;
        public TimeInterval date;
        public ClientCard client;
        public SpecialistCard specialist;
        public string specialization;
        public CabinetCard cabinet;

        public ReceptionCard(ulong Id = 0, TimeInterval Date = null, ClientCard Client = null, SpecialistCard Specialist = null, string Specialization = null, CabinetCard Cabinet = null)
        {
            id = Id;
            date = Date;
            client = Client;
            specialist = Specialist;
            specialization = Specialization;
            cabinet = Cabinet;
        }

        public override string ToString()
        {
            return "Client " + client + " on Specialist " + specialist + ". " + specialization + " in cabinet " + cabinet + "." + date;
        }

        public override bool Equals(object obj)
        {
            ReceptionCard rcpt = obj as ReceptionCard;
            if (rcpt == null) return false;
            return date == rcpt.date && client == rcpt.client && specialist == rcpt.specialist && specialization == rcpt.specialization && cabinet == rcpt.cabinet;
        }

        public override int GetHashCode()
        {
            return date.GetHashCode() ^ client.GetHashCode() ^ specialization.GetHashCode() ^ specialist.GetHashCode() ^ cabinet.GetHashCode();
        }

    }

    /// <summary>
    /// Класс реализует сущность кабинета в простейшем виде. Служит для упрощения и структуризации.
    /// </summary>
    public class CabinetCard
    {
        public UInt16 id;
        private string name;

        public CabinetCard(string name, UInt16 Id = 0)
        {
            id = Id;
            if (String.IsNullOrWhiteSpace(name))
                name = "default cab";
                
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //public bool available = true;

        public override string ToString()
        {
            return name;// +available.ToString();
        }

        public override bool Equals(object obj)
        {
            CabinetCard cab = obj as CabinetCard;
            if (cab == null) return false;
            return name == cab.name;
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }

    #region Класс FIO - необходимость использования - под вопросом. Убрал покуда.
    /*
    /// <summary>
    /// Класс реализует сущность ФИО в простейшем виде. Служит для упрощения и структуризации.
    /// </summary>
    public class FIO
    {
        public UInt16 id;
        public string surname;
        public string name;
        public string patronymic;

        public FIO(string Name, string Surname, string Patronimyc, UInt16 Id = 0)
        {
            id = Id;
            name = Name.Trim();
            surname = Surname.Trim();
            patronymic = Patronimyc.Trim();
        }

        /// <summary>
        /// Фамилия Имя Отчество с разделителем запятой
        /// </summary>
        /// <param name="fullname"></param>
        public FIO(string fullname, UInt16 Id = 0)
        {
            id = Id;
            string[] fio = fullname.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            switch (fio.Length)
            {
                case 1:
                    surname = fio[0].Trim();
                    name = "НЕ ЗАДАНО";
                    patronymic = "НЕ ЗАДАНО";
                    break;
                case 2:
                    surname = fio[0].Trim();
                    name = fio[1].Trim();
                    patronymic = "НЕ ЗАДАНО";
                    break;
                case 3:
                    surname = fio[0].Trim();
                    name = fio[1].Trim();
                    patronymic = fio[2].Trim();
                    break;
                default:
                    surname = "НЕ ЗАДАНО";
                    name = "НЕ ЗАДАНО";
                    patronymic = "НЕ ЗАДАНО";
                    break;
            }

        }

        public override string ToString()
        {
            return surname + "," + name + "," + patronymic;
        }

        public override bool Equals(object obj)
        {
            FIO fio = obj as FIO;
            if (fio == null)
            {
                string tmp = obj as string;
                if (tmp == null) return false;
                fio = new FIO(tmp);
            }
            return name == fio.name && surname == fio.surname && patronymic == fio.patronymic;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ surname.GetHashCode() ^ patronymic.GetHashCode();
        }


    }
    */

    #endregion
    #region Класс Specialization - необходимость использования - под вопросом. Убрал покуда. Функционал перенесён в таблицу многие-ко-многим БД
    /*
    /// <summary>
    /// Класс реализует дополнительный функционал, который может быть использован при хранении названия специальности в базе.
    /// </summary>
    public class Specialization
    {
        public byte id;
        private string title;

        public Specialization(string specTitle, byte Id = 0)
        {
            id = Id;
            if (String.IsNullOrWhiteSpace(specTitle))
                title = "bad name";
            else
                title = specTitle;
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    return;
                title = value;
            }
        }

        /// <summary>
        /// Сформировать список специализаций на основе 64-битного числа без знака.
        /// </summary>
        /// <param name="source">Число, на основе которого формировать список.</param>
        /// <param name="allSpecs">Словарь всех специализаций, где ключём служит позиция в UInt64.</param>
//         static public List<Specialization> GetSpecializationsFromULong(UInt64 source, List<Specialization> allSpecs)
//         {
//             List<Specialization> result = new List<Specialization>();
// 
//             for (int i = 0; i < 64; i++)
//             {
//                 if ( (source & ( 1UL << i)) != 0) result.Add(allSpecs[i]);
//             }
//             return result;
//         }

        /// <summary>
        /// Сформировать 64-битное число без знака на основе списка специализаций.
        /// </summary>
        /// <param name="source">Список специализаций.</param>
        /// <param name="allSpecs">Словарь всех специализаций, где ключём служит позиция в UInt64.</param>
//         static public UInt64 GetULongFromSpecializations(List<Specialization> source, List<Specialization> allSpecs)
//         {
//             ulong result = 0;
// 
//             //Такое фундаментальное ограничение будет до тех пор, пока в БД специальности у одного специалиста представлены ulong (64 бита).
// //             byte[] byteArray = new byte[64];
// //             for (int i = 0; i < 64;i++ )
// //             {
// //                 byteArray[i] = 0;
// //             }
// 
//             foreach (var spec in source)
//             {
//                 result |= 1UL << allSpecs.Find(v => v.id == spec.id).id;
//             }
// 
//             //result = BitConverter.ToUInt64(byteArray, 0);
// 
//             return result;
//         }

        public override string ToString()
        {
            return title;
        }

        public override bool Equals(object obj)
        {
            Specialization spec = obj as Specialization;
            if (spec == null) 
                return false;
            else
                return title.Equals(spec.title);
        }

        public override int GetHashCode()
        {
            return title.GetHashCode();
        }
    }
    */
    #endregion
    /// <summary>
    /// Класс представляет временной интервал между двумя датами.
    /// </summary>
    public class TimeInterval
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
                if (value == default(DateTime)) 
                    throw new DateSetException(this, value, "Set to default(DateTime) happen.");
                if (endDate != default(DateTime) && value > endDate) 
                    throw new DateSetException(this, value, "Set StartDate after EndDate happen.");
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (value == default(DateTime)) 
                    throw new DateSetException(this, value, "Set to default(DateTime) happen.");
                if (startDate != default(DateTime) && value < startDate) 
                    throw new DateSetException(this, value, "Set EndDate before StartDate happen.");
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
    }
}
