namespace Source.Models
{
	public partial class Role
	{
		public int RoleId { get; set; }
		public string RoleTitle { get; set; } = null!;

		public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
	}
}
