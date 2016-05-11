using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
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

		public bool CreateInputOutput(int milestoneId , HttpPostedFileBase file)
		{
			var inputOutputs = Helper.PareInputOutput(file.InputStream);
			foreach (var x in inputOutputs)
			{
				var temp = new InputOutput()
							{
								Input = x[0],
								Output = x[1],
								MilestoneId = milestoneId
							};
				_db.InputOutputs.Add(temp);
			}
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
		public List<InputOutputViewModel> GetInputsOutputsViewModel(int milestoneId)
		{
			var model = new List<InputOutputViewModel>();
			var allExpInputs = GetExpectedInputOutputsByMilestoneId(milestoneId);
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

		public Tuple<int, int> GetJavascriptResultTuples(int milestoneId)
		{
			var fails = 0;
			var pass = 0;
			var inputoutputs = GetExpectedInputOutputsByMilestoneId(milestoneId);
			foreach (var item in inputoutputs)
			{
				var tempUserOutput = Helper.RunJavaScriptCode( item.Input?.Replace("\\n","\n") , 600);
				if (tempUserOutput.Equals(item.Output.Replace("\\n","\n"), StringComparison.OrdinalIgnoreCase))
				{
					pass++;
				}
				else
				{
					fails++;
				}
				var temp = new UserOutput()
							{
								UserId = HttpContext.Current.User.Identity.GetUserId(),
								InputId = item.Id,
								Output = tempUserOutput
							};
				_db.UserOutputs.Add(temp);
			}
			_db.SaveChanges();
			return Tuple.Create(pass, fails);
		}
	}
}