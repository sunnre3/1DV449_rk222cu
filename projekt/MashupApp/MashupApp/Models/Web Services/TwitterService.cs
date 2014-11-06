using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MashupApp.Models.Web_Services
{
	public class TwitterService
	{
		private string consumerKey;
		private string consumerSecret;

		private string baseUrl = "https://api.twitter.com/1.1/";

		public TwitterService(string consumerKey, string consumerSecret)
		{
			this.consumerKey = consumerKey;
			this.consumerSecret = consumerSecret;
		}

		public List<Trend> GetTrends()
		{
			// Create a list which will be returned.
			var entries = new List<Trend>();

			// Get the Bearer token.
			var authString = GetBearerToken();

			// Using WebClient() to make the request to the API.
			// NOTE: Future implementations should use HttpClient() instead.
			using (var w = new WebClient())
			{
				// Set the url to the trends/place with the WOEID of Sweden.
				var requestString = baseUrl + "trends/place.json?id=23424954";

				// Add the header.
				w.Headers.Add("Authorization", "Bearer " + authString);

				// Send the request to the server.
				var response = w.DownloadString(requestString);

				// Parse the response to a JArray.
				var json = JArray.Parse(response);

				// Because of how Twitter delivers their data we need nested loops.
				foreach (var item in json)
				{
					foreach (var trend in item["trends"])
					{
						// Variable for the hashtag.
						var hashtag = (string)trend["name"];

						// Create a Trend object and add it to the list.
						entries.Add(new Trend
						{
							Hashtag = new Hashtag() { Name = hashtag.Replace("#", "").Replace(" ", "") },
							CreatedAt = DateTime.Now,
							ExpiresAt = DateTime.Now.AddMinutes(2)
						});
					}
				}
			}

			// Return the list.
			return entries;
		}

		public List<Tweet> GetTweetsByHashtag(Hashtag hashtag, int limit = 15)
		{
			// Create a list which will be returned.
			var entries = new List<Tweet>();

			// Get the Bearer token.
			var authString = GetBearerToken();

			// Using WebClient() to make the request to the API.
			// NOTE: Future implementations should use HttpClient() instead.
			using (var w = new WebClient())
			{
				// Set the url to the trends/place with the WOEID of Sweden.
				var requestString = String.Format("{0}search/tweets.json?q=%23{1}&count={2}&language=sv",
					baseUrl, WebUtility.UrlEncode(hashtag.Name), limit);

				// Add the header.
				w.Headers.Add("Authorization", "Bearer " + authString);

				// Send the request to the server.
				var response = w.DownloadString(requestString);

				// Parse the response to a JArray.
				var json = JObject.Parse(response);

				// Because of how Twitter delivers their data we need nested loops.
				foreach (var item in json["statuses"])
				{
					// Add it to the List.
					entries.Add(new Tweet
					{
						HashtagId = hashtag.Id,
						Username = (string)item["user"]["screen_name"],
						Name = (string)item["user"]["name"],
						ProfileImageUrl = (string)item["user"]["profile_image_url_https"],
						Text = (string)item["text"],
						Permalink = String.Format("https://twitter.com/{0}/status/{1}/",
							(string)item["user"]["screen_name"], (string)item["id_str"]),
						CreatedAt = DateTime.Now,
						ExpiresAt = DateTime.Now.AddMinutes(2)
					});
				}
			}

			// Return the list.
			return entries;
		}

		private string GetBearerToken()
		{
			// The URL where we request the OAuth token.
			var oauthTokenUrl = "https://api.twitter.com/oauth2/token/";

			// Encode to Base64.
			var authString = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(
				consumerKey + ":" + consumerSecret));

			// Make the POST request using HttpClient().
			using (var w = new WebClient())
			{
			// Add the headers needed.
				w.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
				w.Headers.Add("Authorization", "Basic " + authString);

				// Convert the content body to a byte array.
				var content = Encoding.ASCII.GetBytes("grant_type=client_credentials");

				// Send request to the server.
				var response = w.UploadData(oauthTokenUrl, "POST", content);

				// Initiate a JObject to parse the JSON response.
				var json = JObject.Parse(Encoding.ASCII.GetString(response));

				// Return the Bearer token.
				return (string)json["access_token"];
			}
		}
	}
}