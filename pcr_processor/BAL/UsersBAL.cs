using pcr_processor.DAL;
using System.Data;

namespace pcr_processor.BAL
{
	public static class UsersBAL
	{
		public static DataTable FilterUsers()
		{
			return UsersDAL.FilterUsers("FilterUsers");
		}

		public static int UpdateUsers(int id)
		{
			return UsersDAL.UpdateUsers("UpdateUsers", id);
		}
	}
}
