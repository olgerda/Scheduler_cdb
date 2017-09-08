using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

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
    public interface INamedEntity: IHaveID, INotifyPropertyChanged, ICloneable
    {
        string Name { get; set; }
    }

    public delegate List<IReception> GetClientReceptionsList(IClient client);
    /// <summary>
    /// Интерфейс клиента.
    /// </summary>
    public interface IClient : INamedEntity, IDummy
    {
        [Description("Комментарий")]
        string Comment { get; set; }

        [Description("Стандартное время приёма")]
        TimeSpan GenerallyTime { get; set; }

        [Description("Стандартная стоимость приёма")]
        int GenerallyPrice { get; set; }

        [Description("Баланс")]
        int Balance { get; set; }

        [Description("В чёрном списке")]
        bool BlackListed { get; set; }

        [Description("Необходимость оповещения через СМС")]
        bool NeedSMS { get; set; }


        [Description("Телефоны")]
        HashSet<string> Telephones { get; set; }

        [Description("Администратор")]
        string Administrator { get; set; }


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
        [Description("Не работает")]
        bool NotWorking { get; set; }

        [Description("Специальности")]
        HashSet<string> Specialisations { get; set; }
        
        Dictionary<int, int> GetCosts();

        void CostsFunction(GetCosts func);
    }
    
    /// <summary>
    /// Интерфейс кабинета.
    /// </summary>
    public interface ICabinet : INamedEntity, IDummy
    {
        [Description("Доступность для планирования")]
        bool Availability { get; set; }
        [Description("Записи только для комментариев")]
        bool CommentOnly { get; set; }
    }

    public delegate void DisposeReception(IHaveID item);
    /// <summary>
    /// Интерфейс записи на приём.
    /// </summary>
    public interface IReception : IDummy, IHaveID
    {
        [Description("Интервал приёма")]
        ITimeInterval ReceptionTimeInterval { get; set; }

        [Description("Клиент на приёме")]
        IClient Client { get; set; }

        [Description("Специалист на приёме")]
        ISpecialist Specialist { get; set; }

        [Description("Кабинет приёма")]
        ICabinet Cabinet { get; set; }

        [Description("Специализация")]
        string Specialization { get; set; }

        [Description("Аренда")]
        bool Rent { get; set; }

        [Description("Специальная аренда")]
        bool SpecialRent { get; set; }

        [Description("Приём не состоялся")]
        bool ReceptionDidNotTakePlace { get; set; }


        [Description("Администратор")]
        string Administrator { get; set; }

        [Description("Комментарий")]
        string Comment { get; set; }

        [Description("Только комментарий")]
        bool CommentOnlyReception { get; set; }


        void CommitToDatabase();

        [Description("Стоимость приёма")]
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
