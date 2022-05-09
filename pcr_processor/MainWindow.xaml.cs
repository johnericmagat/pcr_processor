using pcr_processor.BAL;
using pcr_processor.Helper;
using pcr_processor.Model;
using Squirrel;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
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

					TxtUser.Text = usersRow[1].ToString();

					int lastOrderNumber = OrdersBAL.GetLastOrderNumberSeries(usersRow[1].ToString());

					DataTable orders = new DataTable();
					orders = OrdersBAL.FilterOrders(usersRow[1].ToString());

					double count = 0;
					double updated = 0;

					foreach (DataRow ordersRow in orders.Rows)
					{
						count = orders.Rows.Count;

						TxtUserRecords.Text = count.ToString();

						OrdersBAL.DeleteDuplicateLaboratoryId(ordersRow[0].ToString());

						DataTable ordersByLabId = new DataTable();
						ordersByLabId = OrdersBAL.FilterOrdersByLaboratoryId(ordersRow[0].ToString());

						foreach (DataRow ordersByLabIdRow in ordersByLabId.Rows)
						{
							lastOrderNumber++;

							OrdersModel objOrders = new OrdersModel();
							objOrders.Id = Int32.Parse(ordersByLabIdRow[0].ToString());
							objOrders.Order_number = usersRow[1].ToString() + "-" + lastOrderNumber.ToString();
							OrdersBAL.UpdateOrders(objOrders);
						}

						OrdersModel order = new OrdersModel();
						order = OrdersBAL.ViewSanitizedLaboratoryId(ordersRow[0].ToString());

						OrdersBAL.UpdateOrdersWithNoSanitizedLaboratoryId(ordersRow[0].ToString(), UInt64.Parse(order.Id.ToString()));

						updated++;

						double val = updated / count;
						double progress = val * 100;

						ProgUserProcess.Dispatcher.Invoke(() => ProgUserProcess.Value = progress,
							DispatcherPriority.Background);
					}

					// Added another loop for username/order_number with multiple dash on suffix

					DataTable ordersDashOne = new DataTable();
					ordersDashOne = OrdersBAL.FilterOrdersDashOne(usersRow[1].ToString());

					foreach (DataRow ordersDashOneRow in ordersDashOne.Rows)
					{
						count = ordersDashOne.Rows.Count;

						TxtUserRecords.Text = count.ToString();

						OrdersBAL.DeleteDuplicateLaboratoryId(ordersDashOneRow[0].ToString());

						DataTable ordersByLabId = new DataTable();
						ordersByLabId = OrdersBAL.FilterOrdersByLaboratoryId(ordersDashOneRow[0].ToString());

						foreach (DataRow ordersByLabIdRow in ordersByLabId.Rows)
						{
							lastOrderNumber++;

							OrdersModel objOrders = new OrdersModel();
							objOrders.Id = Int32.Parse(ordersByLabIdRow[0].ToString());
							objOrders.Order_number = usersRow[1].ToString() + "-" + lastOrderNumber.ToString();
							OrdersBAL.UpdateOrders(objOrders);
						}

						OrdersModel order = new OrdersModel();
						order = OrdersBAL.ViewSanitizedLaboratoryId(ordersDashOneRow[0].ToString());

						OrdersBAL.UpdateOrdersWithNoSanitizedLaboratoryId(ordersDashOneRow[0].ToString(), UInt64.Parse(order.Id.ToString()));

						updated++;

						double val = updated / count;
						double progress = val * 100;

						ProgUserProcess.Dispatcher.Invoke(() => ProgUserProcess.Value = progress,
							DispatcherPriority.Background);
					}

					overAllUpdated += UsersBAL.UpdateUsers(Int32.Parse(usersRow[0].ToString()));

					double overAllVal = overAllUpdated / overAllCount;
					double overAllProgress = overAllVal * 100;

					ProgOverallProcess.Dispatcher.Invoke(() => ProgOverallProcess.Value = overAllProgress,
						DispatcherPriority.Background);
				}

				string content = " record(s) has been successfully updated.";

				MessageBox.Show(overAllCount.ToString(CultureInfo.InvariantCulture) + content, "DONE",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				WriteLogFileHelper.WriteLogFile("Error: " + ex.Message.ToString());
			}
		}

		private void BtnProcess_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Start processing?", "PROCESS",
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				Process();
			}
		}

		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Close this application?", "CLOSE",
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				Application.Current.Shutdown();
			}
		}
	}
}
