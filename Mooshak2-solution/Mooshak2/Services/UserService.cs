using System.Collections.Generic;
using System.Linq;
using Mooshak2.Models;

namespace Mooshak2.Services
{
	public class UserService
	{
		private readonly ApplicationDbContext _db;

		public UserService()
		{
			_db = new ApplicationDbContext();
		}


		public List<UserViewModel> GetAllUsers()
		{
			var users = new List<UserViewModel>();
			var allUsers = (from x in _db.Users select x).ToList();

			if (allUsers.Count == 0)
			{
				return users;
			}
			for (var i = 0; i < allUsers.Count(); i++)
			{
				var user = new UserViewModel();
				user.Id = allUsers[i].Id;
				user.username = allUsers[i].UserName;
				users.Add(user);
			}
			return users;
		}

		public List<UserViewModel> GetAllTeachers()
		{
			var teachers = new List<UserViewModel>();
			var allUsers = GetAllUsers();
			var connection = new IdentityManager();

			foreach (var item in allUsers)
			{
				if (connection.UserIsInRole(item.Id, "Teacher"))
				{
					teachers.Add(item);
				}
			}
			return teachers;
		}

		public List<UserViewModel> GetAllStudents()
		{
			var students = new List<UserViewModel>();
			var allUsers = GetAllUsers();
			var connection = new IdentityManager();

			foreach (var item in allUsers)
			{
				if (connection.UserIsInRole(item.Id, "Teacher") == false)
				{
					students.Add(item);
				}
			}
			return students;
		}

		public ApplicationUser GetUserById(string id)
		{
			return (from x in _db.Users where x.Id == id select x).SingleOrDefault();
		}

		public List<UserViewModel> GetTeachersInCourse(int id)
		{
			var list = _db.Courses.Where(x => x.Id == id).SelectMany(x => x.Users).ToList();
			if (list.Count == 0)
			{
				var emptyList = new List<UserViewModel>();
				return emptyList;
			}
			var teachers = new List<UserViewModel>();
			foreach (var item in list)
			{
				if (new IdentityManager().UserIsInRole(item.Id, "Teacher"))
				{
					var temp = new UserViewModel();
					temp.Id = item.Id;
					temp.username = item.UserName;
					teachers.Add(temp);
				}
			}
			return teachers;
		}

		public List<UserViewModel> GetStudentsInCourse(int id)
		{
			var list = _db.Courses.Where(x => x.Id == id).SelectMany(x => x.Users).ToList();
			if (list.Count == 0)
			{
				var emptyList = new List<UserViewModel>();
				return emptyList;
			}
			var students = new List<UserViewModel>();
			foreach (var item in list)
			{
				if (!new IdentityManager().UserIsInRole(item.Id, "Teacher"))
				{
					var temp = new UserViewModel
								{
									Id = item.Id,
									username = item.UserName
								};
					students.Add(temp);
				}
			}
			return students;
		}
	}
}