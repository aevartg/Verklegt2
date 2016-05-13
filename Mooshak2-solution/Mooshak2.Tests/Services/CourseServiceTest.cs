using System;
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
			mockDb.Courses.Add(c1);
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


	}
}