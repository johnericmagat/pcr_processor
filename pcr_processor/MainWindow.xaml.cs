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
using pcr_processor.Model;
using pcr_processor.BAL;
using System.Data;

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
			catch (Exception)
			{
				
			}
		}
	}
}
