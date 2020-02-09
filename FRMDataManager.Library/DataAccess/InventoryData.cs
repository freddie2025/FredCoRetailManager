using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public class InventoryData
	{
		public List<InventoryModel> GetInventory()
		{
			SqlDataAccess sql = new SqlDataAccess();

			var output = sql.LoadData<InventoryModel, dynamic>("[dbo].[spInventory_GetAll]", new { }, "FRMData");

			return output;
		}

		public void SaveInventoryRecord(InventoryModel item)
		{
			SqlDataAccess sql = new SqlDataAccess();

			sql.SaveData("[dbo].[spInventory_Insert]", item, "FRMData");
		}
	}
}
