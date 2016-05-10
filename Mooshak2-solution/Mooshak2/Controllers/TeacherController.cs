using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooshak2.Models;
using Mooshak2.Services;

namespace Mooshak2.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

	    [HttpGet]
	    public ActionResult CreateAssignment()
	    {
		    var courseId = 1;
		    var model = new CreateAssignmentViewModel()
						{
							CourseId = courseId
						};
		    return View(model);
	    }

		[HttpPost]
		public ActionResult CreateAssignment(CreateAssignmentViewModel model)
		{
			if (!ModelState.IsValid)
			{
			}
			var assign = new AssignmentService().GetAssignmentById(model.CourseId);
			return RedirectToAction("Index");
		}
	}
}