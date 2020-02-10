using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace FRMDataManager.Library.DataAccess
{
	public class ProductData
	{
		private readonly IConfiguration _config;

		public ProductData(IConfiguration config)
		{
			_config = config;
		}

		public List<ProductModel> GetProducts()
		{
			SqlDataAccess sql = new SqlDataAccess(_config);

			var output = sql.LoadData<ProductModel, dynamic>("[dbo].[spProduct_GetAll]", new { }, "FRMData");

			return output;
		}

		public ProductModel GetProductById(int productId)
		{
			SqlDataAccess sql = new SqlDataAccess(_config);

			var output = sql.LoadData<ProductModel, dynamic>("[dbo].[spProduct_GetById]", new { Id = productId }, "FRMData").FirstOrDefault();

			return output;
		}
	}
}
