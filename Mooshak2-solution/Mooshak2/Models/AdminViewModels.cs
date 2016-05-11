using System;
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
		public string [] SelectedStudents { get; set; }
		public SelectList Teachers { get; set; }
		public SelectList Students { get; set; }
	}

	public class EditUserViewModel
	{
		public string Username { get; set; }
		public string Email { get; set; }		
	}

	public class EdtiCourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string[] TeachersInCourse { get; set; }
		public string[] StudentsInCourse { get; set; }
		public SelectList Teachers { get; set; }
		public SelectList Students { get; set; }
	}
}