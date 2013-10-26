namespace Scheduler.DetailViews
{
    partial class ClientCardsView
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
            this.grpClientReceptions = new System.Windows.Forms.GroupBox();
            this.lstClientReceptions = new System.Windows.Forms.ListBox();
            this.grpClientReceptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpClientReceptions
            // 
            this.grpClientReceptions.Controls.Add(this.lstClientReceptions);
            this.grpClientReceptions.Location = new System.Drawing.Point(349, 12);
            this.grpClientReceptions.Name = "grpClientReceptions";
            this.grpClientReceptions.Size = new System.Drawing.Size(390, 262);
            this.grpClientReceptions.TabIndex = 3;
            this.grpClientReceptions.TabStop = false;
            this.grpClientReceptions.Text = "Посещения";
            // 
            // lstClientReceptions
            // 
            this.lstClientReceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstClientReceptions.FormattingEnabled = true;
            this.lstClientReceptions.Location = new System.Drawing.Point(3, 16);
            this.lstClientReceptions.Name = "lstClientReceptions";
            this.lstClientReceptions.Size = new System.Drawing.Size(384, 243);
            this.lstClientReceptions.TabIndex = 0;
            // 
            // ClientCardsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 437);
            this.Controls.Add(this.grpClientReceptions);
            this.Name = "ClientCardsView";
            this.Text = "ClientCardsView";
            this.grpClientReceptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpClientReceptions;
        private System.Windows.Forms.ListBox lstClientReceptions;
    }
}