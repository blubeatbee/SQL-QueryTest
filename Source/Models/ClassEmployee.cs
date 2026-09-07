namespace Source.Models
{
	public partial class ClassEmployee
	{
		public string ClassId { get; set; } = null!;
		public int EmployeeId { get; set; }

		public virtual Class Class { get; set; } = null!;
		public virtual Employee Employee { get; set; } = null!;
	}
}
