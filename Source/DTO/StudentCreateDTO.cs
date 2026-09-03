namespace Source.DTO
{
	public class StudentCreateDTO
	{
		public string Ssn { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string ClassId { get; set; } = null!;
		public DateOnly DateEnrolled { get; set; }
	}
}
