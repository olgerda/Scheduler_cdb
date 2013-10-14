namespace Scheduler
{
    partial class EditReception
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.datetimePickers = new System.Windows.Forms.GroupBox();
            this.timePickerStart = new System.Windows.Forms.DateTimePicker();
            this.timePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.calendarPicker = new System.Windows.Forms.MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.datetimePickers.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.datetimePickers);
            this.splitContainer1.Size = new System.Drawing.Size(1035, 640);
            this.splitContainer1.SplitterDistance = 304;
            this.splitContainer1.TabIndex = 0;
            // 
            // datePicker
            // 
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(7, 16);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(186, 20);
            this.datePicker.TabIndex = 1;
            this.datePicker.ValueChanged += new System.EventHandler(this.BoxDateChangedHandler);
            // 
            // datetimePickers
            // 
            this.datetimePickers.Controls.Add(this.calendarPicker);
            this.datetimePickers.Controls.Add(this.timePickerEnd);
            this.datetimePickers.Controls.Add(this.timePickerStart);
            this.datetimePickers.Controls.Add(this.datePicker);
            this.datetimePickers.Location = new System.Drawing.Point(515, 12);
            this.datetimePickers.Name = "datetimePickers";
            this.datetimePickers.Size = new System.Drawing.Size(200, 228);
            this.datetimePickers.TabIndex = 2;
            this.datetimePickers.TabStop = false;
            this.datetimePickers.Text = "Дата и время";
            // 
            // timePickerStart
            // 
            this.timePickerStart.CustomFormat = "H:mm";
            this.timePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerStart.Location = new System.Drawing.Point(7, 202);
            this.timePickerStart.Name = "timePickerStart";
            this.timePickerStart.ShowUpDown = true;
            this.timePickerStart.Size = new System.Drawing.Size(89, 20);
            this.timePickerStart.TabIndex = 2;
            this.timePickerStart.Value = new System.DateTime(2013, 10, 14, 16, 4, 0, 0);
            // 
            // timePickerEnd
            // 
            this.timePickerEnd.CustomFormat = "H:mm";
            this.timePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerEnd.Location = new System.Drawing.Point(102, 202);
            this.timePickerEnd.Name = "timePickerEnd";
            this.timePickerEnd.ShowUpDown = true;
            this.timePickerEnd.Size = new System.Drawing.Size(91, 20);
            this.timePickerEnd.TabIndex = 3;
            // 
            // calendarPicker
            // 
            this.calendarPicker.Location = new System.Drawing.Point(7, 38);
            this.calendarPicker.MaxSelectionCount = 1;
            this.calendarPicker.Name = "calendarPicker";
            this.calendarPicker.ShowWeekNumbers = true;
            this.calendarPicker.TabIndex = 4;
            this.calendarPicker.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.DateChangedHandler);
            // 
            // EditReception
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 640);
            this.Controls.Add(this.splitContainer1);
            this.Name = "EditReception";
            this.Text = "EditReception";
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.datetimePickers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox datetimePickers;
        private System.Windows.Forms.DateTimePicker timePickerEnd;
        private System.Windows.Forms.DateTimePicker timePickerStart;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.MonthCalendar calendarPicker;
    }
}