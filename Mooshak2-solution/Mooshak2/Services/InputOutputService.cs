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
		private readonly IAppDataContext _db;

		public InputOutputService(IAppDataContext context)
		{
			_db = context ?? new ApplicationDbContext();
		}

		public bool CreateInputOutput(int milestoneId, HttpPostedFileBase file)
		{
			var inputOutputs = Helper.PareInputOutput(file.InputStream);
			foreach (var x in inputOutputs)
			{
				var temp = new InputOutput
							{
								Input = x[0],
								Output = x[1],
								MilestoneId = milestoneId
							};
				_db.InputOutputs.Add(temp);
			}
			return _db.SaveChanges() > 0;
		}

		public List<InputOutput> GetExpectedInputOutputsByMilestoneId(int id)
		{
			var exp = (from x in _db.InputOutputs where x.MilestoneId == id select x).ToList();
			return exp;
		}

		//input output service
		public List<InputOutputViewModel> GetInputsOutputsViewModel(int submissionId, int milestoneId, string userId)
		{
			var model = new List<InputOutputViewModel>();
			var allExpInputs = GetExpectedInputOutputsByMilestoneId(milestoneId);
			var allRealOutputs = GetUserOutputsById(submissionId, userId);
			if (allExpInputs.Count == 0 || allRealOutputs.Count == 0)
			{
				return model;
			}
			var temp = allExpInputs.Zip(allRealOutputs,
				(x, y) => new {expectedInput = x.Input, expextedOutput = x.Output, realOutput = y});
			foreach (var item in temp)
			{
				var x = new InputOutputViewModel
						{
							Input = item.expectedInput,
							ExpectedOutput = item.expextedOutput,
							RealOutput = item.realOutput
						};
				model.Add(x);
			}
			return model;
		}

		private List<string> GetUserOutputsById(int submissionId, string userId)
		{
			return
				(from x in _db.UserOutputs where x.UserId == userId && x.SubmissionId == submissionId select x.Output).ToList();
		}

		public Tuple<int, int, List<string>> GetJavascriptResultTuples(int milestoneId)
		{
			var fails = 0;
			var pass = 0;
			var stringList = new List<string>();
			var inputoutputs = GetExpectedInputOutputsByMilestoneId(milestoneId);
			foreach (var item in inputoutputs)
			{
				var tempUserOutput = Helper.RunJavaScriptCode(item.Input?.Replace("\\n", "\n"), 600);
				if (tempUserOutput.Equals(item.Output.Replace("\\n", "\n"), StringComparison.OrdinalIgnoreCase))
				{
					pass++;
				}
				else
				{
					fails++;
				}
				stringList.Add(tempUserOutput);
			}
			_db.SaveChanges();
			return Tuple.Create(pass, fails, stringList);
		}
	}
}