using Source.Repositories.IRepositories;

namespace Source.Persistent
{
	/// <summary>Serves as the main access point to data access layer.</summary>
	/// <remarks>An UnitOfWork pattern should encapsulate all project specific repositories.</remarks>
	public interface IUnitOfWork : IDisposable
	{
		//IAdministratorRepository Administrators { get; }
		IEmployeeRepository Employees { get; }
		//IGradingRepository Gradings { get; }
		//IHumanRepository Humans { get; }
		//IPrincipalRepository Principals { get; }
		IStudentRepository Students { get; }
		//ITeacherRepository Teachers { get; }

		/// <summary>Save all changes made to tracked entities to DbContext.</summary>
		/// <returns>The number of rows affected by the database operation.</returns>
		int Save();
		/// <summary>Save all changes made to tracked entities to DbContext.</summary>
		/// <returns>The number of rows affected by the database operation.</returns>
		Task<int> SaveAsync();
	}
}
