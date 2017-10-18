using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalendarControl3_Interfaces
{

    /// <summary>
    /// Интерфейс таблицы, по которой может быть построен контрол с отображением сущностей по столбцам со свободным расположением по вертикали.
    /// </summary>
    public interface ITable2ControlInterface : ICanCustomizeLook
    {
        /// <summary>
        /// Получить число столбцов для отображения
        /// </summary>
        int ColumnCount { get; }
        /// <summary>
        /// Получить значение, от которого отталкиваться при отрисовке от верхнего края.
        /// </summary>
        int MinValue { get; set; }
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
    }

    public interface IColumn2ControlInterface : ICanCustomizeLook
    {
        string Name { get; set; }
        List<IEntity2ControlInterface> Entities { get; }
    }

    public interface IEntity2ControlInterface : ICanCustomizeLook
    {
        string StringToShow { get; }
        /// <summary>
        /// Получить верхнюю границу отрисовки сущности.
        /// </summary>
        int TopLevel { get; }
        /// <summary>
        /// Получить нижнюю границу отрисовки сущности.
        /// </summary>
        /// <returns></returns>
        int BottomLevel { get; }

        bool IsIntersectWith(IEntity2ControlInterface second);
    }

    public interface ICanCustomizeLook
    {
        IColorPalette Coloring { get; set; }
    }

    public interface IColorPalette : ICloneable
    {
        Dictionary<ColorPaletteSelectables, System.Drawing.Color> Colors { get; }
        Dictionary<ColorPaletteSelectables, String> BrushNames { get; }
        List<ColorPaletteSelectables> ActiveColorChangers { get; }

        System.Drawing.Color GetColor(ColorPaletteSelectables selectable);
        String GetBrushName(ColorPaletteSelectables selectable);
        System.Drawing.Brush GetBrush(ColorPaletteSelectables selectable);
        void SetColor(ColorPaletteSelectables selectable, System.Drawing.Color color);
        void SetBrushName(ColorPaletteSelectables selectable, String name);

        System.Drawing.Font Font { get; set; }
    }

    public enum ColorPaletteSelectables
    {
        Main = 1,
        Border = 2,
        Background =3 
    }


}
