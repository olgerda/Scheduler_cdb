namespace Scheduler.Forms
{
    partial class SelectEndDate
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radbtnTypeEveryWeek = new System.Windows.Forms.RadioButton();
            this.radbtnTypeCustom = new System.Windows.Forms.RadioButton();
            this.numCustomDays = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomDays)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(18, 18);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numCustomDays);
            this.groupBox1.Controls.Add(this.radbtnTypeCustom);
            this.groupBox1.Controls.Add(this.radbtnTypeEveryWeek);
            this.groupBox1.Location = new System.Drawing.Point(194, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип автосоздания";
            // 
            // radbtnTypeEveryWeek
            // 
            this.radbtnTypeEveryWeek.AutoSize = true;
            this.radbtnTypeEveryWeek.Location = new System.Drawing.Point(6, 19);
            this.radbtnTypeEveryWeek.Name = "radbtnTypeEveryWeek";
            this.radbtnTypeEveryWeek.Size = new System.Drawing.Size(94, 17);
            this.radbtnTypeEveryWeek.TabIndex = 0;
            this.radbtnTypeEveryWeek.TabStop = true;
            this.radbtnTypeEveryWeek.Text = "Еженедельно";
            this.radbtnTypeEveryWeek.UseVisualStyleBackColor = true;
            // 
            // radbtnTypeCustom
            // 
            this.radbtnTypeCustom.AutoSize = true;
            this.radbtnTypeCustom.Location = new System.Drawing.Point(6, 42);
            this.radbtnTypeCustom.Name = "radbtnTypeCustom";
            this.radbtnTypeCustom.Size = new System.Drawing.Size(150, 17);
            this.radbtnTypeCustom.TabIndex = 1;
            this.radbtnTypeCustom.TabStop = true;
            this.radbtnTypeCustom.Text = "Каждые несколько дней";
            this.radbtnTypeCustom.UseVisualStyleBackColor = true;
            // 
            // numCustomDays
            // 
            this.numCustomDays.Location = new System.Drawing.Point(156, 42);
            this.numCustomDays.Maximum = new decimal(new int[] {
            366,
            0,
            0,
            0});
            this.numCustomDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCustomDays.Name = "numCustomDays";
            this.numCustomDays.Size = new System.Drawing.Size(38, 20);
            this.numCustomDays.TabIndex = 2;
            this.numCustomDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SelectEndDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 195);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.monthCalendar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectEndDate";
            this.Text = "SelectEndDate";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomDays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown numCustomDays;
        public System.Windows.Forms.RadioButton radbtnTypeCustom;
        public System.Windows.Forms.RadioButton radbtnTypeEveryWeek;
    }
}