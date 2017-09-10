using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Scheduler_InterfacesRealisations;

namespace Scheduler.Main
{
    class ProgramSettings
    {
        private static Dictionary<Forms.SettingsForm.ColorChangers, ControlsColors> _default_controlsColors =
            new Dictionary<Forms.SettingsForm.ColorChangers, ControlsColors>()
            {
                {Forms.SettingsForm.ColorChangers.Table, new ControlsColors() { ColorMain = Color.Black, ColorBorder = Color.LightSeaGreen, ColorBackground = Color.Black, Font = new Font("Arial", 10)}},
                {Forms.SettingsForm.ColorChangers.Column, new ControlsColors() { ColorMain = Color.Black, ColorBorder = Color.DarkGreen, ColorBackground = Color.Black, Font = new Font("Arial", 10)}},
                {Forms.SettingsForm.ColorChangers.Entity1, new ControlsColors() { ColorMain = Color.Black, ColorBorder = Color.DarkGreen, ColorBackground = Color.LightYellow, Font = new Font("Arial", 10)} },
                {Forms.SettingsForm.ColorChangers.Entity2, new ControlsColors() { ColorMain = Color.Black, ColorBorder = Color.DarkGreen, ColorBackground = Color.LightYellow, Font = new Font("Arial", 10)} }
            };

        private Dictionary<Forms.SettingsForm.ColorChangers, ControlsColors>
            _controlsColors;
        public Dictionary<Forms.SettingsForm.ColorChangers, ControlsColors> ControlsColors
        {
            get { return _controlsColors; }
            set
            {
                _controlsColors = value;
                if (!suspendEnents && onSettingsChanged != null)
                    onSettingsChanged();
            }
        }

        private Scheduler_Controls_Interfaces.ITimeInterval _timeInterval;
        public Scheduler_Controls_Interfaces.ITimeInterval TimeInterval
        {
            get { return _timeInterval; }
            set
            {
                _timeInterval = value;
            }
        }

        public event Action onSettingsChanged;
        private bool suspendEnents = false;

        public ProgramSettings(Scheduler_Controls_Interfaces.ITimeInterval timeInterval)
        {
            TimeInterval = timeInterval;
        }

        public void FromStrings(string[] input)
        {
            suspendEnents = true;
            if (input == null || input.Length < Enum.GetValues(typeof(Forms.SettingsForm.ColorChangers)).Length + 3)
            {
                ControlsColors = _default_controlsColors;
                FromStrings(ToStrings());
                return;
            }

            ControlsColors = new Dictionary<Forms.SettingsForm.ColorChangers, ControlsColors>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("COLORS:"))
                {
                    for (var j = 0; j < Enum.GetValues(typeof(Forms.SettingsForm.ColorChangers)).Length; j++)
                    {
                        var en = (Forms.SettingsForm.ColorChangers)Enum.Parse(
                            typeof(Forms.SettingsForm.ColorChangers),
                            input[++i]);
                        ControlsColors.Add(en, new ControlsColors());
                        ControlsColors[en].ColorMain = Color.FromArgb(int.Parse(input[++i]));
                        ControlsColors[en].ColorBorder = Color.FromArgb(int.Parse(input[++i]));
                        ControlsColors[en].ColorBackground = Color.FromArgb(int.Parse(input[++i]));
                    }
                    i++;
                }
                else if (input[i].StartsWith("TIMEINTERVAL:") && TimeInterval != null)
                {
                    
                    TimeInterval.StartDate = DateTime.FromBinary(long.Parse(input[++i]));
                    TimeInterval.EndDate = DateTime.FromBinary(long.Parse(input[++i]));
                }
            }
            onSettingsChanged?.Invoke();
            suspendEnents = false;
        }

        public string[] ToStrings()
        {
            var result = new List<string>();
            result.Add("COLORS:");
            foreach (var pair in ControlsColors)
            {
                result.Add(pair.Key.ToString());
                result.Add(pair.Value.ColorMain.ToArgb().ToString());
                result.Add(pair.Value.ColorBorder.ToArgb().ToString());
                result.Add(pair.Value.ColorBackground.ToArgb().ToString());
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
