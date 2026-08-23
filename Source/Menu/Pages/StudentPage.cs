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
		private StudentService service = studentServices;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new NavLink("Return", "Students"),
			new Text(),
			new Text(
				$"{"ID", 5} | {"SSN", -13} | {"Surname", -16} | {"Name", -16} | {"Middle name", -16} | " +
				$"{"Class", -7} | {"Enrolled", -10} | {"Active?", -8} | {"Quit on", -10} | {"Graduated?", -8}"),
			new Text()
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			var pageItems = new List<BaseComponent>();
			pageItems.AddRange([.. this.PageContent]);

			try
			{
				var student = service.GetStudent(App.Id).Result;
				pageItems.Add(new Text(
					$"{student.HumanId,4} | {student.Ssn.Insert(8, "-"),-12} | {student.Surname,-16} | {student.Forname,-16} | {student.Midname ?? null,-16} | " +
					$"{student.CyearId,4}{student.ClassId,3} | {student.DateEnroll,-10} | {student.IsActive,-8} | {student.DateQuit,-10} | {student.IsGraduated,-8}"
					));
			}
			catch
			{
				pageItems.Add(new Text($"Invalid data"));
			}

			return pageItems;
		}
	}
}
