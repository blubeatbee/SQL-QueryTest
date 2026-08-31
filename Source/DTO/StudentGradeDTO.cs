namespace Source.DTO
{
	public class StudentGradeDTO
	{
		public int Grading { get; set; }
		public string Course { get; set; } = null!;
		public DateOnly DateSet { get; set; }
		public string TeacherName { get; set; } = null!;
	}
}
