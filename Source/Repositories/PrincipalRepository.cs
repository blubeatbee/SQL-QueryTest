using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public class PrincipalRepository(GymnasiumDbContext dbContext) : IRepository<Principal, int>
	{
		private readonly GymnasiumDbContext db = dbContext;

		public async Task Delete(Principal entry)
		{
			db.Principals.Remove(entry);
			await db.SaveChangesAsync();
		}
		public async Task Insert(Principal entry)
		{
			db.Principals.Add(entry);
			await db.SaveChangesAsync();
		}
		public async Task Update(Principal entry)
		{
			db.Principals.Update(entry);
			await db.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await db.Principals.AnyAsync(p => p.EmployeeId == id);
		}

		public async Task<Principal?> FindOne(int id)
		{
			var result = await db.Principals.FirstOrDefaultAsync(p => p.EmployeeId == id);
			return result ?? null;
		}

		public IQueryable<Principal>? FindAll()
		{
			var result = db.Principals.AsNoTracking().AsQueryable();
			return (result == null) ? null : result;
		}
	}
}
