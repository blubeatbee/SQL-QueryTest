namespace Source.Models
{
	public partial class Teacher
	{
		public int EmployeeId { get; set; }

		public string? Tasks { get; set; }

		public virtual Employee Employee { get; set; } = null!;

		public virtual ICollection<Grading> Gradings { get; set; } = new List<Grading>();
	}
}
