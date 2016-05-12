using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.EntityClasses
{
	public class UserOutput
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("ApplicationUser")]
		public String UserId { get; set; }
		[ForeignKey("Submission")]
		public int SubmissionId { get; set; }
		[Required]
		public string Output { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual Submission Submission { get; set; }
	}
}