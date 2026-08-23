using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public class HumanRepository(GymnasiumDbContext dbContext) : IRepository<Human, int>
	{
		private readonly GymnasiumDbContext db = dbContext;

		public async Task Delete(Human entry)
		{
			db.Humans.Remove(entry);
			await db.SaveChangesAsync();
		}
		public async Task Insert(Human entry)
		{
			db.Humans.Add(entry);
			await db.SaveChangesAsync();
		}
		public async Task Update(Human entry)
		{
			db.Humans.Update(entry);
			await db.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await db.Humans.AnyAsync(h => h.HumanId == id);
		}

		public async Task<Human?> FindOne(int id)
		{
			var result = await db.Humans.FirstOrDefaultAsync(h => h.HumanId == id);
			return result ?? null;
		}

		public IQueryable<Human>? FindAll()
		{
			var result = db.Humans.AsNoTracking().AsQueryable();
			return (result == null) ? null : result;
		}

		public async Task<int> FindId(string ssn)
		{
			var result = await db.Humans.FirstOrDefaultAsync(u => u.Ssn == ssn);
			return result == null ? -1 : result.HumanId;
		}
	}
}
