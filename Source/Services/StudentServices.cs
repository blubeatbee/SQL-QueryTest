using Source.DTO;
using Source.Models;
using Source.Persistent;
using Source.Services.IServices;

namespace Source.Services
{
	public class StudentService(IUnitOfWork unitOfWork) : IService<int, StudentDto>, IStudentService
	{
		private readonly IUnitOfWork unitOfWork = unitOfWork;

		public async Task<StudentDto> GetStudent(int id)
		{
			var h = await this.unitOfWork.Humans.GetAsync(id) ?? throw new ArgumentNullException();
			var s = await this.unitOfWork.Students.GetAsync(id) ?? throw new ArgumentNullException();

			var student = new StudentDto()
			{
				HumanId = h.HumanId,
				Ssn = h.Ssn,
				Surname = h.Surname,
				Forname = h.Forname,
				Midname = h.Midname,
				CyearId = s.CyearId,
				ClassId = s.ClassId,
				DateEnroll = s.DateEnroll,
				IsActive = s.IsActive,
				DateQuit = s.DateQuit,
				IsGraduated = s.IsGraduated,
			};

			return student;
		}

		public async Task<IList<StudentDto>> GetAllStudents(string filter)
		{
			var students = string.IsNullOrWhiteSpace(filter) ?
				await this.unitOfWork.Students.GetStudentsAsync() :
				await this.unitOfWork.Students.GetStudentsAsync(h => (h.Student!.CyearId + h.Student!.ClassId) == filter)
				?? throw new ArgumentNullException();

			var list = new List<StudentDto>();

			foreach (var s in students)
			{
				list.Add(new StudentDto
				{
					HumanId = s.HumanId,
					Ssn = s.Ssn,
					Surname = s.Surname,
					Forname = s.Forname,
					Midname = s.Midname,
					CyearId = s.Student!.CyearId,
					ClassId = s.Student!.ClassId,
					DateEnroll = s.Student!.DateEnroll,
					IsActive = s.Student!.IsActive,
					DateQuit = s.Student!.DateQuit,
					IsGraduated = s.Student!.IsGraduated,
				});
			}

			return list;
		}

		public async void Create(StudentDto newStudent)
		{
			var resultId = await this.unitOfWork.Humans.FindIdByFilterASync(
				h => h.Ssn == newStudent.Ssn &&
				(h.Surname + h.Forname) == newStudent.Surname + newStudent.Forname);

			if (resultId == -1)
			{
				try
				{
					this.unitOfWork.Humans.Add(new Human()
					{
						Ssn = newStudent.Ssn,
						Surname = newStudent.Surname,
						Midname = newStudent.Midname,
						Forname = newStudent.Forname,
						Age = newStudent.Age,
					});

					var newId = await this.unitOfWork.Humans.FindIdByFilterASync(
						h => h.Ssn == newStudent.Ssn &&
						(h.Surname + h.Forname) == newStudent.Surname + newStudent.Forname);

					this.unitOfWork.Students.Add(new Student()
					{
						StudentId = newId,
						ClassId = newStudent.ClassId,
						CyearId = newStudent.CyearId,
						DateEnroll = newStudent.DateEnroll,
						DateQuit = newStudent.DateQuit,
						IsActive = newStudent.DateQuit == null ? true : false,
						IsGraduated = newStudent.IsGraduated,
					});

					_ = this.unitOfWork.Save();
				}
				catch
				{
					throw;
				}
			}
		}
		
	}
}
