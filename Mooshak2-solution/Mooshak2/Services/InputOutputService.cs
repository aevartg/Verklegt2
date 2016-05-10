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

		public List<InputOutput> GetExpectedInputOutputsByMilestoneId(int id)
		{
			var exp = (from x in _db.InputOutputs where x.MilestoneId == id select x).ToList();
			if (exp.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return exp;
			}
		}
		//input output service
		public List<InputOutputViewModel> getInputsOutputsViewModel(int milestoneID)
		{
			var model = new List<InputOutputViewModel>();
			var allExpInputs = GetExpectedInputOutputsByMilestoneId(milestoneID);
			foreach (var item in allExpInputs)
			{
				var x = new InputOutputViewModel();
				x.Input = item.Input;
				x.ExpectedOutput = item.Output;
				x.RealOutput = item.Output; //þessi lína myndi ekki ná í expected output heldur raunverulegt output notandans
				model.Add(x);
			}
			if (model.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return model;
			}
		}
	}
}