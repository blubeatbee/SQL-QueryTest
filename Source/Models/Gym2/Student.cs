namespace Source.Models.Gym2
{
	public class Student
	{
		public int StudentId { get; set; }
		public string? ClassId { get; set; }
		public string Ssn { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public DateOnly DateEnrolled { get; set; }
		public DateOnly? DateQuit { get; set; }
		public bool IsActive { get; set; }

		public virtual Class? Class { get; set; }
		public virtual ICollection<Grading> Gradings { get; set; } = new List<Grading>();
	}
}
