using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public class UserData : IUserData
	{
		private readonly ISqlDataAccess _sql;

		public UserData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<UserModel> GetUserById(string Id)
		{
			var output = _sql.LoadData<UserModel, dynamic>("[dbo].[spUser_Lookup]", new { Id }, "FRMData");

			return output;
		}
	}
}
