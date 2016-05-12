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
			else if(HttpContext.User.IsInRole("Administrator"))
			{
				return RedirectToAction("Index", "Admin");
			}
			return RedirectToAction("Index", "Student");
		}
	}
}