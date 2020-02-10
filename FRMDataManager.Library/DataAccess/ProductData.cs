using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace FRMDataManager.Library.DataAccess
{
	public class ProductData : IProductData
	{
		private readonly ISqlDataAccess _sql;

		public ProductData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<ProductModel> GetProducts()
		{
			var output = _sql.LoadData<ProductModel, dynamic>("[dbo].[spProduct_GetAll]", new { }, "FRMData");

			return output;
		}

		public ProductModel GetProductById(int productId)
		{
			var output = _sql.LoadData<ProductModel, dynamic>("[dbo].[spProduct_GetById]", new { Id = productId }, "FRMData").FirstOrDefault();

			return output;
		}
	}
}
