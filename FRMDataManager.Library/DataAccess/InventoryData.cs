using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public class InventoryData : IInventoryData
	{
		private readonly ISqlDataAccess _sql;

		public InventoryData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<InventoryModel> GetInventory()
		{
			var output = _sql.LoadData<InventoryModel, dynamic>("[dbo].[spInventory_GetAll]", new { }, "FRMData");

			return output;
		}

		public void SaveInventoryRecord(InventoryModel item)
		{
			_sql.SaveData("[dbo].[spInventory_Insert]", item, "FRMData");
		}
	}
}
