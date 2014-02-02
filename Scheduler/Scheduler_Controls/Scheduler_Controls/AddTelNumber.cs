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
    

    public partial class AddTelNumber : Form
    {
        public string number;

        public AddTelNumber()
        {
            InitializeComponent(); 
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            number = this.maskedTextBox1.Text;
        }
    }
}
