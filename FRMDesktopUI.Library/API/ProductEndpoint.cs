using FRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FRMDesktopUI.Library.API
{
	public class ProductEndpoint : IProductEndpoint
	{
		private IAPIHelper _apiHelper;

		public ProductEndpoint(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}

		public async Task<List<ProductModel>> GetAll()
		{
			using (HttpResponseMessage respsonse = await _apiHelper.ApiClient.GetAsync("/api/Product"))
			{
				if (respsonse.IsSuccessStatusCode)
				{
					var result = await respsonse.Content.ReadAsAsync<List<ProductModel>>();
					return result;
				}
				else
				{
					throw new Exception(respsonse.ReasonPhrase);
				}
			}
		}
	}
}
