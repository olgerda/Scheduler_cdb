﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using CalendarControl3_Interfaces;
using System.Reflection;

namespace CalendarControl3
{
    public partial class ColumnsView : Control
    {
        /// <summary>
        /// Нужно подобрать некое умолчательное минимальное значение, которое нормально для восприятия. В идеале - вынести в настройки.
        /// </summary>
        private int minimumColumnWidth = 100;
        private int infoColumnWidth = 50;
        /// <summary>
        /// Значение, от которого откладываются сущности.
        /// </summary>
        private int topLevel;
        /// <summary>
        /// Значение, до которого откладываются сущности.
        /// </summary>
        private int bottomLevel;

        /// <summary>
        /// Значение, от которого будет отрисовываться таблица (откуда откладывается topLevel).
        /// </summary>
        private int tableTop;
        /// <summary>
        /// Значение, до которого будет отрисовываться таблица (докуда откладывается bottomLevel).
        /// </summary>
        private int tableBottom;

        /// <summary>
        /// Ширина реально отрисовываемой таблицы. Высчитывается исходя из columnsOnControl, oneColumnWidth и infoColumnWidth
        /// </summary>
        private int RealTableWidth
        {
            get { return infoColumnWidth + oneColumnWidth * columnsOnControl; }
        }

        /// <summary>
        /// Ширина одного столбца.
        /// </summary>
        private int oneColumnWidth;

        /// <summary>
        /// Количество столбцов, которые можно отрисовать за раз.
        /// </summary>
        private int columnsOnControl;

        private int headerHeight;
        private ToolTip tt;
        private Timer tTooltip = new Timer() { Interval = 500 };

        private SortedDictionary<int, string> descriptions;

        /// <summary>
        /// Координаты последнего клика по контролу. Заполняется в обработчике клика.
        /// </summary>
        private struct ClickCoords
        {
            public int level;
            public int column;
        }
        private ClickCoords LastClick;
        private IEntity2ControlInterface lastHoveredEntity;

        /// <summary>
        /// Основа таблицы, по которой строится контрол. Может изменяться в процессе существования контрола под воздействием внешних раздражителей.
        /// </summary>
        ITable2ControlInterface table;

        private Font _defaultFont = new Font("Arial", 10);
        

        public ITable2ControlInterface Table
        {
            get { return table; }
            set
            {
                if (value == null) return;
                table = value;
                descriptions = new SortedDictionary<int, string>(table.GetDescripptionsToValueLevels());

                Refresh();
            }
        }

        private Point lastClickPoint;
        private bool toolTipShowed = false;
        public ColumnsView()
        {
            LastClick = new ClickCoords() { level = -1, column = -1 };
            InitializeComponent();
            this.MinimumSize = new Size(minimumColumnWidth + infoColumnWidth, 500);

            tTooltip.Tick += (o, e) =>
            {
                MouseEventArgs args = new MouseEventArgs(MouseButtons.Right, 1, lastClickPoint.X, lastClickPoint.Y, 0);
                MouseClickHandler(this, args);
                tTooltip.Stop();
            };

            this.MouseMove += (o, e) =>
            {
                LastClick.column = GetAbsoluteColumnNumberOfClick(e.Location);
                LastClick.level = GetVerticalValueOfClick(e.Location);
                var entity = GetEntityOnClick();

                if (lastHoveredEntity != entity || entity == null)
                {
                    tTooltip.Stop();
                    toolTipShowed = false;
                }

                if (entity != null && !toolTipShowed)
                {
                    tTooltip.Start();
                    lastClickPoint = e.Location;
                }

                lastHoveredEntity = entity;
            };
        }

