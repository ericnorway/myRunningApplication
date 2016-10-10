using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyRunningApplication
{
	public class RunningLogic
	{
		private const string connectionString = "Data Source = .\\SQLEXPRESS; Initial Catalog = Running; Integrated Security = True";

		public RunningLogic() {

		}

		// Adds the distance for a date.
		public void AddDistance(DateTime date, double distance, int? duration, string notes) {
			using (SqlConnection conn = new SqlConnection()) {
				conn.ConnectionString = connectionString;
				conn.Open();

				SqlCommand cmd = BuildAddDistanceCommand(conn, date, distance, duration, notes);
				try {
					cmd.ExecuteNonQuery();
				} catch (SqlException ex) {
					if (ex.Number == 2601 || ex.Number == 2627) {
						throw new ArgumentException(string.Format("The distance for date {0} already exists.", date.ToShortDateString()));
					} else {
						throw ex;
					}
				}
			}
		}

		// Builds the SQL command to insert a distance for a date.
		private SqlCommand BuildAddDistanceCommand(SqlConnection conn, DateTime date, double distance, int? duration, string notes) {
			SqlCommand cmd = new SqlCommand("INSERT INTO Distance (Date, Distance, Time, Notes) VALUES (@0, @1, @2, @3);", conn);
			cmd.Parameters.Add(new SqlParameter("0", date));
			cmd.Parameters.Add(new SqlParameter("1", distance));
			cmd.Parameters.Add(new SqlParameter("2", duration));
			cmd.Parameters.Add(new SqlParameter("3", notes));

			return cmd;
		}

		// Gets the week-to-date, month-to-date, and year-to-date distances.
		public Tuple<double, double, double> GetDistancesGeneric() {
			double weekly = 0.0;
			double monthly = 0.0;
			double yearly = 0.0;

			using (SqlConnection conn = new SqlConnection()) {
				conn.ConnectionString = connectionString;
				conn.Open();

				weekly = RunWeeklyQuery(conn);
				monthly = RunMonthlyQuery(conn);
				yearly = RunYearlyQuery(conn);
			}

			return new Tuple<double, double, double>(weekly, monthly, yearly);
		}

		// Gets the total distance ran between two dates.
		public double GetDistanceCustom(DateTime start, DateTime end) {
			double distance = 0.0;

			using (SqlConnection conn = new SqlConnection()) {
				conn.ConnectionString = connectionString;
				conn.Open();

				distance = RunCustomQuery(conn, start, end);
			}

			return distance;
		}

		// Gets the total distance from the beginning of the week until now.
		private double RunWeeklyQuery(SqlConnection conn) {
			double value = 0.0;
			DateTime start = GetBeginningOfWeek();
			DateTime end = DateTime.Now.Date;

			SqlCommand cmd = BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}

		// Gets the total distance from the beginning of the month until now.
		private double RunMonthlyQuery(SqlConnection conn) {
			double value = 0.0;
			DateTime start = GetBeginningOfMonth();
			DateTime end = DateTime.Now.Date;

			SqlCommand cmd = BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}

		// Gets the total distance from the beginning of the year until now.
		private double RunYearlyQuery(SqlConnection conn) {
			double value = 0.0;
			DateTime start = GetBeginningOfYear();
			DateTime end = DateTime.Now.Date;

			SqlCommand cmd = BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}

		// Gets the total distance ran between two dates.
		private double RunCustomQuery(SqlConnection conn, DateTime start, DateTime end) {
			double value = 0.0;
			SqlCommand cmd = BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}

		// Builds the query for getting the total distance between two dates.
		private SqlCommand BuildDistanceQuery(SqlConnection conn, DateTime start, DateTime end) {
			SqlCommand cmd = new SqlCommand("SELECT SUM(Distance) FROM Distance WHERE Date >= @0 AND Date <= @1;", conn);
			cmd.Parameters.Add(new SqlParameter("0", start));
			cmd.Parameters.Add(new SqlParameter("1", end));

			return cmd;
		}

		// Gets the beginning of the week.
		private DateTime GetBeginningOfWeek() {
			DayOfWeek currentDay = DateTime.Now.DayOfWeek;
			return DateTime.Now.AddDays(-1 * (int)currentDay).Date;
		}

		// Gets the beginning of the month.
		private DateTime GetBeginningOfMonth() {
			int currentDay = DateTime.Now.Day;
			return DateTime.Now.AddDays(-1 * (currentDay - 1)).Date;
		}

		// Gets the beginning of the year.
		private DateTime GetBeginningOfYear() {
			int currentDay = DateTime.Now.DayOfYear;
			return DateTime.Now.AddDays(-1 * (currentDay - 1)).Date;
		}
	}
}
