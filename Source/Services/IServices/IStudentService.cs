using Source.DTO;

namespace Source.Services.IServices
{
	public interface IStudentService : IService<int, StudentCreateDTO>
	{
		Task<StudentGetDTO> RetrieveStudentAsync(int id);
		Task<IList<StudentGetDTO>> RetrieveStudentsAsync(string filterClass);
		void AddGradingToStudent(GradingCreateDTO newGrade);
	}
}
