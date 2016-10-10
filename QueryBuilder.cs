using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyRunningApplication
{
	public class QueryBuilder
	{
		public QueryBuilder() {

		}

		// Builds the SQL command to insert a distance for a date.
		public SqlCommand BuildAddDistanceCommand(SqlConnection conn, DateTime date, double distance, int? duration, string notes) {
			SqlCommand cmd = new SqlCommand("INSERT INTO Distance (Date, Distance, Duration, Notes) VALUES (@0, @1, @2, @3);", conn);
			cmd.Parameters.Add(new SqlParameter("0", date));
			cmd.Parameters.Add(new SqlParameter("1", distance));
			cmd.Parameters.Add(new SqlParameter("2", duration));
			cmd.Parameters.Add(new SqlParameter("3", notes));

			return cmd;
		}

		// Builds the query for getting the total distance between two dates.
		public SqlCommand BuildDistanceQuery(SqlConnection conn, DateTime start, DateTime end) {
			SqlCommand cmd = new SqlCommand("SELECT SUM(Distance) FROM Distance WHERE Date >= @0 AND Date <= @1;", conn);
			cmd.Parameters.Add(new SqlParameter("0", start));
			cmd.Parameters.Add(new SqlParameter("1", end));

			return cmd;
		}
	}
}
