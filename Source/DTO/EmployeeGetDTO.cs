namespace Source.DTO
{
	public class EmployeeGetDTO
	{
		public int EmployeeId { get; set; }
		public string Ssn { get; set => field = value.Insert(8, "-"); } = null!;
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string? Role { get; set; } = null!;
		public string Tasks { get; set; } = null!;
		public decimal Salary { get; set; }
		public DateOnly DateHired { get; set; }
		public DateOnly? DateQuit { get; set; }
		public bool IsEmployed { get; set => field = DateQuit != null && DateQuit < DateOnly.FromDateTime(DateTime.Now); }

	}
}
