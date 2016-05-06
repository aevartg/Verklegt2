﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }

	    public ActionResult Create()
	    {
			AdminService connection = new AdminService();
			AdminCourseViewModel model = new AdminCourseViewModel();
		    model.AllTeachers = connection.GetAllTeachers();
		    return View(model);
	    }

	    public ActionResult Edit(int id)
	    {
			AdminService connection = new AdminService();
		    var model = connection.GetAdminCourseViewModel(id);
		    return View(model);
	    }

        public ActionResult ListUsers()
        {
            AdminService connection = new AdminService();
            AdminCourseViewModel model = new AdminCourseViewModel();
            model.AllTeachers = connection.GetAllUsers();//náum i alla usera ekki bara teachers
            return View(model);
        }

		[HttpGet]
	    public ActionResult EditUser()
		{
			string name = "test@test.com";

			IdentityManager connection = new IdentityManager();
		    ApplicationUser user = connection.GetUser(name);
			RegisterViewModel model = new RegisterViewModel();
			model.Email = user.Email;
			model.UserType = connection.UserIsInRole(user.Id, "Teacher");
			model.Password = "hér kæmi password?";
			model.ConfirmPassword = "hér kæmi password?";

			return View(model);
	    }
    }
}