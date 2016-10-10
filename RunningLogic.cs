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
		private QueryBuilder queryBuilder;
		private TimeHelper timeHelper;

		public RunningLogic() {
			queryBuilder = new QueryBuilder();
			timeHelper = new TimeHelper();
		}

		// Adds the distance for a date.
		public void AddDistance(DateTime date, double distance, int? duration, string notes) {
			using (SqlConnection conn = new SqlConnection()) {
				conn.ConnectionString = connectionString;
				conn.Open();

				SqlCommand cmd = queryBuilder.BuildAddDistanceCommand(conn, date, distance, duration, notes);
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
			DateTime start = timeHelper.GetBeginningOfWeek();
			DateTime end = DateTime.Now.Date;

			SqlCommand cmd = queryBuilder.BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}

		// Gets the total distance from the beginning of the month until now.
		private double RunMonthlyQuery(SqlConnection conn) {
			double value = 0.0;
			DateTime start = timeHelper.GetBeginningOfMonth();
			DateTime end = DateTime.Now.Date;

			SqlCommand cmd = queryBuilder.BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}

		// Gets the total distance from the beginning of the year until now.
		private double RunYearlyQuery(SqlConnection conn) {
			double value = 0.0;
			DateTime start = timeHelper.GetBeginningOfYear();
			DateTime end = DateTime.Now.Date;

			SqlCommand cmd = queryBuilder.BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}

		// Gets the total distance ran between two dates.
		private double RunCustomQuery(SqlConnection conn, DateTime start, DateTime end) {
			double value = 0.0;
			SqlCommand cmd = queryBuilder.BuildDistanceQuery(conn, start, end);
			using (SqlDataReader reader = cmd.ExecuteReader()) {
				reader.Read();
				value = reader.GetDouble(0);
			}
			return value;
		}
	}
}
