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
            this.grpReceptions = new System.Windows.Forms.GroupBox();
            this.lstReceptions = new System.Windows.Forms.ListBox();
            this.btnLoadReceptions = new System.Windows.Forms.Button();
            this.grpComment = new System.Windows.Forms.GroupBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.grpGeneralParams = new System.Windows.Forms.GroupBox();
            this.numGenerallyPrice = new System.Windows.Forms.NumericUpDown();
            this.dateGenerallyTime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numBalance = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chkBlackList = new System.Windows.Forms.CheckBox();
            this.grpContacts = new System.Windows.Forms.GroupBox();
            this.grpContactsMail = new System.Windows.Forms.GroupBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.grpContactsTelephones = new System.Windows.Forms.GroupBox();
            this.lstTelephones = new System.Windows.Forms.ListBox();
            this.chkSMS = new System.Windows.Forms.CheckBox();
            this.btnRemoveTelephone = new System.Windows.Forms.Button();
            this.btnAddTelephone = new System.Windows.Forms.Button();
            this.txtAdministrator = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.txtFIO = new System.Windows.Forms.TextBox();
            this.grpMain.SuspendLayout();
            this.grpReceptions.SuspendLayout();
            this.grpComment.SuspendLayout();
            this.grpGeneralParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGenerallyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBalance)).BeginInit();
            this.grpContacts.SuspendLayout();
            this.grpContactsMail.SuspendLayout();
            this.grpContactsTelephones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.grpReceptions);
            this.grpMain.Controls.Add(this.grpComment);
            this.grpMain.Controls.Add(this.grpGeneralParams);
            this.grpMain.Controls.Add(this.grpContacts);
            this.grpMain.Controls.Add(this.txtAdministrator);
            this.grpMain.Controls.Add(this.label7);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.btnCommit);
            this.grpMain.Controls.Add(this.txtFIO);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(400, 539);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Карточка клиента";
            // 
            // grpReceptions
            // 
            this.grpReceptions.Controls.Add(this.lstReceptions);
            this.grpReceptions.Controls.Add(this.btnLoadReceptions);
            this.grpReceptions.Location = new System.Drawing.Point(9, 371);
            this.grpReceptions.Name = "grpReceptions";
            this.grpReceptions.Size = new System.Drawing.Size(385, 120);
            this.grpReceptions.TabIndex = 36;
            this.grpReceptions.TabStop = false;
            this.grpReceptions.Text = "Посещения";
            // 
            // lstReceptions
            // 
            this.lstReceptions.Dock = System.Windows.Forms.DockStyle.Right;
            this.lstReceptions.FormattingEnabled = true;
            this.lstReceptions.HorizontalScrollbar = true;
            this.lstReceptions.Location = new System.Drawing.Point(59, 16);
            this.lstReceptions.Name = "lstReceptions";
            this.lstReceptions.Size = new System.Drawing.Size(323, 101);
            this.lstReceptions.TabIndex = 4;
            this.lstReceptions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstReceptions_MouseDoubleClick);
            // 
            // btnLoadReceptions
            // 
            this.btnLoadReceptions.Location = new System.Drawing.Point(8, 16);
            this.btnLoadReceptions.Name = "btnLoadReceptions";
            this.btnLoadReceptions.Size = new System.Drawing.Size(45, 97);
            this.btnLoadReceptions.TabIndex = 13;
            this.btnLoadReceptions.Text = "Загрузить";
            this.btnLoadReceptions.UseVisualStyleBackColor = true;
            this.btnLoadReceptions.Click += new System.EventHandler(this.btnLoadReceptions_Click);
            // 
            // grpComment
            // 
            this.grpComment.Controls.Add(this.txtComment);
            this.grpComment.Location = new System.Drawing.Point(8, 288);
            this.grpComment.Name = "grpComment";
            this.grpComment.Size = new System.Drawing.Size(386, 77);
            this.grpComment.TabIndex = 35;
            this.grpComment.TabStop = false;
            this.grpComment.Text = "Комментарий";
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Location = new System.Drawing.Point(3, 16);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComment.Size = new System.Drawing.Size(380, 58);
            this.txtComment.TabIndex = 3;
            // 
            // grpGeneralParams
            // 
            this.grpGeneralParams.Controls.Add(this.numGenerallyPrice);
            this.grpGeneralParams.Controls.Add(this.dateGenerallyTime);
            this.grpGeneralParams.Controls.Add(this.label8);
            this.grpGeneralParams.Controls.Add(this.label5);
            this.grpGeneralParams.Controls.Add(this.numBalance);
            this.grpGeneralParams.Controls.Add(this.label6);
            this.grpGeneralParams.Controls.Add(this.chkBlackList);
            this.grpGeneralParams.Location = new System.Drawing.Point(8, 44);
            this.grpGeneralParams.Name = "grpGeneralParams";
            this.grpGeneralParams.Size = new System.Drawing.Size(386, 64);
            this.grpGeneralParams.TabIndex = 34;
            this.grpGeneralParams.TabStop = false;
            this.grpGeneralParams.Text = "Основные параметры";
            // 
            // numGenerallyPrice
            // 
            this.numGenerallyPrice.Location = new System.Drawing.Point(144, 38);
            this.numGenerallyPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numGenerallyPrice.Name = "numGenerallyPrice";
            this.numGenerallyPrice.Size = new System.Drawing.Size(51, 20);
            this.numGenerallyPrice.TabIndex = 23;
            this.numGenerallyPrice.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // dateGenerallyTime
            // 
            this.dateGenerallyTime.CustomFormat = "HH:mm";
            this.dateGenerallyTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateGenerallyTime.Location = new System.Drawing.Point(144, 12);
            this.dateGenerallyTime.Name = "dateGenerallyTime";
            this.dateGenerallyTime.ShowUpDown = true;
            this.dateGenerallyTime.Size = new System.Drawing.Size(51, 20);
            this.dateGenerallyTime.TabIndex = 22;
            this.dateGenerallyTime.Value = new System.DateTime(2014, 2, 2, 10, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Баланс:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Стандартное время:";
            // 
            // numBalance
            // 
            this.numBalance.Location = new System.Drawing.Point(305, 12);
            this.numBalance.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numBalance.Name = "numBalance";
            this.numBalance.Size = new System.Drawing.Size(75, 20);
            this.numBalance.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Стандартная стоимость:";
            // 
            // chkBlackList
            // 
            this.chkBlackList.AutoSize = true;
            this.chkBlackList.Location = new System.Drawing.Point(255, 41);
            this.chkBlackList.Name = "chkBlackList";
            this.chkBlackList.Size = new System.Drawing.Size(112, 17);
            this.chkBlackList.TabIndex = 12;
            this.chkBlackList.Text = "В чёрном списке";
            this.chkBlackList.UseVisualStyleBackColor = true;
            // 
            // grpContacts
            // 
            this.grpContacts.Controls.Add(this.grpContactsMail);
            this.grpContacts.Controls.Add(this.grpContactsTelephones);
            this.grpContacts.Location = new System.Drawing.Point(8, 114);
            this.grpContacts.Name = "grpContacts";
            this.grpContacts.Size = new System.Drawing.Size(386, 168);
            this.grpContacts.TabIndex = 33;
            this.grpContacts.TabStop = false;
            this.grpContacts.Text = "Контакты";
            // 
            // grpContactsMail
            // 
            this.grpContactsMail.Controls.Add(this.txtMail);
            this.grpContactsMail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpContactsMail.Location = new System.Drawing.Point(3, 122);
            this.grpContactsMail.Name = "grpContactsMail";
            this.grpContactsMail.Size = new System.Drawing.Size(380, 43);
            this.grpContactsMail.TabIndex = 35;
            this.grpContactsMail.TabStop = false;
            this.grpContactsMail.Text = "Электронная почта";
            // 
            // txtMail
            // 
            this.txtMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMail.Location = new System.Drawing.Point(3, 16);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(374, 20);
            this.txtMail.TabIndex = 32;
            // 
            // grpContactsTelephones
            // 
            this.grpContactsTelephones.Controls.Add(this.lstTelephones);
            this.grpContactsTelephones.Controls.Add(this.chkSMS);
            this.grpContactsTelephones.Controls.Add(this.btnRemoveTelephone);
            this.grpContactsTelephones.Controls.Add(this.btnAddTelephone);
            this.grpContactsTelephones.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpContactsTelephones.Location = new System.Drawing.Point(3, 16);
            this.grpContactsTelephones.Name = "grpContactsTelephones";
            this.grpContactsTelephones.Size = new System.Drawing.Size(380, 100);
            this.grpContactsTelephones.TabIndex = 34;
            this.grpContactsTelephones.TabStop = false;
            this.grpContactsTelephones.Text = "Телефоны";
            // 
            // lstTelephones
            // 
            this.lstTelephones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstTelephones.FormattingEnabled = true;
            this.lstTelephones.Location = new System.Drawing.Point(3, 54);
            this.lstTelephones.Name = "lstTelephones";
            this.lstTelephones.Size = new System.Drawing.Size(374, 43);
            this.lstTelephones.TabIndex = 2;
            // 
            // chkSMS
            // 
            this.chkSMS.AutoSize = true;
            this.chkSMS.Location = new System.Drawing.Point(165, 23);
            this.chkSMS.Name = "chkSMS";
            this.chkSMS.Size = new System.Drawing.Size(49, 17);
            this.chkSMS.TabIndex = 30;
            this.chkSMS.Text = "SMS";
            this.chkSMS.UseVisualStyleBackColor = true;
            // 
            // btnRemoveTelephone
            // 
            this.btnRemoveTelephone.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRemoveTelephone.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemoveTelephone.Location = new System.Drawing.Point(305, 24);
            this.btnRemoveTelephone.Name = "btnRemoveTelephone";
            this.btnRemoveTelephone.Size = new System.Drawing.Size(72, 16);
            this.btnRemoveTelephone.TabIndex = 6;
            this.btnRemoveTelephone.Text = "Удалить";
            this.btnRemoveTelephone.UseVisualStyleBackColor = true;
            this.btnRemoveTelephone.Click += new System.EventHandler(this.btnRemoveTelephone_Click);
            // 
            // btnAddTelephone
            // 
            this.btnAddTelephone.Location = new System.Drawing.Point(3, 19);
            this.btnAddTelephone.Name = "btnAddTelephone";
            this.btnAddTelephone.Size = new System.Drawing.Size(152, 23);
            this.btnAddTelephone.TabIndex = 5;
            this.btnAddTelephone.Text = "Добавить";
            this.btnAddTelephone.UseVisualStyleBackColor = true;
            this.btnAddTelephone.Click += new System.EventHandler(this.btnAddTelephone_Click);
            // 
            // txtAdministrator
            // 
            this.txtAdministrator.Location = new System.Drawing.Point(92, 425);
            this.txtAdministrator.Name = "txtAdministrator";
            this.txtAdministrator.Size = new System.Drawing.Size(301, 20);
            this.txtAdministrator.TabIndex = 27;
            this.txtAdministrator.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 428);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Администратор";
            this.label7.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Имя клиента:";
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(8, 497);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(384, 28);
            this.btnCommit.TabIndex = 7;
            this.btnCommit.Text = "Сохранить изменения";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // txtFIO
            // 
            this.txtFIO.Location = new System.Drawing.Point(85, 18);
            this.txtFIO.Name = "txtFIO";
            this.txtFIO.Size = new System.Drawing.Size(308, 20);
            this.txtFIO.TabIndex = 1;
            // 
            // ClientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.MaximumSize = new System.Drawing.Size(400, 0);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ClientInfo";
            this.Size = new System.Drawing.Size(400, 539);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpReceptions.ResumeLayout(false);
            this.grpComment.ResumeLayout(false);
            this.grpComment.PerformLayout();
            this.grpGeneralParams.ResumeLayout(false);
            this.grpGeneralParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGenerallyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBalance)).EndInit();
            this.grpContacts.ResumeLayout(false);
            this.grpContactsMail.ResumeLayout(false);
            this.grpContactsMail.PerformLayout();
            this.grpContactsTelephones.ResumeLayout(false);
            this.grpContactsTelephones.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBlackList;
        private System.Windows.Forms.Button btnLoadReceptions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numGenerallyPrice;
        private System.Windows.Forms.DateTimePicker dateGenerallyTime;
        private System.Windows.Forms.TextBox txtAdministrator;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSMS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numBalance;
        private System.Windows.Forms.GroupBox grpContacts;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.GroupBox grpReceptions;
        private System.Windows.Forms.GroupBox grpComment;
        private System.Windows.Forms.GroupBox grpGeneralParams;
        private System.Windows.Forms.GroupBox grpContactsMail;
        private System.Windows.Forms.GroupBox grpContactsTelephones;
    }
}
