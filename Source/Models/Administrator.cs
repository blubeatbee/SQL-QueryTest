namespace Source.Models
{
	public partial class Administrator
	{
		public int EmployeeId { get; set; }

		public string? Tasks { get; set; }

		public virtual Employee Employee { get; set; } = null!;
	}
}
