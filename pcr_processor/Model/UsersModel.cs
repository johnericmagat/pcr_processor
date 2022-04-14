using System;

namespace pcr_processor.Model
{
	public class UsersModel
	{
		public Int32 Id { get; set; }
		public String Name { get; set; }
		public String Email { get; set; }
		public DateTime Email_verified_at { get; set; }
		public String Password { get; set; }
		public String Remember_token { get; set; }
		public DateTime Created_at { get; set; }
		public DateTime Updated_at { get; set; }
		public String User_type { get; set; }
		public String Username { get; set; }
	}
}
