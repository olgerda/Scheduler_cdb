﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class SelectEndDate : Form
    {
        public SelectEndDate()
        {
            InitializeComponent();
            radbtnTypeEveryWeek.Checked = true;
            radbtnTypeCustom.Checked = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
