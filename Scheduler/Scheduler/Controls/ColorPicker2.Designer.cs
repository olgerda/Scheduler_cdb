namespace Scheduler.Controls
{
    partial class ColorPicker2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.pnl3 = new System.Windows.Forms.Panel();
            this.brush1 = new Scheduler.Controls.FontCombo.BrushSelect();
            this.brush2 = new Scheduler.Controls.FontCombo.BrushSelect();
            this.brush3 = new Scheduler.Controls.FontCombo.BrushSelect();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fontComboBox1 = new Scheduler.Controls.FontCombo.FontComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.pnl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.brush1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.brush2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.brush3, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(434, 77);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnl1
            // 
            this.pnl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl1.Location = new System.Drawing.Point(3, 3);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(138, 46);
            this.pnl1.TabIndex = 1;
            this.pnl1.Click += new System.EventHandler(this.btn_Click);
            // 
            // pnl2
            // 
            this.pnl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl2.Location = new System.Drawing.Point(147, 3);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(138, 46);
            this.pnl2.TabIndex = 2;
            this.pnl2.Click += new System.EventHandler(this.btn_Click);
            // 
            // pnl3
            // 
            this.pnl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl3.Location = new System.Drawing.Point(291, 3);
            this.pnl3.Name = "pnl3";
            this.pnl3.Size = new System.Drawing.Size(140, 46);
            this.pnl3.TabIndex = 3;
            this.pnl3.Click += new System.EventHandler(this.btn_Click);
            // 
            // brush1
            // 
            this.brush1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brush1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.brush1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.brush1.FormattingEnabled = true;
            this.brush1.Location = new System.Drawing.Point(3, 55);
            this.brush1.Name = "brush1";
            this.brush1.SelectedItem = null;
            this.brush1.Size = new System.Drawing.Size(138, 21);
            this.brush1.TabIndex = 4;
            this.brush1.SelectedIndexChanged += new System.EventHandler(this.brush1_SelectedIndexChanged);
            // 
            // brush2
            // 
            this.brush2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brush2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.brush2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.brush2.FormattingEnabled = true;
            this.brush2.Location = new System.Drawing.Point(147, 55);
            this.brush2.Name = "brush2";
            this.brush2.SelectedItem = null;
            this.brush2.Size = new System.Drawing.Size(138, 21);
            this.brush2.TabIndex = 5;
            this.brush2.SelectedIndexChanged += new System.EventHandler(this.brush1_SelectedIndexChanged);
            // 
            // brush3
            // 
            this.brush3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brush3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.brush3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.brush3.FormattingEnabled = true;
            this.brush3.Location = new System.Drawing.Point(291, 55);
            this.brush3.Name = "brush3";
            this.brush3.SelectedItem = null;
            this.brush3.Size = new System.Drawing.Size(140, 21);
            this.brush3.TabIndex = 6;
            this.brush3.SelectedIndexChanged += new System.EventHandler(this.brush1_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fontComboBox1);
            this.splitContainer1.Size = new System.Drawing.Size(434, 106);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.TabIndex = 1;
            // 
            // fontComboBox1
            // 
            this.fontComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fontComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.fontComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontComboBox1.FormattingEnabled = true;
            this.fontComboBox1.IntegralHeight = false;
            this.fontComboBox1.Location = new System.Drawing.Point(0, 0);
            this.fontComboBox1.MaxDropDownItems = 20;
            this.fontComboBox1.Name = "fontComboBox1";
            this.fontComboBox1.Size = new System.Drawing.Size(434, 21);
            this.fontComboBox1.TabIndex = 0;
            // 
            // ColorPicker2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(55, 50);
            this.Name = "ColorPicker2";
            this.Size = new System.Drawing.Size(434, 106);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.Panel pnl3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FontCombo.FontComboBox fontComboBox1;
        private FontCombo.BrushSelect brush1;
        private FontCombo.BrushSelect brush2;
        private FontCombo.BrushSelect brush3;
    }
}
