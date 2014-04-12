using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_Controls_Interfaces
{
    /// <summary>
    /// Интерфейс, обобщающий значимые. Для типизированных template.
    /// </summary>
    public interface IDummy: System.ComponentModel.INotifyPropertyChanged
    {

    }

    public interface IHaveID
    {
        int ID { get; set; }
    }

    /// <summary>
    /// Коренной интерфейс для сущностей, имеющих имя/название.
    /// </summary>
    public interface INamedEntity: IHaveID
    {
        string Name { get; set; }

        //string ToString();
    }

    public delegate List<IReception> GetClientReceptionsList(IClient client);
    /// <summary>
    /// Интерфейс клиента.
    /// </summary>
    public interface IClient : INamedEntity, IDummy
    {
        string Comment { get; set; }

        bool BlackListed { get; set; }

        HashSet<string> Telephones { get; set; }
        /// <summary>
        /// Проверить, входит ли переданный телефон в список телефонов клиента.
        /// </summary>
        /// <param name="telNumber">Строка с номером телефона.</param>
        /// <returns>true если входит.</returns>
        bool CheckTelephone(string telNumber);
        // 
        //         /// <summary>
        //         /// Добавляет (если ещё нет) или удаляет (если уже есть) номер телефона.
        //         /// </summary>
        //         string Telephone { set; }

        List<IReception> GetReceptions();
        /// <summary>
        /// Установить функцию получения списка посещений данного клиента.
        /// </summary>
        void ReceptionListFuncition(GetClientReceptionsList func);
    }

    /// <summary>
    /// Интерфейс специалиста.
    /// </summary>
    public interface ISpecialist : INamedEntity, IDummy
    {
        bool NotWorking { get; set; }

        HashSet<string> Specialisations { get; set; }

        //         /// <summary>
        //         /// Добавляет (если ещё нет) или удаляет (если уже есть) специализацию.
        //         /// </summary>
        //         string Specialisation { set; }

    }

    /// <summary>
    /// Интерфейс кабинета.
    /// </summary>
    public interface ICabinet : INamedEntity, IDummy
    {
        bool Availability { get; set; }
    }

    public delegate void DisposeReception(IHaveID item);
    /// <summary>
    /// Интерфейс записи на приём.
    /// </summary>
    public interface IReception : IDummy, IHaveID
    {
        ITimeInterval ReceptionTimeInterval { get; set; }
        IClient Client { get; set; }
        ISpecialist Specialist { get; set; }
        ICabinet Cabinet { get; set; }
        string Specialization { get; set; }
        bool Rent { get; set; }

        /// <summary>
        /// Проверить данные на валидность.
        /// </summary>
        /// <returns>null в случае отсутствия проблем. Сообщение с пояснениями, что не так, в случае ошибки.</returns>
        string Validate();

        /// <summary>
        /// Удалить информацию о себе из системы.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Установить функцию удаления записи.
        /// </summary>
        void SetDisposeFunction(DisposeReception func);

        /// <summary>
        /// Отобразить содержимое в виде максимально информативной строки.
        /// </summary>
        string DisplayString { get; }
    }

    /// <summary>
    /// Интерфейс временного интервала приёма.
    /// </summary>
    public interface ITimeInterval
    {
        void SetStartEnd(DateTime startDate, DateTime endDate);
        string Interval();
        DateTime Date { get; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }

    public interface ISpecializationList : IDummy
    {
        HashSet<string> SpecializationList { get; set; }

        ISpecializationList Copy();
    }

    public interface ITelephone
    {
        /// <summary>
        /// Номер телефона как строка из цифр.
        /// </summary>
        string TelephoneNumber { get; set; }

        /// <summary>
        /// Номер телефона в виде отформатированной строки.
        /// </summary>
        string FormattedTelephoneNumber { get; }

    }



    public delegate void SaveChangesHandler<T>(object source, SaveChangesEventArgs<T> e) where T : IDummy;
    public delegate void ShowClientsHandler(object source, ShowClientsEventsArgs e);
    public delegate void CreateChildReceptionHandler(object source, CreateChildReceptionEventArgs e);
    public delegate void CancelReceptionHandler(object source, CancelReceptionEventArgs e);

    public class SaveChangesEventArgs<T> : EventArgs where T : IDummy
    {
        private T ChangedEntity;
        public SaveChangesEventArgs(T input)
        {
            ChangedEntity = input;
        }

        public T Entity
        {
            get { return ChangedEntity; }
        }
    }

    public class ShowClientsEventsArgs : EventArgs
    {
        private string name;
        private string telephone;

        public ShowClientsEventsArgs(string Name = "", string Telephone = "")
        {
            name = Name;
            telephone = Telephone;
        }

        public string Name { get { return name; } }
        public string Telephone { get { return telephone; } }
    }

    public class CreateChildReceptionEventArgs : SaveChangesEventArgs<IReception>
    {
        public CreateChildReceptionEventArgs(IReception reception)
            : base(reception)
        {
        }
    }

    public class CancelReceptionEventArgs : SaveChangesEventArgs<IReception>
    {
        public CancelReceptionEventArgs(IReception reception)
            : base(reception)
        {
        }
    }
}
