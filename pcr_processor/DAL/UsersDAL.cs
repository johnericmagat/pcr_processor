using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace pcr_processor.DAL
{
	public static class UsersDAL
	{
		public static DataTable FilterUsers(string commandString)
		{
			DataTable users = new DataTable();

			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;

			MySqlDataAdapter adt = new MySqlDataAdapter(cmd);
			adt.Fill(users);

			adt.Dispose();
			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();

			return users;
		}

		public static int UpdateUsers(string commandString, int id)
		{
			int isUpdated = 0;

			MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["myConnectionString"].ToString());
			mySqlConnection.Open();

			MySqlCommand cmd = new MySqlCommand(commandString, mySqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@IdParameter", id);
			cmd.ExecuteNonQuery();
			isUpdated++;

			cmd.Dispose();
			mySqlConnection.Close();
			mySqlConnection.Dispose();

			return isUpdated;
		}
	}
}
