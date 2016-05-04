using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.EntityClasses
{
	public class UserCourse
	{
		[Key,ForeignKey("User"),Column(Order = 1)]
		public string UserId { get; set; }
		[Key, ForeignKey("Course"), Column(Order = 0)]
		public int CourseId { get; set; }

		public virtual ApplicationUser User { get; set; }
		public virtual Course Course { get; set; }
	}
}