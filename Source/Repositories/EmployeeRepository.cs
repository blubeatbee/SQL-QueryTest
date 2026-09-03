using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models.Gym2;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class EmployeeRepository : Repository<int, Employee>, IRepository<int, Employee>, IEmployeeRepository
	{
		public EmployeeRepository(GymnasiumDbContext2 context) : base(context)
		{
		}

		public GymnasiumDbContext2 GymnasiumDbContext2 { get { return (GymnasiumDbContext2)base.Context; } }


		public async Task<Employee?> GetTeacherAsync(int id)
		{
			var result = this.GymnasiumDbContext2.Employees.Where(e => e.RoleId == 1);
			return await result.FirstOrDefaultAsync(e => e.EmployeeId == id);
		}

		public async Task<IList<Employee>> GetAllTeachersAsync()
		{
			var query = this.GymnasiumDbContext2.Employees.Where(e => e.RoleId == 1);
			return await query.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

		public async Task<IList<Employee>> GetTeachersByFilterAsync(Expression<Func<Employee, bool>> predicate)
		{
			var query = this.GymnasiumDbContext2.Employees.Where(e => e.RoleId == 1)
				.Where(predicate);
			return await query.AsNoTrackingWithIdentityResolution().ToListAsync();
		}
	}
}
