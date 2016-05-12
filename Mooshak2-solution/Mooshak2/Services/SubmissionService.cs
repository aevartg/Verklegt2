using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Mooshak2.Services
{
	public class SubmissionService
	{
		private ApplicationDbContext _db;

		public SubmissionService()
		{
			_db = new ApplicationDbContext();
		}

		public Submission GetSubmissionById(int id)
		{
			return (from x in _db.Submissions where x.Id == id select x).Single();
		}

		public List<Submission> GetSubmissionsByMilestoneID(int id)
		{
			var subs = (from x in _db.Submissions where x.MilestoneId == id select x).ToList();

			if (subs.Count == 0)
			{
				List<Submission> emptyList = new List<Submission>();
				return emptyList;
			}
			else
			{
				return subs;
			}
		}

		public List<Submission> GetSubmissionsByUserAndMilestoneID(string userID, int milestoneID)
		{
			var subs = (from x in _db.Submissions where (x.UserId == userID && x.MilestoneId == milestoneID) select x).ToList();

			if (subs.Count == 0)
			{
				List<Submission> emptyList = new List<Submission>();
				return emptyList;
			}
			else
			{
				return subs;
			}
		}

		public bool CreateSubmission(HttpPostedFileBase file, int milestoneId,string fileExtension)
		{
			if(file.ContentLength > 0)
			{ 
				var blob = Helper.StreamToBytes(file.InputStream);
				var userId = HttpContext.Current.User.Identity.GetUserId();
				var date = DateTime.Now;
				if (Helper.BytesToFile(fileExtension, blob))
				{
					var temp = new InputOutputService().GetJavascriptResultTuples(milestoneId);
					var submission = new Submission()
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

					submission = (from x in _db.Submissions where (x.SubmitDate == date) select x).First();
					foreach (var x in temp.Item3)
					{
						var userOutput = new UserOutput()
										{
											UserId = userId,
											SubmissionId = submission.Id,
											Output = x
										};
						_db.UserOutputs.Add(userOutput);
					}
					_db.SaveChanges();
				}
			}
			return false;
		}
	}
}