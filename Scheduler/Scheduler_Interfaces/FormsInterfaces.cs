using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_Forms_Interfaces
{
    public delegate void ItemAddedHandler(object item);// where T : Scheduler_Controls_Interfaces.IDummy;
    public delegate void ItemRemovedHandler(object item);// where T : Scheduler_Controls_Interfaces.IDummy;
    public delegate void ItemChangedHandler(object item);// where T : Scheduler_Controls_Interfaces.IDummy;

    public class ItemEventArgs<T> : EventArgs where T : Scheduler_Controls_Interfaces.IDummy 
    {
        T changedItem;
        public ItemEventArgs(T item)
            : base()
        {
            changedItem = item;
        }

        T ChangedItem
        {
            get { return changedItem; }
        }
    }

    public interface IEntityList<T> /*: List<T>*/ where T : Scheduler_Controls_Interfaces.IDummy 
    {
        List<T> List { get; }

        void Add(T item);
        void Remove(T item);
        void ValidateAndUpdate();

        event ItemAddedHandler OnItemAdded;
        event ItemRemovedHandler OnItemRemoved;
        event ItemChangedHandler OnItemChanged;
        IEntityList<T> Copy();
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
        
        //IClientList Copy();
    }

    public interface ISpecialistList : IEntityList<Scheduler_Controls_Interfaces.ISpecialist>
    {
        //List<Scheduler_Controls_Interfaces.ISpecialist> List { get; }

        Scheduler_Controls_Interfaces.ISpecialist FindSpecialistByPartialName(string partialName);

        //ISpecialistList Copy();
    }

    public interface ICabinetList : IEntityList<Scheduler_Controls_Interfaces.ICabinet>
    {
        //ICabinetList Copy();
    }
}
