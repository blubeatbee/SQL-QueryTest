using Source.Services;
using Source.Services.IServices;

namespace Source.Menu.Core
{
	public class AppService
	{
		private readonly UnitOfWork unitOfWork;

		public AppService(UnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			this.CourseService = new CourseService(this.unitOfWork);
			this.EmployeeService = new EmployeeService(this.unitOfWork);
			this.StudentService = new StudentService(this.unitOfWork);
		}

		public ICourseService CourseService { get; private set; }
		public IStudentService StudentService { get; private set; }
		public IEmployeeService EmployeeService { get; private set; }
	}
}
