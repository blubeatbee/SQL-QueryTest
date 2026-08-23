namespace Source.Models
{
	public partial class ClassTeacher
	{
		public string CyearId { get; set; } = null!;

		public string ClassId { get; set; } = null!;

		public int TeacherId { get; set; }

		public DateOnly DateTeachStart { get; set; }

		public DateOnly DateTeachEnd { get; set; }

		public virtual Class Class { get; set; } = null!;

		public virtual Teacher Teacher { get; set; } = null!;
	}
}
