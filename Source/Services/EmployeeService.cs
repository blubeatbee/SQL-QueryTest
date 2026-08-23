using Microsoft.EntityFrameworkCore;
using Source.DTO;
using Source.Models;
using Source.Repositories;
using Source.Services.IServices;

namespace Source.Services
{
	public sealed class EmployeeService(
		HumanRepository humanRepo,
		EmployeeRepository employeeRepo,
		AdministratorRepository administratorRepo,
		PrincipalRepository principalRepo,
		TeacherRepository teacherRepo)
		: IService, ICudService<int, EmployeeDto>
	{
		private readonly EmployeeRepository employeeRepo = employeeRepo;
		private readonly HumanRepository humanRepo = humanRepo;
		private readonly AdministratorRepository administratorRepo = administratorRepo;
		private readonly PrincipalRepository principalRepo = principalRepo;
		private readonly TeacherRepository teacherRepo = teacherRepo;

		public async Task<EmployeeDto> GetEmployee(int id)
		{
			var h = await humanRepo.FindOne(id) ?? throw new ArgumentNullException();
			var e = await employeeRepo.FindOne(id) ?? throw new ArgumentNullException();

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
			var humansQuery = humanRepo.FindAll() ?? throw new ArgumentNullException();
			var employeesQuery = employeeRepo.FindAll() ?? throw new ArgumentNullException();

			switch (filter)
			{
				// Filter teachers only
				case 1:
					employeesQuery = employeesQuery.AsNoTracking()
						.Where(e => e.Teacher != null)
						.Include(e => e.Teacher);
					break;
				// Filter admins only
				case 2:
					employeesQuery = employeesQuery.AsNoTracking()
						.Where(e => e.Administrator != null)
						.Include(e => e.Administrator);
					break;
				// Filter principals only
				case 3:
					employeesQuery = employeesQuery.AsNoTracking()
						.Where(e => e.Principal != null)
						.Include(e => e.Principal);
					break;

				// Default search behaviour (shows every employees)
				default:
					employeesQuery = employeesQuery.AsNoTracking()
						.Include(e => e.Teacher)
						.Include(e => e.Administrator)
						.Include(e => e.Principal);
					break;
			}

			var employees = await employeesQuery.AsNoTracking().ToListAsync();
			var humans = await humansQuery.AsNoTracking()
				.Where(h => h.Employee != null)
				.ToListAsync();

			var employeeList = humans.Join(employees,
			h => h.HumanId,
			e => e.EmployeeId,
			(h, e) => new EmployeeDto
			{
				HumanId = h.HumanId,
				Ssn = h.Ssn,
				Surname = h.Surname,
				Forname = h.Forname,
				Midname = h.Midname,
				Role = e.Teacher != null ? "Teacher" : (e.Administrator != null ? "Administrator" : (e.Principal != null ? "Principal" : null)),
				Salary = e.Salary,
				DateHired = e.DateHired,
				IsEmployed = e.IsEmployed,
				DateQuit = e.DateQuit
			})
			.ToList();

			return employeeList;
		}

		public async void CreateOneEntry(EmployeeDto newEmployee)
		{
			if (await humanRepo.FindId(newEmployee.Ssn) == -1)
			{
				try
				{
					await this.humanRepo.Insert(new Human()
					{
						Ssn = newEmployee.Ssn,
						Surname = newEmployee.Surname,
						Midname = newEmployee.Midname,
						Forname = newEmployee.Forname,
						Age = newEmployee.Age,
					});

					var newId = await humanRepo.FindId(newEmployee.Ssn);

					await this.employeeRepo.Insert(new Employee()
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
							await this.administratorRepo.Insert(new Administrator() { EmployeeId = newId });
							break;
						case "Principal":
							await this.principalRepo.Insert(new Principal() { EmployeeId = newId });
							break;
						case "Teacher":
							await this.teacherRepo.Insert(new Teacher() { EmployeeId = newId });
							break;
						default:
							break;
					}
				}
				catch
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
