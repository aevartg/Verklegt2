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
				if (connection.UserIsInRole(item.Id, "Teacher") == false)
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
			//var teachers = (from user in _db.Users outer join)
			List<UserViewModel> list = new List<UserViewModel>();
			return list;
		}
		public List<UserViewModel> GetStudentsInCourse(int id)
		{
			List<UserViewModel> list = new List<UserViewModel>();
			//var teachers = (from user in _db.Users outer join)
			return list;
		}
	}
}