using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Models;

namespace Mooshak2.Services
{
	public class CourseService
	{
		private ApplicationDbContext _db;

		public CourseService()
		{
			_db = new ApplicationDbContext();
		}

		public ProjectCourseViewModel GetProjectCourseViewModel(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.Id == id);
			if (course == null)
			{
				//TODO
				return null;
			}
			else
			{
				var assignmentList = new AssignmentService().GetProjectAssignmentViewModels(course.Id);
				var courseViewModel = new ProjectCourseViewModel()
									{
										Id = course.Id,
										Name = course.Name,
										AssignmentList = assignmentList
									};
				return courseViewModel;
			}
		}

		public List<ProjectCourseViewModel> GetAllProjectCourseViewModel()
		{
			var courses = (from items in _db.Courses select items).ToList();
			if (courses.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				var courseViewModel = new List<ProjectCourseViewModel>();
				foreach (var course in courses)
				{
					var assignmentList = new AssignmentService().GetProjectAssignmentViewModels(course.Id);
					var courseViewModeltemp = new ProjectCourseViewModel()
					{
						Id = course.Id,
						Name = course.Name,
						AssignmentList = assignmentList
					};
					courseViewModel.Add(courseViewModeltemp);
				}
				return courseViewModel;
			}
		}
	}
}