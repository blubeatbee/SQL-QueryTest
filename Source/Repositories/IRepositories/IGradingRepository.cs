using Source.Models;

namespace Source.Repositories.IRepositories
{
	public interface IGradingRepository : IRepository<int, Grading>
	{
		Task<IList<Grading>> GetGradingsByStudentId(int id);
	}
}
