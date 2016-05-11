using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;
using Mooshak2.Services;

namespace Mooshak2.Controllers
{
	[Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
			var model = new CourseService().GetCourseViewModels();
			return View(model);
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
				return View(model);
			}
			var assignmentService = new AssignmentService();
			if (assignmentService.CreateAssignment(model.Name, model.CourseId,model.DateOpen,model.DateClose))
			{
				var tempAssignment = assignmentService.GetAssignmentByName(model.CourseId, model.Name);
				var milestoneService = new MilestoneService();
				var inputOutputService = new InputOutputService();
				foreach (var x in model.Milestones)
				{
					milestoneService.CreateMilestone(tempAssignment.Id, x.Name, x.Weight);
					var tempMilestone = milestoneService.GetMilestoneByName(tempAssignment.Id, x.Name);
					inputOutputService.CreateInputOutput(tempMilestone.Id, x.File);
				}
			}
			return RedirectToAction("Index");
		}

		public PartialViewResult ContentRender(int id)
		{
			TeacherAssignmentViewModel model = new TeacherAssignmentViewModel();
			Milestone milestone = new MilestoneService().GetMilestoneByID(id);
			model.MilestoneName = milestone.Name;
			model.AssignmentName = new AssignmentService().GetAssignmentById(milestone.AssignmentId).Name;
			model.Submissions = new SubmissionService().GetSubmissionsByMilestoneID(id);
			model.AllStudents = new UserService().GetAllStudents();

			return PartialView("Content", model);
		}
	}
}