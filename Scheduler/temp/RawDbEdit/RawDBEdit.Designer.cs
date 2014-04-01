namespace RawDbEdit
{
    partial class RawDBEdit
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.initToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateFromDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initToolStripMenuItem,
            this.updateToDBToolStripMenuItem,
            this.updateFromDBToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1128, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // initToolStripMenuItem
            // 
            this.initToolStripMenuItem.Name = "initToolStripMenuItem";
            this.initToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.initToolStripMenuItem.Text = "Init";
            this.initToolStripMenuItem.Click += new System.EventHandler(this.initToolStripMenuItem_Click);
            // 
            // updateToDBToolStripMenuItem
            // 
            this.updateToDBToolStripMenuItem.Name = "updateToDBToolStripMenuItem";
            this.updateToDBToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.updateToDBToolStripMenuItem.Text = "UpdateToDB";
            this.updateToDBToolStripMenuItem.Click += new System.EventHandler(this.updateToDBToolStripMenuItem_Click);
            // 
            // updateFromDBToolStripMenuItem
            // 
            this.updateFromDBToolStripMenuItem.Enabled = false;
            this.updateFromDBToolStripMenuItem.Name = "updateFromDBToolStripMenuItem";
            this.updateFromDBToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.updateFromDBToolStripMenuItem.Text = "UpdateFromDB";
            this.updateFromDBToolStripMenuItem.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1128, 531);
            this.tabControl1.TabIndex = 1;
            // 
            // RawDBEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 555);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RawDBEdit";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem initToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateFromDBToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
    }
}

