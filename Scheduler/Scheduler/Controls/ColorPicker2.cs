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
using Scheduler_InterfacesRealisations;
using CalendarControl3_Interfaces;

namespace Scheduler.Controls
{

    public partial class ColorPicker2 : UserControl
    {
        private IColorPalette _colors;
        private bool notUpdate = false;
        public event Action<ColorPicker2> onColorsChanged;
        public ColorPicker2()
        {
            InitializeComponent();
            _colors = new ColorPalette();
            fontComboBox1.Populate(true);
            fontComboBox1.SetSelectedFont();
        }

        public IColorPalette SelectedColors
        {
            get
            {
                UpdateColors();
                return _colors;
            }
            set
            {
                notUpdate = true;
                _colors = value;

                foreach (ColorPaletteSelectables cps in Enum.GetValues(typeof(ColorPaletteSelectables)))
                {
                    var p = (Panel)tableLayoutPanel1.Controls["pnl" + (int)cps];
                    var b = (FontCombo.BrushSelect)tableLayoutPanel1.Controls["brush" + (int)cps];
                    p.BackColor = value.GetColor(cps);
                    b.SelectedText = value.GetBrushName(cps);
                    b.SetColor(p.BackColor);
                }

                fontComboBox1.Font = value.Font;
                notUpdate = false;
                onColorsChanged?.Invoke(this);
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
            UpdateColors();
            onColorsChanged?.Invoke(this);
        }

        void UpdateColors()
        {
            foreach (ColorPaletteSelectables cps in Enum.GetValues(typeof(ColorPaletteSelectables)))
            {
                var p = (Panel)tableLayoutPanel1.Controls["pnl" + (int)cps];
                var b = (FontCombo.BrushSelect)tableLayoutPanel1.Controls["brush" + (int)cps];
                if (_colors.GetColor(cps) != p.BackColor)
                    b.SetColor(p.BackColor);
                _colors.SetColor(cps, p.BackColor);
                _colors.SetBrushName(cps, b.SelectedText);
            }

            _colors.Font = fontComboBox1.Font;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ChangeColor(sender as Panel);
        }

        private void brush1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notUpdate)
                return;
            notUpdate = true;
            UpdateColors();
            onColorsChanged?.Invoke(this);
            notUpdate = false;
        }
    }
}
