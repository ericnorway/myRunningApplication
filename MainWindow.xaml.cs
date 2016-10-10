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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyRunningApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private RunningLogic runningLogic;

		public MainWindow() {
			InitializeComponent();
			runningLogic = new RunningLogic();
			UpdateDistancesShown();
		}

		private void btnNewDistance_Click(object sender, RoutedEventArgs e) {
			NewDistance distanceWindow = new NewDistance(runningLogic);
			if (distanceWindow.ShowDialog() == true) {
				UpdateDistancesShown();
			} else {

			}
		}

		private void btnDateRange_Click(object sender, RoutedEventArgs e) {
			Error errorWindow = new Error("Not implemented yet.");
			errorWindow.ShowDialog();
		}

		private void btnViewDetails_Click(object sender, RoutedEventArgs e) {
			Error errorWindow = new Error("Not implemented yet.");
			errorWindow.ShowDialog();
		}

		private void UpdateDistancesShown() {
			Tuple<double, double, double> values = runningLogic.GetDistancesGeneric();
			lblWeekValue.Content = values.Item1.ToString();
			lblMonthValue.Content = values.Item2.ToString();
			lblYearValue.Content = values.Item3.ToString();
		}
	}
}
