namespace Test
{
    partial class Form1
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
            this.clientInfo1 = new Scheduler_Controls.ClientInfo();
            this.SuspendLayout();
            // 
            // clientInfo1
            // 
            this.clientInfo1.Client = null;
            this.clientInfo1.Location = new System.Drawing.Point(12, 12);
            this.clientInfo1.Name = "clientInfo1";
            this.clientInfo1.Size = new System.Drawing.Size(401, 264);
            this.clientInfo1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 328);
            this.Controls.Add(this.clientInfo1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Scheduler_Controls.ClientInfo clientInfo1;
    }
}

