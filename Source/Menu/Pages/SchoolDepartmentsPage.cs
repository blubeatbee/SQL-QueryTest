using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	public class SchoolDepartmentsPage(ICourseService service) : BasePage
	{
		private readonly ICourseService service = service;

		private int courseAmount;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new NavLink("Return", "Title"),
			new Text(),
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			this.Count();

			List<BaseComponent> pageContent = [
				this.PageContent[0],
				this.PageContent[1],
				new Text($" Number of Currently Active Courses: {this.courseAmount}"),
				this.PageContent[1],
				new Text($" {"Course",-18} | {"Course Period",-24} | {"Class",-7} | {"Class Period",-24} "),
				this.PageContent[1],
			];

			var courses = this.service.RetrieveActiveCoursesAsync().Result.ToList();

			foreach (var c in courses)
			{
				pageContent.Add(new Text(
					$" {c.Title,-18} | {c.CourseStart + " - " + c.CourseEnd,-24} | {c.Class,-7} | {c.ClassStart + " - " + c.ClassEnd,-10} "
					));
			}

			return pageContent;
		}

		private void Count()
		{
			this.courseAmount = this.service.NumberOfActiveCourses().Result;
		}

	}
}
