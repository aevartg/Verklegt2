using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Mooshak2.Models;
using Mooshak2.Services;

namespace Mooshak2.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class AdminController : Controller
	{
		// GET: Admin
		public ActionResult Index()
		{
			var model = new CourseService(null).GetAdminNavCourseViewModels();
			return View(model);
		}

		[HttpGet]
		public PartialViewResult Create()
		{
			UserService connection = new UserService(null);
			var model = new CreateCourseViewModel();
			model.Teachers = new SelectList(connection.GetAllTeachers(), "Id", "username");
			model.Students = new SelectList(connection.GetAllStudents(), "Id", "username");
			return PartialView("_Create", model);
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
				new CourseService(null).CreateCourse(model); 
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
				new CourseService(null).EditCourse(model);
			}
			return RedirectToAction("Index");
		}

		public PartialViewResult ListUsers()
		{
			UserService connection = new UserService(null);
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
				model.Id = user.Id;
			    model.Username = user.UserName;
				model.Email = user.Email;
				if (connection.UserIsInRole(user.Id, "Teacher"))
				{
					model.UserType = 1;
				}else if (connection.UserIsInRole(user.Id, "Administrator"))
				{
					model.UserType = 2;
				}
				model.NavModel = new CourseService(null).GetAdminNavCourseViewModels();
				return View(model);
			}
		}

		[HttpPost]
		public ActionResult EditUser(EditUserSettingsViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			else
			{
				new UserService(null).EditUser(model);

			}
			return RedirectToAction("Index");
		}

		public PartialViewResult ContentRender(int id)
		{

			CourseService c = new CourseService(null);
			var course = c.GetCourseById(id);
			var model = c.GetEditCourseViewModel(course);
			return PartialView("_Content", model);
		}

		public ActionResult DeleteUser(string Id)
		{
			new UserService(null).DeleteUser(Id);
			return RedirectToAction("Index");
		}

		public ActionResult DeleteCourse(int Id)
		{
			new CourseService(null).DeleteCourse(Id);
			return RedirectToAction("Index");
		}

		public PartialViewResult HomeView()
		{
			return PartialView("_HomeView");
		} 
	}
}