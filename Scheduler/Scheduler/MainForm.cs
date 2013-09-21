/*
 * Created by SharpDevelop.
 * User: ANC_04
 * Date: 09.05.2013
 * Time: 14:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms; 

namespace Scheduler
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Дата, на которую строится расписание.
		/// </summary>
		public DateTime schedule_date = DateTime.Today;
		/// <summary>
		/// Флаг, указывающий, была ли дата изменена путем ввода строки в dataPicker (true) или выбором даты в календаре (false)
		/// </summary>
		bool changedByHand = false;

		public MainForm(DbConnect database)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			schedule.ColumnCount = 1 + database.Count("SELECT count(*) FROM cab");
			datePicker.Text = schedule_date.Day.ToString() + "." + 
				schedule_date.Month.ToString() + "." + schedule_date.Year.ToString();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void MonthCalendarDateChanged(object sender, DateRangeEventArgs e)
		{
			changedByHand = false;
			schedule_date = e.Start;
			datePicker.Text = schedule_date.Day.ToString() + "." + 
				schedule_date.Month.ToString() + "." + schedule_date.Year.ToString();
		}
		
		void DatePickerTextChanged(object sender, EventArgs e)
		{
			string[] date = datePicker.Text.Split(' ');
			string[] date_parsed = date[0].Split('.');
//			if(Convert.ToInt32(date_parsed[0]) / 100 == 0)
//				date_parsed[0] = "20" + date_parsed[0];
			if(date_parsed.Length == 3 && date_parsed[2].Length == 4)
				schedule_date = new DateTime(Convert.ToInt32(date_parsed[2]), Convert.ToInt32(date_parsed[1]), Convert.ToInt32(date_parsed[0]));
			if(changedByHand)
			{
				monthCalendar.SetSelectionRange(schedule_date, schedule_date);
				monthCalendar.SetDate(schedule_date);
				
			}
			
		}
//		string DateToString(DateTime date)
//		{
//			string day = date.Day.ToString();
			//if
//		}
	}
}
