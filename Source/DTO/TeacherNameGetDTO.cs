namespace Source.DTO
{
	public class TeacherNameGetDTO
	{
		public int EmployeeId { get; set; }
		public string Ssn { get; set => field = value.Insert(8, "-"); } = null!;
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
	}
}
