using FRMDesktopUI.Library.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FRMDesktopUI.Library.API
{
	public class SaleEndpoint : ISaleEndpoint
	{
		private IAPIHelper _apiHelper;

		public SaleEndpoint(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}

		public async Task PostSale(SaleModel sale)
		{
			using (HttpResponseMessage respsonse = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
			{
				if (respsonse.IsSuccessStatusCode)
				{
					// Log successful call?
				}
				else
				{
					throw new Exception(respsonse.ReasonPhrase);
				}
			}
		}
	}
}
