namespace Scheduler_Controls
{
    partial class SpecialistInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lstSpecialisations = new System.Windows.Forms.CheckedListBox();
            this.chkNotWorking = new System.Windows.Forms.CheckBox();
            this.btnCommit = new System.Windows.Forms.Button();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя специалиста";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(9, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 20);
            this.txtName.TabIndex = 1;
            // 
            // lstSpecialisations
            // 
            this.lstSpecialisations.FormattingEnabled = true;
            this.lstSpecialisations.Location = new System.Drawing.Point(9, 73);
            this.lstSpecialisations.Name = "lstSpecialisations";
            this.lstSpecialisations.Size = new System.Drawing.Size(300, 109);
            this.lstSpecialisations.TabIndex = 2;
            // 
            // chkNotWorking
            // 
            this.chkNotWorking.AutoSize = true;
            this.chkNotWorking.Location = new System.Drawing.Point(9, 188);
            this.chkNotWorking.Name = "chkNotWorking";
            this.chkNotWorking.Size = new System.Drawing.Size(89, 17);
            this.chkNotWorking.TabIndex = 3;
            this.chkNotWorking.Text = "Не работает";
            this.chkNotWorking.UseVisualStyleBackColor = true;
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(9, 211);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(300, 23);
            this.btnCommit.TabIndex = 4;
            this.btnCommit.Text = "Сохранить изменения";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnCommit);
            this.grpMain.Controls.Add(this.txtName);
            this.grpMain.Controls.Add(this.chkNotWorking);
            this.grpMain.Controls.Add(this.lstSpecialisations);
            this.grpMain.Controls.Add(this.label2);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(315, 240);
            this.grpMain.TabIndex = 5;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Карточка специалиста";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Специализации";
            // 
            // SpecialistInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.MaximumSize = new System.Drawing.Size(315, 240);
            this.MinimumSize = new System.Drawing.Size(315, 240);
            this.Name = "SpecialistInfo";
            this.Size = new System.Drawing.Size(315, 240);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckedListBox lstSpecialisations;
        private System.Windows.Forms.CheckBox chkNotWorking;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label label2;
    }
}
