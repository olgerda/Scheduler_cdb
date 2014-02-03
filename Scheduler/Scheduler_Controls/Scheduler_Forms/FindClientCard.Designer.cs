namespace Scheduler_Forms
{
    partial class FindClientCard
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
            this.clientInfoCard = new Scheduler_Controls.ClientInfo();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstClientList = new System.Windows.Forms.ListBox();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnEditClient = new System.Windows.Forms.Button();
            this.grpSelectClient = new System.Windows.Forms.GroupBox();
            this.grpEditMode = new System.Windows.Forms.GroupBox();
            this.btnEditModeOff = new System.Windows.Forms.Button();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.grpSelectClient.SuspendLayout();
            this.grpEditMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientInfoCard
            // 
            this.clientInfoCard.Client = null;
            this.clientInfoCard.Enabled = false;
            this.clientInfoCard.Location = new System.Drawing.Point(287, 10);
            this.clientInfoCard.MaximumSize = new System.Drawing.Size(400, 265);
            this.clientInfoCard.MinimumSize = new System.Drawing.Size(400, 265);
            this.clientInfoCard.Name = "clientInfoCard";
            this.clientInfoCard.Size = new System.Drawing.Size(400, 265);
            this.clientInfoCard.TabIndex = 0;
            // 
            // txtClientName
            // 
            this.txtClientName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtClientName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtClientName.Location = new System.Drawing.Point(85, 22);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(184, 20);
            this.txtClientName.TabIndex = 1;
            this.txtClientName.TextChanged += new System.EventHandler(this.txtClientName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя клиента";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Телефон клиента";
            // 
            // lstClientList
            // 
            this.lstClientList.FormattingEnabled = true;
            this.lstClientList.Location = new System.Drawing.Point(9, 103);
            this.lstClientList.Name = "lstClientList";
            this.lstClientList.Size = new System.Drawing.Size(260, 121);
            this.lstClientList.TabIndex = 11;
            this.lstClientList.SelectedIndexChanged += new System.EventHandler(this.lstClientList_SelectedIndexChanged);
            // 
            // btnAddClient
            // 
            this.btnAddClient.Location = new System.Drawing.Point(9, 74);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(66, 23);
            this.btnAddClient.TabIndex = 13;
            this.btnAddClient.Text = "Добавить";
            this.btnAddClient.UseVisualStyleBackColor = true;
            this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(214, 74);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(55, 23);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Location = new System.Drawing.Point(9, 230);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(260, 29);
            this.button3.TabIndex = 15;
            this.button3.Text = "Использовать выделенные данные";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnEditClient
            // 
            this.btnEditClient.Location = new System.Drawing.Point(97, 74);
            this.btnEditClient.Name = "btnEditClient";
            this.btnEditClient.Size = new System.Drawing.Size(92, 23);
            this.btnEditClient.TabIndex = 16;
            this.btnEditClient.Text = "Редактировать";
            this.btnEditClient.UseVisualStyleBackColor = true;
            this.btnEditClient.Click += new System.EventHandler(this.btnEditClient_Click);
            // 
            // grpSelectClient
            // 
            this.grpSelectClient.Controls.Add(this.btnExit);
            this.grpSelectClient.Controls.Add(this.grpEditMode);
            this.grpSelectClient.Controls.Add(this.txtTelephone);
            this.grpSelectClient.Controls.Add(this.txtClientName);
            this.grpSelectClient.Controls.Add(this.btnEditClient);
            this.grpSelectClient.Controls.Add(this.button3);
            this.grpSelectClient.Controls.Add(this.label3);
            this.grpSelectClient.Controls.Add(this.btnAddClient);
            this.grpSelectClient.Controls.Add(this.lstClientList);
            this.grpSelectClient.Controls.Add(this.label1);
            this.grpSelectClient.Location = new System.Drawing.Point(5, 10);
            this.grpSelectClient.Name = "grpSelectClient";
            this.grpSelectClient.Size = new System.Drawing.Size(276, 265);
            this.grpSelectClient.TabIndex = 17;
            this.grpSelectClient.TabStop = false;
            this.grpSelectClient.Text = "Поиск/выбор клиента";
            // 
            // grpEditMode
            // 
            this.grpEditMode.Controls.Add(this.btnEditModeOff);
            this.grpEditMode.Enabled = false;
            this.grpEditMode.Location = new System.Drawing.Point(0, 265);
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
            // txtTelephone
            // 
            this.txtTelephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTelephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTelephone.Location = new System.Drawing.Point(109, 48);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(160, 20);
            this.txtTelephone.TabIndex = 17;
            this.txtTelephone.TextChanged += new System.EventHandler(this.txtTelephone_TextChanged);
            // 
            // FindClientCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 281);
            this.Controls.Add(this.grpSelectClient);
            this.Controls.Add(this.clientInfoCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 305);
            this.MinimumSize = new System.Drawing.Size(700, 305);
            this.Name = "FindClientCard";
            this.Text = "Поиск карточки клиента";
            this.grpSelectClient.ResumeLayout(false);
            this.grpSelectClient.PerformLayout();
            this.grpEditMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Scheduler_Controls.ClientInfo clientInfoCard;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstClientList;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnEditClient;
        private System.Windows.Forms.GroupBox grpSelectClient;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.GroupBox grpEditMode;
        private System.Windows.Forms.Button btnEditModeOff;
    }
}