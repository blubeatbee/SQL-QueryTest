using Source.Models.Gym2;
using System.Linq.Expressions;

namespace Source.Repositories.IRepositories
{
	public interface IEmployeeRepository : IRepository<int, Employee>
	{
		Task<Employee?> GetTeacherAsync(int id);
		Task<IList<Employee>> GetAllTeachersAsync();
		Task<IList<Employee>> GetTeachersByFilterAsync(Expression<Func<Employee, bool>> predicate);
	}
}
