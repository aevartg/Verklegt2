using System;

namespace Mooshak2.Models
{
	public class SubmissionModels
	{
		public int Id { get; set; }
		public int IdUser { get; set; }
		public int IdMilestone { get; set; }
		public int TestPassed { get; set; }
		public int TestFailed { get; set; }
		public DateTime SubmissionDate { get; set; }
		public byte[] SubmissionBlob { get; set; }
	}
}