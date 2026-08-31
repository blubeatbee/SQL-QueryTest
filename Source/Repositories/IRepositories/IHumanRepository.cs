using Source.Models;
using System.Linq.Expressions;

namespace Source.Repositories.IRepositories
{
	public interface IHumanRepository : IRepository<int, Human>
	{
		Task<int> FindIdByFilterASync(Expression<Func<Human, bool>> predicate);
	}
}
