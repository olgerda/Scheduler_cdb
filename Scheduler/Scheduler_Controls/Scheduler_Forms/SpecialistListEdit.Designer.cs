namespace Scheduler_Forms
{
    partial class SpecialistListEdit
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
            this.specialistInfoCard = new Scheduler_Controls.SpecialistInfo();
            this.grpSelectSpecialist = new System.Windows.Forms.GroupBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.grpEditMode = new System.Windows.Forms.GroupBox();
            this.btnEditModeOff = new System.Windows.Forms.Button();
            this.txtSpecialistName = new System.Windows.Forms.TextBox();
            this.btnEditSpecialist = new System.Windows.Forms.Button();
            this.btnAddSpecialist = new System.Windows.Forms.Button();
            this.lstSpecialistList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpSelectSpecialist.SuspendLayout();
            this.grpEditMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // specialistInfoCard
            // 
            this.specialistInfoCard.Enabled = false;
            this.specialistInfoCard.Location = new System.Drawing.Point(285, 3);
            this.specialistInfoCard.MaximumSize = new System.Drawing.Size(315, 240);
            this.specialistInfoCard.MinimumSize = new System.Drawing.Size(315, 240);
            this.specialistInfoCard.Name = "specialistInfoCard";
            this.specialistInfoCard.Size = new System.Drawing.Size(315, 240);
            this.specialistInfoCard.Spec = null;
            this.specialistInfoCard.TabIndex = 0;
            // 
            // grpSelectSpecialist
            // 
            this.grpSelectSpecialist.Controls.Add(this.btnReturn);
            this.grpSelectSpecialist.Controls.Add(this.grpEditMode);
            this.grpSelectSpecialist.Controls.Add(this.txtSpecialistName);
            this.grpSelectSpecialist.Controls.Add(this.btnEditSpecialist);
            this.grpSelectSpecialist.Controls.Add(this.btnAddSpecialist);
            this.grpSelectSpecialist.Controls.Add(this.lstSpecialistList);
            this.grpSelectSpecialist.Controls.Add(this.label1);
            this.grpSelectSpecialist.Location = new System.Drawing.Point(3, 3);
            this.grpSelectSpecialist.Name = "grpSelectSpecialist";
            this.grpSelectSpecialist.Size = new System.Drawing.Size(276, 240);
            this.grpSelectSpecialist.TabIndex = 18;
            this.grpSelectSpecialist.TabStop = false;
            this.grpSelectSpecialist.Text = "Выбор специалиста";
            // 
            // btnReturn
            // 
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(197, 58);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(71, 23);
            this.btnReturn.TabIndex = 14;
            this.btnReturn.Text = "Возврат";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // grpEditMode
            // 
            this.grpEditMode.Controls.Add(this.btnEditModeOff);
            this.grpEditMode.Enabled = false;
            this.grpEditMode.Location = new System.Drawing.Point(0, 240);
            this.grpEditMode.Name = "grpEditMode";
            this.grpEditMode.Size = new System.Drawing.Size(276, 263);
            this.grpEditMode.TabIndex = 18;
            this.grpEditMode.TabStop = false;
            this.grpEditMode.Text = "Редактирование";
            this.grpEditMode.Visible = false;
            // 
            // btnEditModeOff
            // 
            this.btnEditModeOff.Location = new System.Drawing.Point(9, 84);
            this.btnEditModeOff.Name = "btnEditModeOff";
            this.btnEditModeOff.Size = new System.Drawing.Size(259, 72);
            this.btnEditModeOff.TabIndex = 0;
            this.btnEditModeOff.Text = "Закончить редактирование";
            this.btnEditModeOff.UseVisualStyleBackColor = true;
            this.btnEditModeOff.Click += new System.EventHandler(this.btnEditModeOff_Click);
            // 
            // txtSpecialistName
            // 
            this.txtSpecialistName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtSpecialistName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSpecialistName.Location = new System.Drawing.Point(9, 32);
            this.txtSpecialistName.Name = "txtSpecialistName";
            this.txtSpecialistName.Size = new System.Drawing.Size(260, 20);
            this.txtSpecialistName.TabIndex = 1;
            this.txtSpecialistName.TextChanged += new System.EventHandler(this.txtSpecialistName_TextChanged);
            // 
            // btnEditSpecialist
            // 
            this.btnEditSpecialist.Location = new System.Drawing.Point(90, 58);
            this.btnEditSpecialist.Name = "btnEditSpecialist";
            this.btnEditSpecialist.Size = new System.Drawing.Size(92, 23);
            this.btnEditSpecialist.TabIndex = 16;
            this.btnEditSpecialist.Text = "Редактировать";
            this.btnEditSpecialist.UseVisualStyleBackColor = true;
            this.btnEditSpecialist.Click += new System.EventHandler(this.btnEditSpecialist_Click);
            // 
            // btnAddSpecialist
            // 
            this.btnAddSpecialist.Location = new System.Drawing.Point(9, 58);
            this.btnAddSpecialist.Name = "btnAddSpecialist";
            this.btnAddSpecialist.Size = new System.Drawing.Size(66, 23);
            this.btnAddSpecialist.TabIndex = 13;
            this.btnAddSpecialist.Text = "Добавить";
            this.btnAddSpecialist.UseVisualStyleBackColor = true;
            this.btnAddSpecialist.Click += new System.EventHandler(this.btnAddSpecialist_Click);
            // 
            // lstSpecialistList
            // 
            this.lstSpecialistList.FormattingEnabled = true;
            this.lstSpecialistList.Location = new System.Drawing.Point(9, 87);
            this.lstSpecialistList.Name = "lstSpecialistList";
            this.lstSpecialistList.Size = new System.Drawing.Size(260, 147);
            this.lstSpecialistList.TabIndex = 11;
            this.lstSpecialistList.SelectedIndexChanged += new System.EventHandler(this.lstSpecialistList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя специалиста";
            // 
            // SpecialistListEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 245);
            this.Controls.Add(this.grpSelectSpecialist);
            this.Controls.Add(this.specialistInfoCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SpecialistListEdit";
            this.Text = "SpecialistListEdit";
            this.grpSelectSpecialist.ResumeLayout(false);
            this.grpSelectSpecialist.PerformLayout();
            this.grpEditMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Scheduler_Controls.SpecialistInfo specialistInfoCard;
        private System.Windows.Forms.GroupBox grpSelectSpecialist;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.GroupBox grpEditMode;
        private System.Windows.Forms.Button btnEditModeOff;
        private System.Windows.Forms.TextBox txtSpecialistName;
        private System.Windows.Forms.Button btnEditSpecialist;
        private System.Windows.Forms.Button btnAddSpecialist;
        private System.Windows.Forms.ListBox lstSpecialistList;
        private System.Windows.Forms.Label label1;
    }
}