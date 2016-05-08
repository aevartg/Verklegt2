using System;
using System.Collections.Generic;
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
		public string Name { get; set; }
		public string Weight { get; set; }
		public HttpPostedFileBase File { get; set; }
	}
}