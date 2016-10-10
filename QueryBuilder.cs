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
			SqlCommand cmd = new SqlCommand("INSERT INTO Distance (Date, Distance, Duration, Notes) VALUES (@date, @distance, @duration, @notes);", conn);
			cmd.Parameters.Add(new SqlParameter("date", date));
			cmd.Parameters.Add(new SqlParameter("distance", distance));

			var durPararmeter = new SqlParameter("duration", System.Data.SqlDbType.Int);
			durPararmeter.Value = duration ?? (object)DBNull.Value; // ?? is a null-coalescing operator.
			cmd.Parameters.Add(durPararmeter);

			cmd.Parameters.Add(new SqlParameter("notes", notes));

			return cmd;
		}

		// Builds the query for getting the total distance between two dates.
		public SqlCommand BuildDistanceQuery(SqlConnection conn, DateTime start, DateTime end) {
			SqlCommand cmd = new SqlCommand("SELECT SUM(Distance) FROM Distance WHERE Date >= @begin AND Date <= @end;", conn);
			cmd.Parameters.Add(new SqlParameter("begin", start));
			cmd.Parameters.Add(new SqlParameter("end", end));

			return cmd;
		}
	}
}
