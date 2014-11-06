using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MashupApp.Models.Web_Services
{
	public class InstagramService
	{
		private string clientId;
		private string _instagramApiUrl = "https://api.instagram.com/v1/tags/{0}/media/recent?count={1}&client_id=";

		private string InstagramApiUrl
		{
			get
			{
				return _instagramApiUrl + clientId;
			}
		}

		public InstagramService(string clientId)
		{
			this.clientId = clientId;
		}

		public List<Instagram> GetInstagramsByHashtag(Hashtag hashtag, int limit = 10)
		{
			// Create a list.
			var entries = new List<Instagram>();

			// Using WebClient() to make the GET request.
			// NOTE: Should probably use HttpClient().
			using (var w = new WebClient())
			{
				// Create the request string.
				var requestString = String.Format(InstagramApiUrl, hashtag.Name, limit);

				// Send the request to the server.
				var response = w.DownloadString(requestString);

				// Parse the response to a JObject.
				var json = JObject.Parse(response);

				// Loop through the data and create Instagram objects.
				foreach (var item in json["data"])
				{
					// Instagrams doesn't have to have a caption
					// and are sometimes null.
					var caption = String.Empty;

					if (item["caption"].Count() > 0)
					{
						caption = (string)item["caption"]["text"];
					}

					// Add a new Instagram.
					entries.Add(new Instagram
					{
						HashtagId = hashtag.Id,
						Username = (string)item["user"]["username"],
						ProfileImageUrl = (string)item["user"]["profile_picture"],
						ThumbnailUrl = (string)item["images"]["thumbnail"]["url"],
						ImageUrl = (string)item["images"]["standard_resolution"]["url"],
						Caption = caption,
						Permalink = (string)item["link"],
						CreatedAt = DateTime.Now,
						ExpiresAt = DateTime.Now.AddMinutes(2)
					});
				}
			}

			// Return list.
			return entries;
		}
	}
}