using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class PrincipalRepository : Repository<int, Principal>, IRepository<int, Principal>, IPrincipalRepository
	{
		public PrincipalRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }

	}
}
