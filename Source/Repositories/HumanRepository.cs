using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class HumanRepository : Repository<int, Human>, IRepository<int, Human>, IHumanRepository
	{
		public HumanRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }


		public async Task<int> FindIdByFilterASync(Expression<Func<Human, bool>> predicate)
		{
			var result = await this.GymnasiumDbContext.Humans.FindAsync(predicate);
			return result == null ? -1 : result.HumanId;
		}

		public async Task<IList<Human>> GetStudentsByFilter(Expression<Func<Human, bool>>? predicate = null)
		{
			var query = this.GymnasiumDbContext.Humans
				.Where(h => h.Student != null)
				.Include(h => h.Student);

			if (predicate != null)
			{
				return await query.Where(predicate)
					.AsNoTrackingWithIdentityResolution()
					.ToListAsync();
			}
			return await query.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

		public async Task<IList<Human>> GetEmployeesByFilterAsync(short employeeType = 0, Expression<Func<Human, bool>>? predicate = null)
		{
			var query = (predicate == null) ?
				this.GymnasiumDbContext.Humans.Where(h => h.Employee != null) :
				this.GymnasiumDbContext.Humans.Where(h => h.Employee != null).Where(predicate);

			switch (employeeType)
			{
				default:
					return await query.Include(h => h.Employee)
						.AsNoTrackingWithIdentityResolution()
						.ToListAsync();
				case 1:
					return await query.Where(h => h.Employee!.Teacher != null)
						.Include(h => h.Employee)
						.ThenInclude(h => h!.Teacher)
						.AsNoTrackingWithIdentityResolution()
						.ToListAsync();
				case 2:
					return await query.Where(h => h.Employee!.Administrator != null)
						.Include(h => h.Employee)
						.ThenInclude(h => h!.Administrator)
						.AsNoTrackingWithIdentityResolution()
						.ToListAsync();
				case 3:
					return await query.Where(h => h.Employee!.Principal != null)
						.Include(h => h.Employee)
						.ThenInclude(h => h!.Principal)
						.AsNoTrackingWithIdentityResolution()
						.ToListAsync();
			}
		}

	}
}
