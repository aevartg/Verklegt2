using System;
using System.Web.Mvc;
using Mooshak2.Models;
using Mooshak2.Services;

namespace Mooshak2.Controllers
{
	public class AdminController : Controller
	{
		// GET: Admin
		public ActionResult Index()
		{
			var model = new CourseService().GetAdminNavCourseViewModels();
			return View(model);
		}

		[HttpGet]
		public ActionResult Create()
		{
			UserService connection = new UserService();
			var model = new CreateCourseViewModel();
			model.Teachers = new SelectList(connection.GetAllTeachers(), "Id", "username");
			model.Students = new SelectList(connection.GetAllStudents(), "Id", "username");
			return View(model);
		}


		[HttpPost]
		public ActionResult Create(CreateCourseViewModel model)
		{
			if (!ModelState.IsValid)
			{
				//eitthvað er að
			}
			else
			{
				new CourseService().CreateCourse(model); 
			}

			return RedirectToAction("Index"); 
		}

		[HttpPost]
		public ActionResult Edit(EditCourseViewModel model)
		{
			if (!ModelState.IsValid)
			{
				//eitthvað að
			}
			else
			{
				new CourseService().EditCourse(model);
			}
			return RedirectToAction("Index");
		}

		public PartialViewResult ListUsers()
		{
			UserService connection = new UserService();
			ListUsersViewModel model = new ListUsersViewModel();
			model.AllTeachers = connection.GetAllTeachers();
			model.AllUsers = connection.GetAllUsers();
			model.AllStudents = connection.GetAllStudents();

			return PartialView("_ListUsers", model);
		}

		[HttpPost]
		public ActionResult ListUsers(FormCollection model)
		{
			string username = model.Get("users1");
			TempData["username"] = username;
			return RedirectToAction("EditUser");
		}

		[HttpGet]
		public ActionResult EditUser()
		{
			if (TempData["username"] == null)
			{
				return RedirectToAction("ListUsers");
			}
			else
			{
				string username = Convert.ToString(TempData["username"]);
				IdentityManager connection = new IdentityManager();
				ApplicationUser user = connection.GetUser(username);
				var model = new EditUserSettingsViewModel();
			    model.Username = user.UserName;
				model.Email = user.Email;
				model.UserType = connection.UserIsInRole(user.Id, "Teacher");
				return View(model);
			}
		}

		[HttpPost]
		public ActionResult EditUser(RegisterViewModel model)
		{

			return View("Index");
		}

		public PartialViewResult ContentRender(int id)
		{

			CourseService c = new CourseService();
			var course = c.GetCourseById(id);
			var model = c.GetEditCourseViewModel(course);
			return PartialView("_Content", model);
		}
	}
}