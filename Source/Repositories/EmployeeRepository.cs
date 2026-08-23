using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Repositories.IRepositories;

namespace Source.Repositories
{
	public class EmployeeRepository(GymnasiumDbContext dbContext) : IRepository<Employee, int>
	{
		private GymnasiumDbContext db = dbContext;

		public async Task Delete(Employee entry)
		{
			db.Employees.Remove(entry);
			await db.SaveChangesAsync();
		}
		public async Task Insert(Employee entry)
		{
			db.Employees.Add(entry);
			await db.SaveChangesAsync();
		}
		public async Task Update(Employee entry)
		{
			db.Employees.Update(entry);
			await db.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await db.Employees.AnyAsync(e => e.EmployeeId == id);
		}

		public async Task<Employee?> FindOne(int id)
		{
			var result = await db.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
			return result ?? null;
		}

		public IQueryable<Employee>? FindAll()
		{
			var result = db.Employees.AsNoTracking().AsQueryable();
			return (result == null) ? null : result;
		}
	}
}
