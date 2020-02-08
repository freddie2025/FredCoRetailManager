using FRMDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace FRMDesktopUI.Library.API
{
	public interface ISaleEndpoint
	{
		Task PostSale(SaleModel sale);
	}
}