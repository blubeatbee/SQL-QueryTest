using Microsoft.EntityFrameworkCore.Storage;
using Source.Data;
using Source.Repositories;
using Source.Repositories.IRepositories;

namespace Source.Menu.Core
{
	/// <summary>
	///		Implements the <see cref="IUnitOfWork"/> interface, and serves as the main access point to all repository patterns.
	/// </summary>
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly Gymnasium2Context context;

		/// <summary><inheritdoc cref="UnitOfWork"/></summary>
		public UnitOfWork(Gymnasium2Context context)
		{
			this.context = context;
			this.Courses = new CourseRepository(this.context);
			this.Employees = new EmployeeRepository(this.context);
			this.Gradings = new GradingRepository(this.context);
			this.Students = new StudentRepository(this.context);
		}

		public ICourseRepository Courses { get; private set; }
		public IEmployeeRepository Employees { get; private set; }
		public IGradingRepository Gradings { get; private set; }
		public IStudentRepository Students { get; private set; }

		public int Save()
		{
			try
			{
				return this.context.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<int> SaveAsync()
		{
			try
			{
				return await this.context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			try
			{
				return await this.context.Database.BeginTransactionAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Dispose()
		{
			this.context.Dispose();
		}

	}
}
