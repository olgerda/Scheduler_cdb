using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CalendarControl3
{
    public partial class ColumnsView : UserControl
    {
        /// <summary>
        /// Нужно подобрать некое умолчательное минимальное значение, которое нормально для восприятия. В идеале - вынести в настройки.
        /// </summary>
        private int minimumColumnWidth = 100;
        private int infoColumnWidth = 50;
        /// <summary>
        /// Оставлять ли сверху/сниху некий запас для "переработок"
        /// </summary>
        //private bool isReserveSpaceNeeded = false;
        /// <summary>
        /// Величина запаса для "переработок"
        /// </summary>
        //private int reserveSpace = 0;
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
        /// Значение, от которого отсчитывается левый край информативных столбцов.
        /// </summary>
//        private int tableLeft;
        /// <summary>
        /// Значение, до которого отсчитывается правый край информативных столбцов.
        /// </summary>
//        private int tableRight;
        /// <summary>
        /// Шинира реально отрисовываемой таблицы. Высчитывается исходя из columnsOnControl, oneColumnWidth и infoColumnWidth
        /// </summary>
        private int realTableWidth;

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

        /// <summary>
        /// Основа таблицы, по которой строится контрол. Может изменяться в процессе существования контрола под воздействием внешних раздражителей.
        /// </summary>
        ITable2ControlInterface table;

        public ITable2ControlInterface Table
        {
            get { return table; }
            set
            {
                if (value == null) return;
                table = value;
                MakeTableFromInput();
                Refresh();
            }
        }

        public ColumnsView()
        {
            InitializeComponent();
            
            
            //designtime TEST
             table = TESTCASE.GetTestTable();
             MakeTableFromInput();
            //designtime TEST

        }

//         public ColumnsView(ITable2ControlInterface inputTable)
//         {
//             InitializeComponent();
// 
//             table = inputTable;
// 
//             //MakeTableFromInput();
//         }

        void MakeTableFromInput()
        {
            if (table == null || table.GetColumnCount() == 0) return;
            //tableLeft = infoColumnWidth;

            oneColumnWidth = (this.Width - infoColumnWidth) / table.GetColumnCount();

            
            if (oneColumnWidth < minimumColumnWidth)
            {
                oneColumnWidth = minimumColumnWidth;
            }

            topLevel = table.GetMinValue();
            bottomLevel = table.GetMaxValue();
            
            headerHeight = 40;
            
            tableTop = headerHeight;
            tableBottom = Height - hScrollBar1.Height;

            columnsOnControl = (int)Math.Floor((Width - infoColumnWidth) / (float)oneColumnWidth);

            realTableWidth = infoColumnWidth + oneColumnWidth * columnsOnControl;


            hScrollBar1.Minimum = 0;
            hScrollBar1.Value = 0;
            hScrollBar1.Maximum = table.GetColumnCount() > 0 ? table.GetColumnCount() - 1 : 0;
            hScrollBar1.LargeChange = columnsOnControl;


            //toolTip.ShowAlways = true;
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
                
            }
            else
            { // если создали по правилам (вместе с таблицей) - отрисовываем.

                int currentLeft = infoColumnWidth;
                Font drawFont = new Font("Arial", 10);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                PointF drawPoint;

                //maincolumns
//                 foreach (var column in table.GetColumns())
//                 {
//                     PaintColumn(e.Graphics, column, currentLeft, oneColumnWidth, tableBottom);
//                     currentLeft += oneColumnWidth;
//                 }
                var columns = table.GetColumns();
                for (int i = hScrollBar1.Value; i < hScrollBar1.Value + columnsOnControl; i++)
                //for (int i = hScrollBar1.Value - hScrollBar1.Minimum; i < hScrollBar1.Value; i++)
                {
                    if (i > columns.Count - 1) break;
                    PaintColumn(e.Graphics, columns[i], currentLeft, oneColumnWidth, tableBottom);
                    drawPoint = new PointF(currentLeft + 1f, tableTop - drawFont.Height - 1f);
                    e.Graphics.DrawString(columns[i].GetName(), drawFont, drawBrush, drawPoint);
                    currentLeft += oneColumnWidth;
                }

                e.Graphics.DrawLine(new Pen(Brushes.Black), 0, 0, 0, tableBottom - 1); //левая граница
                e.Graphics.DrawLine(new Pen(Brushes.Black), 0, tableTop, realTableWidth, tableTop); //верхняя граница столбцов
                e.Graphics.DrawLine(new Pen(Brushes.Black), 0, 0, realTableWidth, 0); //верхняя граница шапки
                e.Graphics.DrawLine(new Pen(Brushes.Black), 0, tableBottom - 1, realTableWidth, tableBottom - 1); //нижняя граница

                //infocolumn
                var descriptions_ = table.GetDescripptionsToValueLevels();
                SortedDictionary<int, string> descriptions = new SortedDictionary<int, string>(descriptions_);
                foreach (var pair in descriptions)
                {
                    var y = tableTop + ScaleLevelsToControl(pair.Key);
                    if (y > tableTop && y < tableBottom)
                        e.Graphics.DrawLine(new Pen(Brushes.LightSeaGreen), 0f, y, realTableWidth, y);
                    //if (y < tableTop) y = tableTop;
                    if (y + drawFont.Height > tableBottom) y -= drawFont.Height;
                    drawPoint = new PointF(2f, y);
                    e.Graphics.DrawString(pair.Value, drawFont, drawBrush, drawPoint);
                }
//                descriptions.Sort(CompareByInt);
//                 switch (descriptions.Count)
//                 {
//                     case 0: break;
//                     case 1:
//                         drawPoint = new PointF(2f, 2f);
//                         e.Graphics.DrawString(descriptions[0].ToString(), drawFont, drawBrush, drawPoint);
//                         break;
//                     default:
//                 for (int i = 0; i < descriptions.Count; i++)
//                 {
//                     var y = ScaleLevelsToControl(descriptions[i].ToInt());
//                     if (i != 0 && i != descriptions.Count - 1) //рисуем разделители только у промежуточных
//                         e.Graphics.DrawLine(new Pen(Brushes.LightSeaGreen), 0f, y, realTableWidth, y);
//                     if (y < tableTop) y = tableTop;
//                     if (y + drawFont.Height > tableBottom) y -= drawFont.Height;
//                     drawPoint = new PointF(2f, y);
//                     e.Graphics.DrawString(descriptions[i].ToString(), drawFont, drawBrush, drawPoint);
//                 }
//                         break;
//                 }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            
            base.OnResize(e);
            MakeTableFromInput();
            Refresh();
        }

        /// <summary>
        /// Forces full repaint with recalculating table. Probably will removed and changed for event-based model of redriving.
        /// </summary>
        public void TotalRepaint()
        {
            MakeTableFromInput();
            Refresh();
        }


// 
//         int CompareByInt(IDescription2ControlInterface x, IDescription2ControlInterface y)
//         {
//             if (x == null && y == null) return 0;
//             if (x == null) return -1;
//             if (y == null) return 1;
//             return x.ToInt().CompareTo(y.ToInt());
//         }

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
            
            foreach (var entity in column.GetEntities())
            {
                PaintEntity(g, entity, leftside + 1, width -2 );
            }
            g.DrawLine(new Pen(Brushes.Black), leftside, 0, leftside, height); //рисуем левую линию каждого столбца
            g.DrawLine(new Pen(Brushes.Black), leftside + width, 0, leftside + width, height); //рисуем правую линию
            

        }

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
            Rectangle entRect = new Rectangle(leftside, tableTop + ScaleLevelsToControl(entity.TopLevel()), width, ScaleLevelsToControl(entity.BottomLevel()) - ScaleLevelsToControl(entity.TopLevel()));
            GraphicsPath entShape = GetBarShape(entRect, 10); //10 - магическое число, смотрится хорошо
            g.FillPath(Brushes.LightYellow, entShape);
            g.DrawPath(new Pen(Brushes.DarkGreen, 2f), entShape);


            //напишем в нём текст
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            //PointF drawPoint = new PointF(leftside + 5.0f, tableTop + ScaleLevelsToControl(entity.TopLevel()) + 5.0f);
            RectangleF strRect = new RectangleF((float)entRect.X + 2f, (float)entRect.Y + 2f, entRect.Width - 4f, entRect.Height- 4f);
            //g.DrawString()
            StringFormat format = new StringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoWrap);
            format.Alignment = StringAlignment.Center;
            g.DrawString(entity.StringToShow(), drawFont, drawBrush, strRect, format);//, drawPoint);
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
            return (int)Math.Floor(dotsToReturn/dotsPerPixel);
        }

        /// <summary>
        /// Преобразовать входящее значение в float значение относительно предварительно заданных topLevel и bottomLevel.
        /// </summary>
        /// <param name="input">Значение в терминах float уровней исходных данных.</param>
        /// <returns>Значение в терминах float пикселей контрола.</returns>
