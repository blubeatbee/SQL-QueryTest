namespace Source.Models
{
	public partial class Employee
	{
		public int EmployeeId { get; set; }

		public decimal Salary { get; set; }

		public DateOnly DateHired { get; set; }

		public bool IsEmployed { get; set; }

		public DateOnly? DateQuit { get; set; }

		public virtual Administrator? Administrator { get; set; }

		public virtual Human EmployeeNavigation { get; set; } = null!;

		public virtual Principal? Principal { get; set; }

		public virtual Teacher? Teacher { get; set; }
	}
}
