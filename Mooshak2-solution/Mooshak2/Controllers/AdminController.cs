using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View();
        }
		[HttpGet]
	    public ActionResult Create()
	    {
			AdminService connection = new AdminService();
			AdminCourseViewModel model = new AdminCourseViewModel();
		    model.AllTeachers = connection.GetAllTeachers();

		    return View(model);
	    }
		[HttpGet]
		

	    [HttpPost]
	    public ActionResult Create(AdminCourseViewModel model)
	    {
		    Course newCourse = new Course();
		    newCourse.Id = model.Id;
		    newCourse.Name = model.Name;
			//hér þarf að taka alla Teachera úr TeachersInCourse úr modelinu og linka þá við nýja coursinn
			//og svo að lokum save'a nýja coursinn í database
		    return View("Index"); // eigum eftir að gera index view fyrir Admin
	    }

		[HttpPost]
	    public ActionResult Add(string userName) //tekur við userName, færir þann user yfir í TeachersInCourse og kallar aftur a create nema með því viewi
	    {
			AdminService c = new AdminService();
			AdminCourseViewModel model = new AdminCourseViewModel();
			c.UpdateAdminCourseViewModel(model, userName);
			//Update'a aðeins TeachersInCourse listann í AdminCourseViewModelinu, ss lata user sem er valinn her að ofan inn i þann lista og kalla aftur á create nema með uppfært viewmodel?
		    return View("Create", userName);
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