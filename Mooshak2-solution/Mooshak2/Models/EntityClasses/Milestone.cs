namespace Mooshak2.Models.EntityClasses
{
	public class Milestone
	{
		public int Id { get; set; }
		public int AssignmentId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}