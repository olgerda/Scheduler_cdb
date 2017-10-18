namespace Scheduler.Forms
{
    partial class AdministratorListEdit
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
            this.grpList = new System.Windows.Forms.GroupBox();
            this.lstAdministrators = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.grpEdit = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkNotWork = new System.Windows.Forms.CheckBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpList.SuspendLayout();
            this.grpEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.lstAdministrators);
            this.grpList.Location = new System.Drawing.Point(12, 12);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(200, 248);
            this.grpList.TabIndex = 0;
            this.grpList.TabStop = false;
            this.grpList.Text = "Администраторы";
            // 
            // lstAdministrators
            // 
            this.lstAdministrators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAdministrators.FormattingEnabled = true;
            this.lstAdministrators.Location = new System.Drawing.Point(3, 16);
            this.lstAdministrators.Name = "lstAdministrators";
            this.lstAdministrators.Size = new System.Drawing.Size(194, 229);
            this.lstAdministrators.TabIndex = 0;
            this.lstAdministrators.SelectedIndexChanged += new System.EventHandler(this.lstAdministrators_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(218, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(328, 237);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(59, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(215, 237);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(107, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // grpEdit
            // 
            this.grpEdit.Controls.Add(this.btnCancel);
            this.grpEdit.Controls.Add(this.btnAccept);
            this.grpEdit.Controls.Add(this.chkNotWork);
            this.grpEdit.Controls.Add(this.txtName);
            this.grpEdit.Location = new System.Drawing.Point(218, 41);
            this.grpEdit.Name = "grpEdit";
            this.grpEdit.Size = new System.Drawing.Size(169, 190);
            this.grpEdit.TabIndex = 4;
            this.grpEdit.TabStop = false;
            this.grpEdit.Text = "Администратор";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtName.Location = new System.Drawing.Point(3, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(163, 20);
            this.txtName.TabIndex = 0;
            // 
            // chkNotWork
            // 
            this.chkNotWork.AutoSize = true;
            this.chkNotWork.Location = new System.Drawing.Point(3, 42);
            this.chkNotWork.Name = "chkNotWork";
            this.chkNotWork.Size = new System.Drawing.Size(89, 17);
            this.chkNotWork.TabIndex = 1;
            this.chkNotWork.Text = "Не работает";
            this.chkNotWork.UseVisualStyleBackColor = true;
            // 
            // btnAccept
            // 
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(0, 65);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(166, 23);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "Принять";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(0, 113);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(166, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AdministratorListEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 272);
            this.Controls.Add(this.grpEdit);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grpList);
            this.Name = "AdministratorListEdit";
            this.Text = "AdministratorListEdit";
            this.grpList.ResumeLayout(false);
            this.grpEdit.ResumeLayout(false);
            this.grpEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.ListBox lstAdministrators;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox grpEdit;
        private System.Windows.Forms.CheckBox chkNotWork;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
    }
}