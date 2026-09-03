namespace Source.DTO
{
	public class EmployeeCreateDTO
	{
		public string Ssn { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public int RoleId { get; set; }
		public string? Tasks { get; set; }
		public decimal Salary { get; set; }
		public DateOnly DateHired { get; set; }
	}
}
