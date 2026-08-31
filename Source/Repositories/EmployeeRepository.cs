using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public class EmployeeRepository : Repository<int, Employee>, IRepository<int, Employee>, IEmployeeRepository
	{
		public EmployeeRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }

	}
}
