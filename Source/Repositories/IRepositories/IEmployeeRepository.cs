using Source.Models;
using System.Linq.Expressions;

namespace Source.Repositories.IRepositories
{
	public interface IEmployeeRepository : IRepository<int, Employee>
	{
		Task<Employee?> GetEmployeeWithRoleAsync(int id);
		Task<IList<Employee>> GetEmployeesWithRoleAsync();
		Task<IList<Employee>> GetEmployeesWithRoleAsync(Expression<Func<Employee, bool>> predicate);
		Task<Employee?> GetTeacherAsync(int id);
		Task<IList<Employee>> GetTeachersAsync();
		Task<IList<Employee>> GetTeachersAsync(Expression<Func<Employee, bool>> predicate);
	}
}
