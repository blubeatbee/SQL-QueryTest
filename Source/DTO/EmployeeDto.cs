namespace Source.DTO
{
	public class EmployeeDto
	{
		public int HumanId { get; set; }

		public string Ssn { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Forname { get; set; } = null!;
		public string? Midname { get; set; }
		public int? Age { get; private set => field = Helper.CalculateAge(Ssn); }

		public string? Role { get; set; }
		public decimal Salary { get; set; }
		public DateOnly DateHired { get; set; }
		public bool IsEmployed { get; set; }
		public DateOnly? DateQuit { get; set; }
	}
}
