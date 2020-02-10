using FRMDataManager.Library.Models;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public interface IUserData
	{
		List<UserModel> GetUserById(string Id);
	}
}