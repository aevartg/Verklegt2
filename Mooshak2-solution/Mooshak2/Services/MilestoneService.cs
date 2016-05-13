using System.Collections.Generic;
using System.Linq;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class MilestoneService
	{
		private readonly ApplicationDbContext _db;

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
				return new List<MilestoneNavViewModel>();
			}
			var mileStoneNavViewModels = new List<MilestoneNavViewModel>();
			foreach (var item in milestones)
			{
				var temp = new MilestoneNavViewModel
							{
								Id = item.Id,
								Name = item.Name
							};
				mileStoneNavViewModels.Add(temp);
			}
			return mileStoneNavViewModels;
		}

		public bool CreateMilestone(int assignmentId, string name, int weight)
		{
			var assignment = (from x in _db.Assignments where x.Id == assignmentId select x).Single();
			var temp = new Milestone
						{
							Assignment = assignment,
							AssignmentId = assignment.Id,
							Weight = weight,
							Name = name
						};
			_db.Milestones.Add(temp);
			return _db.SaveChanges() > 0;
		}

		public Milestone GetMilestoneByID(int id)
		{
			var model = (from x in _db.Milestones where x.Id == id select x).SingleOrDefault();

			if (model == null)
			{
				var m = new Milestone();
				return m;
			}
			return model;
		}

		public Milestone GetMilestoneByName(int assignmentId, string name)
		{
			return (from x
				in _db.Milestones
					where (x.AssignmentId == assignmentId) && (x.Name == name)
					select x).SingleOrDefault();
		}

		public List<CreateMilestoneViewModel> GetCreateMilestoneViewModels(int Id)
		{
			var list = (from items in _db.Milestones
					   where items.AssignmentId == Id
					   select items).ToList();
			if (list.Count == 0)
			{
				List<CreateMilestoneViewModel> emptyList = new List<CreateMilestoneViewModel>();
				return emptyList;
			}
			else
			{
				List<CreateMilestoneViewModel> TheList = new List<CreateMilestoneViewModel>();
				foreach (var item in list)
				{
					CreateMilestoneViewModel temp = new CreateMilestoneViewModel();
					temp.Name = item.Name;
					temp.Weight = item.Weight;
					temp.Id = item.Id;
					TheList.Add(temp);
				}
				return TheList;

			}
		}
	}
}