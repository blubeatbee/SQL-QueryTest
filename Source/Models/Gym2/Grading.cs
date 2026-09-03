namespace Source.Models.Gym2
{
	public class Grading
	{
		public int GradingId { get; set; }
		public int Grade { get; set; }
		public DateOnly DateSet { get; set; }
		public int? CourseId { get; set; }
		public int? StudentId { get; set; }
		public int? TeacherId { get; set; }

		public virtual Course? Course { get; set; }
		public virtual Student? Student { get; set; }
		public virtual Employee? Teacher { get; set; }
	}
}