//         float ScaleLevelsToControlF(float input)
//         {
//             int delta = bottomLevel - topLevel;
//             double dotsPerPixel = (double)delta / Height;
//             double dotsToReturn = input - topLevel;
//             return (float)Math.Floor(dotsToReturn / dotsPerPixel);
//         }

        public static GraphicsPath GetBarShape(Rectangle rect, int cornerRad)
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
            GraphicsPath path = new GraphicsPath();
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
        int GetValue(Point click)
        {
            if (click.Y < tableTop || click.Y > tableBottom) return -1;
            
            int delta = bottomLevel - topLevel;
            double dotsPerPixel = (double)delta / (tableBottom - tableTop);
            return topLevel + (int)Math.Floor((click.Y - tableTop) * dotsPerPixel);
        }

        /// <summary>
        /// Получить абсолютный номер столбца, в котором произошёл клик.
        /// </summary>
        /// <param name="click">Точка клика в координатах контрола.</param>
        /// <returns>Zero-based index. -1 если клик вне рабочей таблицы.</returns>
        int GetVisibleColumnNumber(Point click)
        {
            if (click.X <= infoColumnWidth || click.X > realTableWidth) return -1;
            int result = (click.X - infoColumnWidth) / oneColumnWidth;
            if (result > (columnsOnControl - 1)) result--;
            return result;
        }

        /// <summary>
        /// Получить абсолютный номер столбца, в котором произошёл клик.
        /// </summary>
        /// <param name="click">Точка клика в координатах контрола.</param>
        /// <returns>Zero-based index. -1 если клик вне рабочей таблицы.</returns>
        int GetAbsoluteColumnNumber(Point click)
        {
            int relativeNumber = GetVisibleColumnNumber(click);
            if (relativeNumber == -1) return -1;
            return hScrollBar1.Value + relativeNumber;
        }

        public IEntity2ControlInterface GetEntityOnClick(Point click)
        {
            int level = GetValue(click);
            int columnNumber = GetAbsoluteColumnNumber(click);
            if (level < 0 || columnNumber < 0) return null;
            foreach (var entity in table.GetColumns()[columnNumber].GetEntities())
            {
                if (entity.TopLevel() < level && level < entity.BottomLevel()) return entity;  
            }
            return null;
        }

        public string GetColumnNameOnClick(Point click)
        {
            int level = GetValue(click);
            int columnNumber = GetAbsoluteColumnNumber(click);
            if (level < 0 || columnNumber < 0) return null;
            return table.GetColumns()[columnNumber].GetName();
        }
#endregion

        private void MouseClickHandler(object sender, MouseEventArgs e)
        {
            
            
            if (e.Button == MouseButtons.Right)
            { //покажем подсказку по этому месту
                IEntity2ControlInterface clicked = GetEntityOnClick(e.Location);
                if (clicked != null)
                {
                    if (tt == null)
                    {
                        tt = new ToolTip();
                        tt.Active = true;
                        tt.InitialDelay = 10;
                    }
                    tt.Show(clicked.StringToShow(), this);
                    
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
