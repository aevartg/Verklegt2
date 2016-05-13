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
						};
			var assign2 = new Assignment()
			{
				Id = 2,
				Name = "Assignment 1",
			};
			mockDb.Assignments.Add(assign1);
			mockDb.Assignments.Add(assign2);
			_assignmentService = new AssignmentService(mockDb);
		}

		[TestMethod]
		public void TestGetAllAssignments()
		{
			//Arrange
			var nameList = new List<String>();
			nameList.Add("Lab 1");
			nameList.Add("Assignment 1");
			//Act
			var result = _assignmentService.GetAllAssignments();

			//Assert
			for (int i = 0; i < nameList.Count; i++)
			{
				Assert.AreEqual(nameList[i], result[i].Name);
			}
		}

		[TestMethod]
		public void TestGetAssignmentById()
		{
			//Arrange
			const int assignId = 1;
			//Act
			var result = _assignmentService.GetAssignmentById(assignId);

			//Assert
			Assert.AreEqual("Lab 1", result.Name);
		}
	}
}
