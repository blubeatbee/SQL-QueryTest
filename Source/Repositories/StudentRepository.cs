using Source.Data;
using Source.Models.Gym2;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class StudentRepository : Repository<int, Student>, IRepository<int, Student>, IStudentRepository
	{
		public StudentRepository(GymnasiumDbContext2 context) : base(context)
		{
		}

		public GymnasiumDbContext2 GymnasiumDbContext2 { get { return (GymnasiumDbContext2)base.Context; } }

	}
}
