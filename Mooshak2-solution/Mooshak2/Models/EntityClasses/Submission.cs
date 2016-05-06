using System;

namespace Mooshak2.Models.EntityClasses
{
	public class Submission
	{
		public int Id { get; set; }
		public String UserId { get; set; }
		public int MilestoneId { get; set; }
		public int TestPassed { get; set; }
		public int TestFailed { get; set; }
		public DateTime SubmitDate { get; set; }
		public byte[] Blob { get; set; }
	}
}