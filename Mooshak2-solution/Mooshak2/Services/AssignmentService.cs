using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
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

		public List<ProjectAssignmentViewModel> GetProjectAssignmentViewModels(int id)
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
				var assignmentViewModels = new List<ProjectAssignmentViewModel>();
				foreach (var item in test)
				{
					var temp = new ProjectAssignmentViewModel()
					{
						Id = item.Id,
						Name = item.Name
					};
					assignmentViewModels.Add(temp);
				}
				return assignmentViewModels;
			}
		}
	}
}