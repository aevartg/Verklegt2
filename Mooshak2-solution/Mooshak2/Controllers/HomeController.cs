using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooshak2.Models;

namespace Mooshak2.Controllers
{
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
			var manager = new IdentityManager();
			var user = new ApplicationUser();
			user.UserName = "admin@admin.com";
			user.Email = "admin@admin.com";
			if (!manager.UserExists("admin@admin.com"))
			{
				manager.CreateUser(user, "123456");
			}
			ViewBag.Message = "Your application description page.";
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}