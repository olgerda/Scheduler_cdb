using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalendarControl3_Interfaces
{

    /// <summary>
    /// Интерфейс таблицы, по которой может быть построен контрол с отображением сущностей по столбцам со свободным расположением по вертикали.
    /// </summary>
    public interface ITable2ControlInterface
    {
        /// <summary>
        /// Получить число столбцов для отображения
        /// </summary>
        int ColumnCount { get; }
        /// <summary>
        /// Получить значение, от которого отталкиваться при отрисовке от верхнего края.
        /// </summary>
        int MinValue { get; }
        /// <summary>
        /// Получить значение, от которого отталкиваться при отрисовке до нижнего края.
        /// </summary>
        /// <returns></returns>
        int MaxValue { get; }

        /// <summary>
        /// Получить список описаний, которые будут отображены в левом столбце как легенда к строкам таблицы.
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> GetDescripptionsToValueLevels();

        /// <summary>
        /// Получить столбцы для отображения.
        /// </summary>
        /// <returns></returns>
        List<IColumn2ControlInterface> Columns { get; }

        //event EventHandler Changed;
    }

    public interface IColumn2ControlInterface
    {
        string Name {get;}
        List<IEntity2ControlInterface> Entities { get; }
        //event EventHandler Changed;
    }

    public interface IEntity2ControlInterface
    {
        //bool oneStringToShow;
        string StringToShow { get; }
        //List<string> StringsToShow();
        /// <summary>
        /// Получить верхнюю границу отрисовки сущности.
        /// </summary>
        int TopLevel { get; }
        /// <summary>
        /// Получить нижнюю границу отрисовки сущности.
        /// </summary>
        /// <returns></returns>
        int BottomLevel { get; }

        /// <summary>
        /// Получить объект, относящийся к данной сущности.
        /// </summary>
        /// <returns></returns>
        //object GetObject();

        ulong ID { get; }
        
        //event EventHandler Changed;
    }

}
