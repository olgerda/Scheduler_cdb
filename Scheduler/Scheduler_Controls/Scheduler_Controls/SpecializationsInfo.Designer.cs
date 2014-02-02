namespace Scheduler_Controls
{
    partial class SpecializationsInfo
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstSpecializations = new System.Windows.Forms.ListBox();
            this.btnCommit = new System.Windows.Forms.Button();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnCommit);
            this.grpMain.Controls.Add(this.btnRemove);
            this.grpMain.Controls.Add(this.btnAdd);
            this.grpMain.Controls.Add(this.lstSpecializations);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(196, 242);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Карточка специализаций";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(113, 185);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 185);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstSpecializations
            // 
            this.lstSpecializations.FormattingEnabled = true;
            this.lstSpecializations.Location = new System.Drawing.Point(6, 19);
            this.lstSpecializations.Name = "lstSpecializations";
            this.lstSpecializations.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSpecializations.Size = new System.Drawing.Size(182, 160);
            this.lstSpecializations.TabIndex = 0;
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(6, 214);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(182, 23);
            this.btnCommit.TabIndex = 3;
            this.btnCommit.Text = "Сохранить изменения";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // SpecializationsInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "SpecializationsInfo";
            this.Size = new System.Drawing.Size(196, 242);
            this.grpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lstSpecializations;
        private System.Windows.Forms.Button btnCommit;
    }
}
