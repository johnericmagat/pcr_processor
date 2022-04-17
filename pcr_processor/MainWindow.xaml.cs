using pcr_processor.BAL;
using pcr_processor.Helper;
using pcr_processor.Model;
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
				double updated = 0;
				bool hasError = false;
				int errorCount = 0;

				foreach (DataRow usersRow in users.Rows)
				{
					cnt = users.Rows.Count;

					int lastOrderNumber = OrdersBAL.GetLastOrderNumberSeries(usersRow[0].ToString());

					DataTable orders = new DataTable();
					orders = OrdersBAL.FilterOrders(usersRow[0].ToString());

					foreach (DataRow ordersRow in orders.Rows)
					{
						DataTable ordersByLabId = new DataTable();
						ordersByLabId = OrdersBAL.FilterOrdersByLaboratoryId(ordersRow[0].ToString());

						foreach (DataRow ordersByLabIdRow in ordersByLabId.Rows)
						{
							lastOrderNumber++;

							OrdersModel objOrders = new OrdersModel();
							objOrders.Id = Int32.Parse(ordersByLabIdRow[0].ToString());
							objOrders.Order_number = usersRow[0].ToString() + "-" + lastOrderNumber.ToString();
							OrdersBAL.UpdateOrders(objOrders);
						}
					}

					updated++;

					double val = updated / (cnt - errorCount);
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
