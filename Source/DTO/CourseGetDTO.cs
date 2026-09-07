namespace Source.DTO
{
	public class CourseGetDTO
	{
		public string Title { get; set; } = null!;
		public DateOnly CourseStart { get; set; }
		public DateOnly CourseEnd { get; set; }
		public string Class { get; set; } = null!;
		public DateOnly ClassStart { get; set; }
		public DateOnly ClassEnd { get; set; }
	}
}
