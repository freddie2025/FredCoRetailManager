using FRMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRMDesktopUI.Library.API
{
	public interface IProductEndpoint
	{
		Task<List<ProductModel>> GetAll();
	}
}