namespace Source.Models
{
	public partial class Course
	{
		public int CourseId { get; set; }

		public string? Content { get; set; }

		public virtual ICollection<Grading> Gradings { get; set; } = new List<Grading>();
	}
}
