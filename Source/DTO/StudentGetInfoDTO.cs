namespace Source.DTO
{
	public class StudentGetInfoDTO
	{
		public int HumanId { get; set; }

		public string Ssn { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Forname { get; set; } = null!;
		public string? Midname { get; set; }
		public int? Age { get; private set => field = Helper.CalculateAge(Ssn); }

		public string CyearId { get; set; } = null!;
		public string ClassId { get; set; } = null!;
		public DateOnly DateEnroll { get; set; }
		public bool IsActive { get; set; }
		public DateOnly? DateQuit { get; set; }
		public bool? IsGraduated { get; set; }

		public ICollection<StudentGradeDTO> Grades { get; set; } = new List<StudentGradeDTO>();
	}
}
