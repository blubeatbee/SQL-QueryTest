using Source.DTO;
using Source.Models;
using Source.Persistent;
using Source.Services.IServices;

namespace Source.Services
{
	public class EmployeeService(IUnitOfWork unitOfWork) : IService<int, EmployeeDto>, IEmployeeService
	{
		private readonly IUnitOfWork unitOfWork = unitOfWork;

		public async Task<EmployeeDto> GetEmployee(int id)
		{
			var h = await this.unitOfWork.Employees.GetEmployeeAsync(id);

			if (h.Employee == null)
			{
				throw new InvalidOperationException($"Returned object has null value in property: {nameof(h.Employee)}");
			}

			var employee = new EmployeeDto
			{
				HumanId = h.HumanId,
				Ssn = h.Ssn,
				Surname = h.Surname,
				Forname = h.Forname,
				Midname = h.Midname,
				Role = h.Employee.Administrator != null ? "Administrator" : (h.Employee.Principal != null ? "Principal" : (h.Employee.Teacher != null ? "Teacher" : null)),
				Salary = h.Employee.Salary,
				DateHired = h.Employee.DateHired,
				IsEmployed = h.Employee.IsEmployed,
				DateQuit = h.Employee.DateQuit
			};

			return employee;
		}

		public async Task<IList<EmployeeDto>> GetAllEmployees(short employeeType)
		{
			var employees = await this.unitOfWork.Employees.GetEmployeesAsync(employeeType)
				?? throw new ArgumentNullException();

			var employeeList = new List<EmployeeDto>();

			foreach(var e in employees)
			{
				employeeList.Add(new EmployeeDto
				{
					HumanId = e.HumanId,
					Ssn = e.Ssn,
					Surname = e.Surname,
					Forname = e.Forname,
					Midname = e.Midname,
					Role = e.Employee!.Teacher != null ?
						"Teacher" :
						(e.Employee!.Administrator != null ?
							"Administrator" :
							(e.Employee!.Principal != null ?
								"Principal" :
								null)),
					Salary = e.Employee!.Salary,
					DateHired = e.Employee!.DateHired,
					IsEmployed = e.Employee!.IsEmployed,
					DateQuit = e.Employee!.DateQuit,
				});
			}

			return employeeList;
		}

		public async Task<int> CountNumberOfEmployees(short employeeType)
		{
			try
			{
				return employeeType switch
				{
					1 => await this.unitOfWork.Teachers.CountByFilterAsync(t => t.Employee.IsEmployed == true),
					2 => await this.unitOfWork.Administrators.CountByFilterAsync(a => a.Employee.IsEmployed == true),
					3 => await this.unitOfWork.Principals.CountByFilterAsync(p => p.Employee.IsEmployed == true),
					_ => await this.unitOfWork.Employees.CountByFilterAsync(e => e.IsEmployed == true),
				};
			}
			catch
			{
				return 0;
			}
		}

		public async void Create(EmployeeDto newEmployee)
		{

			if (await this.unitOfWork.Humans.FindIdByFilterASync(h => h.Ssn == newEmployee.Ssn &&
				(h.Surname + h.Forname) == newEmployee.Surname + newEmployee.Forname)
				== -1)
			{
				try
				{
					this.unitOfWork.Humans.Add(new Human()
					{
						Ssn = newEmployee.Ssn,
						Surname = newEmployee.Surname,
						Midname = newEmployee.Midname,
						Forname = newEmployee.Forname,
						Age = newEmployee.Age,
					});

					var newId = await this.unitOfWork.Humans.FindIdByFilterASync(h => h.Ssn == newEmployee.Ssn &&
						(h.Surname + h.Forname) == newEmployee.Surname + newEmployee.Forname);

					this.unitOfWork.Employees.Add(new Employee()
					{
						EmployeeId = newId,
						Salary = newEmployee.Salary,
						DateHired = newEmployee.DateHired,
						DateQuit = null,
						IsEmployed = newEmployee.DateQuit == null ? true : false,
					});

					switch (newEmployee.Role)
					{
						case "Administrator":
							this.unitOfWork.Administrators.Add(new Administrator() { EmployeeId = newId });
							break;
						case "Principal":
							this.unitOfWork.Principals.Add(new Principal() { EmployeeId = newId });
							break;
						case "Teacher":
							this.unitOfWork.Teachers.Add(new Teacher() { EmployeeId = newId });
							break;
						default:
							break;
					}

					_ = this.unitOfWork.Save();
				}
				catch
				{
					throw new InvalidOperationException();
				}
			}
		}

	}
}
