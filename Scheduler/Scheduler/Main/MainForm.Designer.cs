/*
 * Created by SharpDevelop.
 * User: ANC_04
 * Date: 09.05.2013
 * Time: 14:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Scheduler
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainView = new System.Windows.Forms.SplitContainer();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.расписаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сформироватьОтчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.спискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кабинетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.специалистыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.специальностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сделатьРезервнуюКопиюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.развернутьРезервнуюКопиюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инструкцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.контактыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).BeginInit();
            this.mainView.Panel2.SuspendLayout();
            this.mainView.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainView
            // 
            this.mainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainView.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.mainView.IsSplitterFixed = true;
            this.mainView.Location = new System.Drawing.Point(0, 24);
            this.mainView.Name = "mainView";
            // 
            // mainView.Panel1
            // 
            this.mainView.Panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            // 
            // mainView.Panel2
            // 
            this.mainView.Panel2.Controls.Add(this.dateTimePicker1);
            this.mainView.Panel2.Controls.Add(this.label1);
            this.mainView.Size = new System.Drawing.Size(696, 407);
            this.mainView.SplitterDistance = 538;
            this.mainView.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 28);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(146, 20);
            this.dateTimePicker1.TabIndex = 7;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Расписание  на";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.расписаниеToolStripMenuItem,
            this.спискиToolStripMenuItem,
            this.базаДанныхToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(696, 24);
            this.mainMenu.TabIndex = 4;
            this.mainMenu.Text = "mainMenu";
            // 
            // расписаниеToolStripMenuItem
            // 
            this.расписаниеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сформироватьОтчетToolStripMenuItem,
            this.закрытьToolStripMenuItem});
            this.расписаниеToolStripMenuItem.Name = "расписаниеToolStripMenuItem";
            this.расписаниеToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.расписаниеToolStripMenuItem.Text = "Файл";
            // 
            // сформироватьОтчетToolStripMenuItem
            // 
            this.сформироватьОтчетToolStripMenuItem.Name = "сформироватьОтчетToolStripMenuItem";
            this.сформироватьОтчетToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.сформироватьОтчетToolStripMenuItem.Text = "Сформировать отчет";
            // 
            // закрытьToolStripMenuItem
            // 
            this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.закрытьToolStripMenuItem.Text = "Закрыть";
            this.закрытьToolStripMenuItem.Click += new System.EventHandler(this.закрытьToolStripMenuItem_Click);
            // 
            // спискиToolStripMenuItem
            // 
            this.спискиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.кабинетыToolStripMenuItem,
            this.специалистыToolStripMenuItem,
            this.специальностиToolStripMenuItem});
            this.спискиToolStripMenuItem.Name = "спискиToolStripMenuItem";
            this.спискиToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.спискиToolStripMenuItem.Text = "Списки";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // кабинетыToolStripMenuItem
            // 
            this.кабинетыToolStripMenuItem.Name = "кабинетыToolStripMenuItem";
            this.кабинетыToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.кабинетыToolStripMenuItem.Text = "Кабинеты";
            this.кабинетыToolStripMenuItem.Click += new System.EventHandler(this.кабинетыToolStripMenuItem_Click);
            // 
            // специалистыToolStripMenuItem
            // 
            this.специалистыToolStripMenuItem.Name = "специалистыToolStripMenuItem";
            this.специалистыToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.специалистыToolStripMenuItem.Text = "Специалисты";
            this.специалистыToolStripMenuItem.Click += new System.EventHandler(this.специалистыToolStripMenuItem_Click);
            // 
            // специальностиToolStripMenuItem
            // 
            this.специальностиToolStripMenuItem.Name = "специальностиToolStripMenuItem";
            this.специальностиToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.специальностиToolStripMenuItem.Text = "Специальности";
            this.специальностиToolStripMenuItem.Click += new System.EventHandler(this.специальностиToolStripMenuItem_Click);
            // 
            // базаДанныхToolStripMenuItem
            // 
            this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сделатьРезервнуюКопиюToolStripMenuItem,
            this.развернутьРезервнуюКопиюToolStripMenuItem});
            this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.базаДанныхToolStripMenuItem.Text = "База данных";
            // 
            // сделатьРезервнуюКопиюToolStripMenuItem
            // 
            this.сделатьРезервнуюКопиюToolStripMenuItem.Name = "сделатьРезервнуюКопиюToolStripMenuItem";
            this.сделатьРезервнуюКопиюToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.сделатьРезервнуюКопиюToolStripMenuItem.Text = "Сделать резервную копию";
            this.сделатьРезервнуюКопиюToolStripMenuItem.Click += new System.EventHandler(this.сделатьРезервнуюКопиюToolStripMenuItem_Click);
            // 
            // развернутьРезервнуюКопиюToolStripMenuItem
            // 
            this.развернутьРезервнуюКопиюToolStripMenuItem.Name = "развернутьРезервнуюКопиюToolStripMenuItem";
            this.развернутьРезервнуюКопиюToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.развернутьРезервнуюКопиюToolStripMenuItem.Text = "Развернуть резервную копию";
            this.развернутьРезервнуюКопиюToolStripMenuItem.Click += new System.EventHandler(this.развернутьРезервнуюКопиюToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.инструкцииToolStripMenuItem,
            this.контактыToolStripMenuItem});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Visible = false;
            // 
            // инструкцииToolStripMenuItem
            // 
            this.инструкцииToolStripMenuItem.Name = "инструкцииToolStripMenuItem";
            this.инструкцииToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.инструкцииToolStripMenuItem.Text = "Инструкции";
            // 
            // контактыToolStripMenuItem
            // 
            this.контактыToolStripMenuItem.Name = "контактыToolStripMenuItem";
            this.контактыToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.контактыToolStripMenuItem.Text = "Контакты";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 431);
            this.Controls.Add(this.mainView);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Scheduler";
            this.mainView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).EndInit();
            this.mainView.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripMenuItem контактыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem инструкцииToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem развернутьРезервнуюКопиюToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem сделатьРезервнуюКопиюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сформироватьОтчетToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem расписаниеToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.SplitContainer mainView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem спискиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кабинетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem специалистыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem специальностиToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
	}
}
