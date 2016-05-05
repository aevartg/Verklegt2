using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Models;

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
	}
}