namespace Scheduler_Controls
{
    partial class ClientInfo
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
            this.btnLoadReceptions = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.btnRemoveTelephone = new System.Windows.Forms.Button();
            this.btnAddTelephone = new System.Windows.Forms.Button();
            this.lstReceptions = new System.Windows.Forms.ListBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lstTelephones = new System.Windows.Forms.ListBox();
            this.txtFIO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkBlackList = new System.Windows.Forms.CheckBox();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnLoadReceptions);
            this.grpMain.Controls.Add(this.label2);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.btnCommit);
            this.grpMain.Controls.Add(this.btnRemoveTelephone);
            this.grpMain.Controls.Add(this.btnAddTelephone);
            this.grpMain.Controls.Add(this.lstReceptions);
            this.grpMain.Controls.Add(this.txtComment);
            this.grpMain.Controls.Add(this.lstTelephones);
            this.grpMain.Controls.Add(this.txtFIO);
            this.grpMain.Controls.Add(this.label4);
            this.grpMain.Controls.Add(this.label3);
            this.grpMain.Controls.Add(this.chkBlackList);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(400, 265);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Карточка клиента";
            // 
            // btnLoadReceptions
            // 
            this.btnLoadReceptions.Location = new System.Drawing.Point(9, 63);
            this.btnLoadReceptions.Name = "btnLoadReceptions";
            this.btnLoadReceptions.Size = new System.Drawing.Size(69, 20);
            this.btnLoadReceptions.TabIndex = 13;
            this.btnLoadReceptions.Text = "Загрузить";
            this.btnLoadReceptions.UseVisualStyleBackColor = true;
            this.btnLoadReceptions.Click += new System.EventHandler(this.btnLoadReceptions_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Посещения";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Имя клиента";
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(9, 230);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(384, 28);
            this.btnCommit.TabIndex = 7;
            this.btnCommit.Text = "Сохранить изменения";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnRemoveTelephone
            // 
            this.btnRemoveTelephone.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemoveTelephone.Location = new System.Drawing.Point(92, 208);
            this.btnRemoveTelephone.Name = "btnRemoveTelephone";
            this.btnRemoveTelephone.Size = new System.Drawing.Size(72, 16);
            this.btnRemoveTelephone.TabIndex = 6;
            this.btnRemoveTelephone.Text = "Удалить";
            this.btnRemoveTelephone.UseVisualStyleBackColor = true;
            this.btnRemoveTelephone.Click += new System.EventHandler(this.btnRemoveTelephone_Click);
            // 
            // btnAddTelephone
            // 
            this.btnAddTelephone.Location = new System.Drawing.Point(9, 201);
            this.btnAddTelephone.Name = "btnAddTelephone";
            this.btnAddTelephone.Size = new System.Drawing.Size(77, 23);
            this.btnAddTelephone.TabIndex = 5;
            this.btnAddTelephone.Text = "Добавить";
            this.btnAddTelephone.UseVisualStyleBackColor = true;
            this.btnAddTelephone.Click += new System.EventHandler(this.btnAddTelephone_Click);
            // 
            // lstReceptions
            // 
            this.lstReceptions.FormattingEnabled = true;
            this.lstReceptions.HorizontalScrollbar = true;
            this.lstReceptions.Location = new System.Drawing.Point(85, 47);
            this.lstReceptions.Name = "lstReceptions";
            this.lstReceptions.Size = new System.Drawing.Size(308, 82);
            this.lstReceptions.TabIndex = 4;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(85, 135);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(308, 45);
            this.txtComment.TabIndex = 3;
            // 
            // lstTelephones
            // 
            this.lstTelephones.FormattingEnabled = true;
            this.lstTelephones.Location = new System.Drawing.Point(170, 181);
            this.lstTelephones.Name = "lstTelephones";
            this.lstTelephones.Size = new System.Drawing.Size(223, 43);
            this.lstTelephones.TabIndex = 2;
            // 
            // txtFIO
            // 
            this.txtFIO.Location = new System.Drawing.Point(85, 19);
            this.txtFIO.Name = "txtFIO";
            this.txtFIO.Size = new System.Drawing.Size(308, 20);
            this.txtFIO.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Комментарий";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Телефоны";
            // 
            // chkBlackList
            // 
            this.chkBlackList.AutoSize = true;
            this.chkBlackList.Location = new System.Drawing.Point(9, 99);
            this.chkBlackList.Name = "chkBlackList";
            this.chkBlackList.Size = new System.Drawing.Size(62, 17);
            this.chkBlackList.TabIndex = 12;
            this.chkBlackList.Text = "RedList";
            this.chkBlackList.UseVisualStyleBackColor = true;
            // 
            // ClientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.MaximumSize = new System.Drawing.Size(400, 265);
            this.MinimumSize = new System.Drawing.Size(400, 265);
            this.Name = "ClientInfo";
            this.Size = new System.Drawing.Size(400, 265);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.Button btnRemoveTelephone;
        private System.Windows.Forms.Button btnAddTelephone;
        private System.Windows.Forms.ListBox lstReceptions;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ListBox lstTelephones;
        private System.Windows.Forms.TextBox txtFIO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkBlackList;
        private System.Windows.Forms.Button btnLoadReceptions;
    }
}
