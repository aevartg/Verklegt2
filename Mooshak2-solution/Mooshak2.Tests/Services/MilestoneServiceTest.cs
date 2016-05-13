using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;
using Mooshak2.Services;

namespace Mooshak2.Tests.Services
{
	[TestClass]
	public class MilestoneServiceTest
	{
		private MilestoneService _milestoneService;

		[TestInitialize]
		public void Initialize()
		{
			var mockDb = new MockDataContext();
			var milestone1 = new Milestone()
			{
				Name = "Part A",
				Id = 1,
			};
			mockDb.Milestones.Add(milestone1);

			_milestoneService = new MilestoneService(mockDb);
		}
		[TestMethod]
		public void TestGetAllUsers()
		{
			//Arrange
			const int milestoneId = 1;
			//Act
			var result = _milestoneService.GetMilestoneByID(milestoneId);

			//Assert
			Assert.AreEqual("Part A", result.Name);
		}
	}
}
