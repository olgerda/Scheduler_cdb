namespace Scheduler_Forms
{
    partial class Reports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.date2 = new System.Windows.Forms.DateTimePicker();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.cmbReportKindSelect = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.date2);
            this.groupBox2.Controls.Add(this.date1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Период отчёта";
            // 
            // date2
            // 
            this.date2.Location = new System.Drawing.Point(6, 45);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(201, 20);
            this.date2.TabIndex = 1;
            this.date2.CloseUp += new System.EventHandler(this.date2_ValueChanged);
            // 
            // date1
            // 
            this.date1.Location = new System.Drawing.Point(6, 19);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(201, 20);
            this.date1.TabIndex = 0;
            this.date1.CloseUp += new System.EventHandler(this.date1_ValueChanged);
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Location = new System.Drawing.Point(12, 148);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(213, 25);
            this.btnCreateReport.TabIndex = 2;
            this.btnCreateReport.Text = "Сформировать отчёт";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // cmbReportKindSelect
            // 
            this.cmbReportKindSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportKindSelect.Items.AddRange(new object[] {
            "Отчёт по специалистам",
            "Отчёт по кабинетам",
            "Отчёт по специализациям"});
            this.cmbReportKindSelect.Location = new System.Drawing.Point(12, 89);
            this.cmbReportKindSelect.Name = "cmbReportKindSelect";
            this.cmbReportKindSelect.Size = new System.Drawing.Size(213, 21);
            this.cmbReportKindSelect.TabIndex = 3;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "report.csv";
            this.saveFileDialog1.Filter = "Excel csv|*.csv";
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 178);
            this.Controls.Add(this.cmbReportKindSelect);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker date2;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.ComboBox cmbReportKindSelect;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}