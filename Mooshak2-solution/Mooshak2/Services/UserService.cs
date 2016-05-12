using Mooshak2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToDB.SqlQuery;

namespace Mooshak2.Services
{
	public class UserService
	{
		private ApplicationDbContext _db;

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
			else
			{
				for (int i = 0; i < allUsers.Count(); i++)
				{
					UserViewModel user = new UserViewModel();
					user.Id = allUsers[i].Id;
					user.username = allUsers[i].UserName;
					users.Add(user);
				};
				return users;
			}
		}

		public List<UserViewModel> GetAllTeachers()
		{
			var teachers = new List<UserViewModel>();
			var allUsers = GetAllUsers();
			IdentityManager connection = new IdentityManager();

			foreach (var item in allUsers)
			{
				if (connection.UserIsInRole(item.Id, "Teacher") == true)
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
			IdentityManager connection = new IdentityManager();

			foreach (var item in allUsers)
			{
				if (!connection.UserIsInRole(item.Id, "Teacher") && !connection.UserIsInRole(item.Id, "Administrator"))
				{
					students.Add(item);
				}
			}
				return students;
		}

		public ApplicationUser GetUserByID(string id)
		{
			return (from x in _db.Users where x.Id == id select x).SingleOrDefault();
		}

		public List<UserViewModel> GetTeachersInCourse(int id)
		{
			var list = (_db.Courses.Where(x => x.Id == id).SelectMany(x => x.Users)).ToList();
			if (list.Count == 0)
			{
				List<UserViewModel> emptyList = new List<UserViewModel>();
				return emptyList;
			}
			else
			{
				List<UserViewModel> teachers = new List<UserViewModel>();
				foreach (var item in list)
				{
					if (new IdentityManager().UserIsInRole(item.Id, "Teacher"))
					{
						UserViewModel temp = new UserViewModel();
						temp.Id = item.Id;
						temp.username = item.UserName;
						teachers.Add(temp);
					}
				}
				return teachers;
			}
			
		}
		public List<UserViewModel> GetStudentsInCourse(int id)
		{
			var list = (_db.Courses.Where(x => x.Id == id).SelectMany(x => x.Users)).ToList();
			if (list.Count == 0)
			{
				List<UserViewModel> emptyList = new List<UserViewModel>();
				return emptyList;
			}
			else
			{
				List<UserViewModel> students = new List<UserViewModel>();
				foreach (var item in list)
				{
					if (!new IdentityManager().UserIsInRole(item.Id, "Teacher"))
					{
						UserViewModel temp = new UserViewModel();
						temp.Id = item.Id;
						temp.username = item.UserName;
						students.Add(temp);
					}
				}
				return students;
			}
		}
	}
}