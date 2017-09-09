using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Scheduler.Main
{
    class ProgramSettings
    {
        private Dictionary<Scheduler.Forms.SettingsForm.ColorChangers, Controls.ColorPicker.ControlsColors>
            _controlsColors;
        public Dictionary<Scheduler.Forms.SettingsForm.ColorChangers, Controls.ColorPicker.ControlsColors> ControlsColors
        {
            get { return _controlsColors; }
            set
            {
                _controlsColors = value;
                onSettingsChanged?.Invoke();
            }
        }

        private Scheduler_Controls_Interfaces.ITimeInterval _timeInterval;
        public Scheduler_Controls_Interfaces.ITimeInterval TimeInterval
        {
            get { return _timeInterval; }
            set
            {
                _timeInterval = value;
                onSettingsChanged?.Invoke();
            }
        }

        public event Action onSettingsChanged;

        public ProgramSettings(Scheduler_Controls_Interfaces.ITimeInterval timeInterval)
        {
            TimeInterval = timeInterval;
        }

        public void FromString(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("COLORS:"))
                {
                    for (var j = 0; i < Enum.GetValues(typeof(Scheduler.Forms.SettingsForm.ColorChangers)).Length; j++)
                    {
                        var en = (Scheduler.Forms.SettingsForm.ColorChangers)Enum.Parse(
                            typeof(Scheduler.Forms.SettingsForm.ColorChangers),
                            input[++i]);
                        ControlsColors.Add(en, new Controls.ColorPicker.ControlsColors());
                        ControlsColors[en].ColorMain = Color.FromArgb(int.Parse(input[++i]));
                        ControlsColors[en].ColorBorder = Color.FromArgb(int.Parse(input[++i]));
                        ControlsColors[en].ColorBackground = Color.FromArgb(int.Parse(input[++i]));
                    }
                    i++;
                }
                else if (input[i].StartsWith("TIMEINTERVAL:"))
                {
                    TimeInterval.StartDate = DateTime.FromBinary(long.Parse(input[++i]));
                    TimeInterval.EndDate = DateTime.FromBinary(long.Parse(input[++i]));
                }
            }
            onSettingsChanged?.Invoke();
        }

        public IEnumerable<string> ToStrings()
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
            result.Add(TimeInterval.StartDate.ToBinary().ToString());
            result.Add(TimeInterval.EndDate.ToBinary().ToString());

            return result;
        }
    }
}
