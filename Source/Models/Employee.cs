namespace Source.Models
{
	public partial class Employee
	{
		public int EmployeeId { get; set; }
		public string Ssn { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public int? RoleId { get; set; }
		public string? Tasks { get; set; }
		public decimal Salary { get; set; }
		public DateOnly DateHired { get; set; }
		public DateOnly? DateQuit { get; set; }
		public bool IsEmployed { get; set; }

		public virtual Role? Role { get; set; }
		public virtual ICollection<Grading> Gradings { get; set; } = new List<Grading>();
	}
}
