using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public class InventoryData
	{
		private readonly IConfiguration _config;

		public InventoryData(IConfiguration config)
		{
			_config = config;
		}

		public List<InventoryModel> GetInventory()
		{
			SqlDataAccess sql = new SqlDataAccess(_config);

			var output = sql.LoadData<InventoryModel, dynamic>("[dbo].[spInventory_GetAll]", new { }, "FRMData");

			return output;
		}

		public void SaveInventoryRecord(InventoryModel item)
		{
			SqlDataAccess sql = new SqlDataAccess(_config);

			sql.SaveData("[dbo].[spInventory_Insert]", item, "FRMData");
		}
	}
}
