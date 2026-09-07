using Microsoft.EntityFrameworkCore.Storage;
using Source.Repositories.IRepositories;

namespace Source.Menu.Core
{
	/// <summary>Serves as the main access point to data access layer.</summary>
	/// <remarks>An UnitOfWork pattern should encapsulate all project specific repositories.</remarks>
	public interface IUnitOfWork : IDisposable
	{
		ICourseRepository Courses { get; }
		IEmployeeRepository Employees { get; }
		IGradingRepository Gradings { get; }
		IStudentRepository Students { get; }

		/// <summary>Save all changes made to tracked entities to DbContext.</summary>
		/// <returns>The number of rows affected by the database operation.</returns>
		int Save();
		/// <summary>Save all changes made to tracked entities to DbContext.</summary>
		/// <returns>The number of rows affected by the database operation.</returns>
		Task<int> SaveAsync();

		Task<IDbContextTransaction> BeginTransactionAsync();
	}
}
