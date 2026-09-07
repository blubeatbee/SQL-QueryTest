using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class StudentRepository : Repository<int, Student>, IRepository<int, Student>, IStudentRepository
	{
		public StudentRepository(Gymnasium2Context context) : base(context)
		{
		}

		public Gymnasium2Context Gymnasium2Context { get { return (Gymnasium2Context)base.Context; } }

		public async Task<Student?> GetStudentWithGradesAsync(int id)
		{
			return await this.Gymnasium2Context.Students.Include(s => s.Gradings)
					.ThenInclude(g => g.Course)
				.Include(s => s.Gradings)
					.ThenInclude(g => g.Teacher)
				.FirstOrDefaultAsync(e => e.StudentId == id);
		}
		public async Task<IList<Student>> GetStudentsWithGradesAsync()
		{
			return await this.Gymnasium2Context.Students.Include(s => s.Gradings)
					.ThenInclude(g => g.Course)
				.Include(s => s.Gradings)
					.ThenInclude(g => g.Teacher)
				.AsNoTrackingWithIdentityResolution()
				.ToListAsync();
		}
		public async Task<IList<Student>> GetStudentsWithGradesAsync(Expression<Func<Student, bool>> predicate)
		{
			return await this.Gymnasium2Context.Students.Where(predicate)
				.Include(s => s.Gradings)
					.ThenInclude(g => g.Course)
				.Include(s => s.Gradings)
					.ThenInclude(g => g.Teacher)
				.AsNoTrackingWithIdentityResolution()
				.ToListAsync();
		}
	}
}
