using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace Scheduler.Controls.FontCombo
{
    public class FontComboBox : ComboBox
    {
        int _maxWidth = 0;
        bool _displayNameNormalFont = true;

        const string SAMPLE = " - Строка теста";
        const int DEFAULT_SIZE = 10;

        Font _arial = new Font("Arial", DEFAULT_SIZE);

        public FontComboBox()
        {
            MaxDropDownItems = 20;
            IntegralHeight = false;
            Sorted = false;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        public void Populate(bool displayNameNormalFont)
        {
            _displayNameNormalFont = displayNameNormalFont;

            foreach (FontFamily ff in FontFamily.Families)
            {
                if (ff.IsStyleAvailable(FontStyle.Regular))
                {
                    Items.Add(ff.Name);
                }
            }

            if (Items.Count > 0)
            {
                SelectedIndex = 0;
            }
        }

        public void SetSelectedFont(string font = "Arial")
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ToString().Equals(font, StringComparison.OrdinalIgnoreCase))
                {
                    SelectedItem = Items[i];
                    break;
                }
            }
        }

        protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
        {
            if (e.Index > -1)
            {
                int w = 0;
                string fontName = Items[e.Index].ToString();
                Font tmpFont = new Font(fontName, DEFAULT_SIZE);
                Graphics g = CreateGraphics();
                if (_displayNameNormalFont)
                {
                    SizeF fontSize = g.MeasureString(SAMPLE, tmpFont);
                    SizeF captionSize = g.MeasureString(fontName, _arial);
                    e.ItemHeight = (int)Math.Max(fontSize.Height, captionSize.Width);
                    w = (int)(fontSize.Width + captionSize.Width);
                }
                else
                {
                    SizeF s = g.MeasureString(fontName, tmpFont);
                    e.ItemHeight = (int)s.Height;
                    w = (int)s.Width;
                }
                _maxWidth = Math.Max(_maxWidth, w);
                e.ItemHeight = Math.Min(e.ItemHeight, 20);
            }
            base.OnMeasureItem(e);
        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                string fontName = Items[e.Index].ToString();
                Font tmpFont = new Font(fontName, DEFAULT_SIZE);

                if (_displayNameNormalFont)
                {
                    Graphics g = CreateGraphics();
                    int w = (int)g.MeasureString(fontName, _arial).Width;

                    if ((e.State & DrawItemState.Focus) == 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                        e.Graphics.DrawString(fontName, _arial, new SolidBrush(SystemColors.WindowText), e.Bounds.X * 2, e.Bounds.Y);
                        e.Graphics.DrawString(SAMPLE, tmpFont, new SolidBrush(SystemColors.WindowText), e.Bounds.X * 2 + w, e.Bounds.Y);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds);
                        e.Graphics.DrawString(fontName, _arial, new SolidBrush(SystemColors.HighlightText), e.Bounds.X * 2, e.Bounds.Y);
                        e.Graphics.DrawString(SAMPLE, tmpFont, new SolidBrush(SystemColors.HighlightText), e.Bounds.X * 2 + w, e.Bounds.Y);
                    }
                }
                else
                {
                    if ((e.State & DrawItemState.Focus) == 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                        e.Graphics.DrawString(fontName, tmpFont, new SolidBrush(SystemColors.WindowText), e.Bounds.X * 2, e.Bounds.Y);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds);
                        e.Graphics.DrawString(fontName, tmpFont, new SolidBrush(SystemColors.HighlightText), e.Bounds.X * 2, e.Bounds.Y);
                    }
                }
            }
            base.OnDrawItem(e);
        }

        protected override void OnDropDown(System.EventArgs e)
        {
            this.DropDownWidth = _maxWidth + 30;
        }
    }

    public class BrushSelect : ComboBox
    {
        private int _selectedIndex;

        // Data for each color in the list
        public class BrushInfo
        {
            public string Text { get; set; }
            public Brush Brush { get; set; }

            public BrushInfo(string text, Brush brush)
            {
                Text = text;
                Brush = brush;
            }
        }

        public BrushSelect()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += OnDrawItem;
            SetColor(Color.Green);
        }

        // Populate control with standard colors
        public void SetColor(Color color)
        {
            var selected = SelectedText;
            foreach (BrushInfo bi in Items)
                bi.Brush.Dispose();
            Items.Clear();

            //Type brushesType = typeof(Brushes);
            //// Get all static properties
            //var properties = brushesType.GetProperties(BindingFlags.Static | BindingFlags.Public);
            //foreach (var prop in properties)
            //{
            //    string name = prop.Name;
            //    SolidBrush brush = (SolidBrush)prop.GetValue(null, null);
            //    Items.Add(new BrushInfo(name, brush));
            //}
            Items.Add(new BrushInfo("Solid", new SolidBrush(color)));
            foreach (HatchStyle en in Enum.GetValues(typeof(HatchStyle)))
                Items.Add(new BrushInfo(en.ToString(), new HatchBrush(en, color)));
            SelectedText = selected;
        }

        // Draw list item
        protected void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                // Get this color
                BrushInfo bi = (BrushInfo)Items[e.Index];

                // Fill background
                e.DrawBackground();

                // Draw color box
                Rectangle rect = new Rectangle();
                rect.X = e.Bounds.X + 2;
                rect.Y = e.Bounds.Y + 2;
                rect.Width = 18;
                rect.Height = e.Bounds.Height - 5;
                e.Graphics.FillRectangle(bi.Brush, rect);
                e.Graphics.DrawRectangle(SystemPens.WindowText, rect);

                // Write color name
                Brush brush;
                if ((e.State & DrawItemState.Selected) != DrawItemState.None)
                    brush = SystemBrushes.HighlightText;
                else
                    brush = SystemBrushes.WindowText;
                e.Graphics.DrawString(bi.Text, Font, brush,
                    e.Bounds.X + rect.X + rect.Width + 2,
                    e.Bounds.Y + ((e.Bounds.Height - Font.Height) / 2));

                // Draw the focus rectangle if appropriate
                if ((e.State & DrawItemState.NoFocusRect) == DrawItemState.None)
                    e.DrawFocusRectangle();
            }
        }

        /// <summary>
        /// Gets or sets the currently selected item.
        /// </summary>
        public new BrushInfo SelectedItem
        {
            get
            {
                return (BrushInfo)base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;
            }
        }

        /// <summary>
        /// Gets the text of the selected item, or sets the selection to
        /// the item with the specified text.
        /// </summary>
        public new string SelectedText
        {
            get
            {
                if (SelectedIndex >= 0)
                    return SelectedItem.Text;
                return String.Empty;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((BrushInfo)Items[i]).Text == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the value of the selected item, or sets the selection to
        /// the item with the specified value.
        /// </summary>
        public new Brush SelectedValue
        {
            get
            {
                if (SelectedIndex >= 0)
                    return SelectedItem.Brush;
                return Brushes.White;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((BrushInfo)Items[i]).Brush == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }


    //public class HSComboBox : ComboBox
    //{
    //    public HSComboBox() : base()
    //    {
    //        this.DrawMode = DrawMode.OwnerDrawVariable;
    //        this.SetStyle(ControlStyles.DoubleBuffer, true);
    //        this.InitializeDropDown();
    //    }

    //    ~HSComboBox()
    //    {
    //        base.Dispose();
    //        this.Dispose(true);
    //    }

    //    protected void InitializeDropDown()
    //    {
    //        foreach (string styleName in Enum.GetNames(typeof(HatchStyle)))
    //        {
    //            this.Items.Add(styleName);
    //        }
    //    }

    //    protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
    //    {
    //        // The following method should generally be called before drawing.
    //        // It is actually superfluous here, since the subsequent drawing
    //        // will completely cover the area of interest.
    //        //e.DrawBackground();
    //        //The system provides the context
    //        //into which the owner custom-draws the required graphics.
    //        //The context into which to draw is e.graphics.
    //        //The index of the item to be painted is e.Index.
    //        //The painting should be done into the area described by e.Bounds.

    //        if (e.Index != -1)
    //        {
    //            Graphics g = e.Graphics;
    //            Rectangle r = e.Bounds;

    //            Rectangle rd = r;
    //            rd.Width = rd.Left + 25;
    //            Rectangle rt = r;
    //            r.X = rd.Right;
    //            string displayText = this.Items[e.Index].ToString();

    //            HatchStyle hs = (HatchStyle)Enum.Parse(typeof(HatchStyle), displayText, true); ;
    //            // TODO add user selected foreground and background colors here

    //            HatchBrush b = new HatchBrush(hs, Color.Black, e.BackColor);
    //            g.DrawRectangle(new Pen(Color.Black, 2), rd.X + 3, rd.Y + 2, rd.Width - 4, rd.Height - 4);
    //            g.FillRectangle(b, new Rectangle(rd.X + 3, rd.Y + 2, rd.Width - 4, rd.Height - 4));

    //            StringFormat sf = new StringFormat();
    //            sf.Alignment = StringAlignment.Near;

    //            //If the current item has focus.
    //            if ((e.State & DrawItemState.Focus) == 0)
    //            {
    //                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), r);
    //                e.Graphics.DrawString(displayText, this.Font, new SolidBrush(SystemColors.WindowText), r, sf);
    //            }
    //            else
    //            {
    //                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), r);
    //                e.Graphics.DrawString(displayText, this.Font, new SolidBrush(SystemColors.HighlightText), r, sf);
    //            }
    //        }

    //        //Draws a focus rectangle on the specified graphics surface and within the specified bounds.
    //        e.DrawFocusRectangle();
    //    }

    //    protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
    //    {
    //        //Work out what the text will be
    //        string displayText = this.Items[e.Index].ToString();

    //        //Get width & height of string
    //        SizeF stringSize = e.Graphics.MeasureString(displayText, this.Font);

    //        //Account for top margin
    //        stringSize.Height += 5;

    //        // set hight to text height
    //        e.ItemHeight = (int)stringSize.Height;

    //        // set width to text width
    //        e.ItemWidth = (int)stringSize.Width;
    //    }
    //}

}