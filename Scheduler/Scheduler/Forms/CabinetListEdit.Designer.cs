namespace Scheduler_Forms
{
    partial class CabinetListEdit
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
            this.cabinetInfoCard = new Scheduler_Controls.CabinetInfo();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpCabinetList = new System.Windows.Forms.GroupBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lstCabinets = new System.Windows.Forms.ListBox();
            this.grpCabinetList.SuspendLayout();
            this.SuspendLayout();
            // 
            // cabinetInfoCard
            // 
            this.cabinetInfoCard.Cabinet = null;
            this.cabinetInfoCard.Enabled = false;
            this.cabinetInfoCard.Location = new System.Drawing.Point(2, 179);
            this.cabinetInfoCard.MaximumSize = new System.Drawing.Size(320, 65);
            this.cabinetInfoCard.MinimumSize = new System.Drawing.Size(320, 65);
            this.cabinetInfoCard.Name = "cabinetInfoCard";
            this.cabinetInfoCard.Size = new System.Drawing.Size(320, 65);
            this.cabinetInfoCard.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 148);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpCabinetList
            // 
            this.grpCabinetList.Controls.Add(this.btnReturn);
            this.grpCabinetList.Controls.Add(this.btnEdit);
            this.grpCabinetList.Controls.Add(this.lstCabinets);
            this.grpCabinetList.Controls.Add(this.btnAdd);
            this.grpCabinetList.Location = new System.Drawing.Point(2, 2);
            this.grpCabinetList.Name = "grpCabinetList";
            this.grpCabinetList.Size = new System.Drawing.Size(320, 177);
            this.grpCabinetList.TabIndex = 2;
            this.grpCabinetList.TabStop = false;
            this.grpCabinetList.Text = "Список кабинетов";
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(239, 148);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "Возврат";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(113, 148);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lstCabinets
            // 
            this.lstCabinets.FormattingEnabled = true;
            this.lstCabinets.Location = new System.Drawing.Point(6, 19);
            this.lstCabinets.Name = "lstCabinets";
            this.lstCabinets.Size = new System.Drawing.Size(308, 121);
            this.lstCabinets.TabIndex = 2;
            this.lstCabinets.SelectedIndexChanged += new System.EventHandler(this.lstCabinets_SelectedIndexChanged);
            // 
            // CabinetListEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 248);
            this.Controls.Add(this.grpCabinetList);
            this.Controls.Add(this.cabinetInfoCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CabinetListEdit";
            this.Text = "CabinetListEdit";
            this.grpCabinetList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Scheduler_Controls.CabinetInfo cabinetInfoCard;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpCabinetList;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ListBox lstCabinets;
    }
}