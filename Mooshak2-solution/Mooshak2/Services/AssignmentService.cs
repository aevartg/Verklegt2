using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class AssignmentService
	{
		private readonly IAppDataContext _db;

		public AssignmentService(IAppDataContext context)
		{
			_db = context ?? new ApplicationDbContext();
		}

		public List<AssignmentNavViewModel> GetAssignmentNavViewModels(int id)
		{
			var assignments = (from items in _db.Assignments
								where items.CourseId == id
								select items).ToList();
			if (assignments.Count == 0)
			{
				var emptyList = new List<AssignmentNavViewModel>();
				return emptyList;
			}
			var assignmentNavViewModels = new List<AssignmentNavViewModel>();
			var service = new MilestoneService(null);
			foreach (var item in assignments)
			{
				var temp = new AssignmentNavViewModel
							{
								Id = item.Id,
								Milestones = service.GetMilestoneNavById(item.Id),
								Name = item.Name,
								CloseDate = item.DateClose,
								OpenDate = item.DateOpen
							};
				assignmentNavViewModels.Add(temp);
			}
			return assignmentNavViewModels;
		}

		public bool CreateAssignment(string name, int courseId, DateTime dateOpen, DateTime dateClose)
		{
			var course = (from x in _db.Courses where x.Id == courseId select x).Single();
			var temp = new Assignment
						{
							Course = course,
							CourseId = course.Id,
							Name = name,
							DateOpen = dateOpen,
							DateClose = dateClose
						};
			_db.Assignments.Add(temp);
			return _db.SaveChanges() > 0;
		}

		public List<Assignment> GetAllAssignments()
		{
			return (from x in _db.Assignments select x).ToList();
		}

		public Assignment GetAssignmentById(int id)
		{
			return _db.Assignments.SingleOrDefault(x => x.Id == id);
		}

		public AssignmentViewModel GetAssignmentViewModel(string userId, int milestoneId)
		{
			var model = new AssignmentViewModel();
			var m = new MilestoneService(null);
			var s = new SubmissionService(null);
			var milestone = m.GetMilestoneByID(milestoneId);
			model.Id = milestoneId;
			model.Name = milestone.Name;
			var subs = s.GetSubmissionsByUserAndMilestoneId(userId, milestoneId);			
			model.Submissions= s.MakeSubViewList(subs);
			return model;
		}

		public Assignment GetAssignmentByName(int courseId, string name)
		{
			return (from x
				in _db.Assignments
					where (courseId == x.CourseId) && (name == x.Name)
					select x).SingleOrDefault();
		}

		public EditAssignmentViewModel GetEditAssignmentViewModel(int Id)
		{
			EditAssignmentViewModel model = new EditAssignmentViewModel();
			var assign = GetAssignmentById(Id);
			if (assign == null)
			{
				//throw exeption
				return null;
			}
			else
			{
				model.Name = assign.Name;
				model.AssignId = Id;
				model.CourseId = assign.CourseId;
				model.DateClose = assign.DateClose;
				model.DateOpen = assign.DateOpen;
				model.Milestones = new MilestoneService(null).GetCreateMilestoneViewModels(Id);
				model.NavModel = new CourseService(null).GetCourseViewModels();
				return model;
			}
		}

		public void EditAssignment(EditAssignmentViewModel model)
		{
			Assignment assignment = GetAssignmentById(model.AssignId);
			assignment.Name = model.Name;
			assignment.DateClose = model.DateClose;
			assignment.DateOpen = model.DateOpen;

			var tempAssignment = GetAssignmentById(model.AssignId);
			var milestoneService = new MilestoneService(null);
			var inputOutputService = new InputOutputService(null);
			foreach (var item in model.Milestones)
			{
				Milestone tempM = milestoneService.GetMilestoneByID(item.Id);
				_db.Milestones.Attach(tempM);
				_db.Milestones.Remove(tempM);
			}
			_db.SaveChanges();
			foreach (var x in model.Milestones)
			{
				milestoneService.CreateMilestone(tempAssignment.Id, x.Name, x.Weight);
				var tempMilestone = milestoneService.GetMilestoneByName(tempAssignment.Id, x.Name);
				inputOutputService.CreateInputOutput(tempMilestone.Id, x.File);
			}
		}

		public void DeleteAssignment(int Id)
		{
			Assignment assignment = GetAssignmentById(Id);
			_db.Assignments.Remove(assignment);
			_db.SaveChanges();
		}
	}
}