using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler.Controls
{
    public partial class EditItemWithDescriptions : UserControl
    {
        public EditItemWithDescriptions(Type type = null)
        {
            InitializeComponent();

            if (type == null)
                return;

            var props = type.GetProperties(System.Reflection.BindingFlags.Public);

        }



    }
}
