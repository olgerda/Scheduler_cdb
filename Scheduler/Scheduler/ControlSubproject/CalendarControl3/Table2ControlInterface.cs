using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalendarControl3
{

    /// <summary>
    /// Интерфейс таблицы, по которой может быть построен контрол с отображением сущностей по столбцам со свободным расположением по вертикали.
    /// </summary>
    public interface ITable2ControlInterface
    {
        /// <summary>
        /// Получить число столбцов для отображения
        /// </summary>
        int GetColumnCount();
        /// <summary>
        /// Получить значение, от которого отталкиваться при отрисовке от верхнего края.
        /// </summary>
        int GetMinValue();
        /// <summary>
        /// Получить значение, от которого отталкиваться при отрисовке до нижнего края.
        /// </summary>
        /// <returns></returns>
        int GetMaxValue();

        /// <summary>
        /// Получить список описаний, которые будут отображены в левом столбце как легенда к строкам таблицы.
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> GetDescripptionsToValueLevels();

        /// <summary>
        /// Получить столбцы для отображения.
        /// </summary>
        /// <returns></returns>
        List<IColumn2ControlInterface> GetColumns();

        //event EventHandler Changed;
    }

    public interface IColumn2ControlInterface
    {
        string GetName();
        List<IEntity2ControlInterface> GetEntities();
        //event EventHandler Changed;
    }

    public interface IEntity2ControlInterface
    {
        //bool oneStringToShow;
        string StringToShow();
        //List<string> StringsToShow();
        /// <summary>
        /// Получить верхнюю границу отрисовки сущности.
        /// </summary>
        int TopLevel();
        /// <summary>
        /// Получить нижнюю границу отрисовки сущности.
        /// </summary>
        /// <returns></returns>
        int BottomLevel();

        /// <summary>
        /// Получить объект, относящийся к данной сущности.
        /// </summary>
        /// <returns></returns>
        object GetObject();

        ulong GetID();
        
        //event EventHandler Changed;
    }

}
