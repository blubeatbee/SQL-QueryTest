using Source.DTO;

namespace Source.Services.IServices
{
	public interface IEmployeeService : IService<int, EmployeeCreateDTO>
	{
		Task<EmployeeGetDTO> RetrieveEmployeeAsync(int Id);
		//Task<TeacherGetDTO> RetrieveTeacherAsync(int id);
		Task<IList<EmployeeGetDTO>> RetrieveEmployeesByRoleAsync(short employeeRole);
		//Task<IList<TeacherGetDTO>> RetrieveTeachersAsync();
		Task<int> NumberOfActiveEmployees(short employeeRole);
	}
}
