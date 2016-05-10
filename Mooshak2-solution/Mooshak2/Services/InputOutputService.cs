using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class InputOutputService
	{
		private ApplicationDbContext _db;
		public InputOutputService()
		{
			_db = new ApplicationDbContext();
		}

		public bool CreateInputOutput(string input, string output, int milestoneId)
		{
			var milestone = (from x in _db.Milestones where x.Id == milestoneId select x).Single();
			var temp = new InputOutput()
						{
							Input = input,
							Output = output,
							Milestone = milestone,
							MilestoneId = milestone.Id
						};
			_db.InputOutputs.Add(temp);
			return (_db.SaveChanges() > 0);
		}
	}
}