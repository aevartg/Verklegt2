using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooshak2.Models;

namespace Mooshak2.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			if(HttpContext.User.IsInRole("Teacher"))
			{
				return RedirectToAction("Index", "Teacher");
			}
			else//tjekka hvort hann sé student
			{
				return RedirectToAction("Index", "Student");
			}
				
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}