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
					
			_db.Users.ToList().ForEach((x) =>
			{
				users.Add(x);
			});

			if (users == null)
			{
				//TODO
				return null;
			}
			else
			{
				return users;
			}				
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
			
			model.Name = GetCourseByID(id).Name;
			model.AllTeachers = GetAllUsers(); //nær í alla notendur núna, kann ekki að ná bara í teachers
			model.Id = id;

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

		//fleiri föll sem munu vera notuð annarsstaðar liklega

		public List<ApplicationUser> GetAllTeachers()
		{
			var teachers = new List<ApplicationUser>();

			//velja alla kennara, útfrá roleID?

			if (teachers == null)
			{
				//TODO
				return null;
			}
			else
			{
				return teachers;
			}
			
			
		}

		public List<ApplicationUser> GetAllStudents()
		{
			var students = new List<ApplicationUser>();
			
			//velja alla kennara, útfrá roleID?

			if (students == null)
			{
				//TODO
				return null;
			}
			else
			{
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
			
			_db.Courses.ToList().ForEach((x) =>
			{
				courses.Add(x);
			});

			if (courses == null)
			{
				//TODO
				return null;
			}
			else
			{
				return courses;
			}
			
			
		}

		public List<Assignment> GetAllAssignments()
		{
			var assigns = new List<Assignment>();

			_db.Assignments.ToList().ForEach((x) =>
			{
				assigns.Add(x);
			});

			if (assigns == null)
			{
				//TODO
				return null;
			}
			else
			{
				return assigns;
			}
		}

		public List<Submission> GetSubmissionsByMilestoneID(int id)
		{
			var subs = new List<Submission>();
			//var subs = _db.Submissions.Where(x => x.IdMilestone == id);     -    virkar þetta?

			_db.Submissions.ToList().ForEach((x) =>
			{
				if (x.IdMilestone == id)
				{
					subs.Add(x);
				}
			});

			if (subs == null)
			{
				//TODO
				return null;
			}
			else
			{
				return subs;
			}
		}

		public List<Submission> GetSubmissionsByUserID(int UserID, int milestoneID)
		{
			var subs = new List<Submission>();
			//var subs = _db.Submissions.Where(x => x.IdUser == id);     -    virkar þetta?

			_db.Submissions.ToList().ForEach((x) =>
			{
				if (x.IdUser == UserID && x.IdMilestone == milestoneID)
				{
					subs.Add(x);
				}
			});

			if (subs == null)
			{
				//TODO
				return null;
			}
			else
			{
				
				return subs;
			}
		}
	}
}