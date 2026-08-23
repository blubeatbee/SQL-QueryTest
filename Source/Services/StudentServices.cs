using Microsoft.EntityFrameworkCore;
using Source.DTO;
using Source.Models;
using Source.Repositories;
using Source.Services.IServices;

namespace Source.Services
{
	public sealed class StudentService(StudentRepository studentRepo, HumanRepository humanRepo) : IService, ICudService<int, StudentDto>
	{
		private readonly StudentRepository studentRepo = studentRepo;
		private readonly HumanRepository humanRepo = humanRepo;

		public async Task<StudentDto> GetStudent(int id)
		{
			var h = await humanRepo.FindOne(id) ?? throw new ArgumentNullException();
			var s = await studentRepo.FindOne(id) ?? throw new ArgumentNullException();

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
			var humansQuery = humanRepo.FindAll() ?? throw new ArgumentNullException();

			humansQuery = humansQuery.Where(h => h.Student != null);

			if (!string.IsNullOrWhiteSpace(filter))
			{
				return await humansQuery.AsNoTracking()
					.Select(h => new StudentDto
					{
						HumanId = h.HumanId,
						Ssn = h.Ssn,
						Surname = h.Surname,
						Forname = h.Forname,
						Midname = h.Midname,
						CyearId = h.Student!.CyearId,
						ClassId = h.Student!.ClassId,
						DateEnroll = h.Student!.DateEnroll,
						IsActive = h.Student!.IsActive,
						DateQuit = h.Student!.DateQuit,
						IsGraduated = h.Student!.IsGraduated,
					})
					.Where(h => (h.CyearId + h.ClassId) == filter)
					.ToListAsync();
			}

			return await humansQuery.AsNoTracking()
				.Select(h => new StudentDto
				{
					HumanId = h.HumanId,
					Ssn = h.Ssn,
					Surname = h.Surname,
					Forname = h.Forname,
					Midname = h.Midname,
					CyearId = h.Student!.CyearId,
					ClassId = h.Student!.ClassId,
					DateEnroll = h.Student!.DateEnroll,
					IsActive = h.Student!.IsActive,
					DateQuit = h.Student!.DateQuit,
					IsGraduated = h.Student!.IsGraduated,
				})
				.ToListAsync();
		}

		public async void CreateOneEntry(StudentDto newStudent)
		{
			var resultId = await humanRepo.FindId(newStudent.Ssn);

			if (resultId == -1)
			{
				try
				{
					await this.humanRepo.Insert(new Human()
					{
						Ssn = newStudent.Ssn,
						Surname = newStudent.Surname,
						Midname = newStudent.Midname,
						Forname = newStudent.Forname,
						Age = newStudent.Age,
					});

					var newId = await humanRepo.FindId(newStudent.Ssn);

					await this.studentRepo.Insert(new Student()
					{
						StudentId = newId,
						ClassId = newStudent.ClassId,
						CyearId = newStudent.CyearId,
						DateEnroll = newStudent.DateEnroll,
						DateQuit = newStudent.DateQuit,
						IsActive = newStudent.DateQuit == null ? true : false,
						IsGraduated = newStudent.IsGraduated,
					});
				}
				catch
				{
					throw;
				}
			}
		}
		public async void DeleteStudent(int id)
		{
			var student = await studentRepo.FindOne(id)
				?? throw new ArgumentNullException(nameof(id), $"No student with that {nameof(id)} exists.");

			// GymnasiumDbContext.cs: OnDelete behaviour set to DeleteBehavior.Cascade
			// which means the equivalent entry in Human table should be deleted also.
			await studentRepo.Delete(student);

			if (await humanRepo.Exists(id))
			{
				throw new InvalidOperationException($"The equivalent entry with {nameof(id)} was not deleted in the Human table.");
			}

			return;
		}
		public async void UpdateStudent(int id, StudentDto updatedStudent)
		{
			var human = await humanRepo.FindOne(id)
				?? throw new ArgumentNullException(nameof(id), $"No human with that {nameof(id)} exists.");
			var student = await studentRepo.FindOne(id)
				?? throw new ArgumentNullException(nameof(id), $"No student with that {nameof(id)} exists.");

			human.Ssn = updatedStudent.Ssn;
			human.Surname = updatedStudent.Surname;
			human.Midname = updatedStudent.Midname;
			human.Forname = updatedStudent.Forname;
			human.Age = updatedStudent.Age;

			await humanRepo.Update(human);

			student.ClassId = updatedStudent.ClassId;
			student.CyearId = updatedStudent.CyearId;
			student.DateEnroll = updatedStudent.DateEnroll;
			student.DateQuit = updatedStudent.DateQuit;
			student.IsActive = updatedStudent.DateQuit == null ? true : false;
			student.IsGraduated = updatedStudent.IsGraduated;

			await studentRepo.Update(student);

			return;
		}

	}
}
