using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Models
{
	public class AssignmentViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Submission> Submissions { get; set; }
	}

	public class AssignmentTabViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class AssignmentNavViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<MilestoneNavViewModel> Milestones { get; set; }
	}

	public class CreateAssignmentViewModel
	{
		[Required]
		public int CourseId { get; set; }

		[Required(ErrorMessage = "Name Required")]
		public string Name { get; set; }

		[Required]
		public DateTime DateOpen { get; set; }

		[Required]
		public DateTime DateClose { get; set; }

		[Required(ErrorMessage = "Milestones Required")]
		public IEnumerable<CreateMilestoneViewModel> Milestones { get; set; }
	}

	public class TeacherAssignmentViewModel
	{
		public string AssignmentName { get; set; }
		public string MilestoneName { get; set; }
		public List<UserViewModel> AllStudents { get; set; }
		public List<Submission> Submissions { get; set; }
	}
}