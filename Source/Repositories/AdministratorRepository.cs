using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class AdministratorRepository : Repository<int, Administrator>, IRepository<int, Administrator>, IAdministratorRepository
	{
		public AdministratorRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }

	}
}
