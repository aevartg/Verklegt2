using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class CourseViewModel
	{
		public int Id { get; set; }

		public String Name { get; set; }
	}

	public class ProjectCourseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<ProjectAssignmentViewModel> AssignmentList { get; set; }	
	}
}