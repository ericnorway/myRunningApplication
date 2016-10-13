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
			string[] words = content.Split();
			StringBuilder sb = new StringBuilder();
			int lineLength = 0;

			for (int i = 0; i < words.Length; i++) {
				if (lineLength + words[i].Length >= 24) {
					lineLength = 0;
					sb.Append(Environment.NewLine);
				}
				sb.Append(words[i]);
				sb.Append(" ");
				lineLength += words[i].Length + 1;
			}

			lblError.Content = sb.ToString();
		}

		private void btnOK_Click(object sender, RoutedEventArgs e) {
			Close();
		}
	}
}
