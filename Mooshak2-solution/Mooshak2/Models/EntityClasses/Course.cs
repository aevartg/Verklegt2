using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mooshak2.Models.EntityClasses
{
	public class Course
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public ICollection<ApplicationUser> Users { get; set; }
	}
}