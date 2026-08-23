namespace Source.Models
{
	public partial class Class
	{
		public string CyearId { get; set; } = null!;

		public string ClassId { get; set; } = null!;

		public DateOnly DateStart { get; set; }

		public DateOnly DateEnd { get; set; }

		public virtual ICollection<Student> Students { get; set; } = new List<Student>();
	}
}
