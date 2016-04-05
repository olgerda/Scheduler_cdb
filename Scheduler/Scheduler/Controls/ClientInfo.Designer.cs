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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numGenerallyPrice = new System.Windows.Forms.NumericUpDown();
            this.dateGenerallyTime = new System.Windows.Forms.DateTimePicker();
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtAdministrator = new System.Windows.Forms.TextBox();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGenerallyPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.txtAdministrator);
            this.grpMain.Controls.Add(this.label7);
            this.grpMain.Controls.Add(this.label6);
            this.grpMain.Controls.Add(this.label5);
            this.grpMain.Controls.Add(this.numGenerallyPrice);
            this.grpMain.Controls.Add(this.dateGenerallyTime);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(180, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Стандартная стоимость:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Стандартное время:";
            // 
            // numGenerallyPrice
            // 
            this.numGenerallyPrice.Location = new System.Drawing.Point(318, 158);
            this.numGenerallyPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numGenerallyPrice.Name = "numGenerallyPrice";
            this.numGenerallyPrice.Size = new System.Drawing.Size(75, 20);
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
            this.dateGenerallyTime.Location = new System.Drawing.Point(122, 158);
            this.dateGenerallyTime.Name = "dateGenerallyTime";
            this.dateGenerallyTime.ShowUpDown = true;
            this.dateGenerallyTime.Size = new System.Drawing.Size(51, 20);
            this.dateGenerallyTime.TabIndex = 22;
            this.dateGenerallyTime.Value = new System.DateTime(2014, 2, 2, 10, 0, 0, 0);
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
            this.lstReceptions.Size = new System.Drawing.Size(308, 56);
            this.lstReceptions.TabIndex = 4;
            this.lstReceptions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstReceptions_MouseDoubleClick);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(85, 135);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(308, 20);
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
            this.chkBlackList.Location = new System.Drawing.Point(9, 92);
            this.chkBlackList.Name = "chkBlackList";
            this.chkBlackList.Size = new System.Drawing.Size(62, 17);
            this.chkBlackList.TabIndex = 12;
            this.chkBlackList.Text = "RedList";
            this.chkBlackList.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Администратор";
            // 
            // txtAdministrator
            // 
            this.txtAdministrator.Location = new System.Drawing.Point(92, 109);
            this.txtAdministrator.Name = "txtAdministrator";
            this.txtAdministrator.Size = new System.Drawing.Size(301, 20);
            this.txtAdministrator.TabIndex = 27;
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
            ((System.ComponentModel.ISupportInitialize)(this.numGenerallyPrice)).EndInit();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numGenerallyPrice;
        private System.Windows.Forms.DateTimePicker dateGenerallyTime;
        private System.Windows.Forms.TextBox txtAdministrator;
        private System.Windows.Forms.Label label7;
    }
}
