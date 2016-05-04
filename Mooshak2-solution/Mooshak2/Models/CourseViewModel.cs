using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class CourseViewModel
	{
		public String Name { get; set; }
		public int Id { get; set; }
		public List<String> TestList { get; set; } = new List<string>();

		public  List<ApplicationUser> AvailableUsers { get; set; } = new List<ApplicationUser>();
	}
}