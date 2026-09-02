using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public class GradingRepository : Repository<int, Grading>, IRepository<int, Grading>, IGradingRepository
	{
		public GradingRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }

		public async Task<IList<Grading>> GetGradingsByStudentId(int id)
		{
			var query = this.GymnasiumDbContext.Gradings.Where(g => g.GradingId == id)
				.Include(g => g.Course);
			return await query.ToListAsync();
		}

	}
}
