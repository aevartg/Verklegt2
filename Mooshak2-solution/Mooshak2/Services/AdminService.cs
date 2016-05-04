using Mooshak2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Services
{
	
	public class AdminService
	{
		//ApplicationDbContext db = new ApplicationDbContext();  -  afhverju má þetta ekki?-SDS

		private ApplicationDbContext _db;

		public AdminService()
		{
			_db = new ApplicationDbContext();
		}

		public List<ApplicationUser> GetAllUsers()
		{
			var users = new List<ApplicationUser>();
			if (users == null)
			{
				//TODO
				return null;
			}
			else
			{
				_db.Users.ToList().ForEach((x) =>
				{
					users.Add(x);
				});

				return users;
				}
			//þetta fall skilar engu núna, erum ekki með neina usera
		}

		public Course GetCourseByID(int id)
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

		public AdminCourseViewModel GetAdminCourseViewModel(int id)
		{
			var model = new AdminCourseViewModel();
			if (model == null)
			{
				//TODO
				return null;
			}
			else
			{
				model.Name = GetCourseByID(id).Name;
				model.AllTeachers = GetAllUsers(); //nær í alla notendur núna, kann ekki að ná bara í teachers
				model.Id = id;
				return model;
			}
			
		}

		//fleiri föll sem munu vera notuð annarsstaðar liklega

		public List<ApplicationUser> GetAllTeachers()
		{
			var teachers = new List<ApplicationUser>();
			if (teachers == null)
			{
				//TODO
				return null;
			}
			else
			{
				//velja alla kennara, útfrá roleID?
				return teachers;
			}
		}

		public List<ApplicationUser> GetAllStudents()
		{
			var students = new List<ApplicationUser>();
			if (students == null)
			{
				//TODO
				return null;
			}
			else
			{
				//velja alla nemendur, útfrá roleID?
				return students;
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

		public List<Course> GetAllCourses()
		{
			var courses = new List<Course>();
			if (courses == null)
			{
				//TODO
				return null;
			}
			else
			{
				_db.Courses.ToList().ForEach((x) =>
				{
					courses.Add(x);
				});

				return courses;
			}
		}

		public List<Assignment> GetAllAssignments()
		{
			var assigns = new List<Assignment>();
			if (assigns == null)
			{
				//TODO
				return null;
			}
			else
			{
				_db.Assignments.ToList().ForEach((x) =>
				{
					assigns.Add(x);
				});

				return assigns;
			}
		}

		public List<Submission> GetSubmissionsByMilestoneID(int id)
		{
			var subs = new List<Submission>();
			//var subs = _db.Submissions.Where(x => x.IdMilestone == id);     -    virkar þetta?
			if (subs == null)
			{
				//TODO
				return null;
			}
			else
			{
				_db.Submissions.ToList().ForEach((x) =>
				{
					if (x.IdMilestone == id)
					{
						subs.Add(x);
					}
				});
				return subs;
			}
		}

		public List<Submission> GetSubmissionsByUserID(int id)
		{
			var subs = new List<Submission>();
			//var subs = _db.Submissions.Where(x => x.IdUser == id);     -    virkar þetta?
			if (subs == null)
			{
				//TODO
				return null;
			}
			else
			{
				_db.Submissions.ToList().ForEach((x) =>
				{
					if (x.IdUser == id)
					{
						subs.Add(x);
					}
				});
				return subs;
			}
		}
	}
}