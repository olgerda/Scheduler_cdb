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
            this.numericPrice = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.btnShowClientCard = new System.Windows.Forms.Button();
            this.cmbSpecialist = new System.Windows.Forms.ComboBox();
            this.btnCreateChildReception = new System.Windows.Forms.Button();
            this.btnCancelReception = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCabinet = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateDate = new System.Windows.Forms.DateTimePicker();
            this.btnCommit = new System.Windows.Forms.Button();
            this.chkRent = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlClient = new System.Windows.Forms.Panel();
            this.txtTelephone = new System.Windows.Forms.MaskedTextBox();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSpecialisation = new System.Windows.Forms.ComboBox();
            this.txtAdministrator = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrice)).BeginInit();
            this.pnlClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.txtAdministrator);
            this.grpMain.Controls.Add(this.label8);
            this.grpMain.Controls.Add(this.numericPrice);
            this.grpMain.Controls.Add(this.label7);
            this.grpMain.Controls.Add(this.btnShowClientCard);
            this.grpMain.Controls.Add(this.cmbSpecialist);
            this.grpMain.Controls.Add(this.btnCreateChildReception);
            this.grpMain.Controls.Add(this.btnCancelReception);
            this.grpMain.Controls.Add(this.label6);
            this.grpMain.Controls.Add(this.cmbCabinet);
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
            this.grpMain.Size = new System.Drawing.Size(595, 150);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Карточка записи";
            // 
            // numericPrice
            // 
            this.numericPrice.Location = new System.Drawing.Point(302, 62);
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
            this.label7.Location = new System.Drawing.Point(302, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Стоимость";
            // 
            // btnShowClientCard
            // 
            this.btnShowClientCard.Location = new System.Drawing.Point(370, 10);
            this.btnShowClientCard.Name = "btnShowClientCard";
            this.btnShowClientCard.Size = new System.Drawing.Size(220, 23);
            this.btnShowClientCard.TabIndex = 18;
            this.btnShowClientCard.Text = "Показать/выбрать карту клиента";
            this.btnShowClientCard.UseVisualStyleBackColor = true;
            this.btnShowClientCard.Click += new System.EventHandler(this.btnShowClientCard_Click);
            // 
            // cmbSpecialist
            // 
            this.cmbSpecialist.FormattingEnabled = true;
            this.cmbSpecialist.Location = new System.Drawing.Point(74, 38);
            this.cmbSpecialist.Name = "cmbSpecialist";
            this.cmbSpecialist.Size = new System.Drawing.Size(222, 21);
            this.cmbSpecialist.TabIndex = 9;
            this.cmbSpecialist.SelectedIndexChanged += new System.EventHandler(this.cmbSpecialist_SelectedIndexChanged);
            // 
            // btnCreateChildReception
            // 
            this.btnCreateChildReception.Location = new System.Drawing.Point(9, 122);
            this.btnCreateChildReception.Name = "btnCreateChildReception";
            this.btnCreateChildReception.Size = new System.Drawing.Size(168, 23);
            this.btnCreateChildReception.TabIndex = 16;
            this.btnCreateChildReception.Text = "Назначить следующий приём";
            this.btnCreateChildReception.UseVisualStyleBackColor = true;
            this.btnCreateChildReception.Click += new System.EventHandler(this.btnCreateChildReception_Click);
            // 
            // btnCancelReception
            // 
            this.btnCancelReception.Location = new System.Drawing.Point(183, 122);
            this.btnCancelReception.Name = "btnCancelReception";
            this.btnCancelReception.Size = new System.Drawing.Size(103, 23);
            this.btnCancelReception.TabIndex = 17;
            this.btnCancelReception.Text = "Отменить приём";
            this.btnCancelReception.UseVisualStyleBackColor = true;
            this.btnCancelReception.Click += new System.EventHandler(this.btnCancelReception_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Кабинет";
            // 
            // cmbCabinet
            // 
            this.cmbCabinet.FormattingEnabled = true;
            this.cmbCabinet.Location = new System.Drawing.Point(74, 62);
            this.cmbCabinet.Name = "cmbCabinet";
            this.cmbCabinet.Size = new System.Drawing.Size(125, 21);
            this.cmbCabinet.TabIndex = 14;
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
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(302, 122);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(286, 23);
            this.btnCommit.TabIndex = 19;
            this.btnCommit.Text = "Сохранить изменения";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата";
            // 
            // pnlClient
            // 
            this.pnlClient.Controls.Add(this.txtTelephone);
            this.pnlClient.Controls.Add(this.txtClientName);
            this.pnlClient.Controls.Add(this.label2);
            this.pnlClient.Controls.Add(this.label3);
            this.pnlClient.Controls.Add(this.label5);
            this.pnlClient.Controls.Add(this.cmbSpecialisation);
            this.pnlClient.Location = new System.Drawing.Point(370, 36);
            this.pnlClient.Name = "pnlClient";
            this.pnlClient.Size = new System.Drawing.Size(220, 80);
            this.pnlClient.TabIndex = 24;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(61, 26);
            this.txtTelephone.Mask = "+0 (999) 000-0000";
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(156, 20);
            this.txtTelephone.TabIndex = 8;
            this.txtTelephone.Text = "7";
            this.txtTelephone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(61, 2);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(156, 20);
            this.txtClientName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Клиент";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Телефон";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Специализация";
            // 
            // cmbSpecialisation
            // 
            this.cmbSpecialisation.FormattingEnabled = true;
            this.cmbSpecialisation.Location = new System.Drawing.Point(95, 52);
            this.cmbSpecialisation.Name = "cmbSpecialisation";
            this.cmbSpecialisation.Size = new System.Drawing.Size(122, 21);
            this.cmbSpecialisation.TabIndex = 11;
            // 
            // txtAdministrator
            // 
            this.txtAdministrator.Location = new System.Drawing.Point(92, 93);
            this.txtAdministrator.Name = "txtAdministrator";
            this.txtAdministrator.Size = new System.Drawing.Size(272, 20);
            this.txtAdministrator.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Администратор";
            // 
            // ReceptionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.MaximumSize = new System.Drawing.Size(595, 150);
            this.MinimumSize = new System.Drawing.Size(595, 150);
            this.Name = "ReceptionInfo";
            this.Size = new System.Drawing.Size(595, 150);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrice)).EndInit();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericPrice;
        private System.Windows.Forms.TextBox txtAdministrator;
        private System.Windows.Forms.Label label8;
    }
}
