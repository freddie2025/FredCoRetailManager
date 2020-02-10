using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRMDataManager.Library.DataAccess
{
	public class SaleData
	{
		private readonly IConfiguration _config;

		public SaleData(IConfiguration config)
		{
			_config = config;
		}

		public void SaveSale(SaleModel saleInfo, string cashierId)
		{
			// TODO: Make this SOLID/Dry/Better
			// Start filling in the sale detail models we will save to the database
			List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
			ProductData products = new ProductData(_config);
			var taxRate = ConfigHelper.GetTaxRate()/100;

			foreach (var item in saleInfo.SaleDetails)
			{
				var detail = new SaleDetailDBModel
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity
				};

				// Get the information about this product
				var productInfo = products.GetProductById(item.ProductId);

				if (productInfo == null)
				{
					throw new Exception($"The product id of { item.ProductId } could not be found in the database.");
				}

				detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

				if (productInfo.IsTaxable)
				{
					detail.Tax = (detail.PurchasePrice * taxRate);
				}

				details.Add(detail);
			}

			// Create the sale model
			SaleDBModel sale = new SaleDBModel
			{ 
				SubTotal = details.Sum(x => x.PurchasePrice),
				Tax = details.Sum(x => x.Tax),
				CashierId = cashierId
			};

			sale.Total = sale.SubTotal + sale.Tax;

			using (SqlDataAccess sql = new SqlDataAccess(_config))
			{
				sql.StartTransaction("FRMData");

				try
				{
					// Save the sale model
					sql.SaveDataInTransaction<SaleDBModel>("[dbo].[spSale_Insert]", sale);

					// Get the ID from the sale model
					sale.Id = sql.LoadDataInTransaction<int, dynamic>("[dbo].[spSale_Lookup]", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

					// Finish filling in the sale detail models
					foreach (var item in details)
					{
						item.SaleId = sale.Id;
						// Save the sale detail models
						sql.SaveDataInTransaction("[dbo].[spSaleDetail_Insert]", item);
					}
					
					sql.CommitTransaction();
				}
				catch
				{
					sql.RollbackTransaction();
					throw;
				}
			}
		}

		public List<SaleReportModel> GetSaleReport()
		{
			SqlDataAccess sql = new SqlDataAccess(_config);

			var output = sql.LoadData<SaleReportModel, dynamic>("[dbo].[spSale_SaleReport]", new { }, "FRMData");

			return output;
		}
	}
}
