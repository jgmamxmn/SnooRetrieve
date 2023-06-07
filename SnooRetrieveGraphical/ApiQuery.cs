using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SnooRetrieve
{
	internal class ApiQuery
	{
		public const string USER_AGENT = "SNOO/351 CFNetwork/1121.2 Darwin/19.2.0";
		public const string BASE_URL = "https://snoo-api.happiestbaby.com";
		public const string LOGIN_ENDPOINT = "/us/login";
		public const string DATA_ENDPOINT = "/ss/v2/sessions/aggregated";
		public const string CURRENT_ENDPOINT = "/ss/v2/sessions/last";


		static string _token=null;
		static DateTime _tokenExpiry;
		static string _refreshToken;

		public static async Task<string> GetToken(string username, string password)
		{
			if (!string.IsNullOrEmpty(_token) && _tokenExpiry > DateTime.UtcNow)
			{
				return _token;
			}

			Dictionary<string, string> payload = new Dictionary<string, string>
			{
				{ "username", username },
				{ "password", password }
			};

			var jsonData = await QueryApiAsync(BASE_URL + LOGIN_ENDPOINT, "post", payload, null);
			var data = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResult>(jsonData);

			_token = data.access_token;
			_tokenExpiry = DateTime.UtcNow.AddSeconds(data.expires_in);
			_refreshToken = data.refresh_token;
			return _token;
		}

		public static async Task<DayResult> GetDayHistory(string token, DateOnly date)
		{

			var json = await QueryApiAsync(BASE_URL + DATA_ENDPOINT, "get", new Dictionary<string, string>
			{
				{ "startTime", date.ToString("MM/dd/yyyy") + " 00:00:00" }
			}, token);

			return Newtonsoft.Json.JsonConvert.DeserializeObject<DayResult>(json);
		}

		public static string BlockingQueryApi(string queryUrl, string httpMethod, Dictionary<string, string> queryParams, string token)
		{
			var T = QueryApiAsync(queryUrl, httpMethod, queryParams, token);
			T.Wait();
			return T.Result;
		}
		public static async Task<string> QueryApiAsync(string queryUrl, string httpMethod, Dictionary<string, string> queryParams, string token)
		{
			using (HttpClient client = new HttpClient())
			{
				// Set user agent header
				client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);

				// Set basic authentication if username and password are provided
				/*if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
				{
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}")));
				}*/
				if(!string.IsNullOrEmpty(token))
				{
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				}

				// Append query parameters to the URL\
				if (queryParams != null && queryParams.Any())
				{
					switch (httpMethod.ToLower())
					{
						case "get":
							queryUrl += "?";
							foreach (var param in queryParams)
							{
								queryUrl += $"{param.Key}={param.Value}&";
							}
							queryUrl = queryUrl.TrimEnd('&');
							break;
						case "post":
							// handled below
							break;
						default:
							throw new NotImplementedException();
					}
				}


				// Make the API request
				HttpResponseMessage response = httpMethod.ToLower() switch
				{
					"get" => await client.GetAsync(queryUrl),
					"post" => await client.PostAsync(queryUrl, new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(queryParams ?? new Dictionary<string, string>()), Encoding.UTF8, "application/json")),
					_ => throw new NotImplementedException()
				};
				response.EnsureSuccessStatusCode();

				// Read and return the response content
				string responseContent = await response.Content.ReadAsStringAsync();
				return responseContent;
			}
		}
	}

	public class Level
	{
		public string sessionId { get; set; }

		/// <summary>
		/// "asleep", "soothing"
		/// </summary>
		public string type { get; set; }
		public string startTime { get; set; }
		public int stateDuration { get; set; }
		public bool isActive { get; set; }
		public DateTime startTimeFormatted { get; set; }
	}

	public class DayResult
	{
		public List<Level> levels { get; set; }
		public int naps { get; set; }
		public int longestSleep { get; set; }
		public int totalSleep { get; set; }
		public int daySleep { get; set; }
		public int nightSleep { get; set; }
		public int nightWakings { get; set; }

		public void dump()
		{
			Console.WriteLine($"naps: {naps}");
			Console.WriteLine($"longest sleep: {longestSleep}");
			Console.WriteLine($"total sleep: {totalSleep}");
			Console.WriteLine($"day sleep: {daySleep}");
			Console.WriteLine($"night sleep: {nightSleep}");
			Console.WriteLine($"night wakings: {nightWakings}");
			foreach (var l in levels)
			{
				Console.WriteLine($"{l.type} - {l.startTimeFormatted} : {l.stateDuration}");
			}
		}
	}

	public class LoginResult
	{
		public string token_type { get; set; }
		public int expires_in { get; set; }
		public string access_token { get; set; }
		public string scope { get; set; }
		public string refresh_token { get; set; }
		public List<string> groups { get; set; }
		public string userId { get; set; }
	}

}
