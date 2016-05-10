using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooshak2.Models.EntityClasses
{
	public class Milestone
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Assignment")]
		public int AssignmentId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int Weight { get; set; }

		public virtual Assignment Assignment { get; set; }
	}
}