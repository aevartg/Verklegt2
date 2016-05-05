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

		public List<UserViewModel> GetAllUsers()
		{
			var users = new List<UserViewModel>();
			var allUsers = (from x in _db.Users select x).ToList();

			for(int i = 0; i < allUsers.Count(); i++)
			{
				UserViewModel user = new UserViewModel();
				user.Id = allUsers[i].Id;
				user.username = allUsers[i].UserName;
				users.Add(user);
			};

			if (users.Count == 0)
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

			if (model == null)  //hvernig á að villumeðhöndla her?
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

			if (teachers.Count == 0)
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

			if (students.Count == 0)
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
			var allCourses = (from x in _db.Courses select x).ToList();

			if (allCourses.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return allCourses;
			}
			
			
		}

		public List<Assignment> GetAllAssignments()
		{
			var allAssigns = (from x in _db.Assignments select x).ToList();
			
			

			if (allAssigns.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return allAssigns;
			}
		}

		public List<Submission> GetSubmissionsByMilestoneID(int id)
		{
			var subs = (from x in _db.Submissions where x.IdMilestone == id select x).ToList();

			if (subs.Count == 0)
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
			var subs = (from x in _db.Submissions where x.IdMilestone == milestoneID && x.IdUser == UserID select x).ToList();

			if (subs.Count == 0)
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