        void MakeTableFromInput()
        {
            if (table == null || table.ColumnCount == 0) return;

            int columnCount = table.ColumnCount;

            while (true)
            {
                oneColumnWidth = (this.Width - infoColumnWidth) / columnCount;

                if (oneColumnWidth < minimumColumnWidth)
                {
                    columnCount--;
                    if (columnCount == 0)
                    {
                        oneColumnWidth = minimumColumnWidth;
                        break;
                    }
                }
                else
                    break;
            }

            topLevel = table.MinValue;
            bottomLevel = table.MaxValue;

            headerHeight = 40;

            tableTop = headerHeight;
            tableBottom = Height - hScrollBar1.Height;

            columnsOnControl = (int)Math.Floor((Width - infoColumnWidth) / (float)oneColumnWidth);

            hScrollBar1.Minimum = 0;
            hScrollBar1.Value = 0;
            hScrollBar1.Maximum = table.ColumnCount > 0 ? table.ColumnCount - 1 : 0;
            hScrollBar1.LargeChange = columnsOnControl;
        }



        /// <summary>
        /// Есть острое желание порисовать.
        /// </summary>
        /// <param name="e">Инструменты для рисования.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (table == null)
            { // в случае умолчательного создания - нифига не делаем.
                return;
            }

            // если создали по правилам (вместе с таблицей) - отрисовываем.
            int currentLeft = infoColumnWidth;

            PointF drawPoint;

            //infocolumn

            using (var drawFont = (Font)table.Coloring.Font.Clone())
            using (var brush = table.Coloring.GetBrush(ColorPaletteSelectables.Main))
            {
                foreach (var pair in descriptions)
                {
                    var y = tableTop + ScaleLevelsToControl(pair.Key);
                    if (y + drawFont.Height > tableBottom) y -= drawFont.Height;
                    drawPoint = new PointF(2f, y);
                    e.Graphics.DrawString(pair.Value, drawFont, brush, drawPoint);
                }

                //maincolumns
                var columns = table.Columns;
                for (int i = hScrollBar1.Value; i < hScrollBar1.Value + columnsOnControl; i++)
                {
                    if (i > columns.Count - 1) break;
                    PaintColumn(e.Graphics, columns[i], currentLeft, oneColumnWidth, tableBottom);
                    drawPoint = new PointF(currentLeft + 1f, tableTop - drawFont.Height - 1f);
                    e.Graphics.DrawString(columns[i].Name, drawFont, brush, drawPoint);
                    currentLeft += oneColumnWidth;
                }
            }

            using (var pen = new Pen(Color.Black))
            {
                e.Graphics.DrawLine(pen, 0, 0, 0, tableBottom - 1); //левая граница
                e.Graphics.DrawLine(pen, 0, tableTop, RealTableWidth,
                    tableTop); //верхняя граница столбцов
                e.Graphics.DrawLine(pen, 0, 0, RealTableWidth, 0); //верхняя граница шапки
                e.Graphics.DrawLine(pen, 0, tableBottom - 1, RealTableWidth,
                    tableBottom - 1); //нижняя граница
            }

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }

        public override void Refresh()
        {
            MakeTableFromInput();
            base.Refresh();
        }
        /// <summary>
        /// Отрисовать столбец вместе с содержащимися в нём сущностями.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="column">Столбец для отрисовки.</param>
        /// <param name="leftside">Левая граница отрисовки столбца.</param>
        /// <param name="width">Ширина отрисовки столбца.</param>
        /// <param name="height">Высота отрисовки столбца.</param>
        void PaintColumn(Graphics g, IColumn2ControlInterface column, int leftside, int width, int height)
        {
            using (var backBrush = column.Coloring.GetBrush(ColorPaletteSelectables.Background))
                g.FillRectangle(backBrush, leftside, 0, width, height);

            using (var pen = new Pen(table.Coloring.GetBrush(ColorPaletteSelectables.Border)))
                foreach (var pair in descriptions)
                {
                    var y = tableTop + ScaleLevelsToControl(pair.Key);
                    if (y >= tableTop && y <= tableBottom)
                        g.DrawLine(pen, leftside, y, leftside + width, y);
                    else
                        continue;
                }

            foreach (var entity in column.Entities)
            {
                PaintEntity(g, entity, leftside + 1, width - 2);
            }

            using (var p = new Pen(column.Coloring.GetBrush(ColorPaletteSelectables.Border)))
            {
                g.DrawLine(p, leftside, 0, leftside, height - 1); //рисуем левую линию каждого столбца
                g.DrawLine(p, leftside + width, 0, leftside + width, height - 1); //рисуем правую линию
            }

        }

