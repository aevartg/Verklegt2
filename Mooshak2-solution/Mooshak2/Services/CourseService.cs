using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
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

		public List<CourseViewModel> GetCourseViewModels()
		{
			var userid = HttpContext.Current.User.Identity.GetUserId();
			var courses = _db.Users
				.Where(x => x.Id == userid)
				.SelectMany(x => x.Courses)
				.ToList();
			if (courses.Count == 0)
			{
				var exception = new EmptyModelException
				{
					Message = "Course Tab View Model Is Empty"
				};
				throw exception;
			}
			else
			{
				var courseViewModel = new List<CourseViewModel>();
				foreach (var course in courses)
				{
					var assignmentList = new AssignmentService().GetAssignmentNavViewModels(course.Id);
					if (assignmentList.Count == 0)
					{
						var exception = new EmptyModelException
						{
							Message = "Assignment Tab View Model Is Empty"
						};
						throw exception;
					}
					var courseViewModeltemp = new CourseViewModel()
					{
						Id = course.Id,
						Name = course.Name,
						Assignments = assignmentList
					};
					courseViewModel.Add(courseViewModeltemp);
				}
				return courseViewModel;
			}
		}
	}
}