using Source.Data;
using Source.Repositories;
using Source.Repositories.IRepositories;

namespace Source.Persistent
{
	/// <summary>
	///		Implements the <see cref="IUnitOfWork"/> interface, and serves as the main access point to all repository patterns.
	/// </summary>
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly GymnasiumDbContext2 context;

		/// <summary><inheritdoc cref="UnitOfWork"/></summary>
		public UnitOfWork(GymnasiumDbContext2 context)
		{
			this.context = context;
			//this.Administrators = new AdministratorRepository(this.context);
			this.Employees = new EmployeeRepository(this.context);
			//this.Gradings = new GradingRepository(this.context);
			//this.Humans = new HumanRepository(this.context);
			//this.Principals = new PrincipalRepository(this.context);
			this.Students = new StudentRepository(this.context);
			//this.Teachers = new TeacherRepository(this.context);
		}

		//public IAdministratorRepository Administrators { get; private set; }
		public IEmployeeRepository Employees { get; private set; }
		//public IGradingRepository Gradings { get; private set; }z
		//public IHumanRepository Humans { get; private set; }
		//public IPrincipalRepository Principals { get; private set; }
		public IStudentRepository Students { get; private set; }
		//public ITeacherRepository Teachers { get; private set; }

		public int Save()
		{
			return this.context.SaveChanges();
		}

		public async Task<int> SaveAsync()
		{
			return await this.context.SaveChangesAsync();
		}

		public void Dispose()
		{
			this.context.Dispose();
		}
	}
}
