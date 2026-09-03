using Source.DTO;
using Source.Persistent;
using Source.Services.IServices;

namespace Source.Services
{
	public class EmployeeService(IUnitOfWork unitOfWork) : IEmployeeService
	{
		private readonly IUnitOfWork unitOfWork = unitOfWork;

		public async Task<EmployeeGetDTO> RetrieveEmployeeAsync(int id)
		{
			var e = await this.unitOfWork.Employees.GetAsync(id) ?? throw new ArgumentNullException(nameof(id));

			var employee = new EmployeeGetDTO
			{
				EmployeeId = e.EmployeeId,
				Ssn = e.Ssn,
				Surname = e.Surname,
				Name = e.Name,
				Role = e.Role == null ? string.Empty : e.Role.RoleTitle,
				Tasks = e.Tasks ?? string.Empty,
				Salary = e.Salary,
				DateHired = e.DateHired,
				DateQuit = e.DateQuit,
				IsEmployed = e.IsEmployed,
			};

			return employee;
		}

		public async Task<IList<EmployeeGetDTO>> RetrieveEmployeesByRoleAsync(short employeeRole)
		{
			var employees = (employeeRole == 0)
				? await this.unitOfWork.Employees.GetAllAsync()
				: await this.unitOfWork.Employees.GetByFilterAsync(e => e.RoleId == employeeRole)
				?? throw new ArgumentNullException();

			var employeeList = new List<EmployeeGetDTO>();

			foreach(var e in employees)
			{
				employeeList.Add(new EmployeeGetDTO
				{
					EmployeeId = e.EmployeeId,
					Ssn = e.Ssn,
					Surname = e.Surname,
					Name = e.Name,
					Role = e.Role == null ? string.Empty : e.Role.RoleTitle,
					Tasks = e.Tasks ?? string.Empty,
					Salary = e.Salary,
					DateHired = e.DateHired,
					DateQuit = e.DateQuit,
					IsEmployed = e.IsEmployed,
				});
			}

			return employeeList;
		}

		public async Task<int> NumberOfActiveEmployees(short employeeRole)
		{
			try
			{
				return employeeRole == 0
					? await this.unitOfWork.Employees
						.CountByFilterAsync(e => e.IsEmployed == true)
					: await this.unitOfWork.Employees
						.CountByFilterAsync(e => e.IsEmployed == true && e.RoleId == employeeRole);
			}
			catch
			{
				return 0;
			}
		}

		public async void Create(EmployeeCreateDTO newEmployee)
		{
			try
			{
				this.unitOfWork.Employees.Add(new()
				{
					Ssn = newEmployee.Ssn,
					Surname = newEmployee.Surname,
					Name = newEmployee.Name,
					RoleId = newEmployee.RoleId,
					Tasks = newEmployee.Tasks,
					Salary = newEmployee.Salary,
					DateHired = newEmployee.DateHired,
					DateQuit = null,
					IsEmployed = newEmployee.DateHired < DateOnly.FromDateTime(DateTime.Now),
				});

				_ = await this.unitOfWork.SaveAsync();
			}
			catch
			{
				throw;
			}
		}

	}
}
