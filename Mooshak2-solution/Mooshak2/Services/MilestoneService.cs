using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class MilestoneService
	{
		private ApplicationDbContext _db;

		public MilestoneService()
		{
			_db = new ApplicationDbContext();
		}

		public List<MilestoneNavViewModel> GetMilestoneNavById(int id)
		{
			var milestones = (from items in _db.Milestones
						where items.AssignmentId == id
						select items).ToList();
			if (milestones.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				var mileStoneNavViewModels = new List<MilestoneNavViewModel>();
				foreach (var item in milestones)
				{
					var temp = new MilestoneNavViewModel()
					{
						Id = item.Id,
						Name = item.Name
					};
					mileStoneNavViewModels.Add(temp);
				}
				return mileStoneNavViewModels;
			}
		}

		public bool CreateMilestone(int assignmentId, string name, int weight)
		{
			var assignment = (from x in _db.Assignments where x.Id == assignmentId select x).Single();
			var temp = new Milestone()
						{
							Assignment = assignment,
							AssignmentId = assignment.Id,
							Description = weight.ToString(),
							Name = name
						};
			_db.Milestones.Add(temp);
			return (_db.SaveChanges() > 0);
		}
	}
}