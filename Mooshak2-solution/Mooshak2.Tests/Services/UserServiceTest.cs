using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mooshak2.Models;
using Mooshak2.Services;

namespace Mooshak2.Tests.Services
{
	[TestClass]
	public class UserServiceTest
	{

		private UserService _userServiceTest;

		[TestInitialize]
		public void Initialize()
		{
			var mockDb = new MockDataContext();
			var user = new ApplicationUser()
						{
							Email = "Admin@admin.com",
							UserName = "Admin@admin.com"
						};
			var user2 = new ApplicationUser()
			{
				Email = "TestTeacher@gmail.com",
				UserName = "TestTeacher@gmail.com"
			};
			mockDb.Users.Add(user);
			mockDb.Users.Add(user2);

			_userServiceTest = new UserService(mockDb);
		}
		[TestMethod]
		public void TestGetAllUsers()
		{
			//Arrange
			var nameList = new List<String>();
			nameList.Add("Admin@admin.com");
			nameList.Add("TestTeacher@gmail.com");
			//Act
			var result = _userServiceTest.GetAllUsers();

			//Assert
			for (int i = 0; i < result.Count; i++)
			{
				Assert.AreEqual(nameList[i], result[i].username);
			}
		}
	}
}
