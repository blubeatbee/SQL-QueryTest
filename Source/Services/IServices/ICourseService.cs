using Source.DTO;

namespace Source.Services.IServices
{
	public interface ICourseService
	{
		Task<IList<CourseGetMiniDTO>> RetrieveCoursesForGradingAsync();
		Task<IList<CourseGetDTO>> RetrieveActiveCoursesAsync();
		Task<int> NumberOfActiveCourses();
	}
}
