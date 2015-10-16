using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_Controls_Interfaces
{
    /// <summary>
    /// Интерфейс, обобщающий значимые. Для типизированных template.
    /// </summary>
    public interface IDummy
    {
        bool IAmChanged { get; }
    }

    public interface IHaveID : IComparable, IEquatable<IHaveID>
    {
        int ID { get; set; }
    }

    /// <summary>
    /// Коренной интерфейс для сущностей, имеющих имя/название.
    /// </summary>
    public interface INamedEntity: IHaveID
    {
        string Name { get; set; }
    }

    public delegate List<IReception> GetClientReceptionsList(IClient client);
    /// <summary>
    /// Интерфейс клиента.
    /// </summary>
    public interface IClient : INamedEntity, IDummy
    {
        string Comment { get; set; }

        TimeSpan GenerallyTime { get; set; }

        int GenerallyPrice { get; set; }

        bool BlackListed { get; set; }

        HashSet<string> Telephones { get; set; }
        /// <summary>
        /// Проверить, входит ли переданный телефон в список телефонов клиента.
        /// </summary>
        /// <param name="telNumber">Строка с номером телефона.</param>
        /// <returns>true если входит.</returns>
        bool CheckTelephone(string telNumber);

        /// <summary>
        /// Получить список посещений данного клиента.
        /// </summary>
        /// <returns></returns>
        List<IReception> GetReceptions();

        /// <summary>
        /// Установить функцию получения списка посещений данного клиента.
        /// </summary>
        void ReceptionListFuncition(GetClientReceptionsList func);
    }

    public delegate Dictionary<int, int> GetCosts(ISpecialist spec);
    /// <summary>
    /// Интерфейс специалиста.
    /// </summary>
    public interface ISpecialist : INamedEntity, IDummy
    {
        bool NotWorking { get; set; }

        HashSet<string> Specialisations { get; set; }

        Dictionary<int, int> GetCosts();

        void CostsFunction(GetCosts func);
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

        void CommitToDatabase();

        int Price { get; set; }

        /// <summary>
        /// Проверить данные на валидность.
        /// </summary>
        /// <returns>null в случае отсутствия проблем. Сообщение с пояснениями, что не так, в случае ошибки.</returns>
        string Validate();

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

        event Scheduler_Forms_Interfaces.ItemAddedHandler OnItemAdded;
        event Scheduler_Forms_Interfaces.ItemRemovedHandler OnItemRemoved;
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
