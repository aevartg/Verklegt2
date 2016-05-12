using System.Collections.Generic;
using System.Web.Mvc;

namespace Mooshak2.Models
{
	public class AdminViewModels
	{
	}

	public class AdminCourseViewModel
	{
		public List<UserViewModel> AllTeachers = new List<UserViewModel>();
		public List<UserViewModel> TeachersInCourse = new List<UserViewModel>();
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class CreateCourseViewModel
	{
		public string Name { get; set; }
		public string[] SelectedTeachers { get; set; }
		public string[] SelectedStudents { get; set; }
		public SelectList Teachers { get; set; }
		public SelectList Students { get; set; }
	}

	public class EditUserViewModel
	{
		public string Username { get; set; }
		public string Email { get; set; }
	}

	public class EditCourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public SelectList TeachersInCourse { get; set; }
		public SelectList StudentsInCourse { get; set; }
		public SelectList Teachers { get; set; }
		public SelectList Students { get; set; }
		public string[] SelectedTeachers { get; set; }
		public string[] SelectedStudents { get; set; }
	}
}