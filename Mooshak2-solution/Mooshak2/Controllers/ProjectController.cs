using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Mooshak2.Models;

namespace Mooshak2.Controllers
{
	public class ProjectController : Controller
	{
		// GET: Project
		public ActionResult Index()
		{
			var model = new List<CourseViewModel>();
			var a = new CourseViewModel();
			var b = new CourseViewModel();
			a.Name = "Forritun";
			b.Name = "EkkiForrun";
			model.Add(a);
			model.Add(b);
			return View(model);
		}

		public PartialViewResult SideTabRender()
		{
			return PartialView("SideTabs");
		}

        public ActionResult UserSettings()
        {
            return View();
        }

        [HttpPost]
		public ActionResult Test(HttpPostedFileBase file)
		{
			var test = Helper.StreamToBytes(file.InputStream);
			if (Helper.BytesToFile("C:\\Users\\Eythor\\Desktop\\uploadtest.js", test))
			{
				return View("Index");
			}
			throw new Exception();
		}
	}
}