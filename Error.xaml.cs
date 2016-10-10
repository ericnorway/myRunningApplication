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
	/// Interaction logic for Error.xaml
	/// </summary>
	public partial class Error : Window
	{
		public Error(string content) {
			InitializeComponent();
			StringBuilder sb = new StringBuilder();
			int count = 0;
			while (count < content.Length) {
				if (content.Length > count + 24) {
					sb.Append(content.Substring(count, 24));
					sb.Append(Environment.NewLine);
				} else {
					sb.Append(content.Substring(count));
				}
				count += 24;
			}

			lblError.Content = sb.ToString();
		}

		private void btnOK_Click(object sender, RoutedEventArgs e) {
			this.Close();
		}
	}
}
