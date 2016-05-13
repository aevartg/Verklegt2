using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mooshak2.Models.EntityClasses;
using Mooshak2.Services;

namespace Mooshak2.Tests
{
	[TestClass]
	public class CourseServiceTest
	{
		private CourseService _courseServiceTest;

		[TestInitialize]
		public void Initialize()
		{
			var mockDb = new MockDataContext();
			var c1 = new Course()
					{
						Id = 1,
						Name = "Gagnaskipan"
					};
			var c2 = new Course()
					{
						Id = 2,
						Name = "Reiknirit"
					};
			var c3 = new Course()
					{
						Id = 3,
						Name = "Forritun1"
					};
			mockDb.Courses.Add(c1);
			mockDb.Courses.Add(c2);
			mockDb.Courses.Add(c3);
			_courseServiceTest = new CourseService(mockDb);
		}

		[TestMethod]
		public void TestGetCourseById1()
		{
			//Arrange
			const int courseId = 1;

			//Act
			var result = _courseServiceTest.GetCourseById(courseId);

			//Assert
			Assert.AreEqual("Gagnaskipan", result.Name);
		}

		[TestMethod]
		public void TestGetAllCourses()
		{
			//Arrange
			var nameList = new List<String>();
			nameList.Add("Gagnaskipan");
			nameList.Add("Reiknirit");
			nameList.Add("Forritun1");
			//Act
			var result = _courseServiceTest.GetAllCourses();

			//Assert
			for(int i = 0; i < result.Count; i++)
			{ 
				Assert.AreEqual(nameList[i], result[i].Name);
			}
		}

		[TestMethod]
		public void TestDeleteCourse()
		{
			//Arrange
			var nameList = new List<String>();
			nameList.Add("Gagnaskipan");
			nameList.Add("Reiknirit");

			//Act
			_courseServiceTest.DeleteCourse(3);
			var result = _courseServiceTest.GetAllCourses();

			//Assert
			for (int i = 0; i < result.Count; i++)
			{
				Assert.AreEqual(nameList[i], result[i].Name);
			}
		}

	}
}