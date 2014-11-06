using MashupApp.Models.Web_Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MashupApp.Models
{
	public class Service
	{
		private Repository repository;

		private TwitterService twitterService;
		private InstagramService instagramService;

		public Service(Repository repository)
		{
			this.repository = repository;

			// Initiate web services.
			twitterService = new TwitterService(
				"A9oUN3S8G34jzB8ulORpGAGZO",
				"MLBxfvTjztnQKYhS0K6yDdVHXFA7V1k1tFeofQ2gN6Rf5U4vO5");

			instagramService = new InstagramService(
				"801ed657e70f48b58f9c78bdf8f430cc");
		}

		public Dictionary<string, object> GetTrends()
		{
			// Get the Trends from repository.
			var trends = repository.GetTrends().ToList();

			// Make sure the Trends are up to date.
			if (trends.Count == 0 || trends.First().ExpiresAt <= DateTime.Now)
			{
				try
				{
					// Get a list of Trends from web service.
					var newTrends = twitterService.GetTrends();

					// Clear the repository of entries.
					repository.ClearTrends();

					// Loop through the list and save them
					// to the repository.
					foreach (var trend in newTrends)
					{
						// Save the hashtag and get the id.
						trend.HashtagId = repository.Add(trend.Hashtag);

						// Save the trend.
						trend.Id = repository.Add(trend);
					}

					// Save changes in repository.
					repository.Save();

					// Assign the newly retrieved entries as
					// the one we will return.
					trends = newTrends;
				} catch (WebException) {}
				// A WebException means that we (for some reason)
				// couldn't reach the API and so we catch the exception
				// and let whatever data we have in the repository be
				// and return that instead (eventhough it might be old).
			}

			// Create a dictionary to hold the trends entries.
			var entries = new List<Dictionary<string, string>>();

			// Loop through the list of trend objects
			// and add each trend name to the string list.
			foreach (var trend in trends)
			{
				entries.Add(new Dictionary<string, string>()
				{
					{ "name", trend.Hashtag.Name },
					{ "createdAt", trend.CreatedAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") },
					{ "expiresAt", trend.ExpiresAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") }
				});
			}

			// Create a dictionary.
			var ret = new Dictionary<string, object>();

			// Add the data.
			ret.Add("meta", GetMetaData(trends));
			ret.Add("data", entries);

			// Return the dictionary.
			return ret;
		}

		public Dictionary<string, object> GetTweetsByHashtag(string hashtagString)
		{
			// Create a new Hashtag object and save it.
			var hashtag = new Hashtag() { Name = hashtagString };
			hashtag.Id = repository.Add(hashtag);

			// Get the Instagrams from repository.
			var tweets = repository.GetTweets(hashtag).ToList();

			// Make sure the Instagrams are up to date.
			if (tweets.Count == 0 || tweets.First().ExpiresAt <= DateTime.Now)
			{
				try
				{
					// Get a list of Tweets from web service.
					var newTweets = twitterService.GetTweetsByHashtag(hashtag);

					// Delete all current (if any) Tweet entries from
					// the repository.
					repository.DeleteInstagramsByHashtag(hashtag);

					// Loop through the list and save them
					// to the repository.
					foreach (var tweet in newTweets)
					{
						tweet.Hashtag = hashtag;
						tweet.Id = repository.Add(tweet);
					}

					// Save changes in repository.
					repository.Save();

					// Assign the newly retrieved entries as
					// the one we will return.
					tweets = newTweets;
				} catch (WebException) {}
				// A WebException means that we (for some reason)
				// couldn't reach the API and so we catch the exception
				// and let whatever data we have in the repository be
				// and return that instead (eventhough it might be old).
			}

			// Create a dictionary to hold the instagrams entries.
			var entries = new List<Dictionary<string, string>>();

			// Loop through the list of trend objects
			// and add each trend name to the string list.
			foreach (var tweet in tweets)
			{
				entries.Add(new Dictionary<string, string>()
				{
					{ "id", tweet.Id.ToString() },
					{ "username", tweet.Username },
					{ "name", tweet.Name },
					{ "userProfileImageUrl", tweet.ProfileImageUrl },
					{ "text", tweet.Text },
					{ "permalink", tweet.Permalink },
					{ "createdAt", tweet.CreatedAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") },
					{ "expiresAt", tweet.ExpiresAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") }
				});
			}

			// Create a dictionary.
			var ret = new Dictionary<string, object>();

			// Add the data.
			ret.Add("meta", GetMetaData(tweets));
			ret.Add("data", entries);

			// Return the dictionary.
			return ret;
		}

		public Dictionary<string, object> GetInstagramsByHashtag(string hashtagString)
		{
			// Create a new Hashtag object and save it.
			var hashtag = new Hashtag() { Name = hashtagString };
			hashtag.Id = repository.Add(hashtag);

			// Get the Instagrams from repository.
			var instagrams = repository.GetInstagrams(hashtag).ToList();

			// Make sure the Instagrams are up to date.
			if (instagrams.Count == 0 || instagrams.First().ExpiresAt <= DateTime.Now)
			{
				try
				{
					// Get a list of Instagrams from web service.
					var newInstagrams = instagramService.GetInstagramsByHashtag(hashtag);

					// Delete all current (if any) Instagram entries from
					// the repository.
					repository.DeleteInstagramsByHashtag(hashtag);

					// Loop through the list and save them
					// to the repository.
					foreach (var instagram in newInstagrams)
					{
						instagram.Hashtag = hashtag;
						instagram.Id = repository.Add(instagram);
					}

					// Save changes in repository.
					repository.Save();

					// Assign the newly retrieved entries as
					// the one we will return.
					instagrams = newInstagrams;
				} catch (WebException) {}
				// A WebException means that we (for some reason)
				// couldn't reach the API and so we catch the exception
				// and let whatever data we have in the repository be
				// and return that instead (eventhough it might be old).
			}

			// Create a dictionary to hold the instagrams entries.
			var entries = new List<Dictionary<string, string>>();

			// Loop through the list of trend objects
			// and add each trend name to the string list.
			foreach (var instagram in instagrams)
			{
				entries.Add(new Dictionary<string, string>()
				{
					{ "id", instagram.Id.ToString() },
					{ "username", instagram.Username },
					{ "userProfileImageUrl", instagram.ProfileImageUrl },
					{ "thumbnailUrl", instagram.ThumbnailUrl },
					{ "imageUrl", instagram.ImageUrl },
					{ "caption", instagram.Caption },
					{ "permalink", instagram.Permalink },
					{ "createdAt", instagram.CreatedAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") },
					{ "expiresAt", instagram.ExpiresAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") }
				});
			}

			// Create a dictionary.
			var ret = new Dictionary<string, object>();

			// Add the data.
			ret.Add("meta", GetMetaData(instagrams));
			ret.Add("data", entries);

			// Return the dictionary.
			return ret;
		}

		#region Meta data helper methods
		private Dictionary<string, string> GetMetaData(List<Trend> list)
		{
			return GetMetaData(
				list.Count,
				(list.Count > 0) ? list.First().CreatedAt : new DateTime(),
				(list.Count > 0) ? list.First().ExpiresAt : new DateTime());
		}

		private Dictionary<string, string> GetMetaData(List<Tweet> list)
		{
			return GetMetaData(
				list.Count,
				(list.Count > 0) ? list.First().CreatedAt : new DateTime(),
				(list.Count > 0) ? list.First().ExpiresAt : new DateTime());
		}

		private Dictionary<string, string> GetMetaData(List<Instagram> list)
		{
			return GetMetaData(
				list.Count,
				(list.Count > 0) ? list.First().CreatedAt : new DateTime(),
				(list.Count > 0) ? list.First().ExpiresAt : new DateTime());
		}

		private Dictionary<string, string> GetMetaData(int numberOfEntries, DateTime createdAt, DateTime expiresAt)
		{
			// Create a Dictionary.
			var dict = new Dictionary<string, string>();

			// Add the number of entries.
			dict.Add("numberOfEntries", numberOfEntries.ToString());

			if (numberOfEntries > 0)
			{
				dict.Add("createdAt", createdAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
				dict.Add("expiresAt", expiresAt.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
			}

			// Return the Dictionary.
			return dict;
		}
		#endregion
	}
}