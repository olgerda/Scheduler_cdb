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
            this.lstAdministratorsOnDuty = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstSpecialistsOnDuty = new System.Windows.Forms.ListBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.расписаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сформироватьОтчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сформироватьИзображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.спискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.арендаторыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дежурстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остальныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.специальностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.специалистыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кабинетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.администраторыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сделатьРезервнуюКопиюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.развернутьРезервнуюКопиюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инструкцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.контактыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.специалистыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.арендаторыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.подробноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подробноToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.подробноToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mainView.Panel2.Controls.Add(this.lstAdministratorsOnDuty);
            this.mainView.Panel2.Controls.Add(this.label2);
            this.mainView.Panel2.Controls.Add(this.label1);
            this.mainView.Panel2.Controls.Add(this.lstSpecialistsOnDuty);
            this.mainView.Panel2.Controls.Add(this.monthCalendar1);
            this.mainView.Panel2MinSize = 50;
            this.mainView.Size = new System.Drawing.Size(832, 407);
            this.mainView.SplitterDistance = 650;
            this.mainView.TabIndex = 3;
            this.mainView.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.mainView_SplitterMoved);
            // 
            // lstAdministratorsOnDuty
            // 
            this.lstAdministratorsOnDuty.FormattingEnabled = true;
            this.lstAdministratorsOnDuty.Location = new System.Drawing.Point(9, 326);
            this.lstAdministratorsOnDuty.Name = "lstAdministratorsOnDuty";
            this.lstAdministratorsOnDuty.Size = new System.Drawing.Size(164, 69);
            this.lstAdministratorsOnDuty.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Администраторы";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Специалисты";
            // 
            // lstSpecialistsOnDuty
            // 
            this.lstSpecialistsOnDuty.FormattingEnabled = true;
            this.lstSpecialistsOnDuty.Location = new System.Drawing.Point(9, 203);
            this.lstSpecialistsOnDuty.Name = "lstSpecialistsOnDuty";
            this.lstSpecialistsOnDuty.Size = new System.Drawing.Size(164, 95);
            this.lstSpecialistsOnDuty.TabIndex = 10;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(9, 9);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 9;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.расписаниеToolStripMenuItem,
            this.спискиToolStripMenuItem,
            this.базаДанныхToolStripMenuItem,
            this.помощьToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.статистикаToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(832, 24);
            this.mainMenu.TabIndex = 4;
            this.mainMenu.Text = "mainMenu";
            // 
            // расписаниеToolStripMenuItem
            // 
            this.расписаниеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сформироватьОтчетToolStripMenuItem,
            this.сформироватьИзображениеToolStripMenuItem,
            this.закрытьToolStripMenuItem});
            this.расписаниеToolStripMenuItem.Name = "расписаниеToolStripMenuItem";
            this.расписаниеToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.расписаниеToolStripMenuItem.Text = "Файл";
            // 
            // сформироватьОтчетToolStripMenuItem
            // 
            this.сформироватьОтчетToolStripMenuItem.Name = "сформироватьОтчетToolStripMenuItem";
            this.сформироватьОтчетToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.сформироватьОтчетToolStripMenuItem.Text = "Сформировать отчет";
            this.сформироватьОтчетToolStripMenuItem.Click += new System.EventHandler(this.сформироватьОтчетToolStripMenuItem_Click);
            // 
            // сформироватьИзображениеToolStripMenuItem
            // 
            this.сформироватьИзображениеToolStripMenuItem.Name = "сформироватьИзображениеToolStripMenuItem";
            this.сформироватьИзображениеToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.сформироватьИзображениеToolStripMenuItem.Text = "Сформировать изображение";
            this.сформироватьИзображениеToolStripMenuItem.Click += new System.EventHandler(this.btnCreteFile_Click);
            // 
            // закрытьToolStripMenuItem
            // 
            this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.закрытьToolStripMenuItem.Text = "Закрыть";
            this.закрытьToolStripMenuItem.Click += new System.EventHandler(this.закрытьToolStripMenuItem_Click);
            // 
            // спискиToolStripMenuItem
            // 
            this.спискиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.арендаторыToolStripMenuItem,
            this.дежурстваToolStripMenuItem,
            this.остальныеToolStripMenuItem});
            this.спискиToolStripMenuItem.Name = "спискиToolStripMenuItem";
            this.спискиToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.спискиToolStripMenuItem.Text = "Списки";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // арендаторыToolStripMenuItem
            // 
            this.арендаторыToolStripMenuItem.Name = "арендаторыToolStripMenuItem";
            this.арендаторыToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.арендаторыToolStripMenuItem.Text = "Арендаторы";
            this.арендаторыToolStripMenuItem.Click += new System.EventHandler(this.арендаторыToolStripMenuItem_Click);
            // 
            // дежурстваToolStripMenuItem
            // 
            this.дежурстваToolStripMenuItem.Name = "дежурстваToolStripMenuItem";
            this.дежурстваToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.дежурстваToolStripMenuItem.Text = "Дежурства";
            this.дежурстваToolStripMenuItem.Click += new System.EventHandler(this.дежурстваToolStripMenuItem_Click);
            // 
            // остальныеToolStripMenuItem
            // 
            this.остальныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.специальностиToolStripMenuItem,
            this.специалистыToolStripMenuItem,
            this.кабинетыToolStripMenuItem,
            this.администраторыToolStripMenuItem});
            this.остальныеToolStripMenuItem.Name = "остальныеToolStripMenuItem";
            this.остальныеToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.остальныеToolStripMenuItem.Text = "Остальные";
            // 
            // специальностиToolStripMenuItem
            // 
            this.специальностиToolStripMenuItem.Name = "специальностиToolStripMenuItem";
            this.специальностиToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.специальностиToolStripMenuItem.Text = "Специальности";
            this.специальностиToolStripMenuItem.Click += new System.EventHandler(this.специальностиToolStripMenuItem_Click);
            // 
            // специалистыToolStripMenuItem
            // 
            this.специалистыToolStripMenuItem.Name = "специалистыToolStripMenuItem";
            this.специалистыToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.специалистыToolStripMenuItem.Text = "Специалисты";
            this.специалистыToolStripMenuItem.Click += new System.EventHandler(this.специалистыToolStripMenuItem_Click);
            // 
            // кабинетыToolStripMenuItem
            // 
            this.кабинетыToolStripMenuItem.Name = "кабинетыToolStripMenuItem";
            this.кабинетыToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.кабинетыToolStripMenuItem.Text = "Кабинеты";
            this.кабинетыToolStripMenuItem.Click += new System.EventHandler(this.кабинетыToolStripMenuItem_Click);
            // 
            // администраторыToolStripMenuItem
            // 
            this.администраторыToolStripMenuItem.Name = "администраторыToolStripMenuItem";
            this.администраторыToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.администраторыToolStripMenuItem.Text = "Администраторы";
            this.администраторыToolStripMenuItem.Click += new System.EventHandler(this.администраторыToolStripMenuItem_Click);
            // 
            // базаДанныхToolStripMenuItem
            // 
            this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сделатьРезервнуюКопиюToolStripMenuItem,
            this.развернутьРезервнуюКопиюToolStripMenuItem});
            this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.базаДанныхToolStripMenuItem.Text = "База данных";
            this.базаДанныхToolStripMenuItem.Visible = false;
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
            this.инструкцииToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.инструкцииToolStripMenuItem.Text = "Инструкции";
            // 
            // контактыToolStripMenuItem
            // 
            this.контактыToolStripMenuItem.Name = "контактыToolStripMenuItem";
            this.контактыToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.контактыToolStripMenuItem.Text = "Контакты";
            this.контактыToolStripMenuItem.Click += new System.EventHandler(this.контактыToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem1,
            this.арендаторыToolStripMenuItem1,
            this.специалистыToolStripMenuItem1});
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            // 
            // клиентыToolStripMenuItem1
            // 
            this.клиентыToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подробноToolStripMenuItem});
            this.клиентыToolStripMenuItem1.Name = "клиентыToolStripMenuItem1";
            this.клиентыToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.клиентыToolStripMenuItem1.Text = "Клиенты";
            this.клиентыToolStripMenuItem1.Click += new System.EventHandler(this.GetClientsStatistics);
            // 
            // специалистыToolStripMenuItem1
            // 
            this.специалистыToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подробноToolStripMenuItem2});
            this.специалистыToolStripMenuItem1.Name = "специалистыToolStripMenuItem1";
            this.специалистыToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.специалистыToolStripMenuItem1.Text = "Специалисты";
            this.специалистыToolStripMenuItem1.Click += new System.EventHandler(this.GetSpecialistsStatistics);
            // 
            // арендаторыToolStripMenuItem1
            // 
            this.арендаторыToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подробноToolStripMenuItem1});
            this.арендаторыToolStripMenuItem1.Name = "арендаторыToolStripMenuItem1";
            this.арендаторыToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.арендаторыToolStripMenuItem1.Text = "Арендаторы";
            this.арендаторыToolStripMenuItem1.Click += new System.EventHandler(this.GetArendatorsStatistics);
            // 
            // подробноToolStripMenuItem
            // 
            this.подробноToolStripMenuItem.Name = "подробноToolStripMenuItem";
            this.подробноToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.подробноToolStripMenuItem.Text = "Подробно";
            this.подробноToolStripMenuItem.Click += new System.EventHandler(this.GetClientsReport);
            // 
            // подробноToolStripMenuItem1
            // 
            this.подробноToolStripMenuItem1.Name = "подробноToolStripMenuItem1";
            this.подробноToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.подробноToolStripMenuItem1.Text = "Подробно";
            this.подробноToolStripMenuItem1.Click += new System.EventHandler(this.GetArendatorsReport);
            // 
            // подробноToolStripMenuItem2
            // 
            this.подробноToolStripMenuItem2.Name = "подробноToolStripMenuItem2";
            this.подробноToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.подробноToolStripMenuItem2.Text = "Подробно";
            this.подробноToolStripMenuItem2.Click += new System.EventHandler(this.GetSpecialistsReport);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 431);
            this.Controls.Add(this.mainView);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Scheduler";
            this.mainView.Panel2.ResumeLayout(false);
            this.mainView.Panel2.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem спискиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem арендаторыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дежурстваToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem остальныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem специальностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem специалистыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кабинетыToolStripMenuItem;
        private System.Windows.Forms.ListBox lstAdministratorsOnDuty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstSpecialistsOnDuty;
        private System.Windows.Forms.ToolStripMenuItem сформироватьИзображениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem администраторыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem арендаторыToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem специалистыToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem подробноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подробноToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem подробноToolStripMenuItem2;
    }
}
