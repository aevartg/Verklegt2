using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Services
{
	public class SubmissionService
	{
		private ApplicationDbContext _db;

		public SubmissionService()
		{
			_db = new ApplicationDbContext();
		}

		public List<Submission> GetSubmissionsByMilestoneID(int id)
		{
			var subs = (from x in _db.Submissions where x.MilestoneId == id select x).ToList();

			if (subs.Count == 0)
			{
				//TODO
				return null;
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
				//TODO
				return null;
			}
			else
			{

				return subs;
			}
		}
	}
}