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
			model.AllTeachers = GetAllTeachers(); 
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

		public List<UserViewModel> GetAllTeachers()
		{
			var teachers = new List<UserViewModel>();
			var allUsers = GetAllUsers();
			IdentityManager connection = new IdentityManager();

			foreach (var item in allUsers)
			{
				if (connection.UserIsInRole(item.Id, "Teacher") == true)
				{
					teachers.Add(item);
				}
			}

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
			var subs = (from x in _db.Submissions where x.MilestoneId == id select x).ToList();

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


		//eftirfarandi fer inní assignmentService, föll sem búa til assignmentviewmodel(ekkert submission í db þannig virkar ekki)
		//nota getSubByUseandMiles og getAssignByID föllin lika

		public List<Submission> GetSubmissionsByUserAndMilestoneID(string userID, int milestoneID)
		{
			var subs = (from x in _db.Submissions where (x.UserId == userID && x.MilestoneId == milestoneID) select x).ToList();

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

		public List<InputOutput> GetExpectedInputOutputsByMilestoneId(int id)
		{
			var exp = (from x in _db.InputOutputs where x.MilestoneId == id select x).ToList();
			if (exp.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return exp;
			}
		}

		public List<InputOutputViewModel> getInputsOutputsViewModel(int milestoneID)
		{
			var model = new List<InputOutputViewModel>();
			var allExpInputs = GetExpectedInputOutputsByMilestoneId(milestoneID);
			foreach (var item in allExpInputs)
			{
				var x = new InputOutputViewModel();
				x.Input = item.Input;
				x.ExpectedOutput = item.Output;
				x.RealOutput = item.Output; //þessi lína myndi ekki ná í expected output heldur raunverulegt output notandans
				model.Add(x);
			}
			if (model.Count == 0)
			{
				//TODO
				return null;
			}
			else
			{
				return model;
			}
		}

		public Milestone getMilestoneByID(int id)
		{
			var model = (from x in _db.Milestones where x.Id == id select x).SingleOrDefault();

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

		public AssignmentViewModel GetAssignmentViewModel(string userID, int milestoneID)
		{
			var model = new AssignmentViewModel();
			var milestone = getMilestoneByID(milestoneID);
			model.Id = GetAssignmentByID(milestone.AssignmentId).Id;
			model.Name = GetAssignmentByID(milestone.Id).Name;
			model.Submissions = GetSubmissionsByUserAndMilestoneID(userID, milestoneID);
			model.InputOutputs = getInputsOutputsViewModel(milestoneID);

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

		//fall sem tengist því að færa teacher úr AllTeacher yfir í TeachersInCourse í New Course fyrir admin
		public void UpdateAdminCourseViewModel(AdminCourseViewModel model, string userName)
		{
			IdentityManager connection = new IdentityManager();
			ApplicationUser user = connection.GetUser(userName);
			UserViewModel userVM = new UserViewModel();
			userVM.username = user.UserName;
			userVM.Id = user.Id;
			model.TeachersInCourse.Add(userVM);
		}
	}
}