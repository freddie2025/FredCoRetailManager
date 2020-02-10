using FRMDataManager.Library.Internal.DataAccess;
using FRMDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FRMDataManager.Library.DataAccess
{
	public class UserData
	{
		private readonly IConfiguration _config;

		public UserData(IConfiguration config)
		{
			_config = config;
		}

		public List<UserModel> GetUserById(string Id)
		{
			SqlDataAccess sql = new SqlDataAccess(_config);

			var p = new { Id = Id };

			var output = sql.LoadData<UserModel, dynamic>("[dbo].[spUser_Lookup]", p, "FRMData");

			return output;
		}
	}
}
