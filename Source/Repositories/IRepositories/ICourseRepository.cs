using Source.Models;
using System.Linq.Expressions;

namespace Source.Repositories.IRepositories
{
	public interface ICourseRepository : IRepository<int, Course>
	{
		Task<IList<Course>> GetCoursesWithClassAsync();
		Task<IList<Course>> GetCoursesWithClassAsync(Expression<Func<Course, bool>> predicate);
	}
}
