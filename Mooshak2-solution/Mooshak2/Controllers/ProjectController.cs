using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Antlr.Runtime;
using Mooshak2.Models;

namespace Mooshak2.Controllers
{
	public class ProjectController : Controller
	{
		public List<CourseViewModel> getmodel()
		{ 
			var model = new List<CourseViewModel>();
			var a = new CourseViewModel();
			var b = new CourseViewModel();
			a.Name = "Forritun";
			a.Id = 1;
			var test1 = "Test1";
			a.TestList.Add(test1);
			b.Name = "EkkiForrun";
			var test2 = "Test2";
			b.TestList.Add(test2);
			b.TestList.Add(test1);
			b.Id = 2;
			model.Add(a);
			model.Add(b);
			return model;
		}

		// GET: Project
		public ActionResult Index()
		{
			var model = getmodel();
			return View(model);
		}

		public PartialViewResult SideTabRender(int id)
		{
			var model = getmodel();
			var test = model.First(i => i.Id == id);
			return PartialView("SideTabs",test);
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