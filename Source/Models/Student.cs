namespace Source.Models
{
	public partial class Student
	{
		public int StudentId { get; set; }

		public string CyearId { get; set; } = null!;

		public string ClassId { get; set; } = null!;

		public DateOnly DateEnroll { get; set; }

		public bool IsActive { get; set; }

		public DateOnly? DateQuit { get; set; }

		public bool? IsGraduated { get; set; }

		public virtual Class Class { get; set; } = null!;

		public virtual ICollection<Grading> Gradings { get; set; } = new List<Grading>();

		public virtual Human StudentNavigation { get; set; } = null!;
	}
}
