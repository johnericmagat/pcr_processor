using System;

namespace pcr_processor.Model
{
	public class OrdersModel
	{
		public Int32 Id { get; set; }
		public String Order_number { get; set; }
		public DateTime Order_date { get; set; }
		public String Customer_name { get; set; }
		public String Email { get; set; }
		public String Customer_address { get; set; }
		public String Original_product_code { get; set; }
		public String Product_code { get; set; }
		public String Result { get; set; }
		public String Result_influenza { get; set; }
		public String Laboratory_id { get; set; }
		public String Test_date { get; set; }
		public String Comment { get; set; }
		public String Is_emailed { get; set; }
		public DateTime Deleted_at { get; set; }
		public DateTime Created_at { get; set; }
		public DateTime Updated_at { get; set; }
		public Int32 Is_completed { get; set; }
		public String Arrival_date { get; set; }
		public Int32 Email_to_clinic { get; set; }
		public String Clinic_email { get; set; }
		public String Sampling_date { get; set; }
		public String Issue_date { get; set; }
		public DateTime Birth_date { get; set; }
		public String Romaji { get; set; }
		public Int32 Medical_certificate { get; set; }
		public String Orderer { get; set; }
		public Int32 Is_paid { get; set; }
		public DateTime Delivery_date { get; set; }
		public String Mutation { get; set; }
		public Decimal Ct_value { get; set; }
		public Int32 Is_selected { get; set; }
		public String Gender { get; set; }
		public Int32 Consent_form { get; set; }
		public String Order_time { get; set; }
		public String Arrival_time { get; set; }
		public String Test_time { get; set; }
		public String Issue_time { get; set; }
		public Int32 Block_flag { get; set; }
		public String UserName { get; set; }
	}
}
