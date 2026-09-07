namespace Source.Models
{
	public partial class Class
	{
		public string ClassId { get; set; } = null!;
		public DateOnly DateStart { get; set; }
		public DateOnly DateEnd { get; set; }

		public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
		public virtual ICollection<Student> Students { get; set; } = new List<Student>();
	}
}
