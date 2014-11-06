using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MashupApp
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Api/Trends",
				url: "api/trends",
				defaults: new { controller = "Trends", action = "Index" }
			);

			routes.MapRoute(
				name: "Api/Instagrams/ByHashtag",
				url: "api/instagrams/hashtag/{hashtag}",
				defaults: new { controller = "Instagrams", action = "GetByHashtag" }
			);

			routes.MapRoute(
				name: "Api/Tweets/ByHashtag",
				url: "api/Tweets/hashtag/{hashtag}",
				defaults: new { controller = "Tweets", action = "GetByHashtag" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}