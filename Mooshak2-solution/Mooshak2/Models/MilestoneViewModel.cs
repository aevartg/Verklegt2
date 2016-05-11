using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class MilestoneViewModel
	{
	}

	public class MilestoneNavViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class CreateMilestoneViewModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public int Weight { get; set; }
		[Required]
		public HttpPostedFileBase File { get; set; }
	}
}