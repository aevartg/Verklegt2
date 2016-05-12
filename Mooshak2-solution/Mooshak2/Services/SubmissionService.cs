using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class SubmissionService
	{
		private readonly ApplicationDbContext _db;

		public SubmissionService()
		{
			_db = new ApplicationDbContext();
		}

		public Submission GetSubmissionById(int id)
		{
			return (from x in _db.Submissions where x.Id == id select x).Single();
		}

		public List<Submission> GetSubmissionsByMilestoneId(int id)
		{
			return (from x in _db.Submissions where x.MilestoneId == id select x).ToList();
		}

		public List<Submission> GetSubmissionsByUserAndMilestoneId(string userId, int milestoneId)
		{
			return (from x in _db.Submissions where x.UserId == userId && x.MilestoneId == milestoneId select x).ToList();
		}

		public bool CreateSubmission(HttpPostedFileBase file, int milestoneId, string fileExtension)
		{
			if (file != null)
			{
				var blob = Helper.StreamToBytes(file.InputStream);
				var userId = HttpContext.Current.User.Identity.GetUserId();
				var date = DateTime.Now;
				if (Helper.BytesToFile(fileExtension, blob))
				{
					var temp = new InputOutputService().GetJavascriptResultTuples(milestoneId);
					var submission = new Submission
									{
										UserId = userId,
										MilestoneId = milestoneId,
										Blob = blob,
										SubmitDate = date,
										TestPassed = temp.Item1,
										TestFailed = temp.Item2,
										FileExtension = fileExtension
									};
					_db.Submissions.Add(submission);
					_db.SaveChanges();

					submission = (from x in _db.Submissions where x.SubmitDate == date select x).First();
					foreach (var x in temp.Item3)
					{
						var userOutput = new UserOutput
										{
											UserId = userId,
											SubmissionId = submission.Id,
											Output = x
										};
						_db.UserOutputs.Add(userOutput);
					}
					_db.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public List<SubmissionViewModel> GetSubmissionViewModelsByMilestone(int Id)
		{
			List<SubmissionViewModel> list = new List<SubmissionViewModel>();
			List<Submission> subs = GetSubmissionsByMilestoneId(Id);
			list = MakeSubViewList(subs);
			return list;
		}

		public List<SubmissionViewModel> MakeSubViewList(List<Submission> subs)
		{
			List<SubmissionViewModel> list = new List<SubmissionViewModel>();
			foreach (var item in subs)
			{
				SubmissionViewModel temp = new SubmissionViewModel();
				temp.Id = item.Id;
				temp.SubmitDate = item.SubmitDate;
				temp.TestFailed = item.TestFailed;
				temp.TestPassed = item.TestPassed;
				temp.UserName = item.ApplicationUser.UserName;
				var totalTest = temp.TestFailed + temp.TestPassed;
				var tempGrade = (double)temp.TestPassed / totalTest;
				temp.Grade = tempGrade * 10;
				list.Add(temp);
			}
			return list;
		}

	}
}