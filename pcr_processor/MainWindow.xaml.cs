using pcr_processor.BAL;
using pcr_processor.Helper;
using System;
using System.Data;
using System.Windows;

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
			FilterUsers();
		}

		private void FilterUsers()
		{
			try
			{
				DataTable users = new DataTable();
				users = UsersBAL.FilterUsers();
			}
			catch (Exception ex)
			{
				WriteLogFileHelper.WriteLogFile("Error: " + ex.Message.ToString());
			}
		}
	}
}
