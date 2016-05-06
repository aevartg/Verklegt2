using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class AdminViewModels
	{
	}

	public class AdminCourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<UserViewModel> AllTeachers { get; set; }
		public List<UserViewModel> TeachersInCourse { get; set; }

	}
}