        StringFormat _format = new StringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoWrap) { Alignment = StringAlignment.Center };

        /// <summary>
        /// Отрисовать сущность в виде прямоугольника со сглаженными сторонами и текстом внутри.
        /// Многова-то вызовов ScaleToControl. Надо будет обкумекать, как это порешать. Например, предрассчёт сократит вызовы с 4х до 2х.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="entity">Сущность для отрисовки.</param>
        /// <param name="leftside">Левый край отрисовки сущности.</param>
        /// <param name="width">Ширина отрисовки сущности.</param>
        void PaintEntity(Graphics g, IEntity2ControlInterface entity, int leftside, int width)
        {
            //нарисуем сглаженный прямоугольник
            Rectangle entRect = new Rectangle(leftside, tableTop + ScaleLevelsToControl(entity.TopLevel), width, ScaleLevelsToControl(entity.BottomLevel) - ScaleLevelsToControl(entity.TopLevel));
            GraphicsPath entShape = GetBarShape(entRect, 10); //10 - магическое число, смотрится хорошо

            using (var brush = entity.Coloring.GetBrush(ColorPaletteSelectables.Background))
                g.FillPath(brush, entShape);
            using (var pen = new Pen(entity.Coloring.GetBrush(ColorPaletteSelectables.Border), 2f))
                g.DrawPath(pen, entShape);

            //напишем в нём текст

            RectangleF strRect = new RectangleF((float)entRect.X + 2f, (float)entRect.Y + 2f, entRect.Width - 4f, entRect.Height - 4f);
            using (var brush = entity.Coloring.GetBrush(ColorPaletteSelectables.Main))
                g.DrawString(entity.StringToShow, entity.Coloring.Font ?? _defaultFont, brush, strRect, _format);
        }

        /// <summary>
        /// Преобразовать входящее значение в значение относительно предварительно заданных topLevel и bottomLevel.
        /// </summary>
        /// <param name="input">Значение в терминах уровней исходных данных.</param>
        /// <returns>Значение в терминах пикселей контрола.</returns>
        int ScaleLevelsToControl(int input)
        {
            int delta = bottomLevel - topLevel;
            double dotsPerPixel = (double)delta / (tableBottom - tableTop);
            double dotsToReturn = input - topLevel;
            return (int)Math.Floor(dotsToReturn / dotsPerPixel);
        }

        GraphicsPath path = new GraphicsPath(FillMode.Winding);
        public GraphicsPath GetBarShape(Rectangle rect, int cornerRad)
        {
            /*
             * Взято тут:
             * http://www.programmersforum.ru/showpost.php?p=525742&postcount=3
             */
            int rad = cornerRad;
            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;
            path.Reset();
            path.AddBezier(x, y + rad, x, y, x + rad, y, x + rad, y);
            path.AddLine(x + rad, y, (x + width) - rad, y);
            path.AddBezier((x + width) - rad, y, x + width, y, x + width, y + rad, x + width, y + rad);
            path.AddLine((x + width), (y + rad), (x + width), ((y + height) - rad));
            path.AddBezier((x + width), ((y + height) - rad), (x + width), (y + height),
                ((x + width) - rad), (y + height), ((x + width) - rad), (y + height));
            path.AddLine(((x + width) - rad), (y + height), (x + rad), (y + height));
            path.AddBezier(x + rad, y + height, x, y + height, x, (y + height) - rad, x, (y + height) - rad);
            path.AddLine(x, (y + height) - rad, x, y + rad);
            return path;
        }

        private void HScrollHandler(object sender, ScrollEventArgs e)
        {
            if (e.Type != ScrollEventType.EndScroll) return;
            this.Refresh();
        }

        #region Получение объекта под мышью (для обработки кликов)
        /// <summary>
        /// Получить значение между topLevel и bottomLevel куда ткнулась мыша.
        /// </summary>
        /// <param name="click">Точка клика в координатах контрола.</param>
        /// <returns>-1 - вне зоны таблицы.</returns>
        public int GetVerticalValueOfClick(Point click)
        {
            if (click.Y < tableTop || click.Y > tableBottom) return -1;

            int delta = bottomLevel - topLevel;
            double dotsPerPixel = (double)delta / (tableBottom - tableTop);
            return topLevel + (int)Math.Truncate((click.Y - tableTop) * dotsPerPixel);
        }

        /// <summary>
        /// Получить номер столбца как он отрисован, в котором произошёл клик.
        /// </summary>
        /// <param name="click">Точка клика в координатах контрола.</param>
        /// <returns>Zero-based index. -1 если клик вне рабочей таблицы.</returns>
        int GetVisibleColumnNumberOfClick(Point click)
        {
            if (click.X <= infoColumnWidth || click.X > RealTableWidth) return -1;
            int result = (click.X - infoColumnWidth) / oneColumnWidth;
            if (result > (columnsOnControl - 1)) result--;
            return result;
        }

        /// <summary>
        /// Получить абсолютный номер столбца, в котором произошёл клик.
        /// </summary>
        /// <param name="click">Точка клика в координатах контрола.</param>
        /// <returns>Zero-based index. -1 если клик вне рабочей таблицы.</returns>
        int GetAbsoluteColumnNumberOfClick(Point click)
        {
            int relativeNumber = GetVisibleColumnNumberOfClick(click);
            if (relativeNumber == -1) return -1;
            return hScrollBar1.Value + relativeNumber;
        }

        public IEntity2ControlInterface GetNearestTopEntity()
        {
            if (LastClick.column == -1 || LastClick.level == -1 || table.Columns[LastClick.column].Entities.Count == 0)
                return null;
            IEntity2ControlInterface result = table.Columns[LastClick.column].Entities
                .OrderByDescending(e => e.BottomLevel)
                .Where(e => e.BottomLevel <= LastClick.level)
                .FirstOrDefault();

            return result;
        }

        public IEntity2ControlInterface GetEntityOnClick()
        {
            if (LastClick.level < 0 || LastClick.column < 0 || table.ColumnCount < 1) return null;
            foreach (var entity in table.Columns[LastClick.column].Entities)
            {
                if (entity.TopLevel < LastClick.level && LastClick.level < entity.BottomLevel) return entity;
            }
            return null;
        }

        public string GetColumnNameOnClick()
        {
            if (LastClick.level < 0 || LastClick.column < 0) return null;
            return table.Columns[LastClick.column].Name;
        }
        #endregion

        #region Получение полноразмерной картинки контрола
        public Bitmap GenerateBitmap()
        {
            if (table == null)
                return null;

            int fullwidth = oneColumnWidth * table.ColumnCount + infoColumnWidth + 1;
            int fullheight = Height;

            int bckpColumnsOnControl = columnsOnControl;
            columnsOnControl = table.ColumnCount;

            Bitmap bmp = new Bitmap(fullwidth, fullheight);
            Graphics g = Graphics.FromImage(bmp);

            g.FillRectangle(Brushes.White, 0, 0, fullwidth, fullheight);

            PaintEventArgs e = new PaintEventArgs(g, new Rectangle());

            OnPaint(e);

            columnsOnControl = bckpColumnsOnControl;

            return bmp;
        }
        #endregion

        private void MouseClickHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            { //покажем подсказку по этому месту
                LastClick.column = GetAbsoluteColumnNumberOfClick(e.Location);
                LastClick.level = GetVerticalValueOfClick(e.Location);

                IEntity2ControlInterface clicked = GetEntityOnClick();
                if (clicked != null)
                {
                    if (tt == null)
                    {
                        tt = new ToolTip();
                        tt.Active = true;
                        tt.InitialDelay = 10;
                    }
                    tt.Show(clicked.StringToShow, this);
                    toolTipShowed = true;
                }
            }
        }

        private void MouseEnterHandler(object sender, EventArgs e)
        {
            if (tt != null)
            {
                tt.Dispose();
                tt = null;
            }
        }



    }
}
