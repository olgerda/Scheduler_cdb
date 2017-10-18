namespace Scheduler.Forms
{
    partial class SettingsForm
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
            Scheduler_InterfacesRealisations.ControlsColors controlsColors1 = new Scheduler_InterfacesRealisations.ControlsColors();
            Scheduler_InterfacesRealisations.ControlsColors controlsColors2 = new Scheduler_InterfacesRealisations.ControlsColors();
            Scheduler_InterfacesRealisations.ControlsColors controlsColors3 = new Scheduler_InterfacesRealisations.ControlsColors();
            Scheduler_InterfacesRealisations.ControlsColors controlsColors4 = new Scheduler_InterfacesRealisations.ControlsColors();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.columnsControl = new CalendarControl3.ColumnsView();
            this.grpColors = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpColorsArendators = new System.Windows.Forms.GroupBox();
            this.colorPicker4 = new Scheduler.Controls.ColorPicker();
            this.grpColorsMainTable = new System.Windows.Forms.GroupBox();
            this.colorPicker1 = new Scheduler.Controls.ColorPicker();
            this.grpColorsColumns = new System.Windows.Forms.GroupBox();
            this.colorPicker2 = new Scheduler.Controls.ColorPicker();
            this.grpColorsClients = new System.Windows.Forms.GroupBox();
            this.colorPicker3 = new Scheduler.Controls.ColorPicker();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.grpTest.SuspendLayout();
            this.grpColors.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpColorsArendators.SuspendLayout();
            this.grpColorsMainTable.SuspendLayout();
            this.grpColorsColumns.SuspendLayout();
            this.grpColorsClients.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 532);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 58);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(231, 549);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 41);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.columnsControl);
            this.grpTest.Enabled = false;
            this.grpTest.Location = new System.Drawing.Point(330, 12);
            this.grpTest.Name = "grpTest";
            this.grpTest.Size = new System.Drawing.Size(306, 578);
            this.grpTest.TabIndex = 2;
            this.grpTest.TabStop = false;
            this.grpTest.Text = "Тест";
            // 
            // columnsControl
            // 
            this.columnsControl.BackColor = System.Drawing.Color.White;
            this.columnsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnsControl.Location = new System.Drawing.Point(3, 16);
            this.columnsControl.MinimumSize = new System.Drawing.Size(150, 500);
            this.columnsControl.Name = "columnsControl";
            this.columnsControl.Size = new System.Drawing.Size(300, 559);
            this.columnsControl.TabIndex = 0;
            this.columnsControl.Table = null;
            // 
            // grpColors
            // 
            this.grpColors.Controls.Add(this.tableLayoutPanel1);
            this.grpColors.Location = new System.Drawing.Point(12, 12);
            this.grpColors.Name = "grpColors";
            this.grpColors.Size = new System.Drawing.Size(312, 514);
            this.grpColors.TabIndex = 3;
            this.grpColors.TabStop = false;
            this.grpColors.Text = "Цветовая схема";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grpColorsArendators, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.grpColorsMainTable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpColorsColumns, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpColorsClients, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 495);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grpColorsArendators
            // 
            this.grpColorsArendators.Controls.Add(this.colorPicker4);
            this.grpColorsArendators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpColorsArendators.Location = new System.Drawing.Point(3, 372);
            this.grpColorsArendators.Name = "grpColorsArendators";
            this.grpColorsArendators.Size = new System.Drawing.Size(300, 120);
            this.grpColorsArendators.TabIndex = 3;
            this.grpColorsArendators.TabStop = false;
            this.grpColorsArendators.Text = "Записи арендаторов";
            // 
            // colorPicker4
            // 
            this.colorPicker4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorPicker4.Location = new System.Drawing.Point(3, 16);
            this.colorPicker4.MinimumSize = new System.Drawing.Size(55, 50);
            this.colorPicker4.Name = "colorPicker4";
            this.colorPicker4.Size = new System.Drawing.Size(294, 101);
            this.colorPicker4.TabIndex = 1;
            // 
            // grpColorsMainTable
            // 
            this.grpColorsMainTable.Controls.Add(this.colorPicker1);
            this.grpColorsMainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpColorsMainTable.Location = new System.Drawing.Point(3, 3);
            this.grpColorsMainTable.Name = "grpColorsMainTable";
            this.grpColorsMainTable.Size = new System.Drawing.Size(300, 117);
            this.grpColorsMainTable.TabIndex = 0;
            this.grpColorsMainTable.TabStop = false;
            this.grpColorsMainTable.Text = "Основная таблица";
            // 
            // colorPicker1
            // 
            this.colorPicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorPicker1.Location = new System.Drawing.Point(3, 16);
            this.colorPicker1.MinimumSize = new System.Drawing.Size(55, 50);
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(294, 98);
            this.colorPicker1.TabIndex = 0;
            // 
            // grpColorsColumns
            // 
            this.grpColorsColumns.Controls.Add(this.colorPicker2);
            this.grpColorsColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpColorsColumns.Location = new System.Drawing.Point(3, 126);
            this.grpColorsColumns.Name = "grpColorsColumns";
            this.grpColorsColumns.Size = new System.Drawing.Size(300, 117);
            this.grpColorsColumns.TabIndex = 1;
            this.grpColorsColumns.TabStop = false;
            this.grpColorsColumns.Text = "Столбцы";
            // 
            // colorPicker2
            // 
            this.colorPicker2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorPicker2.Location = new System.Drawing.Point(3, 16);
            this.colorPicker2.MinimumSize = new System.Drawing.Size(55, 50);
            this.colorPicker2.Name = "colorPicker2";
            this.colorPicker2.Size = new System.Drawing.Size(294, 98);
            this.colorPicker2.TabIndex = 1;
            // 
            // grpColorsClients
            // 
            this.grpColorsClients.Controls.Add(this.colorPicker3);
            this.grpColorsClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpColorsClients.Location = new System.Drawing.Point(3, 249);
            this.grpColorsClients.Name = "grpColorsClients";
            this.grpColorsClients.Size = new System.Drawing.Size(300, 117);
            this.grpColorsClients.TabIndex = 2;
            this.grpColorsClients.TabStop = false;
            this.grpColorsClients.Text = "Записи основные";
            // 
            // colorPicker3
            // 
            this.colorPicker3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorPicker3.Location = new System.Drawing.Point(3, 16);
            this.colorPicker3.MinimumSize = new System.Drawing.Size(55, 50);
            this.colorPicker3.Name = "colorPicker3";
            this.colorPicker3.Size = new System.Drawing.Size(294, 98);
            this.colorPicker3.TabIndex = 1;
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Location = new System.Drawing.Point(116, 558);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(96, 32);
            this.btnRestoreDefaults.TabIndex = 4;
            this.btnRestoreDefaults.Text = "По умолчанию";
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 602);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Controls.Add(this.grpColors);
            this.Controls.Add(this.grpTest);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.grpTest.ResumeLayout(false);
            this.grpColors.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpColorsArendators.ResumeLayout(false);
            this.grpColorsMainTable.ResumeLayout(false);
            this.grpColorsColumns.ResumeLayout(false);
            this.grpColorsClients.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpTest;
        private CalendarControl3.ColumnsView columnsControl;
        private System.Windows.Forms.GroupBox grpColors;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpColorsArendators;
        private System.Windows.Forms.GroupBox grpColorsMainTable;
        private System.Windows.Forms.GroupBox grpColorsColumns;
        private System.Windows.Forms.GroupBox grpColorsClients;
        private Controls.ColorPicker colorPicker4;
        private Controls.ColorPicker colorPicker1;
        private Controls.ColorPicker colorPicker2;
        private Controls.ColorPicker colorPicker3;
        private System.Windows.Forms.Button btnRestoreDefaults;
    }
}