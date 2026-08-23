using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a, and allows for the writing of a, single employee.
	/// </summary>
	/// <param name="employeeService">The service pattern object that accesses employee table.</param>
	public class EmployeePage(EmployeeService employeeService) : BasePage
	{
		private EmployeeService service = employeeService;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new NavLink("Return", "Employees"),
			new Text(),
			new Text(
				$"{"ID", 5} | {"SSN", -13} | {"Surname", -16} | {"Name", -16} | {"Middle name", -16} | " +
				$"{"Role", -16} | {"Salary",-12} | {"Hired on",-10} | {"Quit on",-10} |"),
			new Text()
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			var pageContent = new List<BaseComponent>();
			pageContent.AddRange([.. this.PageContent]);

			try
			{
				var employee = service.GetEmployee(App.Id).Result;
				pageContent.Add(new Text(
					$"{employee.HumanId,4} | {employee.Ssn.Insert(8, "-"),-13} | {employee.Surname,-16} | {employee.Forname,-16} | {employee.Midname ?? null,-16} | " +
					$"{employee.Role ?? null,-16} | {employee.Salary,12} | {employee.DateHired,-10} | {employee.DateQuit,-10} |"
					));
			}
			catch
			{
				pageContent.Add(new Text("Invalid data"));
			}

			return pageContent;
		}
	}
}
