namespace Source.Models
{
	public partial class Grading
	{
		public int GradingId { get; set; }

		public int CourseId { get; set; }

		public int StudentId { get; set; }

		public int TeacherId { get; set; }

		public int Grading1 { get; set; }

		public DateOnly DateSet { get; set; }

		public virtual Course Course { get; set; } = null!;

		public virtual Student Student { get; set; } = null!;

		public virtual Teacher Teacher { get; set; } = null!;
	}
}
