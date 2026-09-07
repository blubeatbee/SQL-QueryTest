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
			new NavLink("Add New Grading to Student", "CreateGrade"),
			new Text(),
			new Text($" {"Date Set",-10} | {"Grade",-5} | {"Course",-16} | {"Set by Teacher",-40}")
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			var pageContent = new List<BaseComponent>();
			pageContent.AddRange([
				this.PageContent[1],
				]);

			try
			{
				var s = service.RetrieveStudentAsync(App.Id).Result;
				pageContent.AddRange([
					new Text(
						$"\n         {"ID"}: {s.StudentId}" +
						$"\n        {"SSN"}: {s.Ssn}" +
						$"\n       {"Name"}: {s.Surname} {s.Name}" +
						$"\n      {"Class"}: {s.ClassId}" +
						$"\n{"Enroll date"}: {s.DateEnrolled}" +
						$"\n  {"Quit date"}: {(s.DateQuit != null ? s.DateQuit : "--/--/----")}" +
						$"\n     {"Active"}: {s.IsActive}"
					),
					this.PageContent[2],
					this.PageContent[3],
					this.PageContent[2],
					]);

				foreach (var g in s.Grades)
				{
					pageContent.Add(new Text(
						$" {g.DateSet,-10} | {g.Grading,-5} | {g.CourseTitle,-16} | {g.TeacherName,-40}"
						));
				}
			}
			catch
			{
				pageContent.Add(this.PageContent[0]);
			}

			return pageContent;
		}
	}
}
