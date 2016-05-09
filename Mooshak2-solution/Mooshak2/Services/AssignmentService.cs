﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class AssignmentService
	{
		private ApplicationDbContext _db;

		public AssignmentService()
		{
			_db = new ApplicationDbContext();
		}

		public List<AssignmentTabViewModel> GetAssignmentTabViewModels(int id)
		{
			var test = (from items in _db.Assignments
						where items.CourseId == id
						select items).ToList();
			if (test.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				var assignmentViewModels = new List<AssignmentTabViewModel>();
				foreach (var item in test)
				{
					var temp = new AssignmentTabViewModel()
					{
						Id = item.Id,
						Name = item.Name
					};
					assignmentViewModels.Add(temp);
				}
				return assignmentViewModels;
			}
		}

		public List<AssignmentNavViewModel> GetAssignmentNavViewModels(int id)
		{
			var assignments = (from items in _db.Assignments
						where items.CourseId == id
						select items).ToList();
			if (assignments.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				var assignmentNavViewModels = new List<AssignmentNavViewModel>();
				var service = new MilestoneService();
				foreach (var item in assignments)
				{
					var temp = new AssignmentNavViewModel()
								{
									Id = item.Id,
									Milestones = service.GetMilestoneNavById(item.Id),
									Name = item.Name
								};
					assignmentNavViewModels.Add(temp);
				}
				return assignmentNavViewModels;
			}
		}

		public bool CreateAssignment(string name)
		{
			return (_db.SaveChanges() > 0) ? true : false;
		}
	}
}