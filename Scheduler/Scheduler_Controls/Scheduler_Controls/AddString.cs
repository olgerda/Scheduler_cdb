using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduler_Controls
{
    public partial class AddString : Form
    {
        private string text;
        public string TextInputed { get { return text; } }
        public AddString()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
        }


    }
}
