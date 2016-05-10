using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class AssignmentService
	{
		private ApplicationDbContext _db;

		public AssignmentService()
		{
			_db = new ApplicationDbContext();
		}

		public List<AssignmentTabViewModel> GetAssignmentTabViewModels(int id)
		{
			var test = (from items in _db.Assignments
						where items.CourseId == id
						select items).ToList();
			if (test.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				var assignmentViewModels = new List<AssignmentTabViewModel>();
				foreach (var item in test)
				{
					var temp = new AssignmentTabViewModel()
					{
						Id = item.Id,
						Name = item.Name
					};
					assignmentViewModels.Add(temp);
				}
				return assignmentViewModels;
			}
		}

		public List<AssignmentNavViewModel> GetAssignmentNavViewModels(int id)
		{
			var assignments = (from items in _db.Assignments
						where items.CourseId == id
						select items).ToList();
			if (assignments.Count == 0)
			{
				var exception = new EmptyModelException
				{
					Message = "Assignment Nav View Model Is Empty"
				};
				throw exception;
			}
			else
			{
				var assignmentNavViewModels = new List<AssignmentNavViewModel>();
				var service = new MilestoneService();
				foreach (var item in assignments)
				{
					var temp = new AssignmentNavViewModel()
								{
									Id = item.Id,
									Milestones = service.GetMilestoneNavById(item.Id),
									Name = item.Name
								};
					assignmentNavViewModels.Add(temp);
				}
				return assignmentNavViewModels;
			}
		}

		public bool CreateAssignment(string name, int courseId)
		{
			var course = (from x in _db.Courses where x.Id == courseId select x).Single();
			var temp = new Assignment()
						{
							Course = course,
							CourseId = course.Id,
							Name = name
						};
			_db.Assignments.Add(temp);
			return (_db.SaveChanges() > 0) ? true : false;
		}

		public List<Assignment> GetAllAssignments()
		{
			var allAssigns = (from x in _db.Assignments select x).ToList();



			if (allAssigns.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return allAssigns;
			}
		}

		public Assignment GetAssignmentByID(int id)
		{
			var assign = _db.Assignments.SingleOrDefault(x => x.Id == id);

			if (assign == null)
			{
				//TODO
				return null;
			}
			else
			{
				return assign;
			}
		}
		public AssignmentViewModel GetAssignmentViewModel(string userID, int milestoneID)
		{
			var model = new AssignmentViewModel();
			MilestoneService m = new MilestoneService();
			SubmissionService s = new SubmissionService();
			InputOutputService i = new InputOutputService();
			var milestone = m.getMilestoneByID(milestoneID);
			model.Id = GetAssignmentByID(milestone.AssignmentId).Id;
			model.Name = GetAssignmentByID(milestone.Id).Name;
			model.Submissions = s.GetSubmissionsByUserAndMilestoneID(userID, milestoneID);
			model.InputOutputs = i.getInputsOutputsViewModel(milestoneID);

			if (model == null)
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