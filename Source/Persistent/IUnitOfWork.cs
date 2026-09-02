using Source.Repositories.IRepositories;

namespace Source.Persistent
{
	/// <summary>
	///		Serves as a layer of abstraction between Service and the Repository layer.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		IAdministratorRepository Administrators { get; }
		IEmployeeRepository Employees { get; }
		IGradingRepository Gradings { get; }
		IHumanRepository Humans { get; }
		IPrincipalRepository Principals { get; }
		IStudentRepository Students { get; }
		ITeacherRepository Teachers { get; }

		/// <summary>
		///		Invokes save changes.
		/// </summary>
		/// <returns>The number of rows affected by the database operation.</returns>
		int Save();
		/// <summary>
		///		Invokes save 
		/// </summary>
		/// <returns>The number of rows affected by the database operation.</returns>
		Task<int> SaveAsync();
	}
}
