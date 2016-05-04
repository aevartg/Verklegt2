using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Models;

namespace Mooshak2.fonts.Services
{
	public class UserService
	{

		public List<ApplicationUser> GetAvailableUsers()
		{

			List<ApplicationUser> users = new List<ApplicationUser>();
			users.Add(new ApplicationUser() { Value = "", Text = " - Choose a category - " });
			db.Categories.ToList().ForEach((x) =>
			{
				users.Add(new ApplicationUser() { Value = x.ID.ToString(), Text = x.name });
			});
			return users;
		}
	}
}