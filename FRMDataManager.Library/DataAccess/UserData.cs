using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public class UserData
	{
		public List<UserModel> GetUserById(string Id)
		{
			SqlDataAccess sql = new SqlDataAccess();

			var p = new { Id = Id };

			var output = sql.LoadData<UserModel, dynamic>("[dbo].[spUserLookup]", p, "FRMData");

			return output;
		}
	}
}
