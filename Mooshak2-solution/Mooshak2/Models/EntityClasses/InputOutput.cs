using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooshak2.Models.EntityClasses
{
	public class InputOutput
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Milestone")]
		public int MilestoneId { get; set; }

		public string Input { get; set; }

		[Required]
		public string Output { get; set; }

		public virtual Milestone Milestone { get; set; }
	}
}