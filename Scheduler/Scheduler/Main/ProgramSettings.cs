using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CalendarControl3_Interfaces;
using Scheduler_Controls_Interfaces;
using Scheduler_InterfacesRealisations;
using Scheduler_DBobjects_Intefraces;

namespace Scheduler.Main
{
    //class ProgramSettings
    //{
    //    private static Dictionary<Forms.SettingsForm.ColorChangers, ColorPalette> _default_controlsColors =
    //        new Dictionary<Forms.SettingsForm.ColorChangers, ColorPalette>()
    //        {
    //            {Forms.SettingsForm.ColorChangers.Table, new ColorPalette { ColorMain = Color.Black, ColorBorder = Color.LightSeaGreen, ColorBackground = Color.Black, Font = new Font("Arial", 10)}},
    //            {Forms.SettingsForm.ColorChangers.Column, new ColorPalette {ColorMain = Color.Black, ColorBorder = Color.DarkGreen, ColorBackground = Color.Black, Font = new Font("Arial", 10)}},
    //            {Forms.SettingsForm.ColorChangers.Entity1, new ColorPalette {ColorMain = Color.Black, ColorBorder = Color.DarkGreen, ColorBackground = Color.LightYellow, Font = new Font("Arial", 10)}},
    //            {Forms.SettingsForm.ColorChangers.Entity2, new ColorPalette {ColorMain = Color.Black, ColorBorder = Color.DarkGreen, ColorBackground = Color.LightYellow, Font = new Font("Arial", 10)}}
    //        };

    //    private Dictionary<Forms.SettingsForm.ColorChangers, ColorPalette>
    //        _controlsColors;
    //    public Dictionary<Forms.SettingsForm.ColorChangers, ColorPalette> ControlsColors
    //    {
    //        get { return _controlsColors; }
    //        set
    //        {
    //            _controlsColors = value;
    //            if (!suspendEnents && onSettingsChanged != null)
    //                onSettingsChanged();
    //        }
    //    }

    //    private Scheduler_Controls_Interfaces.ITimeInterval _timeInterval;
    //    public Scheduler_Controls_Interfaces.ITimeInterval TimeInterval
    //    {
    //        get { return _timeInterval; }
    //        set
    //        {
    //            _timeInterval = value;
    //        }
    //    }

    //    public event Action onSettingsChanged;
    //    private bool suspendEnents = false;

    //    public ProgramSettings(Scheduler_Controls_Interfaces.ITimeInterval timeInterval)
    //    {
    //        TimeInterval = timeInterval;
    //    }

    //    public void FromStrings(string[] input)
    //    {
    //        suspendEnents = true;
    //        if (input == null || input.Length < Enum.GetValues(typeof(Forms.SettingsForm.ColorChangers)).Length + 3)
    //        {
    //            ControlsColors = _default_controlsColors;
    //            FromStrings(ToStrings());
    //            return;
    //        }

    //        ControlsColors = new Dictionary<Forms.SettingsForm.ColorChangers, ColorPalette>();
    //        for (int i = 0; i < input.Length; i++)
    //        {
    //            if (input[i].StartsWith("COLORS:"))
    //            {
    //                for (var j = 0; j < Enum.GetValues(typeof(Forms.SettingsForm.ColorChangers)).Length; j++)
    //                {
    //                    var en = (Forms.SettingsForm.ColorChangers)Enum.Parse(
    //                        typeof(Forms.SettingsForm.ColorChangers),
    //                        input[++i]);
    //                    ControlsColors.Add(en, new ColorPalette());
    //                    ControlsColors[en].ColorMain = Color.FromArgb(int.Parse(input[++i]));
    //                    ControlsColors[en].ColorBorder = Color.FromArgb(int.Parse(input[++i]));
    //                    ControlsColors[en].ColorBackground = Color.FromArgb(int.Parse(input[++i]));
    //                }
    //                i++;
    //            }
    //            else if (input[i].StartsWith("TIMEINTERVAL:") && TimeInterval != null)
    //            {

    //                TimeInterval.StartDate = DateTime.FromBinary(long.Parse(input[++i]));
    //                TimeInterval.EndDate = DateTime.FromBinary(long.Parse(input[++i]));
    //            }
    //        }
    //        onSettingsChanged?.Invoke();
    //        suspendEnents = false;
    //    }

