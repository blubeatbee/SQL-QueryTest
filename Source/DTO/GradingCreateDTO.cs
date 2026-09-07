namespace Source.DTO
{
	public class GradingCreateDTO
	{
		public int Grade { get; set; }
		public DateOnly DateSet { get; set; }
		public int? CourseId { get; set; }
		public int? StudentId { get; set; }
		public int? TeacherId { get; set; }
	}
}
