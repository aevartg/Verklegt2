using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooshak2.Models.EntityClasses
{
	public class Submission
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }

		[Required]
		[ForeignKey("Milestone")]
		public int MilestoneId { get; set; }

		public int TestPassed { get; set; }
		public int TestFailed { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH-mm-ss}", ApplyFormatInEditMode = true)]
		public DateTime SubmitDate { get; set; }

		[Required]
		public byte[] Blob { get; set; }

		[Required]
		public string FileExtension { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual Milestone Milestone { get; set; }
	}
}