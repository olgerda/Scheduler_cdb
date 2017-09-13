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
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.chkReceptionDidNotTakePlace = new System.Windows.Forms.CheckBox();
            this.btnCreateChildReception = new System.Windows.Forms.Button();
            this.btnCommit = new System.Windows.Forms.Button();
            this.btnCancelReception = new System.Windows.Forms.Button();
            this.grpOther = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSpecialist = new System.Windows.Forms.ComboBox();
            this.numericPrice = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCabinet = new System.Windows.Forms.ComboBox();
            this.txtAdministrator = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSpecialisation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpComment = new System.Windows.Forms.GroupBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.grpReceptionParams = new System.Windows.Forms.GroupBox();
            this.chkSpecialRent = new System.Windows.Forms.CheckBox();
            this.chkRent = new System.Windows.Forms.CheckBox();
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.txtTelephone = new System.Windows.Forms.MaskedTextBox();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.btnShowClientCard = new System.Windows.Forms.Button();
            this.grpDateTime = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dateDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.chkArendatorsList = new System.Windows.Forms.CheckBox();
            this.grpMain.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrice)).BeginInit();
            this.grpComment.SuspendLayout();
            this.grpReceptionParams.SuspendLayout();
            this.grpClient.SuspendLayout();
            this.grpDateTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.grpActions);
            this.grpMain.Controls.Add(this.grpOther);
            this.grpMain.Controls.Add(this.grpComment);
            this.grpMain.Controls.Add(this.grpReceptionParams);
            this.grpMain.Controls.Add(this.grpClient);
            this.grpMain.Controls.Add(this.grpDateTime);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(570, 282);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Карточка записи";
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.chkReceptionDidNotTakePlace);
            this.grpActions.Controls.Add(this.btnCreateChildReception);
            this.grpActions.Controls.Add(this.btnCommit);
            this.grpActions.Controls.Add(this.btnCancelReception);
            this.grpActions.Location = new System.Drawing.Point(6, 226);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(561, 50);
            this.grpActions.TabIndex = 35;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Действия";
            // 
            // chkReceptionDidNotTakePlace
            // 
            this.chkReceptionDidNotTakePlace.AutoSize = true;
            this.chkReceptionDidNotTakePlace.Location = new System.Drawing.Point(307, 23);
            this.chkReceptionDidNotTakePlace.Name = "chkReceptionDidNotTakePlace";
            this.chkReceptionDidNotTakePlace.Size = new System.Drawing.Size(131, 17);
            this.chkReceptionDidNotTakePlace.TabIndex = 21;
            this.chkReceptionDidNotTakePlace.Text = "Приём не состоялся";
            this.chkReceptionDidNotTakePlace.UseVisualStyleBackColor = true;
            // 
            // btnCreateChildReception
            // 
            this.btnCreateChildReception.Location = new System.Drawing.Point(3, 19);
            this.btnCreateChildReception.Name = "btnCreateChildReception";
            this.btnCreateChildReception.Size = new System.Drawing.Size(165, 23);
            this.btnCreateChildReception.TabIndex = 16;
            this.btnCreateChildReception.Text = "Назначить следующий приём";
            this.btnCreateChildReception.UseVisualStyleBackColor = true;
            this.btnCreateChildReception.Click += new System.EventHandler(this.btnCreateChildReception_Click);
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(174, 19);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(127, 23);
            this.btnCommit.TabIndex = 19;
            this.btnCommit.Text = "Сохранить изменения";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnCancelReception
            // 
            this.btnCancelReception.Location = new System.Drawing.Point(450, 19);
            this.btnCancelReception.Name = "btnCancelReception";
            this.btnCancelReception.Size = new System.Drawing.Size(105, 23);
            this.btnCancelReception.TabIndex = 17;
            this.btnCancelReception.Text = "Отменить приём";
            this.btnCancelReception.UseVisualStyleBackColor = true;
            this.btnCancelReception.Click += new System.EventHandler(this.btnCancelReception_Click);
            // 
            // grpOther
            // 
            this.grpOther.Controls.Add(this.label4);
            this.grpOther.Controls.Add(this.cmbSpecialist);
            this.grpOther.Controls.Add(this.numericPrice);
            this.grpOther.Controls.Add(this.label7);
            this.grpOther.Controls.Add(this.cmbCabinet);
            this.grpOther.Controls.Add(this.txtAdministrator);
            this.grpOther.Controls.Add(this.label8);
            this.grpOther.Controls.Add(this.label6);
            this.grpOther.Controls.Add(this.cmbSpecialisation);
            this.grpOther.Controls.Add(this.label5);
            this.grpOther.Location = new System.Drawing.Point(180, 97);
            this.grpOther.Name = "grpOther";
            this.grpOther.Size = new System.Drawing.Size(387, 121);
            this.grpOther.TabIndex = 34;
            this.grpOther.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Специалист:";
            // 
            // cmbSpecialist
            // 
            this.cmbSpecialist.FormattingEnabled = true;
            this.cmbSpecialist.Location = new System.Drawing.Point(98, 13);
            this.cmbSpecialist.Name = "cmbSpecialist";
            this.cmbSpecialist.Size = new System.Drawing.Size(283, 21);
            this.cmbSpecialist.TabIndex = 9;
            this.cmbSpecialist.SelectedIndexChanged += new System.EventHandler(this.cmbSpecialist_SelectedIndexChanged);
            // 
            // numericPrice
            // 
            this.numericPrice.Location = new System.Drawing.Point(319, 67);
            this.numericPrice.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericPrice.Name = "numericPrice";
            this.numericPrice.Size = new System.Drawing.Size(62, 20);
            this.numericPrice.TabIndex = 27;
            this.numericPrice.ThousandsSeparator = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Стоимость:";
            // 
            // cmbCabinet
            // 
            this.cmbCabinet.FormattingEnabled = true;
            this.cmbCabinet.Location = new System.Drawing.Point(98, 67);
            this.cmbCabinet.Name = "cmbCabinet";
            this.cmbCabinet.Size = new System.Drawing.Size(127, 21);
            this.cmbCabinet.TabIndex = 14;
            // 
            // txtAdministrator
            // 
            this.txtAdministrator.Location = new System.Drawing.Point(98, 94);
            this.txtAdministrator.Name = "txtAdministrator";
            this.txtAdministrator.Size = new System.Drawing.Size(283, 20);
            this.txtAdministrator.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Администратор:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Кабинет:";
            // 
            // cmbSpecialisation
            // 
            this.cmbSpecialisation.FormattingEnabled = true;
            this.cmbSpecialisation.Location = new System.Drawing.Point(98, 40);
            this.cmbSpecialisation.Name = "cmbSpecialisation";
            this.cmbSpecialisation.Size = new System.Drawing.Size(283, 21);
            this.cmbSpecialisation.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Специализация:";
            // 
            // grpComment
            // 
            this.grpComment.Controls.Add(this.txtComment);
            this.grpComment.Location = new System.Drawing.Point(6, 97);
            this.grpComment.Name = "grpComment";
            this.grpComment.Size = new System.Drawing.Size(168, 121);
            this.grpComment.TabIndex = 33;
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
            this.txtComment.Size = new System.Drawing.Size(162, 102);
            this.txtComment.TabIndex = 0;
            // 
            // grpReceptionParams
            // 
            this.grpReceptionParams.Controls.Add(this.chkSpecialRent);
            this.grpReceptionParams.Controls.Add(this.chkRent);
            this.grpReceptionParams.Controls.Add(this.chkArendatorsList);
            this.grpReceptionParams.Location = new System.Drawing.Point(475, 19);
            this.grpReceptionParams.Name = "grpReceptionParams";
            this.grpReceptionParams.Size = new System.Drawing.Size(92, 72);
            this.grpReceptionParams.TabIndex = 32;
            this.grpReceptionParams.TabStop = false;
            this.grpReceptionParams.Text = "Параметры";
            // 
            // chkSpecialRent
            // 
            this.chkSpecialRent.AutoSize = true;
            this.chkSpecialRent.Location = new System.Drawing.Point(6, 44);
            this.chkSpecialRent.Name = "chkSpecialRent";
            this.chkSpecialRent.Size = new System.Drawing.Size(58, 17);
            this.chkSpecialRent.TabIndex = 28;
            this.chkSpecialRent.Text = "Песок";
            this.chkSpecialRent.UseVisualStyleBackColor = true;
            // 
            // chkRent
            // 
            this.chkRent.AutoSize = true;
            this.chkRent.Location = new System.Drawing.Point(6, 18);
            this.chkRent.Name = "chkRent";
            this.chkRent.Size = new System.Drawing.Size(63, 17);
            this.chkRent.TabIndex = 13;
            this.chkRent.Text = "Аренда";
            this.chkRent.UseVisualStyleBackColor = true;
            this.chkRent.CheckedChanged += new System.EventHandler(this.chkRent_CheckedChanged);
            // 
            // grpClient
            // 
            this.grpClient.Controls.Add(this.txtTelephone);
            this.grpClient.Controls.Add(this.txtClientName);
            this.grpClient.Controls.Add(this.btnShowClientCard);
            this.grpClient.Location = new System.Drawing.Point(180, 19);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(289, 72);
            this.grpClient.TabIndex = 31;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Клиент";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(6, 45);
            this.txtTelephone.Mask = "+0 (999) 000-0000";
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(99, 20);
            this.txtTelephone.TabIndex = 8;
            this.txtTelephone.Text = "7";
            this.txtTelephone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtClientName
            // 
            this.txtClientName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtClientName.Location = new System.Drawing.Point(3, 16);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(283, 20);
            this.txtClientName.TabIndex = 5;
            // 
            // btnShowClientCard
            // 
            this.btnShowClientCard.Location = new System.Drawing.Point(111, 43);
            this.btnShowClientCard.Name = "btnShowClientCard";
            this.btnShowClientCard.Size = new System.Drawing.Size(175, 23);
            this.btnShowClientCard.TabIndex = 18;
            this.btnShowClientCard.Text = "Показать/выбрать карту клиента";
            this.btnShowClientCard.UseVisualStyleBackColor = true;
            this.btnShowClientCard.Click += new System.EventHandler(this.btnShowClientCard_Click);
            // 
            // grpDateTime
            // 
            this.grpDateTime.Controls.Add(this.label10);
            this.grpDateTime.Controls.Add(this.label9);
            this.grpDateTime.Controls.Add(this.dateDate);
            this.grpDateTime.Controls.Add(this.dateTimeStart);
            this.grpDateTime.Controls.Add(this.dateTimeEnd);
            this.grpDateTime.Location = new System.Drawing.Point(6, 19);
            this.grpDateTime.Name = "grpDateTime";
            this.grpDateTime.Size = new System.Drawing.Size(168, 72);
            this.grpDateTime.TabIndex = 30;
            this.grpDateTime.TabStop = false;
            this.grpDateTime.Text = "Дата и время приёма";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(89, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "по";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "с";
            // 
            // dateDate
            // 
            this.dateDate.CustomFormat = "dd.MM.yyyy";
            this.dateDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.dateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDate.Location = new System.Drawing.Point(3, 16);
            this.dateDate.Name = "dateDate";
            this.dateDate.Size = new System.Drawing.Size(162, 20);
            this.dateDate.TabIndex = 20;
            this.dateDate.ValueChanged += new System.EventHandler(this.dateDate_ValueChanged);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "HH:mm";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(23, 45);
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
            this.dateTimeEnd.Location = new System.Drawing.Point(114, 45);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowUpDown = true;
            this.dateTimeEnd.Size = new System.Drawing.Size(51, 20);
            this.dateTimeEnd.TabIndex = 22;
            this.dateTimeEnd.Value = new System.DateTime(2014, 2, 2, 13, 51, 0, 0);
            // 
            // chkArendatorsList
            // 
            this.chkArendatorsList.AutoSize = true;
            this.chkArendatorsList.Enabled = false;
            this.chkArendatorsList.Location = new System.Drawing.Point(6, 50);
            this.chkArendatorsList.Name = "chkArendatorsList";
            this.chkArendatorsList.Size = new System.Drawing.Size(80, 17);
            this.chkArendatorsList.TabIndex = 29;
            this.chkArendatorsList.Text = "Арендатор";
            this.chkArendatorsList.UseVisualStyleBackColor = true;
            this.chkArendatorsList.Visible = false;
            // 
            // ReceptionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "ReceptionInfo";
            this.Size = new System.Drawing.Size(570, 282);
            this.grpMain.ResumeLayout(false);
            this.grpActions.ResumeLayout(false);
            this.grpActions.PerformLayout();
            this.grpOther.ResumeLayout(false);
            this.grpOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrice)).EndInit();
            this.grpComment.ResumeLayout(false);
            this.grpComment.PerformLayout();
            this.grpReceptionParams.ResumeLayout(false);
            this.grpReceptionParams.PerformLayout();
            this.grpClient.ResumeLayout(false);
            this.grpClient.PerformLayout();
            this.grpDateTime.ResumeLayout(false);
            this.grpDateTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
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
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericPrice;
        private System.Windows.Forms.TextBox txtAdministrator;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.GroupBox grpReceptionParams;
        private System.Windows.Forms.CheckBox chkSpecialRent;
        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.GroupBox grpDateTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpOther;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.CheckBox chkReceptionDidNotTakePlace;
        private System.Windows.Forms.CheckBox chkArendatorsList;
    }
}
