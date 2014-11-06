﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MashupApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class MashupAppEntities : DbContext
    {
        public MashupAppEntities()
            : base("name=MashupAppEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Instagram> Instagram { get; set; }
        public DbSet<Trend> Trend { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<Hashtag> Hashtag { get; set; }
    
        public virtual ObjectResult<Nullable<decimal>> uspAddInstagram(Nullable<int> hashtagId, string username, string profileImageUrl, string thumbnailUrl, string imageUrl, string caption, string permalink, Nullable<System.DateTime> createdAt, Nullable<System.DateTime> expiresAt)
        {
            var hashtagIdParameter = hashtagId.HasValue ?
                new ObjectParameter("HashtagId", hashtagId) :
                new ObjectParameter("HashtagId", typeof(int));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var profileImageUrlParameter = profileImageUrl != null ?
                new ObjectParameter("ProfileImageUrl", profileImageUrl) :
                new ObjectParameter("ProfileImageUrl", typeof(string));
    
            var thumbnailUrlParameter = thumbnailUrl != null ?
                new ObjectParameter("ThumbnailUrl", thumbnailUrl) :
                new ObjectParameter("ThumbnailUrl", typeof(string));
    
            var imageUrlParameter = imageUrl != null ?
                new ObjectParameter("ImageUrl", imageUrl) :
                new ObjectParameter("ImageUrl", typeof(string));
    
            var captionParameter = caption != null ?
                new ObjectParameter("Caption", caption) :
                new ObjectParameter("Caption", typeof(string));
    
            var permalinkParameter = permalink != null ?
                new ObjectParameter("Permalink", permalink) :
                new ObjectParameter("Permalink", typeof(string));
    
            var createdAtParameter = createdAt.HasValue ?
                new ObjectParameter("CreatedAt", createdAt) :
                new ObjectParameter("CreatedAt", typeof(System.DateTime));
    
            var expiresAtParameter = expiresAt.HasValue ?
                new ObjectParameter("ExpiresAt", expiresAt) :
                new ObjectParameter("ExpiresAt", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("uspAddInstagram", hashtagIdParameter, usernameParameter, profileImageUrlParameter, thumbnailUrlParameter, imageUrlParameter, captionParameter, permalinkParameter, createdAtParameter, expiresAtParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> uspAddTrend(Nullable<int> hashtagId, Nullable<System.DateTime> createdAt, Nullable<System.DateTime> expiresAt)
        {
            var hashtagIdParameter = hashtagId.HasValue ?
                new ObjectParameter("HashtagId", hashtagId) :
                new ObjectParameter("HashtagId", typeof(int));
    
            var createdAtParameter = createdAt.HasValue ?
                new ObjectParameter("CreatedAt", createdAt) :
                new ObjectParameter("CreatedAt", typeof(System.DateTime));
    
            var expiresAtParameter = expiresAt.HasValue ?
                new ObjectParameter("ExpiresAt", expiresAt) :
                new ObjectParameter("ExpiresAt", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("uspAddTrend", hashtagIdParameter, createdAtParameter, expiresAtParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> uspAddTweet(Nullable<int> hashtagId, string username, string name, string profileImageUrl, string text, string permalink, Nullable<System.DateTime> createdAt, Nullable<System.DateTime> expiresAt)
        {
            var hashtagIdParameter = hashtagId.HasValue ?
                new ObjectParameter("HashtagId", hashtagId) :
                new ObjectParameter("HashtagId", typeof(int));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var profileImageUrlParameter = profileImageUrl != null ?
                new ObjectParameter("ProfileImageUrl", profileImageUrl) :
                new ObjectParameter("ProfileImageUrl", typeof(string));
    
            var textParameter = text != null ?
                new ObjectParameter("Text", text) :
                new ObjectParameter("Text", typeof(string));
    
            var permalinkParameter = permalink != null ?
                new ObjectParameter("Permalink", permalink) :
                new ObjectParameter("Permalink", typeof(string));
    
            var createdAtParameter = createdAt.HasValue ?
                new ObjectParameter("CreatedAt", createdAt) :
                new ObjectParameter("CreatedAt", typeof(System.DateTime));
    
            var expiresAtParameter = expiresAt.HasValue ?
                new ObjectParameter("ExpiresAt", expiresAt) :
                new ObjectParameter("ExpiresAt", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("uspAddTweet", hashtagIdParameter, usernameParameter, nameParameter, profileImageUrlParameter, textParameter, permalinkParameter, createdAtParameter, expiresAtParameter);
        }
    
        public virtual int uspClearInstagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspClearInstagrams");
        }
    
        public virtual int uspClearTrends()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspClearTrends");
        }
    
        public virtual int uspClearTweets()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspClearTweets");
        }
    
        public virtual int uspDeleteInstagramsByTrendId(Nullable<int> trendId)
        {
            var trendIdParameter = trendId.HasValue ?
                new ObjectParameter("TrendId", trendId) :
                new ObjectParameter("TrendId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspDeleteInstagramsByTrendId", trendIdParameter);
        }
    
        public virtual int uspDeleteTweetsByTrendId(Nullable<int> trendId)
        {
            var trendIdParameter = trendId.HasValue ?
                new ObjectParameter("TrendId", trendId) :
                new ObjectParameter("TrendId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspDeleteTweetsByTrendId", trendIdParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> uspAddHashtag(string name)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("uspAddHashtag", nameParameter);
        }
    
        public virtual int uspDeleteInstagramsByHashtagId(Nullable<int> hashtagId)
        {
            var hashtagIdParameter = hashtagId.HasValue ?
                new ObjectParameter("HashtagId", hashtagId) :
                new ObjectParameter("HashtagId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspDeleteInstagramsByHashtagId", hashtagIdParameter);
        }
    
        public virtual int uspDeleteTweetsByHashtagId(Nullable<int> hashtagId)
        {
            var hashtagIdParameter = hashtagId.HasValue ?
                new ObjectParameter("HashtagId", hashtagId) :
                new ObjectParameter("HashtagId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspDeleteTweetsByHashtagId", hashtagIdParameter);
        }
    }
}
