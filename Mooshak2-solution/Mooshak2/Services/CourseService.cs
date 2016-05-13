using System;
using System.Collections.Generic;
using System.Data.Entity;
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
		private readonly IAppDataContext _db;

		public CourseService(IAppDataContext context)
		{
			_db = context ?? new ApplicationDbContext();
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
					assignmentList = new AssignmentService(null).GetAssignmentNavViewModels(course.Id);

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
			UserService c = new UserService(null);
			model.Name = GetCourseById(id).Name;
			model.AllTeachers = c.GetAllTeachers();
			model.Id = id;

			if (model.Name == null && model.AllTeachers.Count == 0) //hvernig á að villumeðhöndla her?
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

		public void EditCourse(EditCourseViewModel model)
		{
			var course = _db.Courses.Where(y => y.Id == model.Id).Include(x => x.Users).SingleOrDefault();
			course.Name = model.Name;
			ICollection<ApplicationUser> tempList = new List<ApplicationUser>();
			UserService c = new UserService(null);
			List<UserViewModel> teachersBefore = c.GetTeachersInCourse(course.Id);
			List<UserViewModel> studentsBefore = c.GetStudentsInCourse(course.Id);
			var usersBefore = teachersBefore.Concat(studentsBefore);
			List<ApplicationUser> tempBefore = new List<ApplicationUser>();
			foreach (var x in usersBefore)
			{
				var tempUser = (from u in _db.Users where u.Id == x.Id select u).First();
				tempBefore.Add(tempUser);
			}
			if(model.SelectedStudents != null)
			{ 
				foreach (var s in model.SelectedStudents)
				{
					var tempStudent = (from u in _db.Users where s == u.Id select u).First();
					tempList.Add(tempStudent);
				}
			}

			foreach (var t in model.SelectedTeachers)
			{
				var tempTeacher = (from u in _db.Users where t == u.Id select u).First();
				tempList.Add(tempTeacher);
			}

			foreach (var u in tempBefore)
			{
				course.Users.Remove(u);// - hér er villa, náum ekki að removea tengslin..
			}

			foreach (var u1 in tempList)
			{
				course.Users.Add(u1);
			}
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

		public EditCourseViewModel GetEditCourseViewModel(Course course)
		{
			EditCourseViewModel model = new EditCourseViewModel();
			model.Id = course.Id;
			model.Name = course.Name;

			List<UserViewModel> tempTeachersList = new UserService(null).GetAllTeachers();
			List<UserViewModel> tempTeachersInCourseList = new UserService(null).GetTeachersInCourse(course.Id);
			List<UserViewModel> tempRemoveTeacherList = new List<UserViewModel>();
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

			List<UserViewModel> tempStudentList = new UserService(null).GetAllStudents();
			List<UserViewModel> tempStudentsInCourseList = new UserService(null).GetStudentsInCourse(course.Id);
			List<UserViewModel> tempRemovestudentList = new List<UserViewModel>();
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

		public void DeleteCourse(int Id)
		{
			Course course = GetCourseById(Id);
			_db.Courses.Remove(course);
			_db.SaveChanges();
		}
	}
}