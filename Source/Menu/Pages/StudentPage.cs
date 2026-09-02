using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a, and allows for the writing of a, single student.
	/// </summary>
	/// <param name="studentServices">The service pattern object that accesses student table.</param>
	public class StudentPage(StudentService studentServices) : BasePage
	{
		private readonly StudentService service = studentServices;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text($"Invalid Data"),
			new NavLink("Return", "Students"),
			new Text(),
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			var pageContent = new List<BaseComponent>();
			pageContent.AddRange([
				this.PageContent[1],
				this.PageContent[2],
				]);

			try
			{
				var student = service.GetStudentAsync(App.Id).Result;
				pageContent.Add(new Text(
					$"\n          {"ID"}: {student.HumanId}" +
					$"\n         {"SSN"}: {student.Ssn.Insert(8, "-")}" +
					$"\n        {"Name"}: {student.Surname}{student.Forname}{student.Midname ?? null}" +
					$"\n       {"Class"}: {student.CyearId}{student.ClassId}" +
					$"\n {"Enroll date"}: {student.DateEnroll}" +
					$"\n   {"Quit date"}: {(student.DateQuit != null ? student.DateQuit : "--/--/----")}" +
					$"\n   {"Is Active"}: {student.IsActive}" +
					$"\n   {"Graduated"}: {student.IsGraduated ?? false}"
					));
			}
			catch
			{
				pageContent.Add(this.PageContent[0]);
			}

			return pageContent;
		}
	}
}
