using Source.Models;
using System.Linq.Expressions;

namespace Source.Repositories.IRepositories
{
	public interface IStudentRepository : IRepository<int, Student>
	{
		Task<Student?> GetStudentWithGradesAsync(int id);
		Task<IList<Student>> GetStudentsWithGradesAsync();
		Task<IList<Student>> GetStudentsWithGradesAsync(Expression<Func<Student, bool>> predicate);
	}
}
