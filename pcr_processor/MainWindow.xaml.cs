using pcr_processor.BAL;
using pcr_processor.Helper;
using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace pcr_processor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
		}

		private void BtnProcess_Click(object sender, RoutedEventArgs e)
		{
			Process();
		}

		private void Process()
		{
			try
			{
				DataTable users = new DataTable();
				users = UsersBAL.FilterUsers();

				double cnt = 0;
				double inserted = 0;
				bool hasError = false;
				int errorCount = 0;

				foreach (DataRow row in users.Rows)
				{
					cnt = users.Rows.Count;

					inserted++;

					double val = inserted / (cnt - errorCount);
					double progress = val * 100;

					ProgProcess.Dispatcher.Invoke(() => ProgProcess.Value = progress,
						DispatcherPriority.Background);
				}

				string content = " record(s) has been successfully updated.";

				if (hasError)
				{
					content = " record(s) has been successfully updated.\nThere is/are {0} record(s) not updated.";
					content = String.Format(content, errorCount);
				}
				MessageBox.Show((cnt - errorCount).ToString(CultureInfo.InvariantCulture) + content, "PROCESS",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				WriteLogFileHelper.WriteLogFile("Error: " + ex.Message.ToString());
			}
		}
	}
}
