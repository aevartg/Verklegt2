using System.IO;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mooshak2.Services;


namespace Mooshak2.Controllers
{
	[Authorize]
	public class StudentController : Controller
	{
		// GET: Project
		public ActionResult Index()
		{
			var model = new CourseService().GetCourseViewModels();
			return View(model);
		}

		public PartialViewResult ContentRender(int id)
		{
			var model = new AssignmentService().GetAssignmentViewModel(HttpContext.User.Identity.GetUserId(), id);
			return PartialView("_content", model);
		}

		public PartialViewResult InputOutputRender(int submissionId, int milestoneId)
		{
			var model = new InputOutputService().GetInputsOutputsViewModel(submissionId, milestoneId,
				HttpContext.User.Identity.GetUserId());
			return PartialView("_InputOutput", model);
		}

		[HttpPost]
		public PartialViewResult SubmitForm(int id,HttpPostedFileBase file)
		{
			new SubmissionService().CreateSubmission(file, id,Path.GetExtension(file.FileName));
			var model = new AssignmentService().GetAssignmentViewModel(HttpContext.User.Identity.GetUserId(), id);
			return PartialView("_content",model);
		}

		public ActionResult Download(int submissionId)
		{
			var x = new SubmissionService().GetSubmissionById(submissionId);
			return File(x.Blob, "application/javascript", "temp.js");
		}
	}
}