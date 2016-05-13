using System;
using System.Collections.Generic;

namespace Mooshak2.Models
{
	public class CourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<AssignmentNavViewModel> Assignments { get; set; }
	}

	public class AdminNavCourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

	}
}