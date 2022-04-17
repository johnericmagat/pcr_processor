using pcr_processor.DAL;
using pcr_processor.Model;
using System.Data;

namespace pcr_processor.BAL
{
	public static class OrdersBAL
	{
		public static DataTable FilterOrders(string OrderNumber)
		{
			return OrdersDAL.FilterOrders("FilterOrders", OrderNumber);
		}

		public static DataTable FilterOrdersByLaboratoryId(string LaboratoryId)
		{
			return OrdersDAL.FilterOrdersByLaboratoryId("FilterOrdersByLaboratoryId", LaboratoryId);
		}

		public static int GetLastOrderNumberSeries(string OrderNumber)
		{
			return OrdersDAL.GetLastOrderNumberSeries("GetLastOrderNumberSeries", OrderNumber);
		}

		public static void UpdateOrders(OrdersModel orders)
		{
			OrdersDAL.UpdateOrders("UpdateOrders", orders);
		}
	}
}
