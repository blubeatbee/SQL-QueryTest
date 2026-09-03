using Source.DTO;
using Source.Persistent;
using Source.Services.IServices;

namespace Source.Services
{
	public class StudentService(IUnitOfWork unitOfWork) : IStudentService
	{
		private readonly IUnitOfWork unitOfWork = unitOfWork;

		public async Task<StudentGetDTO> RetrieveStudentAsync(int id)
		{
			var s = await this.unitOfWork.Students.GetAsync(id) ?? throw new ArgumentNullException(nameof(id));

			var student = new StudentGetDTO()
			{
				StudentId = s.StudentId,
				Ssn = s.Ssn,
				Surname = s.Surname,
				Name = s.Name,
				ClassId = s.ClassId ?? string.Empty,
				DateEnrolled = s.DateEnrolled,
				DateQuit = s.DateQuit,
				IsActive = s.IsActive,
			};

			foreach (var g in s.Gradings)
			{
				student.Grades.Add(new StudentGetGradeDTO()
				{
					CourseTitle = g.Course!.Title ?? string.Empty,
					Grading = g.Grade,
					DateSet = g.DateSet,
					TeacherName = (g.Teacher!.Surname + g.Teacher!.Name) ?? string.Empty,
				});
			}

			return student;
		}

		public async Task<IList<StudentGetDTO>> RetrieveStudentsAsync(string filterClass)
		{
			var students = string.IsNullOrWhiteSpace(filterClass)
				? await this.unitOfWork.Students.GetAllAsync()
				: await this.unitOfWork.Students.GetByFilterAsync(s => s.ClassId == filterClass)
				?? throw new ArgumentNullException();

			var studentList = new List<StudentGetDTO>();

			foreach (var s in students)
			{
				studentList.Add(new StudentGetDTO
				{
					StudentId = s.StudentId,
					Ssn = s.Ssn,
					Surname = s.Surname,
					Name = s.Name,
					ClassId = s.ClassId ?? string.Empty,
					DateEnrolled = s.DateEnrolled,
					DateQuit = s.DateQuit,
					IsActive = s.IsActive,
				});
			}

			return studentList;
		}

		public async void Create(StudentCreateDTO newStudent)
		{
			try
			{
				this.unitOfWork.Students.Add(new()
				{
					Ssn = newStudent.Ssn,
					Surname = newStudent.Surname,
					Name = newStudent.Name,
					ClassId = newStudent.ClassId,
					DateEnrolled = newStudent.DateEnrolled,
					DateQuit = null,
					IsActive = newStudent.DateEnrolled < DateOnly.FromDateTime(DateTime.Now),
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
