﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Models
{
	public class AdminViewModels
	{
	}

	public class AdminCourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<UserViewModel> AllTeachers = new List<UserViewModel>();
		public List<UserViewModel> TeachersInCourse = new List<UserViewModel>();
	}

	public class CreateCourseViewModel
	{
		public string Name { get; set; }
		public string[] SelectedTeachers { get; set; }
		public SelectList Teachers { get; set; }
	}
}