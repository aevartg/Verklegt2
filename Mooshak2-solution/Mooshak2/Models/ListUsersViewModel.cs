using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class ListUsersViewModel
	{
		public List<UserViewModel> AllTeachers { get; set; }
		public List<UserViewModel> AllUsers { get; set; }
		public List<UserViewModel> AllStudents { get; set; }
	}
}