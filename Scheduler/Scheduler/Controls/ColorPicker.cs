using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Scheduler.Controls
{
    
    public partial class ColorPicker : UserControl
    {
        public class ControlsColors
        {
            public Color ColorMain;
            public Color ColorBackground;
            public Color ColorBorder;
            public Font Font;
        }

        private ControlsColors _colors;
        public event Action onColorsChanged;
        public ColorPicker()
        {
            InitializeComponent();
            _colors = new ControlsColors();
        }

        public ControlsColors SelectedColors
        {
            get
            {
                _colors.ColorMain = pnl1.BackColor;
                _colors.ColorBorder = pnl2.BackColor;
                _colors.ColorBackground = pnl3.BackColor;
                return _colors;
            }
            set
            {
                _colors = value;
                pnl1.BackColor = value.ColorMain;
                pnl2.BackColor = value.ColorBorder;
                pnl3.BackColor = value.ColorBackground;
                onColorsChanged?.Invoke();
            }
        }

        void ChangeColor(Panel panelToDrawColor)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.AllowFullOpen = true;
                dialog.AnyColor = true;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                panelToDrawColor.BackColor = dialog.Color;
            }
            onColorsChanged?.Invoke();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var name = (sender as Control).Name;
            var pnl = tableLayoutPanel1.Controls["pnl" + name.Substring(name.Length - 1)] as Panel;
            ChangeColor(pnl);
        }
    }
}
