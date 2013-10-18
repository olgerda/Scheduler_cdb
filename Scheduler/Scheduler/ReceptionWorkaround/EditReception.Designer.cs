namespace Scheduler
{
    partial class EditReception
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAcceptChanges = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.chkRent = new System.Windows.Forms.CheckBox();
            this.grpCabinet = new System.Windows.Forms.GroupBox();
            this.cmbbxCabinet = new System.Windows.Forms.ComboBox();
            this.grpSpecialist = new System.Windows.Forms.GroupBox();
            this.cmbbxSpecialist = new System.Windows.Forms.ComboBox();
            this.btnCancelReception = new System.Windows.Forms.Button();
            this.btnNextReception = new System.Windows.Forms.Button();
            this.grpSpecialization = new System.Windows.Forms.GroupBox();
            this.cmbbxSpecialization = new System.Windows.Forms.ComboBox();
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkClientinRedBox = new System.Windows.Forms.CheckBox();
            this.grpClientFIO = new System.Windows.Forms.GroupBox();
            this.cmbbxClientFIO = new System.Windows.Forms.ComboBox();
            this.grpClientTelephone = new System.Windows.Forms.GroupBox();
            this.txtClientTelephone = new System.Windows.Forms.MaskedTextBox();
            this.grpClientComment = new System.Windows.Forms.GroupBox();
            this.txtClientComment = new System.Windows.Forms.TextBox();
            this.btnShowClientCard = new System.Windows.Forms.Button();
            this.grpDateTimePickers = new System.Windows.Forms.GroupBox();
            this.calendarPicker = new System.Windows.Forms.MonthCalendar();
            this.timePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.timePickerStart = new System.Windows.Forms.DateTimePicker();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpCabinet.SuspendLayout();
            this.grpSpecialist.SuspendLayout();
            this.grpSpecialization.SuspendLayout();
            this.grpClient.SuspendLayout();
            this.grpClientFIO.SuspendLayout();
            this.grpClientTelephone.SuspendLayout();
            this.grpClientComment.SuspendLayout();
            this.grpDateTimePickers.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnAcceptChanges);
            this.splitContainer1.Panel2.Controls.Add(this.btnReturn);
            this.splitContainer1.Panel2.Controls.Add(this.chkRent);
            this.splitContainer1.Panel2.Controls.Add(this.grpCabinet);
            this.splitContainer1.Panel2.Controls.Add(this.grpSpecialist);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelReception);
            this.splitContainer1.Panel2.Controls.Add(this.btnNextReception);
            this.splitContainer1.Panel2.Controls.Add(this.grpSpecialization);
            this.splitContainer1.Panel2.Controls.Add(this.grpClient);
            this.splitContainer1.Panel2.Controls.Add(this.grpDateTimePickers);
            this.splitContainer1.Size = new System.Drawing.Size(793, 317);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnAcceptChanges
            // 
            this.btnAcceptChanges.Location = new System.Drawing.Point(404, 287);
            this.btnAcceptChanges.Name = "btnAcceptChanges";
            this.btnAcceptChanges.Size = new System.Drawing.Size(143, 23);
            this.btnAcceptChanges.TabIndex = 18;
            this.btnAcceptChanges.Text = "Подтвердить изменения";
            this.btnAcceptChanges.UseVisualStyleBackColor = true;
            this.btnAcceptChanges.Click += new System.EventHandler(this.btnAcceptChanges_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(553, 287);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 17;
            this.btnReturn.Text = "Вернуться";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // chkRent
            // 
            this.chkRent.AutoSize = true;
            this.chkRent.Location = new System.Drawing.Point(3, 291);
            this.chkRent.Name = "chkRent";
            this.chkRent.Size = new System.Drawing.Size(63, 17);
            this.chkRent.TabIndex = 16;
            this.chkRent.Text = "Аренда";
            this.chkRent.UseVisualStyleBackColor = true;
            this.chkRent.CheckedChanged += new System.EventHandler(this.RentCheckChanged);
            // 
            // grpCabinet
            // 
            this.grpCabinet.Controls.Add(this.cmbbxCabinet);
            this.grpCabinet.Location = new System.Drawing.Point(209, 238);
            this.grpCabinet.Name = "grpCabinet";
            this.grpCabinet.Size = new System.Drawing.Size(419, 43);
            this.grpCabinet.TabIndex = 15;
            this.grpCabinet.TabStop = false;
            this.grpCabinet.Text = "Кабинет";
            // 
            // cmbbxCabinet
            // 
            this.cmbbxCabinet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbbxCabinet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbxCabinet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbxCabinet.Location = new System.Drawing.Point(3, 16);
            this.cmbbxCabinet.Name = "cmbbxCabinet";
            this.cmbbxCabinet.Size = new System.Drawing.Size(413, 21);
            this.cmbbxCabinet.TabIndex = 0;
            // 
            // grpSpecialist
            // 
            this.grpSpecialist.Controls.Add(this.cmbbxSpecialist);
            this.grpSpecialist.Location = new System.Drawing.Point(209, 179);
            this.grpSpecialist.Name = "grpSpecialist";
            this.grpSpecialist.Size = new System.Drawing.Size(419, 52);
            this.grpSpecialist.TabIndex = 9;
            this.grpSpecialist.TabStop = false;
            this.grpSpecialist.Text = "Специалист";
            // 
            // cmbbxSpecialist
            // 
            this.cmbbxSpecialist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbbxSpecialist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbxSpecialist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbxSpecialist.Location = new System.Drawing.Point(3, 16);
            this.cmbbxSpecialist.Name = "cmbbxSpecialist";
            this.cmbbxSpecialist.Size = new System.Drawing.Size(413, 21);
            this.cmbbxSpecialist.TabIndex = 1;
            // 
            // btnCancelReception
            // 
            this.btnCancelReception.Location = new System.Drawing.Point(266, 287);
            this.btnCancelReception.Name = "btnCancelReception";
            this.btnCancelReception.Size = new System.Drawing.Size(123, 23);
            this.btnCancelReception.TabIndex = 10;
            this.btnCancelReception.Text = "Отменить приём";
            this.btnCancelReception.UseVisualStyleBackColor = true;
            // 
            // btnNextReception
            // 
            this.btnNextReception.Location = new System.Drawing.Point(72, 287);
            this.btnNextReception.Name = "btnNextReception";
            this.btnNextReception.Size = new System.Drawing.Size(176, 23);
            this.btnNextReception.TabIndex = 9;
            this.btnNextReception.Text = "Назначить следующий приём";
            this.btnNextReception.UseVisualStyleBackColor = true;
            // 
            // grpSpecialization
            // 
            this.grpSpecialization.Controls.Add(this.cmbbxSpecialization);
            this.grpSpecialization.Location = new System.Drawing.Point(3, 237);
            this.grpSpecialization.Name = "grpSpecialization";
            this.grpSpecialization.Size = new System.Drawing.Size(200, 44);
            this.grpSpecialization.TabIndex = 8;
            this.grpSpecialization.TabStop = false;
            this.grpSpecialization.Text = "Специализация";
            // 
            // cmbbxSpecialization
            // 
            this.cmbbxSpecialization.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbbxSpecialization.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbxSpecialization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbxSpecialization.Location = new System.Drawing.Point(3, 16);
            this.cmbbxSpecialization.Name = "cmbbxSpecialization";
            this.cmbbxSpecialization.Size = new System.Drawing.Size(194, 21);
            this.cmbbxSpecialization.TabIndex = 1;
            // 
            // grpClient
            // 
            this.grpClient.Controls.Add(this.button1);
            this.grpClient.Controls.Add(this.chkClientinRedBox);
            this.grpClient.Controls.Add(this.grpClientFIO);
            this.grpClient.Controls.Add(this.grpClientTelephone);
            this.grpClient.Controls.Add(this.grpClientComment);
            this.grpClient.Controls.Add(this.btnShowClientCard);
            this.grpClient.Location = new System.Drawing.Point(209, 3);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(419, 170);
            this.grpClient.TabIndex = 3;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Клиент";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 36);
            this.button1.TabIndex = 17;
            this.button1.Text = "Создать новую карточку клиента";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chkClientinRedBox
            // 
            this.chkClientinRedBox.AutoSize = true;
            this.chkClientinRedBox.Enabled = false;
            this.chkClientinRedBox.Location = new System.Drawing.Point(9, 110);
            this.chkClientinRedBox.Name = "chkClientinRedBox";
            this.chkClientinRedBox.Size = new System.Drawing.Size(119, 17);
            this.chkClientinRedBox.TabIndex = 16;
            this.chkClientinRedBox.Text = "В красном списке";
            this.chkClientinRedBox.UseVisualStyleBackColor = true;
            // 
            // grpClientFIO
            // 
            this.grpClientFIO.Controls.Add(this.cmbbxClientFIO);
            this.grpClientFIO.Location = new System.Drawing.Point(6, 16);
            this.grpClientFIO.Name = "grpClientFIO";
            this.grpClientFIO.Size = new System.Drawing.Size(404, 42);
            this.grpClientFIO.TabIndex = 15;
            this.grpClientFIO.TabStop = false;
            this.grpClientFIO.Text = "ФИО";
            // 
            // cmbbxClientFIO
            // 
            this.cmbbxClientFIO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbbxClientFIO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbxClientFIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbxClientFIO.Location = new System.Drawing.Point(3, 16);
            this.cmbbxClientFIO.Name = "cmbbxClientFIO";
            this.cmbbxClientFIO.Size = new System.Drawing.Size(398, 21);
            this.cmbbxClientFIO.TabIndex = 0;
            this.cmbbxClientFIO.SelectedIndexChanged += new System.EventHandler(this.ClientFIOChangeHandler);
            // 
            // grpClientTelephone
            // 
            this.grpClientTelephone.Controls.Add(this.txtClientTelephone);
            this.grpClientTelephone.Location = new System.Drawing.Point(6, 64);
            this.grpClientTelephone.Name = "grpClientTelephone";
            this.grpClientTelephone.Size = new System.Drawing.Size(139, 40);
            this.grpClientTelephone.TabIndex = 12;
            this.grpClientTelephone.TabStop = false;
            this.grpClientTelephone.Text = "Телефон";
            // 
            // txtClientTelephone
            // 
            this.txtClientTelephone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClientTelephone.Location = new System.Drawing.Point(3, 16);
            this.txtClientTelephone.Mask = "(999) 000-0000";
            this.txtClientTelephone.Name = "txtClientTelephone";
            this.txtClientTelephone.Size = new System.Drawing.Size(133, 20);
            this.txtClientTelephone.TabIndex = 0;
            this.txtClientTelephone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtClientTelephone.TextChanged += new System.EventHandler(this.TelephoneNumberChangeHandler);
            // 
            // grpClientComment
            // 
            this.grpClientComment.Controls.Add(this.txtClientComment);
            this.grpClientComment.Location = new System.Drawing.Point(148, 64);
            this.grpClientComment.Name = "grpClientComment";
            this.grpClientComment.Size = new System.Drawing.Size(262, 63);
            this.grpClientComment.TabIndex = 14;
            this.grpClientComment.TabStop = false;
            this.grpClientComment.Text = "Комментарий";
            // 
            // txtClientComment
            // 
            this.txtClientComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClientComment.Location = new System.Drawing.Point(3, 16);
            this.txtClientComment.Multiline = true;
            this.txtClientComment.Name = "txtClientComment";
            this.txtClientComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClientComment.Size = new System.Drawing.Size(256, 44);
            this.txtClientComment.TabIndex = 0;
            // 
            // btnShowClientCard
            // 
            this.btnShowClientCard.Location = new System.Drawing.Point(6, 130);
            this.btnShowClientCard.Name = "btnShowClientCard";
            this.btnShowClientCard.Size = new System.Drawing.Size(136, 36);
            this.btnShowClientCard.TabIndex = 11;
            this.btnShowClientCard.Text = "Показать карточку клиента";
            this.btnShowClientCard.UseVisualStyleBackColor = true;
            // 
            // grpDateTimePickers
            // 
            this.grpDateTimePickers.Controls.Add(this.calendarPicker);
            this.grpDateTimePickers.Controls.Add(this.timePickerEnd);
            this.grpDateTimePickers.Controls.Add(this.timePickerStart);
            this.grpDateTimePickers.Controls.Add(this.datePicker);
            this.grpDateTimePickers.Location = new System.Drawing.Point(3, 3);
            this.grpDateTimePickers.Name = "grpDateTimePickers";
            this.grpDateTimePickers.Size = new System.Drawing.Size(200, 228);
            this.grpDateTimePickers.TabIndex = 2;
            this.grpDateTimePickers.TabStop = false;
            this.grpDateTimePickers.Text = "Дата и время";
            // 
            // calendarPicker
            // 
            this.calendarPicker.Location = new System.Drawing.Point(7, 38);
            this.calendarPicker.MaxSelectionCount = 1;
            this.calendarPicker.Name = "calendarPicker";
            this.calendarPicker.ShowWeekNumbers = true;
            this.calendarPicker.TabIndex = 4;
            this.calendarPicker.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.DateChangedHandler);
            // 
            // timePickerEnd
            // 
            this.timePickerEnd.CustomFormat = "H:mm";
            this.timePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerEnd.Location = new System.Drawing.Point(102, 202);
            this.timePickerEnd.Name = "timePickerEnd";
            this.timePickerEnd.ShowUpDown = true;
            this.timePickerEnd.Size = new System.Drawing.Size(91, 20);
            this.timePickerEnd.TabIndex = 3;
            // 
            // timePickerStart
            // 
            this.timePickerStart.CustomFormat = "H:mm";
            this.timePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerStart.Location = new System.Drawing.Point(7, 202);
            this.timePickerStart.Name = "timePickerStart";
            this.timePickerStart.ShowUpDown = true;
            this.timePickerStart.Size = new System.Drawing.Size(89, 20);
            this.timePickerStart.TabIndex = 2;
            this.timePickerStart.Value = new System.DateTime(2013, 10, 14, 16, 4, 0, 0);
            // 
            // datePicker
            // 
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(7, 16);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(186, 20);
            this.datePicker.TabIndex = 1;
            this.datePicker.ValueChanged += new System.EventHandler(this.BoxDateChangedHandler);
            // 
            // EditReception
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 317);
            this.Controls.Add(this.splitContainer1);
            this.Name = "EditReception";
            this.Text = "EditReception";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingFormHandler);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpCabinet.ResumeLayout(false);
            this.grpSpecialist.ResumeLayout(false);
            this.grpSpecialization.ResumeLayout(false);
            this.grpClient.ResumeLayout(false);
            this.grpClient.PerformLayout();
            this.grpClientFIO.ResumeLayout(false);
            this.grpClientTelephone.ResumeLayout(false);
            this.grpClientTelephone.PerformLayout();
            this.grpClientComment.ResumeLayout(false);
            this.grpClientComment.PerformLayout();
            this.grpDateTimePickers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpDateTimePickers;
        private System.Windows.Forms.DateTimePicker timePickerEnd;
        private System.Windows.Forms.DateTimePicker timePickerStart;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.MonthCalendar calendarPicker;
        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.Button btnShowClientCard;
        private System.Windows.Forms.Button btnCancelReception;
        private System.Windows.Forms.Button btnNextReception;
        private System.Windows.Forms.GroupBox grpSpecialization;
        private System.Windows.Forms.ComboBox cmbbxSpecialization;
        private System.Windows.Forms.GroupBox grpCabinet;
        private System.Windows.Forms.ComboBox cmbbxCabinet;
        private System.Windows.Forms.GroupBox grpSpecialist;
        private System.Windows.Forms.ComboBox cmbbxSpecialist;
        private System.Windows.Forms.GroupBox grpClientFIO;
        private System.Windows.Forms.GroupBox grpClientTelephone;
        private System.Windows.Forms.MaskedTextBox txtClientTelephone;
        private System.Windows.Forms.GroupBox grpClientComment;
        private System.Windows.Forms.TextBox txtClientComment;
        private System.Windows.Forms.CheckBox chkRent;
        private System.Windows.Forms.ComboBox cmbbxClientFIO;
        private System.Windows.Forms.Button btnAcceptChanges;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.CheckBox chkClientinRedBox;
        private System.Windows.Forms.Button button1;
    }
}