using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_Controls_Interfaces
{
    /// <summary>
    /// Коренной интерфейс для сущностей, имеющих имя/название.
    /// </summary>
    public interface INamedEntity
    {
        string Name { get; set; }
    }

    /// <summary>
    /// Интерфейс клиента.
    /// </summary>
    public interface IClient : INamedEntity
    {
        string Comment { get; set; }

        bool BlackListed { get; set; }

        HashSet<string> Telephones { get; set; }
// 
//         /// <summary>
//         /// Добавляет (если ещё нет) или удаляет (если уже есть) номер телефона.
//         /// </summary>
//         string Telephone { set; }

        List<string> Receptions { get; }
    }

    /// <summary>
    /// Интерфейс специалиста.
    /// </summary>
    public interface ISpecialist : INamedEntity
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
    public interface ICabinet : INamedEntity
    {
        bool Availability { get; set; }
    }

    /// <summary>
    /// Интерфейс записи на приём.
    /// </summary>
    public interface IReception
    {
        ITimeInterval ReceptionTimeInterval { get; set; }
        IClient client { get; set; }
        ISpecialist specialist { get; set; }
        ICabinet cabinet { get; set; }

        string specialization { get; set; }
        bool rent { get; set; }

        /// <summary>
        /// Проверить данные на валидность.
        /// </summary>
        /// <returns>null в случае отсутствия проблем. Сообщение с пояснениями, что не так, в случае ошибки.</returns>
        string Validate();
    }

    /// <summary>
    /// Интерфейс временного интервала приёма.
    /// </summary>
    public interface ITimeInterval
    {
        DateTime Date { get; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
