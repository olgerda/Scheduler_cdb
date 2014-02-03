using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_Forms_Interfaces
{
    public interface IClientList
    {
        List<Scheduler_Controls_Interfaces.IClient> List { get; }

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
}
