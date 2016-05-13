using System.Data.Entity;
using Mooshak2.Models;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Tests
{
	class MockDataContext : IAppDataContext
	{
		/// <summary>
		/// Sets up the fake database.
		/// </summary>
		public MockDataContext()
		{
			// We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
			Users = new InMemoryDbSet<ApplicationUser>();
			Courses = new InMemoryDbSet<Course>();
			Milestones = new InMemoryDbSet<Milestone>();
			Assignments = new InMemoryDbSet<Assignment>();
			InputOutputs = new InMemoryDbSet<InputOutput>();
			Submissions = new InMemoryDbSet<Submission>();
			UserOutputs = new InMemoryDbSet<UserOutput>();
		}

		public IDbSet<Course> Courses { get; set; }
		public IDbSet<ApplicationUser> Users { get; set; }
		public IDbSet<Milestone> Milestones { get; set; }
		public IDbSet<Assignment> Assignments { get; set; }
		public IDbSet<InputOutput> InputOutputs { get; set; }
		public IDbSet<Submission> Submissions { get; set; }
		public IDbSet<UserOutput> UserOutputs { get; set; }
		// TODO: bætið við fleiri færslum hér
		// eftir því sem þeim fjölgar í AppDataContext klasanum ykkar!

		public int SaveChanges()
		{
			// Pretend that each entity gets a database id when we hit save.
			int changes = 0;

			return changes;
		}

		public void Dispose()
		{
			// Do nothing!
		}
	}
}
