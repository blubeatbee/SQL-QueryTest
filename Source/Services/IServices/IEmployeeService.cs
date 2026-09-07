using Source.DTO;

namespace Source.Services.IServices
{
	public interface IEmployeeService : IService<int, EmployeeCreateDTO>
	{
		Task<EmployeeGetDTO> RetrieveEmployeeAsync(int id);
		Task<IList<EmployeeGetDTO>> RetrieveEmployeesByRoleAsync(short employeeRole);
		Task<IList<TeacherNameGetDTO>> RetrieveActiveTeachersAsync();
		Task<int> NumberOfActiveEmployees(short employeeRole);
	}
}
