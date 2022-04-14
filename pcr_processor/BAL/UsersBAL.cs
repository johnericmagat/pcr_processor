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
	}
}
