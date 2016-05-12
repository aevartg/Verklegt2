using System;
using System.Collections.Generic;
using System.Linq;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class AssignmentService
	{
		private readonly ApplicationDbContext _db;

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
				var emptyList = new List<AssignmentNavViewModel>();
				return emptyList;
			}
			var assignmentNavViewModels = new List<AssignmentNavViewModel>();
			var service = new MilestoneService();
			foreach (var item in assignments)
			{
				var temp = new AssignmentNavViewModel
							{
								Id = item.Id,
								Milestones = service.GetMilestoneNavById(item.Id),
								Name = item.Name
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
			var m = new MilestoneService();
			var s = new SubmissionService();
			var milestone = m.GetMilestoneByID(milestoneId);
			model.Id = milestoneId;
			model.Name = milestone.Name;
			model.Submissions = s.GetSubmissionsByUserAndMilestoneId(userId, milestoneId);
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
				model.Milestones = new MilestoneService().GetCreateMilestoneViewModels(Id);
				model.NavModel = new CourseService().GetCourseViewModels();
				return model;
			}
		}

		public void EditAssignment(int Id)
		{
			
		}
	}
}