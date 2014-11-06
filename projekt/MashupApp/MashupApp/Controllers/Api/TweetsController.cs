using MashupApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MashupApp.Controllers.Api
{
    public class TweetsController : Controller
    {
		private Repository repository;
		private Service service;

		public TweetsController()
		{
			repository = new Repository();
			service = new Service(repository);
		}

		public ActionResult GetByHashtag(string hashtag)
		{
			try
			{
				// List of instagrams.
				var tweets = service.GetTweetsByHashtag(hashtag);

				// Return list as JSON.
				return Json(tweets, JsonRequestBehavior.AllowGet);
			}

			catch (WebException)
			{
				// Set the response code.
				Response.StatusCode = 503;

				// Create a Dictionary to send a message
				// from the API.
				var dict = new Dictionary<string, string>();
				dict.Add("error", "An error occured connecting to the API.");
				dict.Add("statusCode", "503");
				return Json(dict, JsonRequestBehavior.AllowGet);
			}

			catch
			{
				// Set the response code.
				Response.StatusCode = 500;

				// Create a Dictionary to send a message
				// from the API.
				var dict = new Dictionary<string, string>();
				dict.Add("error", "An unknown error occured.");
				dict.Add("statusCode", "500");
				return Json(dict, JsonRequestBehavior.AllowGet);
			}
		}

    }
}
