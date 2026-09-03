namespace Source.Models.Gym2
{
	public class Course
	{
		public int CourseId { get; set; }
		public string Title { get; set; } = null!;
		public DateOnly DateStart { get; set; }
		public DateOnly DateEnd { get; set; }
		public string ClassId { get; set; } = null!;

		public virtual Class Class { get; set; } = null!;
		public virtual ICollection<Grading> Gradings { get; set; } = new List<Grading>();
	}
}
