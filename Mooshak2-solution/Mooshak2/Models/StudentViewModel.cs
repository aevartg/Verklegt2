using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class StudentViewModel
	{
	}

	public class SubmissionViewModel
	{
		public int Id { get; set; }
		public int TestPassed { get; set; }
		public int TestFailed { get; set; }
		public DateTime SubmitDate { get; set; }
		public string UserName { get; set; }
		public double Grade { get; set; }
		public int MilestoneId { get; set; }
	}

	public class InputOutputViewModel
	{
		public string Input { get; set; }
		public string RealOutput { get; set; }
		public string ExpectedOutput { get; set; }
	}
}