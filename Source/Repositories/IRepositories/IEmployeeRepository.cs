using Source.Models;
using System.Linq.Expressions;

namespace Source.Repositories.IRepositories
{
	public interface IEmployeeRepository : IRepository<int, Employee>
	{
		Task<Human> GetEmployeeAsync(int id);
		Task<IList<Human>> GetEmployeesAsync(short employeeType);
		Task<IList<Human>> GetEmployeesAsync(short employeeType, Expression<Func<Human, bool>> predicate);
	}
}
