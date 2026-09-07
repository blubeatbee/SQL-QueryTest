using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class CourseRepository : Repository<int, Course>, IRepository<int, Course>, ICourseRepository
	{
		public CourseRepository(Gymnasium2Context context) : base(context)
		{
		}

		public Gymnasium2Context Gymnasium2Context { get { return (Gymnasium2Context)base.Context; } }


		public async Task<IList<Course>> GetCoursesWithClassAsync()
		{
			return await this.Gymnasium2Context.Courses.Include(c => c.Class)
				.AsNoTrackingWithIdentityResolution().ToListAsync();
		}

		public async Task<IList<Course>> GetCoursesWithClassAsync(Expression<Func<Course, bool>> predicate)
		{
			return await this.Gymnasium2Context.Courses.Where(predicate)
				.Include(c => c.Class)
				.AsNoTrackingWithIdentityResolution()
				.ToListAsync();
		}
	}
}
