using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;
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
				new CourseService().CreateCourse(model); //new AdminService().CreateCourse(model);
			}

			return RedirectToAction("Index"); // eigum eftir að gera index view fyrir Admin
		}

		public ActionResult Edit(int id)
		{
			CourseService connection = new CourseService();
			var model = connection.GetAdminCourseViewModel(id);
			return View(model);
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
			var model = new EdtiCourseViewModel();
			CourseService c = new CourseService();
			Course course = new Course();
			course = c.GetCourseById(id);
			model = c.GetEdtiCourseViewModel(course);
			return PartialView("_Content", model);
		}
	}
}