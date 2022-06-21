using MySql.Data.MySqlClient;
using pcr_processor.Model;
using System;
using System.Configuration;
using System.Data;

namespace pcr_processor.DAL
{
	public static class OrdersDAL
	{
		public static DataTable FilterOrders(string commandString, string OrderNumber)
		{
			DataTable orders = new DataTable();

			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@OrderNumberParameter", OrderNumber);

			MySqlDataAdapter adt = new MySqlDataAdapter(cmd);
			adt.Fill(orders);

			adt.Dispose();
			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();

			return orders;
		}

		public static DataTable FilterOrdersDashOne(string commandString, string OrderNumber)
		{
			DataTable orders = new DataTable();

			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@OrderNumberParameter", OrderNumber);

			MySqlDataAdapter adt = new MySqlDataAdapter(cmd);
			adt.Fill(orders);

			adt.Dispose();
			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();

			return orders;
		}

		public static DataTable FilterOrdersByLaboratoryId(string commandString, string LaboratoryId)
		{
			DataTable orders = new DataTable();

			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@LaboratoryIdParameter", LaboratoryId);

			MySqlDataAdapter adt = new MySqlDataAdapter(cmd);
			adt.Fill(orders);

			adt.Dispose();
			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();

			return orders;
		}

		public static int GetLastOrderNumberSeries(string commandString, string OrderNumber)
		{
			int lastOrderNumber = 0;

			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@OrderNumberParameter", OrderNumber);

			lastOrderNumber = Int32.Parse(cmd.ExecuteScalar().ToString());

			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();

			return lastOrderNumber;
		}

		public static void UpdateOrders(string commandString, OrdersModel orders)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@IdParameter", orders.Id);
			cmd.Parameters.AddWithValue("@OrderNumberParameter", orders.Order_number);
			cmd.Parameters.AddWithValue("@UserNameParameter", orders.UserName);
			cmd.ExecuteNonQuery();

			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();
		}

		public static OrdersModel ViewSanitizedLaboratoryId(string commandString, string LaboratoryId)
		{
			OrdersModel order = new OrdersModel();
			DataTable orders = new DataTable();

			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@LaboratoryIdParameter", LaboratoryId);

			MySqlDataAdapter adt = new MySqlDataAdapter(cmd);
			adt.Fill(orders);
			adt.Dispose();

			order.Id = Int32.Parse(orders.Rows[0][0].ToString());
			order.Laboratory_id = orders.Rows[0][1].ToString();

			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();
			orders.Dispose();

			return order;
		}

		public static void UpdateOrdersWithNoSanitizedLaboratoryId(string commandString, string laboratoryId, ulong id)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@LaboratoryIdParameter", laboratoryId);
			cmd.Parameters.AddWithValue("@IdParameter", id);
			cmd.ExecuteNonQuery();

			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();
		}

		public static void DeleteDuplicateLaboratoryId(string commandString, string laboratoryId)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@LaboratoryIdParameter", laboratoryId);
			cmd.ExecuteNonQuery();

			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();
		}
	}
}
