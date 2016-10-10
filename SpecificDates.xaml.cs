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

namespace MyRunningApplication
{
	/// <summary>
	/// Interaction logic for SpecificDates.xaml
	/// </summary>
	public partial class SpecificDates : Window
	{
		private RunningLogic runningLogic;

		public SpecificDates(RunningLogic runningLogic) {
			InitializeComponent();
			this.runningLogic = runningLogic;
		}

		private void btnGetDistance_Click(object sender, RoutedEventArgs e) {
			try {
				DateTime start = GetStartDate();
				DateTime end = GetEndDate();

				if (start > end) {
					throw new ArgumentException("Start date cannot be after end date.");
				}

				double distance = runningLogic.GetDistanceCustom(start, end);
				lblDistanceValue.Content = distance.ToString("F1");
			}
			catch (Exception ex) {
				Error errorWindow = new Error(ex.Message);
				errorWindow.ShowDialog();
			}
		}

		private void btnClose_Click(object sender, RoutedEventArgs e) {
			Close();
		}

		private DateTime GetStartDate() {
			return datePickerStart.SelectedDate.Value.Date;
		}

		private DateTime GetEndDate() {
			return datePickerEnd.SelectedDate.Value.Date;
		}
	}
}
