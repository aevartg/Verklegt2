﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult FailFish()
		{
			Response.StatusCode = 404;
			Response.TrySkipIisCustomErrors = true;
			return View();
		}
	}
}