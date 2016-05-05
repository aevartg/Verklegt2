using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Models.EntityClasses;

namespace Mooshak2.Models
{
	public class AssignmentViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Submission> Submissions { get; set; }
		public List<InputOutputViewModel> InputOutputs { get; set; }
	}

	public class AssignmentTabViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class AssignmentNavViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<MilestoneNavViewModel> List { get; set; }
	}
}