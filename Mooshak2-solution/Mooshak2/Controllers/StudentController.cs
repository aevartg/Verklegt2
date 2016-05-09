using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Antlr.Runtime;
using Microsoft.AspNet.Identity;
using Mooshak2.Models;
using Mooshak2.Services;


namespace Mooshak2.Controllers
{
	public class StudentController : Controller
	{
		// GET: Project
		public ActionResult Index()
		{
			var model = new CourseService().GetCourseViewModels();
			return View(model);
		}

		public PartialViewResult SideTabRender(int id)
		{
			var model = new AssignmentService().GetAssignmentNavViewModels(id);
			return PartialView("_SideTabs",model);
		}

		public PartialViewResult SideTabDataRender(int id)
		{
			var model = new AdminService().GetAssignmentViewModel(HttpContext.User.Identity.GetUserId(), id);
			return PartialView("SideTabsData", model);
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
				
			}
			throw new Exception();
		}
	}
}