using pcr_processor.BAL;
using pcr_processor.Helper;
using pcr_processor.Model;
using Squirrel;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
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

			Task.Run(() => CheckAndApplyUpdate()).GetAwaiter().GetResult();
			this.Title += $" v.{GetVersionHelper.GetVersion()}";
		}

		private async Task CheckAndApplyUpdate()
		{
			try
			{
				bool updated = false;
				using (var updateManager = new UpdateManager(ConfigurationManager.AppSettings["fileServerLocation"].ToString()))
				{
					var updateInfo = await updateManager.CheckForUpdate();
					if (updateInfo.ReleasesToApply != null &&
						updateInfo.ReleasesToApply.Count > 0)
					{
						var releaseEntry = await updateManager.UpdateApp();
						updated = true;
					}
				}
				if (updated)
				{
					UpdateManager.RestartApp("pcr_processor.exe");
				}
			}
			catch
			{
			}
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

				double overAllCount = 0;
				double overAllUpdated = 0;

				foreach (DataRow usersRow in users.Rows)
				{
					overAllCount = users.Rows.Count;

					int lastOrderNumber = OrdersBAL.GetLastOrderNumberSeries(usersRow[0].ToString());

					DataTable orders = new DataTable();
					orders = OrdersBAL.FilterOrders(usersRow[0].ToString());

					double count = 0;
					double updated = 0;

					foreach (DataRow ordersRow in orders.Rows)
					{
						count = orders.Rows.Count;

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

						updated++;

						double val = updated / count;
						double progress = val * 100;

						ProgUserProcess.Dispatcher.Invoke(() => ProgUserProcess.Value = progress,
							DispatcherPriority.Background);
					}

					overAllUpdated++;

					double overAllVal = overAllUpdated / overAllCount;
					double overAllProgress = overAllVal * 100;

					ProgOverallProcess.Dispatcher.Invoke(() => ProgOverallProcess.Value = overAllProgress,
						DispatcherPriority.Background);
				}

				string content = " record(s) has been successfully updated.";

				MessageBox.Show(overAllCount.ToString(CultureInfo.InvariantCulture) + content, "PROCESS",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				WriteLogFileHelper.WriteLogFile("Error: " + ex.Message.ToString());
			}
		}
	}
}
