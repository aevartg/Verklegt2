using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
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

		public CourseTabViewModel GetCourseTabViewModel(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.Id == id);
			if (course == null)
			{
				//TODO
				return null;
			}
			else
			{
				var assignmentList = new AssignmentService().GetAssignmentTabViewModels(course.Id);
				var courseViewModel = new CourseTabViewModel()
									{
										Id = course.Id,
										Name = course.Name,
										AssignmentList = assignmentList
									};
				return courseViewModel;
			}
		}

		public List<CourseTabViewModel> GetCourseTabViewModels()
		{
			var userid = HttpContext.Current.User.Identity.GetUserId();
			var courses = _db.Users
				.Where(x => x.Id == userid)
				.SelectMany(x => x.Courses)
				.ToList();
			if (courses.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				var courseViewModel = new List<CourseTabViewModel>();
				foreach (var course in courses)
				{
					var assignmentList = new AssignmentService().GetAssignmentTabViewModels(course.Id);
					var courseViewModeltemp = new CourseTabViewModel()
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