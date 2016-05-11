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
		[ForeignKey("InputOutput")]
		public int InputId { get; set; }
		[ForeignKey("ApplicationUser")]
		public String UserId { get; set; }
		[Required]
		public string Output { get; set; }

		public virtual InputOutput InputOutput { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}