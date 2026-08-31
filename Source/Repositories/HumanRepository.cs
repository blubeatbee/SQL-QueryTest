using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Source.Repositories
{
	public class HumanRepository : Repository<int, Human>, IRepository<int, Human>, IHumanRepository
	{
		public HumanRepository(GymnasiumDbContext context) : base(context)
		{
		}

		public GymnasiumDbContext GymnasiumDbContext { get { return (GymnasiumDbContext)base.Context; } }


		public async Task<int> FindIdByFilterASync(Expression<Func<Human, bool>> predicate)
		{
			var result = await this.GymnasiumDbContext.Humans.FindAsync(predicate);
			return result == null ? -1 : result.HumanId;
		}


	}
}
