using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooshak2.Models.EntityClasses
{
	public class Assignment
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Course")]
		public int CourseId { get; set; }
		[Required]
		public string Name { get; set; }

		public virtual Course Course { get; set; }
	}
}