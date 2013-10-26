namespace Scheduler.DetailViews
{
    partial class ClientInfoControl
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
            this.grpClientTelephones = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstClientTelephones = new System.Windows.Forms.ListBox();
            this.btnClientTelephoneRemove = new System.Windows.Forms.Button();
            this.btnClientTelephoneAdd = new System.Windows.Forms.Button();
            this.txtClientTelephone = new System.Windows.Forms.MaskedTextBox();
            this.grpClientName = new System.Windows.Forms.GroupBox();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.grpClientComment = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnClientCommentApply = new System.Windows.Forms.Button();
            this.txtClientComment = new System.Windows.Forms.TextBox();
            this.grpClientTelephones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpClientName.SuspendLayout();
            this.grpClientComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpClientTelephones
            // 
            this.grpClientTelephones.Controls.Add(this.splitContainer1);
            this.grpClientTelephones.Location = new System.Drawing.Point(3, 54);
            this.grpClientTelephones.Name = "grpClientTelephones";
            this.grpClientTelephones.Size = new System.Drawing.Size(331, 128);
            this.grpClientTelephones.TabIndex = 4;
            this.grpClientTelephones.TabStop = false;
            this.grpClientTelephones.Text = "Телефоны";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstClientTelephones);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnClientTelephoneRemove);
            this.splitContainer1.Panel2.Controls.Add(this.btnClientTelephoneAdd);
            this.splitContainer1.Panel2.Controls.Add(this.txtClientTelephone);
            this.splitContainer1.Size = new System.Drawing.Size(325, 109);
            this.splitContainer1.SplitterDistance = 79;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstClientTelephones
            // 
            this.lstClientTelephones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstClientTelephones.FormattingEnabled = true;
            this.lstClientTelephones.Location = new System.Drawing.Point(0, 0);
            this.lstClientTelephones.Name = "lstClientTelephones";
            this.lstClientTelephones.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstClientTelephones.Size = new System.Drawing.Size(325, 79);
            this.lstClientTelephones.TabIndex = 0;
            this.lstClientTelephones.SelectedIndexChanged += new System.EventHandler(this.lstClientTelephones_SelectedIndexChanged);
            // 
            // btnClientTelephoneRemove
            // 
            this.btnClientTelephoneRemove.Location = new System.Drawing.Point(249, 3);
            this.btnClientTelephoneRemove.Name = "btnClientTelephoneRemove";
            this.btnClientTelephoneRemove.Size = new System.Drawing.Size(73, 20);
            this.btnClientTelephoneRemove.TabIndex = 2;
            this.btnClientTelephoneRemove.Text = "Удалить";
            this.btnClientTelephoneRemove.UseVisualStyleBackColor = true;
            this.btnClientTelephoneRemove.Click += new System.EventHandler(this.btnClientTelephoneRemove_Click);
            // 
            // btnClientTelephoneAdd
            // 
            this.btnClientTelephoneAdd.Location = new System.Drawing.Point(178, 3);
            this.btnClientTelephoneAdd.Name = "btnClientTelephoneAdd";
            this.btnClientTelephoneAdd.Size = new System.Drawing.Size(65, 20);
            this.btnClientTelephoneAdd.TabIndex = 1;
            this.btnClientTelephoneAdd.Text = "Добавить";
            this.btnClientTelephoneAdd.UseVisualStyleBackColor = true;
            this.btnClientTelephoneAdd.Click += new System.EventHandler(this.btnClientTelephoneAdd_Click);
            // 
            // txtClientTelephone
            // 
            this.txtClientTelephone.AsciiOnly = true;
            this.txtClientTelephone.Location = new System.Drawing.Point(3, 3);
            this.txtClientTelephone.Mask = "+7 (999) 000-0000";
            this.txtClientTelephone.Name = "txtClientTelephone";
            this.txtClientTelephone.Size = new System.Drawing.Size(171, 20);
            this.txtClientTelephone.TabIndex = 0;
            this.txtClientTelephone.TextChanged += new System.EventHandler(this.txtClientTelephone_TextChanged);
            // 
            // grpClientName
            // 
            this.grpClientName.Controls.Add(this.txtClientName);
            this.grpClientName.Location = new System.Drawing.Point(3, 3);
            this.grpClientName.Name = "grpClientName";
            this.grpClientName.Size = new System.Drawing.Size(331, 45);
            this.grpClientName.TabIndex = 3;
            this.grpClientName.TabStop = false;
            this.grpClientName.Text = "ФИО Клиента";
            // 
            // txtClientName
            // 
            this.txtClientName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClientName.Location = new System.Drawing.Point(3, 16);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(325, 20);
            this.txtClientName.TabIndex = 0;
            this.txtClientName.TextChanged += new System.EventHandler(this.txtClientName_TextChanged);
            this.txtClientName.Leave += new System.EventHandler(this.txtClientName_Leave);
            // 
            // grpClientComment
            // 
            this.grpClientComment.Controls.Add(this.splitContainer2);
            this.grpClientComment.Location = new System.Drawing.Point(6, 188);
            this.grpClientComment.Name = "grpClientComment";
            this.grpClientComment.Size = new System.Drawing.Size(331, 128);
            this.grpClientComment.TabIndex = 5;
            this.grpClientComment.TabStop = false;
            this.grpClientComment.Text = "Комментарий";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtClientComment);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnClientCommentApply);
            this.splitContainer2.Size = new System.Drawing.Size(325, 109);
            this.splitContainer2.SplitterDistance = 79;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnClientCommentApply
            // 
            this.btnClientCommentApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClientCommentApply.Location = new System.Drawing.Point(0, 0);
            this.btnClientCommentApply.Name = "btnClientCommentApply";
            this.btnClientCommentApply.Size = new System.Drawing.Size(325, 26);
            this.btnClientCommentApply.TabIndex = 1;
            this.btnClientCommentApply.Text = "Подтвердить изменения комментария";
            this.btnClientCommentApply.UseVisualStyleBackColor = true;
            this.btnClientCommentApply.Click += new System.EventHandler(this.btnClientCommentApply_Click);
            // 
            // txtClientComment
            // 
            this.txtClientComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClientComment.Location = new System.Drawing.Point(0, 0);
            this.txtClientComment.Multiline = true;
            this.txtClientComment.Name = "txtClientComment";
            this.txtClientComment.Size = new System.Drawing.Size(325, 79);
            this.txtClientComment.TabIndex = 0;
            // 
            // ClientInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpClientComment);
            this.Controls.Add(this.grpClientTelephones);
            this.Controls.Add(this.grpClientName);
            this.MaximumSize = new System.Drawing.Size(340, 320);
            this.MinimumSize = new System.Drawing.Size(340, 320);
            this.Name = "ClientInfoControl";
            this.Size = new System.Drawing.Size(340, 320);
            this.grpClientTelephones.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpClientName.ResumeLayout(false);
            this.grpClientName.PerformLayout();
            this.grpClientComment.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpClientTelephones;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstClientTelephones;
        private System.Windows.Forms.Button btnClientTelephoneRemove;
        private System.Windows.Forms.Button btnClientTelephoneAdd;
        private System.Windows.Forms.MaskedTextBox txtClientTelephone;
        private System.Windows.Forms.GroupBox grpClientName;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.GroupBox grpClientComment;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtClientComment;
        private System.Windows.Forms.Button btnClientCommentApply;
    }
}
