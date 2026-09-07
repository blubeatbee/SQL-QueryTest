using Source.DTO;
using Source.Menu.Core;
using Source.Services.IServices;

namespace Source.Services
{
	public class CourseService(IUnitOfWork unitOfWork) : ICourseService
	{
		private readonly IUnitOfWork unitOfWork = unitOfWork;

		public async Task<IList<CourseGetMiniDTO>> RetrieveCoursesForGradingAsync()
		{
			var courses = await this.unitOfWork.Courses.GetCoursesWithClassAsync()
				?? throw new ArgumentNullException();

			var courseList = new List<CourseGetMiniDTO>();

			try
			{
				foreach (var c in courses)
				{
					courseList.Add(new CourseGetMiniDTO()
					{
						Courseid = c.CourseId,
						Title = c.Title,
						Class = c.ClassId,
					});
				}
			}
			catch
			{
				throw;
			}

			return courseList;
		}

		public async Task<IList<CourseGetDTO>> RetrieveActiveCoursesAsync()
		{
			var currentDate = DateOnly.FromDateTime(DateTime.Now);

			var courses = await this.unitOfWork.Courses.GetCoursesWithClassAsync(c => c.DateEnd > currentDate && c.DateStart < currentDate)
				?? throw new ArgumentNullException();

			IList<CourseGetDTO> courseList = new List<CourseGetDTO>();

			try
			{
				foreach (var c in courses)
				{
					courseList.Add(new CourseGetDTO()
					{
						Title = c.Title,
						CourseStart = c.DateStart,
						CourseEnd = c.DateEnd,
						Class = c.ClassId,
						ClassStart = c.Class!.DateStart,
						ClassEnd = c.Class!.DateEnd,
					});
				}
			}
			catch
			{
				throw;
			}

			return courseList;
		}

		public async Task<int> NumberOfActiveCourses()
		{
			var currentDate = DateOnly.FromDateTime(DateTime.Now);
			try
			{
				return await this.unitOfWork.Courses
					.CountByFilterAsync(c => c.DateStart < currentDate && c.DateEnd > currentDate);
			}
			catch
			{
				return 0;
			}
		}

	}
}
