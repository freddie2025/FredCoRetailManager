﻿using FRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FRMDesktopUI.Library.API
{
	public class APIHelper : IAPIHelper
	{
		private HttpClient _apiClient;
		private ILoggedInUserModel _loggedInUser;

		public APIHelper(ILoggedInUserModel loggedInUser)
		{
			InitializeClient();
			_loggedInUser = loggedInUser;
		}

		public HttpClient ApiClient
		{
			get 
			{
				return _apiClient;
			}
		}

		private void InitializeClient()
		{
			string api = ConfigurationManager.AppSettings["api"];

			_apiClient = new HttpClient();
			_apiClient.BaseAddress = new Uri(api);
			_apiClient.DefaultRequestHeaders.Accept.Clear();
			_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<AuthenticatedUser> Authenticate(string username, string password)
		{
			var data = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("grant_type", "password"),
				new KeyValuePair<string, string>("username", username),
				new KeyValuePair<string, string>("password", password)
			}); ;

			using (HttpResponseMessage respsonse = await _apiClient.PostAsync("/Token", data))
			{
				if (respsonse.IsSuccessStatusCode)
				{
					var result = await respsonse.Content.ReadAsAsync<AuthenticatedUser>();
					return result;
				}
				else
				{
					throw new Exception(respsonse.ReasonPhrase);
				}
			}
		}

		public void LogOffUser()
		{
			_apiClient.DefaultRequestHeaders.Clear();
		}

		public async Task GetLoggedInUserInfo(string token)
		{
			_apiClient.DefaultRequestHeaders.Clear();
			_apiClient.DefaultRequestHeaders.Accept.Clear();
			_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

			using (HttpResponseMessage respsonse = await _apiClient.GetAsync("/api/User"))
			{
				if (respsonse.IsSuccessStatusCode)
				{
					var result = await respsonse.Content.ReadAsAsync<LoggedInUserModel>();
					_loggedInUser.CreatedDate = result.CreatedDate;
					_loggedInUser.EmailAddress = result.EmailAddress;
					_loggedInUser.FirstName = result.FirstName;
					_loggedInUser.Id = result.Id;
					_loggedInUser.LastName = result.LastName;
					_loggedInUser.Token = token;
				}
				else
				{
					throw new Exception(respsonse.ReasonPhrase);
				}
			}
		}
	}
}
