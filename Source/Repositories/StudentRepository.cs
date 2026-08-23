using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public class StudentRepository(GymnasiumDbContext dbContext) : IRepository<Student, int>
	{
		private readonly GymnasiumDbContext db = dbContext;

		public async Task Delete(Student entry)
		{
			db.Students.Remove(entry);
			await db.SaveChangesAsync();
		}
		public async Task Insert(Student entry)
		{
			db.Students.Add(entry);
			await db.SaveChangesAsync();
		}
		public async Task Update(Student entry)
		{
			db.Students.Update(entry);
			await db.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await db.Students.AnyAsync(s => s.StudentId == id);
		}

		public async Task<Student?> FindOne(int id)
		{
			var result = await db.Students.FirstOrDefaultAsync(s => s.StudentId == id);
			return result ?? null;
		}

		public IQueryable<Student>? FindAll()
		{
			var result = db.Students.AsNoTracking().AsQueryable();
			return (result == null) ? null : result;
		}
	}
}
