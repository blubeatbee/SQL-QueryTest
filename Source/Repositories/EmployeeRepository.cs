using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class EmployeeRepository : Repository<int, Employee>, IRepository<int, Employee>, IEmployeeRepository
	{
		public EmployeeRepository(Gymnasium2Context context) : base(context)
		{
		}

		public Gymnasium2Context Gymnasium2DbContext { get { return (Gymnasium2Context)base.Context; } }

		public async Task<Employee?> GetEmployeeWithRoleAsync(int id)
		{
			return await this.Gymnasium2DbContext.Employees.Include(e => e.Role)
				.FirstOrDefaultAsync(e => e.EmployeeId == id);
		}

		public async Task<IList<Employee>> GetEmployeesWithRoleAsync()
		{
			return await this.Gymnasium2DbContext.Employees.Include(e => e.Role)
				.AsNoTrackingWithIdentityResolution().ToListAsync();
		}
		public async Task<IList<Employee>> GetEmployeesWithRoleAsync(Expression<Func<Employee, bool>> predicate)
		{
			return await this.Gymnasium2DbContext.Employees.Include(e => e.Role)
				.Where(predicate)
				.AsNoTrackingWithIdentityResolution()
				.ToListAsync();
		}

		public async Task<Employee?> GetTeacherAsync(int id)
		{
			return await this.Gymnasium2DbContext.Employees.Where(e => e.RoleId == 1)
				.FirstOrDefaultAsync(e => e.EmployeeId == id);
		}

		public async Task<IList<Employee>> GetTeachersAsync()
		{
			return await this.Gymnasium2DbContext.Employees.Where(e => e.RoleId == 1)
				.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

		public async Task<IList<Employee>> GetTeachersAsync(Expression<Func<Employee, bool>> predicate)
		{
			return await this.Gymnasium2DbContext.Employees.Where(e => e.RoleId == 1)
				.Where(predicate)
				.AsNoTrackingWithIdentityResolution()
				.ToListAsync();
		}
	}
}
