using MashupApp.Models;
using MashupApp.Models.Web_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MashupApp.Controllers
{
    public class TrendsController : Controller
    {
		private Repository repository;
		private Service service;

		public TrendsController()
		{
			repository = new Repository();
			service = new Service(repository);
		}

		public ActionResult Index()
		{
			try
			{
				// List of trends.
				var trends = service.GetTrends();

				// Return list as JSON.
				return Json(trends, JsonRequestBehavior.AllowGet);
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
