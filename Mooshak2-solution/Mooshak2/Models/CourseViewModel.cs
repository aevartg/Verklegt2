using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class CourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<AssignmentNavViewModel> Assignments { get; set; }
	}

	public class CourseTabViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<AssignmentTabViewModel> AssignmentList { get; set; }	
	}

	public class AdminNavCourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

}