using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Mooshak2.Models.EntityClasses
{
	public class UserOutput
	{
		[Key]
		public int Id { get; set; }
		
		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }

		[Required]
		[ForeignKey("Submission")]
		public int SubmissionId { get; set; }

		[Required]
		public string Output { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual Submission Submission { get; set; }
	}
}