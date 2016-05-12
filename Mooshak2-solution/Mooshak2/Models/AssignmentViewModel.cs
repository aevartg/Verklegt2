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
		public List<SubmissionViewModel> Submissions { get; set; }
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

	public class EditAssignmentViewModel
	{
		public int AssignId { get; set; }
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
		public List<CourseViewModel> NavModel { get; set; }
	}

	public class TeacherAssignmentViewModel
	{
		public int AssignId { get; set; }
		public string AssignmentName { get; set; }
		public string MilestoneName { get; set; }
		public List<UserViewModel> AllStudents { get; set; }
		public List<SubmissionViewModel> Submissions { get; set; }
	}

	public class SubmissionViewModel
	{
		public int Id { get; set; }
		public int TestPassed { get; set; }
		public int TestFailed { get; set; }
		public DateTime SubmitDate { get; set; }
		public string UserName { get; set; }
		public double Grade { get; set; }
		public int MilestoneId { get; set; }
	}
}