using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyRunningApplication
{
	public class TimeHelper
	{
		public TimeHelper() {

		}

		// Gets the beginning of the week.
		public DateTime GetBeginningOfWeek() {
			DayOfWeek currentDay = DateTime.Now.DayOfWeek;
			return DateTime.Now.AddDays(-1 * (int)currentDay).Date;
		}

		// Gets the beginning of the month.
		public DateTime GetBeginningOfMonth() {
			int currentDay = DateTime.Now.Day;
			return DateTime.Now.AddDays(-1 * (currentDay - 1)).Date;
		}

		// Gets the beginning of the year.
		public DateTime GetBeginningOfYear() {
			int currentDay = DateTime.Now.DayOfYear;
			return DateTime.Now.AddDays(-1 * (currentDay - 1)).Date;
		}
	}
}
