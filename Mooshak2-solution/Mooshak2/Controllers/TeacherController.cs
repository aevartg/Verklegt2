using System.Net.Mime;
using System.Web.Mvc;
using Mooshak2.Models;
using Mooshak2.Services;

namespace Mooshak2.Controllers
{
	[Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
			var model = new CourseService(null).GetCourseViewModels();
			return View(model);
		}

	    [HttpGet]
	    public PartialViewResult CreateAssignment(int courseId)
	    {
		    var model = new CreateAssignmentViewModel()
						{
							CourseId = courseId
						};
		    return PartialView("_CreateAssignment", model);
	    }

		[HttpPost]
		public ActionResult CreateAssignment(CreateAssignmentViewModel model)
		{
			//if (!ModelState.IsValid)
			//{
			//	return PartialView("_CreateAssignment", model);
			//}
			var assignmentService = new AssignmentService(null);
			if (assignmentService.CreateAssignment(model.Name, model.CourseId,model.DateOpen,model.DateClose))
			{
				var tempAssignment = assignmentService.GetAssignmentByName(model.CourseId, model.Name);
				var milestoneService = new MilestoneService(null);
				var inputOutputService = new InputOutputService(null);
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
			var model = new TeacherAssignmentViewModel();
			var milestone = new MilestoneService(null).GetMilestoneByID(id);
			model.AssignId = milestone.AssignmentId;
			model.MilestoneName = milestone.Name;
			model.AssignmentName = new AssignmentService(null).GetAssignmentById(milestone.AssignmentId).Name;
			model.Submissions = new SubmissionService(null).GetSubmissionViewModelsByMilestone(id);
			model.AllStudents = new UserService(null).GetAllStudents();

			return PartialView("Content", model);
		}

		public ActionResult EditAssignment(int Id)
		{
			var model = new AssignmentService(null).GetEditAssignmentViewModel(Id);
			return View(model);

		}

		[HttpPost]
		public ActionResult EditAssignment(EditAssignmentViewModel model)
		{

				new AssignmentService(null).EditAssignment(model);
				return RedirectToAction("Index");
			
		}
		public ActionResult Download(int submissionId)
		{
			var x = new SubmissionService(null).GetSubmissionById(submissionId);
			return File(x.Blob, "application/javascript", x.SubmitDate.ToShortDateString() + "_submissionId" + submissionId + x.FileExtension);
		}

		public ActionResult DeleteAssignment(int Id)
		{
			new AssignmentService(null).DeleteAssignment(Id);
			return RedirectToAction("Index");
		}

	}
}