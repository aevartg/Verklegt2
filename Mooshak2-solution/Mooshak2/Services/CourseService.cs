using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

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
				List<CourseViewModel> emptyList = new List<CourseViewModel>();
				return emptyList;
			}
			else
			{
				var courseViewModel = new List<CourseViewModel>();
				foreach (var course in courses)
				{
					var assignmentList = new List<AssignmentNavViewModel>();
						assignmentList = new AssignmentService().GetAssignmentNavViewModels(course.Id);

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

		public Course GetCourseById(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.Id == id);
			if (course == null)
			{
				//TODO
				return null;
			}
			else
			{
				return course;
			}

		}

		public AdminCourseViewModel GetAdminCourseViewModel(int id) // er þetta notað?
		{
			var model = new AdminCourseViewModel();
			UserService c = new UserService();
			model.Name = GetCourseById(id).Name;
			model.AllTeachers = c.GetAllTeachers();
			model.Id = id;

			if (model.Name == null && model.AllTeachers.Count == 0)  //hvernig á að villumeðhöndla her?
			{
				//TODO
				return null;
			}
			else
			{
				return model;
			}
		}

		public List<Course> GetAllCourses()
		{
			var allCourses = (from x in _db.Courses select x).ToList();

			if (allCourses.Count == 0)
			{
				List<Course> emptyList = new List<Course>();
				return emptyList;
			}
			else
			{
				return allCourses;
			}
		}

		public void CreateCourse(CreateCourseViewModel model)
		{
			ICollection<ApplicationUser> tempList = new List<ApplicationUser>();
			var course = new Course()
			{
				Name = model.Name
			};
			course.Users = tempList;
			foreach (var x in model.SelectedTeachers)
			{
				var tempTeacher = (from u in _db.Users where x == u.Id select u).FirstOrDefault();

				course.Users.Add(tempTeacher);
			}
			foreach (var s in model.SelectedStudents)
			{
				var tempStudent = (from u in _db.Users where s == u.Id select u).First();
				course.Users.Add(tempStudent);
			}
			_db.Courses.Add(course);
			_db.SaveChanges();
		}

		public void UpdateAdminCourseViewModel(AdminCourseViewModel model, string userName)
		{
			IdentityManager connection = new IdentityManager();
			ApplicationUser user = connection.GetUser(userName);
			UserViewModel userVM = new UserViewModel();
			userVM.username = user.UserName;
			userVM.Id = user.Id;
			model.TeachersInCourse.Add(userVM);
		}

		public List<AdminNavCourseViewModel> GetAdminNavCourseViewModels()
		{
			List<AdminNavCourseViewModel> list = new List<AdminNavCourseViewModel>();
			var courses = GetAllCourses();
			foreach (var item in courses)
			{
				AdminNavCourseViewModel temp = new AdminNavCourseViewModel();
				temp.Id = item.Id;
				temp.Name = item.Name;
				list.Add(temp);
			}
			return list;
		}

		public EdtiCourseViewModel GetEdtiCourseViewModel(Course course)
		{
			EdtiCourseViewModel model = new EdtiCourseViewModel();
			model.Id = course.Id;
			model.Name = course.Name;
			model.Teachers = new SelectList(new UserService().GetAllTeachers(), "Id", "username");
			model.Students = new SelectList(new UserService().GetAllStudents(), "Id", "username");
			var t = new UserService().GetTeachersInCourse(course.Id);
			for (int i = 0; i < t.Count; i++)
			{
				model.TeachersInCourse[i] = t[i].username;
			}
			var s = new UserService().GetStudentsInCourse(course.Id);
			for (int i = 0; i < t.Count; i++)
			{
				model.StudentsInCourse[i] = s[i].username;
			}
			return model;
		}

	}
}