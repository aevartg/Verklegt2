using System;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Controllers
{
	public class ProjectController : Controller
	{
		// GET: Project
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult UserSettings()
        {
            return View();
        }

        [HttpPost]
		public ActionResult Test(HttpPostedFileBase file)
		{
			var test = Helper.StreamToBytes(file.InputStream);
			if (Helper.BytesToFile("C:\\Users\\Eythor\\Desktop\\uploadtest.js", test))
			{
				return View("Index");
			}
			throw new Exception();
		}
	}
}