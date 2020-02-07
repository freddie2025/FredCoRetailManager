using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public class ProductData
	{
		public List<ProductModel> GetProducts()
		{
			SqlDataAccess sql = new SqlDataAccess();

			var output = sql.LoadData<ProductModel, dynamic>("[dbo].[spProduct_GetAll]", new { }, "FRMData");

			return output;
		}
	}
}
