using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public class GradingRepository : Repository<int, Grading>, IRepository<int, Grading>, IGradingRepository
	{
		public GradingRepository(Gymnasium2Context context) : base(context)
		{
		}

		public Gymnasium2Context Gymnasium2DbContext { get { return (Gymnasium2Context)base.Context; } }


		public async Task<IList<Grading>> GetGradingsByStudentIdAsync(int id)
		{
			return await this.Gymnasium2DbContext.Gradings.Where(g => g.StudentId == id)
				.Include(g => g.Teacher)
				.Include(g => g.Course)
				.ToListAsync();
		}

		public async Task<IList<Grading>> GetGradingsByTeacherIdAsync(int id)
		{
			return await this.Gymnasium2DbContext.Gradings.Where(g => g.TeacherId == id)
				.Include(g => g.Student)
				.Include(g => g.Course)
				.ToListAsync();
		}
	}
}
