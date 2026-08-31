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
			var h = await this.unitOfWork.Humans.GetAsync(id) ?? throw new ArgumentNullException();
			var e = await this.unitOfWork.Employees.GetAsync(id) ?? throw new ArgumentNullException();

			var employee = new EmployeeDto
			{
				HumanId = h.HumanId,
				Ssn = h.Ssn,
				Surname = h.Surname,
				Forname = h.Forname,
				Midname = h.Midname,
				Role = e.Administrator != null ? "Administrator" : (e.Principal != null ? "Principal" : (e.Teacher != null ? "Teacher" : null)),
				Salary = e.Salary,
				DateHired = e.DateHired,
				IsEmployed = e.IsEmployed,
				DateQuit = e.DateQuit
			};

			return employee;
		}

		public async Task<IList<EmployeeDto>> GetAllEmployees(short filter)
		{
			var employees = await this.unitOfWork.Humans.GetEmployeesByFilterAsync(filter, h => h.Employee != null)
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

		public async Task<int> CountNumberOfTeachers()
		{
			try
			{
				return await this.unitOfWork.Teachers.CountByFilterAsync(t => t.Employee.IsEmployed == true);
			}
			catch
			{
				return 0;
			}
		}

		public async Task<int> CountNumberOfAdministrators()
		{
			try
			{
				return await this.unitOfWork.Administrators.CountByFilterAsync(a => a.Employee.IsEmployed == true);
			}
			catch
			{
				return 0;
			}
		}

		public async Task<int> CountNumberOfPrincipals()
		{
			try
			{
				return await this.unitOfWork.Principals.CountByFilterAsync(p => p.Employee.IsEmployed == true);
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
