using Source.Models;

namespace Source.Repositories.IRepositories
{
	public interface IStudentRepository : IRepository<int, Student>
	{
		Task<IList<Student>> GetStudentsWithGradesAsync();
	}
}
