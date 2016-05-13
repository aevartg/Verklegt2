using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class TeacherViewModels
	{
	}

	public class TeacherAssignmentViewModel
	{
		public int AssignId { get; set; }
		public string AssignmentName { get; set; }
		public string MilestoneName { get; set; }
		public List<UserViewModel> AllStudents { get; set; }
		public List<SubmissionViewModel> Submissions { get; set; }
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
		public List<CreateMilestoneViewModel> Milestones { get; set; }
		public List<CourseViewModel> NavModel { get; set; }
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

	public class AssignmentNavViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime OpenDate { get; set; }
		public DateTime CloseDate { get; set; }
		public List<MilestoneNavViewModel> Milestones { get; set; }
	}

	public class AssignmentTabViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class AssignmentViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<SubmissionViewModel> Submissions { get; set; }
	}

	public class MilestoneNavViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class CreateMilestoneViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public int Weight { get; set; }

		[Required]
		public HttpPostedFileBase File { get; set; }

		public int Id { get; set; }
	}
}