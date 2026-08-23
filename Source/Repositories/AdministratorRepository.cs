using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public sealed class AdministratorRepository(GymnasiumDbContext dbContext) : IRepository<Administrator, int>
	{
		private readonly GymnasiumDbContext db = dbContext;

		public async Task Delete(Administrator entry)
		{
			db.Administrators.Remove(entry);
			await db.SaveChangesAsync();
		}
		public async Task Insert(Administrator entry)
		{
			db.Administrators.Add(entry);
			await db.SaveChangesAsync();
		}
		public async Task Update(Administrator entry)
		{
			db.Administrators.Update(entry);
			await db.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await db.Administrators.AnyAsync(a => a.EmployeeId == id);
		}

		public async Task<Administrator?> FindOne(int id)
		{
			var result = await db.Administrators.FirstOrDefaultAsync(a => a.EmployeeId == id);
			return result ?? null;
		}

		public IQueryable<Administrator>? FindAll()
		{
			var result = db.Administrators.AsNoTracking().AsQueryable();
			return (result == null) ? null : result;
		}
	}
}
