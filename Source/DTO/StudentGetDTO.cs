namespace Source.DTO
{
	public class StudentGetDTO
	{
		public int StudentId { get; set; }
		public string Ssn { get; set => field = value.Insert(8, "-"); } = null!;
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string ClassId { get; set; } = null!;
		public DateOnly DateEnrolled { get; set; }
		public DateOnly? DateQuit { get; set; }
		public bool IsActive { get; set => field = DateQuit == null; }

		public ICollection<StudentGetGradeDTO> Grades { get; set; } = new List<StudentGetGradeDTO>();
	}
}
