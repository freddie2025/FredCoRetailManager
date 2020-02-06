using FRMDesktopUI.Models;
using System.Threading.Tasks;

namespace FRMDesktopUI.Helpers
{
	public interface IAPIHelper
	{
		Task<AuthenticatedUser> Authenticate(string username, string password);
	}
}