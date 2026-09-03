namespace Source.DTO
{
	public class StudentGetGradeDTO
	{
		public int Grading { get; set; }
		public string CourseTitle { get; set; } = null!;
		public string TeacherName { get; set; } = null!;
		public DateOnly DateSet { get; set; }
	}
}
