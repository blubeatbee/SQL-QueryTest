using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class StudentRepository : Repository<int, Student>, IRepository<int, Student>, IStudentRepository
	{
		public StudentRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }

		public async Task<IList<Student>> GetStudentsWithGradesAsync()
		{
			var result = await this.GymnasiumDbContext.Students
				.Include(s => s.Gradings)
				.ThenInclude(g => g.Course)
				.ToListAsync();
			return result;
		}

		public async Task<IList<Human>> GetStudentsAsync()
		{
			var query = this.GymnasiumDbContext.Humans
				.Where(h => h.Student != null).Include(h => h.Student);
			return await query.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

		public async Task<IList<Human>> GetStudentsAsync(Expression<Func<Human, bool>> predicate)
		{
			var query = this.GymnasiumDbContext.Humans.Where(predicate)
				.Where(h => h.Student != null)
				.Include(h => h.Student);
			return await query.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

	}
}
