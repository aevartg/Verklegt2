using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
		    return View();
	    }

	    public ActionResult Edit(int id)
	    {
			AdminService connection = new AdminService();
		    var model = connection.GetAdminCourseViewModel(id);
		    return View(model);
	    }
    }
}