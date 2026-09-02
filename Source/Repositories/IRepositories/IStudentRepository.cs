using Source.Models;
using System.Linq.Expressions;

namespace Source.Repositories.IRepositories
{
	public interface IStudentRepository : IRepository<int, Student>
	{
		Task<Human> GetStudentAsync(int id);
		Task<IList<Student>> GetStudentsWithGradesAsync();
		Task<IList<Human>> GetStudentsAsync();
		Task<IList<Human>> GetStudentsAsync(Expression<Func<Human, bool>> predicate);
	}
}
