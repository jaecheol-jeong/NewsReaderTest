using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventWebMvc.Models;

namespace EventWebMvc.Controllers
{
	public class ErrorController : Controller
	{
		//
		// GET: /Error/
		public ActionResult Index(string model = null)
		{
			if (model != null)
			{
				List<string> des = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<string>>(model);
				return View(des);
			}
			return View();
			
		}

	}
}
