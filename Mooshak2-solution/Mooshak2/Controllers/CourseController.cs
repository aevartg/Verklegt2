using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooshak2.Models;

namespace Mooshak2.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Create()
		{
			/*CourseViewModel model = new CourseViewModel();
			model.AvailableUsers = GetAvailableUsers();
			return View(model);*/
			return View();
		}
	}
}