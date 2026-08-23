namespace Source.Models
{
	public partial class CourseClass
	{
		public int CourseId { get; set; }

		public string CyearId { get; set; } = null!;

		public string ClassId { get; set; } = null!;

		public virtual Class Class { get; set; } = null!;

		public virtual Course Course { get; set; } = null!;
	}
}
