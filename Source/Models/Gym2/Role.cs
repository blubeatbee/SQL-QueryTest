namespace Source.Models.Gym2
{
	public class Role
	{
		public int RoleId { get; set; }
		public string RoleTitle { get; set; } = null!;

		public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
	}
}
