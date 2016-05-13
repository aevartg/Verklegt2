using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser()
		{
			Courses = new List<Course>();
		}

		public ICollection<Course> Courses { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}

	public interface IAppDataContext
	{
		IDbSet<ApplicationUser> Users { get; set; }
		IDbSet<Course> Courses { get; set; }
		IDbSet<Milestone> Milestones { get; set; }
		IDbSet<Assignment> Assignments { get; set; }
		IDbSet<InputOutput> InputOutputs { get; set; }
		IDbSet<Submission> Submissions { get; set; }
		IDbSet<UserOutput> UserOutputs { get; set; }
		int SaveChanges();
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
	{


		public ApplicationDbContext() : base("DefaultConnection", false)
		{
		}
		override public IDbSet<ApplicationUser> Users { get; set; }
		public IDbSet<Course> Courses { get; set; }
		public IDbSet<Milestone> Milestones { get; set; }
		public IDbSet<Assignment> Assignments { get; set; }
		public IDbSet<InputOutput> InputOutputs { get; set; }
		public IDbSet<Submission> Submissions { get; set; }
		public IDbSet<UserOutput> UserOutputs { get; set; }

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}