namespace Source.Models
{
	public partial class Human
	{
		public int HumanId { get; set; }

		public string Ssn { get; set; } = null!;

		public string Surname { get; set; } = null!;

		public string Forname { get; set; } = null!;

		public string? Midname { get; set; }

		public int? Age { get; set; }

		public virtual Employee? Employee { get; set; }

		public virtual Student? Student { get; set; }
	}
}
