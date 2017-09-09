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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.columnsControl = new CalendarControl3.ColumnsView();
            this.grpTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 136);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 58);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(131, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 41);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.columnsControl);
            this.grpTest.Enabled = false;
            this.grpTest.Location = new System.Drawing.Point(233, 12);
            this.grpTest.Name = "grpTest";
            this.grpTest.Size = new System.Drawing.Size(328, 488);
            this.grpTest.TabIndex = 2;
            this.grpTest.TabStop = false;
            this.grpTest.Text = "Тест";
            // 
            // columnsControl
            // 
            //this.columnsControl.AutoScroll = true;
            this.columnsControl.BackColor = System.Drawing.Color.White;
            this.columnsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnsControl.Location = new System.Drawing.Point(3, 16);
            this.columnsControl.MinimumSize = new System.Drawing.Size(150, 500);
            this.columnsControl.Name = "columnsControl";
            this.columnsControl.Size = new System.Drawing.Size(322, 500);
            this.columnsControl.TabIndex = 0;
            this.columnsControl.Table = null;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 512);
            this.Controls.Add(this.grpTest);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.grpTest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpTest;
        private CalendarControl3.ColumnsView columnsControl;
    }
}