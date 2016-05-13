using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;
using Mooshak2.Services;

namespace Mooshak2.Tests.Services
{
	[TestClass]
	public class AssignmentServiceTest
	{
		private AssignmentService _assignmentService;

		[TestInitialize]
		public void Initialize()
		{
			var mockDb = new MockDataContext();
			var assign1 = new Assignment()
						{
							Id = 1,
							Name = "Lab 1",
							DateOpen =
						}
			_assignmentService = new AssignmentService(mockDb);
		}

		[TestMethod]
		public void TestGetAllUsers()
		{
			//Arrange
			var nameList = new List<String>();
			nameList.Add("Admin@admin.com");
			nameList.Add("TestTeacher@gmail.com");
			//Act
			var result = _assignmentService.GetAllAssignments();

			//Assert
			for (int i = 0; i < result.Count; i++)
			{
				Assert.AreEqual(nameList[i], result[i].username);
			}
		}
	}
	}
}
