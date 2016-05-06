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
		public string Description { get; set; }
		public virtual Assignment Assignment { get; set; }
	}
}