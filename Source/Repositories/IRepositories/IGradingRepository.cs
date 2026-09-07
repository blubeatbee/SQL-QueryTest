using Source.Models;

namespace Source.Repositories.IRepositories
{
	public interface IGradingRepository : IRepository<int, Grading>
	{
		Task<IList<Grading>> GetGradingsByStudentIdAsync(int id);
		Task<IList<Grading>> GetGradingsByTeacherIdAsync(int id);
	}
}
