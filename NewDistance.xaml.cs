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
	/// Interaction logic for NewDistance.xaml
	/// </summary>
	public partial class NewDistance : Window
	{
		private RunningLogic runningLogic;

		public NewDistance(RunningLogic runningLogic) {
			InitializeComponent();
			this.runningLogic = runningLogic;
		}

		private void btnOK_Click(object sender, RoutedEventArgs e) {
			try {
				DateTime date = GetDate();
				double distance = GetDistance();
				int? duration = GetDuration();
				string notes = GetNotes();
				runningLogic.AddDistance(date, distance, duration, notes);
				this.Close();
			} catch (Exception ex) {
				Error errorWindow = new Error(ex.Message);
				errorWindow.ShowDialog();
			}
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e) {
			this.Close();
		}

		private void txtDistance_GotFocus(object sender, RoutedEventArgs e) {
			if (txtDistance.Text == "(miles)") {
				txtDistance.Text = "";
			}
		}

		private void txtTime_GotFocus(object sender, RoutedEventArgs e) {
			if (txtDuration.Text == "hh:mm:ss") {
				txtDuration.Text = "";
			}
		}

		private void txtDistance_LostFocus(object sender, RoutedEventArgs e) {
			if (txtDistance.Text == "") {
				txtDistance.Text = "(miles)";
			}
		}

		private void txtTime_LostFocus(object sender, RoutedEventArgs e) {
			if (txtDuration.Text == "") {
				txtDuration.Text = "hh:mm:ss";
			}
		}

		private DateTime GetDate() {
			DateTime now = DateTime.Now.Date;
			DateTime date = datePicker1.SelectedDate.Value.Date;
			if (date > now) {
				throw new ArgumentException(string.Format("Date {0} is in the future.", date));
			}
			return date;
		}

		private double GetDistance() {
			string temp = txtDistance.Text;
			double distance = 0.0;
			if (temp == "" || temp == "(miles)") {
				throw new ArgumentException("Please enter a distance.");
			} else if (!double.TryParse(temp, out distance)) {
				throw new ArgumentException(string.Format("Could not parse {0}.", temp));
			}
			return distance;
		}

		private int? GetDuration() {
			string temp = txtDuration.Text;
			int? durationNullable;
			int duration = 0;
			if (temp == "" || temp == "hh:mm:ss") {
				durationNullable = null;
			}
			else {
				string[] parts = temp.Split(':');
				foreach (string part in parts) {
					int tempDuration;
					if (!int.TryParse(part, out tempDuration)) {
						throw new ArgumentException(string.Format("Could not parse {0}.", temp));
					}
					duration = duration * 60 + tempDuration;
				}
				
				durationNullable = duration;
			}
			return durationNullable;
		}

		private string GetNotes() {
			return txtNotes.Text;
		}
	}
}