    //    public string[] ToStrings()
    //    {
    //        var result = new List<string>();
    //        result.Add("COLORS:");
    //        foreach (var pair in ControlsColors)
    //        {
    //            result.Add(pair.Key.ToString());
    //            result.Add(pair.Value.ColorMain.ToArgb().ToString());
    //            result.Add(pair.Value.ColorBorder.ToArgb().ToString());
    //            result.Add(pair.Value.ColorBackground.ToArgb().ToString());
    //        }

    //        result.Add("TIMEINTERVAL:");
    //        if (TimeInterval != null)
    //        {
    //            result.Add(TimeInterval.StartDate.ToBinary().ToString());
    //            result.Add(TimeInterval.EndDate.ToBinary().ToString());
    //        }
    //        else
    //        {
    //            result.Add(DateTime.Now.ToBinary().ToString());
    //            result.Add(DateTime.Now.AddHours(1).ToBinary().ToString());
    //        }

    //        return result.ToArray();
    //    }
    //}

    static class ProgramSettings2
    {
        private static
            Dictionary<ColorChangers, IColorPalette>
            _default_controlsColors = new Dictionary<ColorChangers, IColorPalette>();

        static ProgramSettings2()
        {
            foreach (ColorChangers en in Enum.GetValues(typeof(ColorChangers)))
            {
                var cp = new ColorPalette();
                cp.SetColor(ColorPaletteSelectables.Main, Color.Black);
                cp.SetColor(ColorPaletteSelectables.Border, Color.Green);
                cp.SetColor(ColorPaletteSelectables.Background, Color.LightYellow);
                _default_controlsColors.Add(en, cp);
            }

            FromStrings(null);
        }

        private static
            Dictionary<ColorChangers, IColorPalette>
            selectedColors = new Dictionary<ColorChangers, IColorPalette>();

        public static ITimeInterval TimeInterval { get; set; }

        public static Dictionary<ColorChangers, IColorPalette> ControlsColors
        {
            get { return selectedColors; }
            set { selectedColors = value; }
        }



        public static IColorPalette Get(ColorChangers select)
        {
            if (selectedColors.ContainsKey(select))
                return selectedColors[select];

            return (IColorPalette)_default_controlsColors[select].Clone();
        }

        public static void Set(ColorChangers select, IColorPalette colors)
        {
            if (!selectedColors.ContainsKey(select))
                selectedColors.Add(select, null);
            selectedColors[select] = colors;
        }

        public static void FromStrings(string[] input)
        {
            if (input == null ||
                input.Length != (Enum.GetValues(typeof(ColorChangers)).Length * (Enum.GetValues(typeof(ColorPaletteSelectables)).Length + 1) + 4))
            {
                ControlsColors = _default_controlsColors;
                FromStrings(ToStrings());
                return;
            }

            ControlsColors = new Dictionary<ColorChangers, IColorPalette>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("COLORS:"))
                {
                    for (var j = 0; j < Enum.GetValues(typeof(ColorChangers)).Length; j++)
                    {
                        var en = (ColorChangers)Enum.Parse(typeof(ColorChangers), input[++i]);
                        ControlsColors.Add(en, new ColorPalette());
                        foreach (ColorPaletteSelectables en2 in Enum.GetValues(typeof(ColorPaletteSelectables)))
                        {
                            ControlsColors[en].SetColor(en2, Color.FromArgb(int.Parse(input[++i])));
                        }
                    }
                    i++;
                }
                else if (input[i].StartsWith("TIMEINTERVAL:") && TimeInterval != null)
                {
                    TimeInterval.StartDate = DateTime.FromBinary(long.Parse(input[++i]));
                    TimeInterval.EndDate = DateTime.FromBinary(long.Parse(input[++i]));
                }
            }
        }

        public static string[] ToStrings()
        {
            var result = new List<string>();
            result.Add("COLORS:");
            foreach (var pair in ControlsColors)
            {
                result.Add(pair.Key.ToString());
                foreach (ColorPaletteSelectables en2 in Enum.GetValues(typeof(ColorPaletteSelectables)))
                {
                    result.Add(pair.Value.GetColor(en2).ToArgb().ToString());
                }
            }

            result.Add("TIMEINTERVAL:");
            if (TimeInterval != null)
            {
                result.Add(TimeInterval.StartDate.ToBinary().ToString());
                result.Add(TimeInterval.EndDate.ToBinary().ToString());
            }
            else
            {
                result.Add(DateTime.Now.ToBinary().ToString());
                result.Add(DateTime.Now.AddHours(1).ToBinary().ToString());
            }

            return result.ToArray();
        }
    }
}
