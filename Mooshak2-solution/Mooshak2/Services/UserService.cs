using Mooshak2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

			for (int i = 0; i < allUsers.Count(); i++)
			{
				UserViewModel user = new UserViewModel();
				user.Id = allUsers[i].Id;
				user.username = allUsers[i].UserName;
				users.Add(user);
			};

			if (users.Count == 0)
			{
				//TODO 
				return null;
			}
			else
			{
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

			if (teachers.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return teachers;
			}
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

			if (students.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return students;
			}

		}

		public ApplicationUser GetUserByID(string id)
		{
			return (from x in _db.Users where x.Id == id select x).SingleOrDefault();
		}
	}
}