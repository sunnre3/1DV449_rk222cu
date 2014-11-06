using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MashupApp.Models
{
	public class Repository : IDisposable
	{
		private bool _disposed = false;
		private MashupAppEntities _entities = new MashupAppEntities();

		public int Add(Hashtag hashtag)
		{
			// Use SP to add so we get the ID back.
			var id = _entities.uspAddHashtag(hashtag.Name);

			// Convert result row to integer.
			return Convert.ToInt32(id.FirstOrDefault().Value);
		}
		
		public int Add(Trend trend)
		{
			// Use SP to add so we get the ID back.
			var id = _entities.uspAddTrend(
				trend.HashtagId,
				trend.CreatedAt,
				trend.ExpiresAt
			);

			// Convert result row to integer.
			return Convert.ToInt32(id.FirstOrDefault().Value);
		}

		public int Add(Tweet tweet)
		{
			// Use SP to add so we get the ID back.
			var id = _entities.uspAddTweet(
				tweet.HashtagId,
				tweet.Username,
				tweet.Name,
				tweet.ProfileImageUrl,
				tweet.Text,
				tweet.Permalink,
				tweet.CreatedAt,
				tweet.ExpiresAt
			);

			// Convert result row to integer.
			return Convert.ToInt32(id.FirstOrDefault().Value);
		}

		public int Add(Instagram instagram)
		{
			// Use SP to add so we get the ID back.
			var id = _entities.uspAddInstagram(
				instagram.HashtagId,
				instagram.Username,
				instagram.ProfileImageUrl,
				instagram.ThumbnailUrl,
				instagram.ImageUrl,
				instagram.Caption,
				instagram.Permalink,
				instagram.CreatedAt,
				instagram.ExpiresAt
			);

			// Convert result row to integer.
			return Convert.ToInt32(id.FirstOrDefault().Value);
		}

		public IQueryable<Hashtag> GetHashtags()
		{
			return _entities.Hashtag.AsQueryable();
		}

		public IQueryable<Trend> GetTrends()
		{
			return _entities.Trend.AsQueryable();
		}

		public IQueryable<Tweet> GetTweets()
		{
			return _entities.Tweet.AsQueryable();
		}

		public IQueryable<Tweet> GetTweets(Hashtag hashtag)
		{
			return _entities.Tweet.Where(tw => tw.HashtagId == hashtag.Id).AsQueryable();
		}

		public IQueryable<Instagram> GetInstagrams()
		{
			return _entities.Instagram.AsQueryable();
		}

		public IQueryable<Instagram> GetInstagrams(Hashtag hashtag)
		{
			return _entities.Instagram.Where(i => i.HashtagId == hashtag.Id).AsQueryable();
		}
		
		public void ClearTrends()
		{
			_entities.uspClearTrends();
		}

		public void ClearTweets()
		{
			_entities.uspClearTweets();
		}

		public void ClearInstagrams()
		{
			_entities.uspClearInstagrams();
		}

		public void DeleteTweetsByHashtag(Hashtag hashtag)
		{
			_entities.uspDeleteTweetsByHashtagId(hashtag.Id);
		}

		public void DeleteInstagramsByHashtag(Hashtag hashtag)
		{
			_entities.uspDeleteInstagramsByHashtagId(hashtag.Id);
		}

		public void Save()
		{
			_entities.SaveChanges();
		}

		#region IDisposable
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_entities.Dispose();
				}
			}

			_disposed = true;
		}
		#endregion
	}
}