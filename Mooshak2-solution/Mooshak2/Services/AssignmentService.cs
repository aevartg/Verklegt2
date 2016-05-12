﻿using System;
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

		public List<AssignmentNavViewModel> GetAssignmentNavViewModels(int id)
		{
			var assignments = (from items in _db.Assignments
						where items.CourseId == id
						select items).ToList();
			if (assignments.Count == 0)
			{
				List<AssignmentNavViewModel> emptyList = new List<AssignmentNavViewModel>();
				return emptyList;
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

		public bool CreateAssignment(string name, int courseId, DateTime dateOpen, DateTime dateClose)
		{
			var course = (from x in _db.Courses where x.Id == courseId select x).Single();
			var temp = new Assignment()
						{
							Course = course,
							CourseId = course.Id,
							Name = name,
							DateOpen = dateOpen,
							DateClose = dateClose
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

		public Assignment GetAssignmentById(int id)
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
		public AssignmentViewModel GetAssignmentViewModel(int submissionId,string userID, int milestoneID)
		{
			var model = new AssignmentViewModel();
			MilestoneService m = new MilestoneService();
			SubmissionService s = new SubmissionService();
			InputOutputService i = new InputOutputService();
			var milestone = m.GetMilestoneByID(milestoneID);
			model.Id = milestoneID;
			model.Name = milestone.Name;
			model.Submissions = s.GetSubmissionsByUserAndMilestoneID(userID, milestoneID);
			model.InputOutputs = i.GetInputsOutputsViewModel(submissionId,milestoneID,userID);

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

		public AssignmentViewModel GetAssignmentViewModel(string userID, int milestoneID)
		{
			var model = new AssignmentViewModel();
			MilestoneService m = new MilestoneService();
			SubmissionService s = new SubmissionService();
			var milestone = m.GetMilestoneByID(milestoneID);
			model.Id = milestoneID;
			model.Name = milestone.Name;
			model.Submissions = s.GetSubmissionsByUserAndMilestoneID(userID, milestoneID);
			model.InputOutputs = new List<InputOutputViewModel>();

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

		public Assignment GetAssignmentByName(int courseId, string name)
		{
			return (from x 
					in _db.Assignments
					where ((courseId == x.CourseId) && (name == x.Name))
					select x).SingleOrDefault();
		}
	}
}