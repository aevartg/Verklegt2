using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	public class CourseService
	{
		private readonly ApplicationDbContext _db;

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
				var emptyList = new List<CourseViewModel>();
				return emptyList;
			}
			var courseViewModel = new List<CourseViewModel>();
			foreach (var course in courses)
			{
				var assignmentList = new AssignmentService().GetAssignmentNavViewModels(course.Id);

				var courseViewModeltemp = new CourseViewModel
										{
											Id = course.Id,
											Name = course.Name,
											Assignments = assignmentList
										};
				courseViewModel.Add(courseViewModeltemp);
			}
			return courseViewModel;
		}

		public Course GetCourseById(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.Id == id);
			return course;
		}

		public AdminCourseViewModel GetAdminCourseViewModel(int id) // er þetta notað?
		{
			var model = new AdminCourseViewModel();
			var c = new UserService();
			model.Name = GetCourseById(id).Name;
			model.AllTeachers = c.GetAllTeachers();
			model.Id = id;

			if (model.Name == null && model.AllTeachers.Count == 0) //hvernig á að villumeðhöndla her?
			{
				//TODO
				return null;
			}
			return model;
		}

		public List<Course> GetAllCourses()
		{
			return (from x in _db.Courses select x).ToList();
		}

		public void CreateCourse(CreateCourseViewModel model)
		{
			ICollection<ApplicationUser> tempList = new List<ApplicationUser>();
			var course = new Course
						{
							Name = model.Name,
							Users = tempList
						};
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
			var connection = new IdentityManager();
			var user = connection.GetUser(userName);
			var userVm = new UserViewModel
						{
							username = user.UserName,
							Id = user.Id
						};
			model.TeachersInCourse.Add(userVm);
		}

		public List<AdminNavCourseViewModel> GetAdminNavCourseViewModels()
		{
			var list = new List<AdminNavCourseViewModel>();
			var courses = GetAllCourses();
			foreach (var item in courses)
			{
				var temp = new AdminNavCourseViewModel
							{
								Id = item.Id,
								Name = item.Name
							};
				list.Add(temp);
			}
			return list;
		}

		public EdtiCourseViewModel GetEdtiCourseViewModel(Course course)
		{
			var model = new EdtiCourseViewModel
						{
							Id = course.Id,
							Name = course.Name
						};

			var tempTeachersList = new UserService().GetAllTeachers();
			var tempTeachersInCourseList = new UserService().GetTeachersInCourse(course.Id);
			var tempRemoveTeacherList = new List<UserViewModel>();
			foreach (var t in tempTeachersList)
			{
				foreach (var t2 in tempTeachersInCourseList)
				{
					if (t.Id == t2.Id)
					{
						tempRemoveTeacherList.Add(t);
					}
				}
			}
			foreach (var item in tempRemoveTeacherList)
			{
				tempTeachersList.Remove(item);
			}

			model.Teachers = new SelectList(tempTeachersList, "Id", "username");
			model.TeachersInCourse = new SelectList(tempTeachersInCourseList, "Id", "username");

			var tempStudentList = new UserService().GetAllStudents();
			var tempStudentsInCourseList = new UserService().GetStudentsInCourse(course.Id);
			var tempRemovestudentList = new List<UserViewModel>();
			foreach (var s in tempStudentList)
			{
				foreach (var s2 in tempStudentsInCourseList)
				{
					if (s.Id == s2.Id)
					{
						tempRemovestudentList.Add(s);
					}
				}
			}
			foreach (var item in tempRemovestudentList)
			{
				tempStudentList.Remove(item);
			}
			model.Students = new SelectList(tempStudentList, "Id", "username");
			model.StudentsInCourse = new SelectList(tempStudentsInCourseList, "Id", "username");
			return model;
		}
	}
}