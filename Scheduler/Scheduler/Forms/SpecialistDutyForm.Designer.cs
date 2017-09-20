namespace Scheduler.Forms
{
    partial class SpecialistDutyForm
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
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.grpDateTime = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.grpSelectedSpecialists = new System.Windows.Forms.GroupBox();
            this.lstDuty = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstSpecialists = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnToDuty = new System.Windows.Forms.Button();
            this.btnToSupplimentary = new System.Windows.Forms.Button();
            this.btnFromDuty = new System.Windows.Forms.Button();
            this.grpDateTime.SuspendLayout();
            this.grpSelectedSpecialists.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(9, 65);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // grpDateTime
            // 
            this.grpDateTime.Controls.Add(this.label10);
            this.grpDateTime.Controls.Add(this.label9);
            this.grpDateTime.Controls.Add(this.monthCalendar1);
            this.grpDateTime.Controls.Add(this.dateTimeStart);
            this.grpDateTime.Controls.Add(this.dateTimeEnd);
            this.grpDateTime.Location = new System.Drawing.Point(351, 12);
            this.grpDateTime.Name = "grpDateTime";
            this.grpDateTime.Size = new System.Drawing.Size(177, 239);
            this.grpDateTime.TabIndex = 31;
            this.grpDateTime.TabStop = false;
            this.grpDateTime.Text = "Дата приёма";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(95, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "по";
            this.label10.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "с";
            this.label9.Visible = false;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "HH:mm";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(25, 16);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowUpDown = true;
            this.dateTimeStart.Size = new System.Drawing.Size(51, 20);
            this.dateTimeStart.TabIndex = 21;
            this.dateTimeStart.Value = new System.DateTime(2014, 2, 2, 13, 51, 0, 0);
            this.dateTimeStart.Visible = false;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "HH:mm";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(120, 16);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowUpDown = true;
            this.dateTimeEnd.Size = new System.Drawing.Size(51, 20);
            this.dateTimeEnd.TabIndex = 22;
            this.dateTimeEnd.Value = new System.DateTime(2014, 2, 2, 13, 51, 0, 0);
            this.dateTimeEnd.Visible = false;
            // 
            // grpSelectedSpecialists
            // 
            this.grpSelectedSpecialists.Controls.Add(this.lstDuty);
            this.grpSelectedSpecialists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSelectedSpecialists.Location = new System.Drawing.Point(3, 3);
            this.grpSelectedSpecialists.Name = "grpSelectedSpecialists";
            this.grpSelectedSpecialists.Size = new System.Drawing.Size(327, 93);
            this.grpSelectedSpecialists.TabIndex = 33;
            this.grpSelectedSpecialists.TabStop = false;
            this.grpSelectedSpecialists.Text = "Специалисты на дежурстве";
            // 
            // lstDuty
            // 
            this.lstDuty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDuty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstDuty.FormattingEnabled = true;
            this.lstDuty.Location = new System.Drawing.Point(3, 16);
            this.lstDuty.Name = "lstDuty";
            this.lstDuty.Size = new System.Drawing.Size(321, 74);
            this.lstDuty.TabIndex = 0;
            this.lstDuty.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstDuty_DrawItem);
            this.lstDuty.SelectedIndexChanged += new System.EventHandler(this.lstDuty_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstSpecialists);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 94);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Специалисты";
            // 
            // lstSpecialists
            // 
            this.lstSpecialists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSpecialists.FormattingEnabled = true;
            this.lstSpecialists.Location = new System.Drawing.Point(3, 16);
            this.lstSpecialists.Name = "lstSpecialists";
            this.lstSpecialists.Size = new System.Drawing.Size(321, 75);
            this.lstSpecialists.TabIndex = 0;
            this.lstSpecialists.SelectedIndexChanged += new System.EventHandler(this.lstSpecialists_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grpSelectedSpecialists, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(333, 239);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.btnToDuty, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnToSupplimentary, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFromDuty, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 102);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(327, 34);
            this.tableLayoutPanel2.TabIndex = 35;
            // 
            // btnToDuty
            // 
            this.btnToDuty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToDuty.Location = new System.Drawing.Point(3, 3);
            this.btnToDuty.Name = "btnToDuty";
            this.btnToDuty.Size = new System.Drawing.Size(103, 28);
            this.btnToDuty.TabIndex = 0;
            this.btnToDuty.Text = "Дежурство";
            this.btnToDuty.UseVisualStyleBackColor = true;
            this.btnToDuty.Click += new System.EventHandler(this.btnToDuty_Click);
            // 
            // btnToSupplimentary
            // 
            this.btnToSupplimentary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToSupplimentary.Location = new System.Drawing.Point(112, 3);
            this.btnToSupplimentary.Name = "btnToSupplimentary";
            this.btnToSupplimentary.Size = new System.Drawing.Size(103, 28);
            this.btnToSupplimentary.TabIndex = 1;
            this.btnToSupplimentary.Text = "Подхват";
            this.btnToSupplimentary.UseVisualStyleBackColor = true;
            this.btnToSupplimentary.Click += new System.EventHandler(this.btnToSupplimentary_Click);
            // 
            // btnFromDuty
            // 
            this.btnFromDuty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFromDuty.Location = new System.Drawing.Point(221, 3);
            this.btnFromDuty.Name = "btnFromDuty";
            this.btnFromDuty.Size = new System.Drawing.Size(103, 28);
            this.btnFromDuty.TabIndex = 2;
            this.btnFromDuty.Text = "Снять";
            this.btnFromDuty.UseVisualStyleBackColor = true;
            this.btnFromDuty.Click += new System.EventHandler(this.btnFromDuty_Click);
            // 
            // SpecialistDutyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 258);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.grpDateTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SpecialistDutyForm";
            this.Text = "Специалисты на дежурстве";
            this.grpDateTime.ResumeLayout(false);
            this.grpDateTime.PerformLayout();
            this.grpSelectedSpecialists.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.GroupBox grpDateTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.GroupBox grpSelectedSpecialists;
        private System.Windows.Forms.ListBox lstDuty;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstSpecialists;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnToDuty;
        private System.Windows.Forms.Button btnToSupplimentary;
        private System.Windows.Forms.Button btnFromDuty;
    }
}