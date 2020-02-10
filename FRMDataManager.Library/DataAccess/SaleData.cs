using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRMDataManager.Library.DataAccess
{
	public class SaleData : ISaleData
	{
		private readonly IProductData _productData;
		private readonly ISqlDataAccess _sql;

		public SaleData(IProductData productData, ISqlDataAccess sql)
		{
			_productData = productData;
			_sql = sql;
		}

		public void SaveSale(SaleModel saleInfo, string cashierId)
		{
			// TODO: Make this SOLID/Dry/Better
			// Start filling in the sale detail models we will save to the database
			List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
			var taxRate = ConfigHelper.GetTaxRate() / 100;

			foreach (var item in saleInfo.SaleDetails)
			{
				var detail = new SaleDetailDBModel
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity
				};

				// Get the information about this product
				var productInfo = _productData.GetProductById(item.ProductId);

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

			_sql.StartTransaction("FRMData");

			try
			{
				// Save the sale model
				_sql.SaveDataInTransaction<SaleDBModel>("[dbo].[spSale_Insert]", sale);

				// Get the ID from the sale model
				sale.Id = _sql.LoadDataInTransaction<int, dynamic>("[dbo].[spSale_Lookup]", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

				// Finish filling in the sale detail models
				foreach (var item in details)
				{
					item.SaleId = sale.Id;
					// Save the sale detail models
					_sql.SaveDataInTransaction("[dbo].[spSaleDetail_Insert]", item);
				}

				_sql.CommitTransaction();
			}
			catch
			{
				_sql.RollbackTransaction();
				throw;
			}
		}

		public List<SaleReportModel> GetSaleReport()
		{
			var output = _sql.LoadData<SaleReportModel, dynamic>("[dbo].[spSale_SaleReport]", new { }, "FRMData");

			return output;
		}
	}
}
