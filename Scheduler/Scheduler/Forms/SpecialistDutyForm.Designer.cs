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
            this.radModeAdministrator = new System.Windows.Forms.RadioButton();
            this.radModeSpecialist = new System.Windows.Forms.RadioButton();
            this.grpSpecialistsOnDuty = new System.Windows.Forms.GroupBox();
            this.lstDuty = new System.Windows.Forms.ListBox();
            this.grpSpecialists = new System.Windows.Forms.GroupBox();
            this.lstSpecialists = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnToDuty = new System.Windows.Forms.Button();
            this.btnToSupplimentary = new System.Windows.Forms.Button();
            this.btnFromDuty = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpDateTime.SuspendLayout();
            this.grpSpecialistsOnDuty.SuspendLayout();
            this.grpSpecialists.SuspendLayout();
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
            this.grpDateTime.Controls.Add(this.btnOK);
            this.grpDateTime.Controls.Add(this.radModeAdministrator);
            this.grpDateTime.Controls.Add(this.radModeSpecialist);
            this.grpDateTime.Controls.Add(this.monthCalendar1);
            this.grpDateTime.Location = new System.Drawing.Point(351, 12);
            this.grpDateTime.Name = "grpDateTime";
            this.grpDateTime.Size = new System.Drawing.Size(177, 306);
            this.grpDateTime.TabIndex = 31;
            this.grpDateTime.TabStop = false;
            this.grpDateTime.Text = "Дата приёма";
            // 
            // radModeAdministrator
            // 
            this.radModeAdministrator.AutoSize = true;
            this.radModeAdministrator.Location = new System.Drawing.Point(9, 42);
            this.radModeAdministrator.Name = "radModeAdministrator";
            this.radModeAdministrator.Size = new System.Drawing.Size(112, 17);
            this.radModeAdministrator.TabIndex = 2;
            this.radModeAdministrator.TabStop = true;
            this.radModeAdministrator.Text = "Администраторы";
            this.radModeAdministrator.UseVisualStyleBackColor = true;
            this.radModeAdministrator.CheckedChanged += new System.EventHandler(this.radModeAdministrator_CheckedChanged);
            // 
            // radModeSpecialist
            // 
            this.radModeSpecialist.AutoSize = true;
            this.radModeSpecialist.Location = new System.Drawing.Point(9, 19);
            this.radModeSpecialist.Name = "radModeSpecialist";
            this.radModeSpecialist.Size = new System.Drawing.Size(93, 17);
            this.radModeSpecialist.TabIndex = 1;
            this.radModeSpecialist.TabStop = true;
            this.radModeSpecialist.Text = "Специалисты";
            this.radModeSpecialist.UseVisualStyleBackColor = true;
            this.radModeSpecialist.CheckedChanged += new System.EventHandler(this.radModeAdministrator_CheckedChanged);
            // 
            // grpSpecialistsOnDuty
            // 
            this.grpSpecialistsOnDuty.Controls.Add(this.lstDuty);
            this.grpSpecialistsOnDuty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSpecialistsOnDuty.Location = new System.Drawing.Point(3, 3);
            this.grpSpecialistsOnDuty.Name = "grpSpecialistsOnDuty";
            this.grpSpecialistsOnDuty.Size = new System.Drawing.Size(327, 127);
            this.grpSpecialistsOnDuty.TabIndex = 33;
            this.grpSpecialistsOnDuty.TabStop = false;
            this.grpSpecialistsOnDuty.Text = "Специалисты на дежурстве";
            // 
            // lstDuty
            // 
            this.lstDuty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDuty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstDuty.FormattingEnabled = true;
            this.lstDuty.Location = new System.Drawing.Point(3, 16);
            this.lstDuty.Name = "lstDuty";
            this.lstDuty.Size = new System.Drawing.Size(321, 108);
            this.lstDuty.TabIndex = 0;
            this.lstDuty.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstDuty_DrawItem);
            this.lstDuty.SelectedIndexChanged += new System.EventHandler(this.lstDuty_SelectedIndexChanged);
            // 
            // grpSpecialists
            // 
            this.grpSpecialists.Controls.Add(this.lstSpecialists);
            this.grpSpecialists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSpecialists.Location = new System.Drawing.Point(3, 176);
            this.grpSpecialists.Name = "grpSpecialists";
            this.grpSpecialists.Size = new System.Drawing.Size(327, 127);
            this.grpSpecialists.TabIndex = 34;
            this.grpSpecialists.TabStop = false;
            this.grpSpecialists.Text = "Специалисты";
            // 
            // lstSpecialists
            // 
            this.lstSpecialists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSpecialists.FormattingEnabled = true;
            this.lstSpecialists.Location = new System.Drawing.Point(3, 16);
            this.lstSpecialists.Name = "lstSpecialists";
            this.lstSpecialists.Size = new System.Drawing.Size(321, 108);
            this.lstSpecialists.TabIndex = 0;
            this.lstSpecialists.SelectedIndexChanged += new System.EventHandler(this.lstSpecialists_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grpSpecialistsOnDuty, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpSpecialists, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(333, 306);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 136);
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
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(9, 277);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(164, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Закрыть";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SpecialistDutyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 330);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.grpDateTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SpecialistDutyForm";
            this.Text = "Специалисты на дежурстве";
            this.grpDateTime.ResumeLayout(false);
            this.grpDateTime.PerformLayout();
            this.grpSpecialistsOnDuty.ResumeLayout(false);
            this.grpSpecialists.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.GroupBox grpDateTime;
        private System.Windows.Forms.GroupBox grpSpecialistsOnDuty;
        private System.Windows.Forms.ListBox lstDuty;
        private System.Windows.Forms.GroupBox grpSpecialists;
        private System.Windows.Forms.ListBox lstSpecialists;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnToDuty;
        private System.Windows.Forms.Button btnToSupplimentary;
        private System.Windows.Forms.Button btnFromDuty;
        private System.Windows.Forms.RadioButton radModeAdministrator;
        private System.Windows.Forms.RadioButton radModeSpecialist;
        private System.Windows.Forms.Button btnOK;
    }
}