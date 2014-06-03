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
            this.btnCreateReportClient = new System.Windows.Forms.Button();
            this.btnCreateReportSpecialist = new System.Windows.Forms.Button();
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
            // btnCreateReportClient
            // 
            this.btnCreateReportClient.Location = new System.Drawing.Point(12, 90);
            this.btnCreateReportClient.Name = "btnCreateReportClient";
            this.btnCreateReportClient.Size = new System.Drawing.Size(213, 25);
            this.btnCreateReportClient.TabIndex = 2;
            this.btnCreateReportClient.Text = "Сформировать отчёт по клиентам";
            this.btnCreateReportClient.UseVisualStyleBackColor = true;
            this.btnCreateReportClient.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // btnCreateReportSpecialist
            // 
            this.btnCreateReportSpecialist.Location = new System.Drawing.Point(12, 121);
            this.btnCreateReportSpecialist.Name = "btnCreateReportSpecialist";
            this.btnCreateReportSpecialist.Size = new System.Drawing.Size(213, 23);
            this.btnCreateReportSpecialist.TabIndex = 3;
            this.btnCreateReportSpecialist.Text = "Сформировать отчёт по специалистам";
            this.btnCreateReportSpecialist.UseVisualStyleBackColor = true;
            this.btnCreateReportSpecialist.Click += new System.EventHandler(this.btnCreateReportSpecialist_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 151);
            this.Controls.Add(this.btnCreateReportSpecialist);
            this.Controls.Add(this.btnCreateReportClient);
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
        private System.Windows.Forms.Button btnCreateReportClient;
        private System.Windows.Forms.Button btnCreateReportSpecialist;
    }
}