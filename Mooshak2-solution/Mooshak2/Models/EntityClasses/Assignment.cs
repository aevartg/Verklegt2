using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooshak2.Models.EntityClasses
{
	public class Assignment
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("Course")]
		public int CourseId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public DateTime DateOpen { get; set; }

		[Required]
		public DateTime DateClose { get; set; }

		public virtual Course Course { get; set; }
	}
}