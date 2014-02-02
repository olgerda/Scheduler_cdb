namespace Scheduler_Controls
{
    partial class ReceptionInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelephone = new System.Windows.Forms.MaskedTextBox();
            this.cmbSpecialist = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSpecialisation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkRent = new System.Windows.Forms.CheckBox();
            this.cmbCabinet = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCreateChildReception = new System.Windows.Forms.Button();
            this.btnCancelReception = new System.Windows.Forms.Button();
            this.btnShowClientCard = new System.Windows.Forms.Button();
            this.btnCommit = new System.Windows.Forms.Button();
            this.dateDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.pnlClient = new System.Windows.Forms.Panel();
            this.grpMain.SuspendLayout();
            this.pnlClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnShowClientCard);
            this.grpMain.Controls.Add(this.cmbSpecialist);
            this.grpMain.Controls.Add(this.btnCreateChildReception);
            this.grpMain.Controls.Add(this.btnCancelReception);
            this.grpMain.Controls.Add(this.cmbSpecialisation);
            this.grpMain.Controls.Add(this.label5);
            this.grpMain.Controls.Add(this.label4);
            this.grpMain.Controls.Add(this.dateTimeEnd);
            this.grpMain.Controls.Add(this.dateTimeStart);
            this.grpMain.Controls.Add(this.dateDate);
            this.grpMain.Controls.Add(this.btnCommit);
            this.grpMain.Controls.Add(this.chkRent);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.pnlClient);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(593, 152);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Карточка записи";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(77, 2);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(209, 20);
            this.txtClientName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Имя клиента";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Телефон клиента";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(100, 26);
            this.txtTelephone.Mask = "+0 (999) 000-0000";
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(186, 20);
            this.txtTelephone.TabIndex = 8;
            this.txtTelephone.Text = "7";
            this.txtTelephone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // cmbSpecialist
            // 
            this.cmbSpecialist.FormattingEnabled = true;
            this.cmbSpecialist.Location = new System.Drawing.Point(79, 38);
            this.cmbSpecialist.Name = "cmbSpecialist";
            this.cmbSpecialist.Size = new System.Drawing.Size(217, 21);
            this.cmbSpecialist.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Специалист";
            // 
            // cmbSpecialisation
            // 
            this.cmbSpecialisation.FormattingEnabled = true;
            this.cmbSpecialisation.Location = new System.Drawing.Point(102, 62);
            this.cmbSpecialisation.Name = "cmbSpecialisation";
            this.cmbSpecialisation.Size = new System.Drawing.Size(194, 21);
            this.cmbSpecialisation.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Специализация";
            // 
            // chkRent
            // 
            this.chkRent.AutoSize = true;
            this.chkRent.Location = new System.Drawing.Point(262, 15);
            this.chkRent.Name = "chkRent";
            this.chkRent.Size = new System.Drawing.Size(63, 17);
            this.chkRent.TabIndex = 13;
            this.chkRent.Text = "Аренда";
            this.chkRent.UseVisualStyleBackColor = true;
            this.chkRent.CheckedChanged += new System.EventHandler(this.chkRent_CheckedChanged);
            // 
            // cmbCabinet
            // 
            this.cmbCabinet.FormattingEnabled = true;
            this.cmbCabinet.Location = new System.Drawing.Point(69, 52);
            this.cmbCabinet.Name = "cmbCabinet";
            this.cmbCabinet.Size = new System.Drawing.Size(217, 21);
            this.cmbCabinet.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Кабинет";
            // 
            // btnCreateChildReception
            // 
            this.btnCreateChildReception.Location = new System.Drawing.Point(9, 93);
            this.btnCreateChildReception.Name = "btnCreateChildReception";
            this.btnCreateChildReception.Size = new System.Drawing.Size(167, 23);
            this.btnCreateChildReception.TabIndex = 16;
            this.btnCreateChildReception.Text = "Назначить следующий приём";
            this.btnCreateChildReception.UseVisualStyleBackColor = true;
            // 
            // btnCancelReception
            // 
            this.btnCancelReception.Location = new System.Drawing.Point(196, 93);
            this.btnCancelReception.Name = "btnCancelReception";
            this.btnCancelReception.Size = new System.Drawing.Size(103, 23);
            this.btnCancelReception.TabIndex = 17;
            this.btnCancelReception.Text = "Отменить приём";
            this.btnCancelReception.UseVisualStyleBackColor = true;
            // 
            // btnShowClientCard
            // 
            this.btnShowClientCard.Location = new System.Drawing.Point(322, 10);
            this.btnShowClientCard.Name = "btnShowClientCard";
            this.btnShowClientCard.Size = new System.Drawing.Size(266, 23);
            this.btnShowClientCard.TabIndex = 18;
            this.btnShowClientCard.Text = "Просмотреть карту клиента";
            this.btnShowClientCard.UseVisualStyleBackColor = true;
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(9, 122);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(579, 23);
            this.btnCommit.TabIndex = 19;
            this.btnCommit.Text = "Сохранить изменения";
            this.btnCommit.UseVisualStyleBackColor = true;
            // 
            // dateDate
            // 
            this.dateDate.CustomFormat = "dd.MM.yyyy";
            this.dateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDate.Location = new System.Drawing.Point(49, 13);
            this.dateDate.Name = "dateDate";
            this.dateDate.Size = new System.Drawing.Size(93, 20);
            this.dateDate.TabIndex = 20;
            this.dateDate.ValueChanged += new System.EventHandler(this.dateDate_ValueChanged);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "HH:mm";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(148, 13);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowUpDown = true;
            this.dateTimeStart.Size = new System.Drawing.Size(51, 20);
            this.dateTimeStart.TabIndex = 21;
            this.dateTimeStart.Value = new System.DateTime(2014, 2, 2, 13, 51, 0, 0);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "HH:mm";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(205, 13);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowUpDown = true;
            this.dateTimeEnd.Size = new System.Drawing.Size(51, 20);
            this.dateTimeEnd.TabIndex = 22;
            this.dateTimeEnd.Value = new System.DateTime(2014, 2, 2, 13, 51, 0, 0);
            // 
            // pnlClient
            // 
            this.pnlClient.Controls.Add(this.txtTelephone);
            this.pnlClient.Controls.Add(this.txtClientName);
            this.pnlClient.Controls.Add(this.label2);
            this.pnlClient.Controls.Add(this.label3);
            this.pnlClient.Controls.Add(this.cmbCabinet);
            this.pnlClient.Controls.Add(this.label6);
            this.pnlClient.Location = new System.Drawing.Point(302, 36);
            this.pnlClient.Name = "pnlClient";
            this.pnlClient.Size = new System.Drawing.Size(288, 80);
            this.pnlClient.TabIndex = 24;
            // 
            // ReceptionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "ReceptionInfo";
            this.Size = new System.Drawing.Size(593, 152);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.pnlClient.ResumeLayout(false);
            this.pnlClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShowClientCard;
        private System.Windows.Forms.Button btnCancelReception;
        private System.Windows.Forms.Button btnCreateChildReception;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCabinet;
        private System.Windows.Forms.CheckBox chkRent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSpecialisation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSpecialist;
        private System.Windows.Forms.MaskedTextBox txtTelephone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateDate;
        private System.Windows.Forms.Panel pnlClient;
    }
}
