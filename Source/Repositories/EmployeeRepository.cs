using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class EmployeeRepository : Repository<int, Employee>, IRepository<int, Employee>, IEmployeeRepository
	{
		public EmployeeRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }

		public async Task<Human> GetEmployeeAsync(int id)
		{
			var result = this.GymnasiumDbContext.Humans.Where(h => h.HumanId == id)
				.Include(h => h.Employee!).ThenInclude(h => h!.Teacher)
				.Include(h => h.Employee!).ThenInclude(h => h!.Administrator)
				.Include(h => h.Employee!).ThenInclude(h => h!.Principal);

			return await result.FirstAsync();
		}

		public async Task<IList<Human>> GetEmployeesAsync(short employeeType)
		{
			var query = this.GymnasiumDbContext.Humans.Where(h => h.Employee != null);

			var query2 = employeeType switch
			{
				1 => query.Where(h => h.Employee!.Teacher != null)
										.Include(h => h.Employee)
										.ThenInclude(h => h!.Teacher)
										.AsQueryable(),
				2 => query.Where(h => h.Employee!.Administrator != null)
										.Include(h => h.Employee)
										.ThenInclude(h => h!.Administrator)
										.AsQueryable(),
				3 => query.Where(h => h.Employee!.Principal != null)
										.Include(h => h.Employee)
										.ThenInclude(h => h!.Principal)
										.AsQueryable(),
				_ => query.Include(h => h.Employee).ThenInclude(h => h!.Administrator)
										.Include(h => h.Employee).ThenInclude(h => h!.Teacher)
										.Include(h => h.Employee).ThenInclude(h => h!.Principal)
										.AsQueryable()
			};

			return await query2.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

		public async Task<IList<Human>> GetEmployeesAsync(short employeeType, Expression<Func<Human, bool>> predicate)
		{
			var query = this.GymnasiumDbContext.Humans.Where(predicate)
				.Where(h => h.Employee != null);

			var query2 = employeeType switch
			{
				1 => query.Where(h => h.Employee!.Teacher != null)
										.Include(h => h.Employee)
										.ThenInclude(h => h!.Teacher)
										.AsQueryable(),
				2 => query.Where(h => h.Employee!.Administrator != null)
										.Include(h => h.Employee)
										.ThenInclude(h => h!.Administrator)
										.AsQueryable(),
				3 => query.Where(h => h.Employee!.Principal != null)
										.Include(h => h.Employee)
										.ThenInclude(h => h!.Principal)
										.AsQueryable(),
				_ => query.Include(h => h.Employee).ThenInclude(h => h!.Administrator)
										.Include(h => h.Employee).ThenInclude(h => h!.Teacher)
										.Include(h => h.Employee).ThenInclude(h => h!.Principal)
										.AsQueryable()
			};

			return await query2.AsNoTrackingWithIdentityResolution().ToListAsync();

		}
	}
}
