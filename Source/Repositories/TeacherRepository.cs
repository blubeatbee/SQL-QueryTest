using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public sealed class TeacherRepository(GymnasiumDbContext dbContext) : IRepository<Teacher, int>
	{
		private readonly GymnasiumDbContext db = dbContext;

		public async Task Delete(Teacher entry)
		{
			db.Teachers.Remove(entry);
			await db.SaveChangesAsync();
		}
		public async Task Insert(Teacher entry)
		{
			db.Teachers.Add(entry);
			await db.SaveChangesAsync();
		}
		public async Task Update(Teacher entry)
		{
			db.Teachers.Update(entry);
			await db.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await db.Teachers.AnyAsync(t => t.EmployeeId == id);
		}

		public async Task<Teacher?> FindOne(int id)
		{
			var result = await db.Teachers.FirstOrDefaultAsync(t => t.EmployeeId == id);
			return result ?? null;
		}

		public IQueryable<Teacher>? FindAll()
		{
			var result = db.Teachers.AsNoTracking().AsQueryable();
			return (result == null) ? null : result;
		}
	}
}
