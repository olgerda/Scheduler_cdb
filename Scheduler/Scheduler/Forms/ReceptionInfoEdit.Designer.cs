namespace Scheduler_Forms
{
    partial class ReceptionInfoEdit
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
            this.receptionInfoCard = new Scheduler_Controls.ReceptionInfo();
            this.SuspendLayout();
            // 
            // receptionInfoCard
            // 
            this.receptionInfoCard.ClientOnReception = null;
            this.receptionInfoCard.Location = new System.Drawing.Point(12, 12);
            this.receptionInfoCard.MaximumSize = new System.Drawing.Size(595, 150);
            this.receptionInfoCard.MinimumSize = new System.Drawing.Size(595, 150);
            this.receptionInfoCard.Mode = Scheduler_Controls.ReceptionInfo.ShowModes.ReadExist;
            this.receptionInfoCard.Name = "receptionInfoCard";
            this.receptionInfoCard.Reception = null;
            this.receptionInfoCard.Size = new System.Drawing.Size(595, 150);
            this.receptionInfoCard.TabIndex = 0;
            // 
            // ReceptionInfoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 177);
            this.Controls.Add(this.receptionInfoCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "ReceptionInfoEdit";
            this.Text = "ReceptionInfoEdit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReceptionInfoEdit_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceptionInfoEdit_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Scheduler_Controls.ReceptionInfo receptionInfoCard;
    }
}