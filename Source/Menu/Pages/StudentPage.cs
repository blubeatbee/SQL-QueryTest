using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a, and allows for the writing of a, single student.
	/// </summary>
	/// <param name="studentServices">The service pattern object that accesses student table.</param>
	public class StudentPage(IStudentService studentServices) : BasePage
	{
		private readonly IStudentService service = studentServices;

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
				var s = service.RetrieveStudentAsync(App.Id).Result;
				pageContent.Add(new Text(
					$"\n         {"ID"}: {s.StudentId}" +
					$"\n        {"SSN"}: {s.Ssn}" +
					$"\n       {"Name"}: {s.Surname}{s.Name}" +
					$"\n      {"Class"}: {s.ClassId}" +
					$"\n{"Enroll date"}: {s.DateEnrolled}" +
					$"\n  {"Quit date"}: {(s.DateQuit != null ? s.DateQuit : "--/--/----")}" +
					$"\n     {"Active"}: {s.IsActive}"
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
