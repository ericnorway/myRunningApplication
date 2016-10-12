using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyRunningApplication
{
	/// <summary>
	/// Interaction logic for GraphWindow.xaml
	/// </summary>
	public partial class GraphWindow : Window
	{
		private Chart distanceChart;

		public GraphWindow(RunningLogic runningLogic, DateTime start, DateTime end) {
			InitializeComponent();

			List<Entry> entries = runningLogic.GetAllDataRange(start, end);

			this.distanceChart = new Chart();
			this.distanceChart.Location = new System.Drawing.Point(0, 0);
			this.distanceChart.Size = new System.Drawing.Size(640, 480);
			this.distanceChart.TabIndex = 0;

			ChartArea chartArea = new ChartArea();
			this.distanceChart.ChartAreas.Add(chartArea);

			Series series = new Series();
			double count = 1;
			foreach (Entry entry in entries) {
				DataPoint dataPoint = new DataPoint(count, entry.distance);
				dataPoint.AxisLabel = entry.date.ToShortDateString();
				series.Points.Add(dataPoint);
				count++;
			}
			this.distanceChart.Series.Add(series);
			
			graphPanel.Controls.Add(this.distanceChart);
		}
	}
}
