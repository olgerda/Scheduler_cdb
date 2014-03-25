using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_Forms_Interfaces
{
    public interface IEntityList<T> where T : Scheduler_Controls_Interfaces.IDummy
    {
        List<T> List { get; }
    }

    public interface IClientList: IEntityList<Scheduler_Controls_Interfaces.IClient>
    {
        //List<Scheduler_Controls_Interfaces.IClient> List { get; }

        /// <summary>
        /// Найти первого клиента, чьё имя начинается с заданной строчки.
        /// </summary>
        /// <param name="partialName"></param>
        /// <returns>null если ничего похожего не найдено.</returns>
        Scheduler_Controls_Interfaces.IClient FindClientByPartialName(string partialName);

        /// <summary>
        /// Найти первого клиента, чей телефон начинается с заданной строчки.
        /// </summary>
        /// <param name="partialName"></param>
        /// <returns>null если ничего похожего не найдено.</returns>
        Scheduler_Controls_Interfaces.IClient FindClientByPartialTelephone(string partialName);
    }

    public interface ISpecialistList : IEntityList<Scheduler_Controls_Interfaces.ISpecialist>
    {
        //List<Scheduler_Controls_Interfaces.ISpecialist> List { get; }

        Scheduler_Controls_Interfaces.ISpecialist FindSpecialistByPartialName(string partialName);
    }

    public interface ICabinetList : IEntityList<Scheduler_Controls_Interfaces.ICabinet>
    {

    }
}
