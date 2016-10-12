using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRunningApplication
{
	public class Entry
	{
		public DateTime date { get; set; }
		public double distance { get; set; }
		public int? duration { get; set; }
		public string notes { get; set; }

		public Entry(DateTime date, double distance, int? duration, string notes) {
			this.date = date;
			this.distance = distance;
			this.duration = duration;
			this.notes = notes;
		}
	}
}
