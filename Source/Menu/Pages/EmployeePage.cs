using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a, and allows for the writing of a, single employee.
	/// </summary>
	/// <param name="employeeService">The service pattern object that accesses employee table.</param>
	public class EmployeePage(IEmployeeService employeeService) : BasePage
	{
		private readonly IEmployeeService service = employeeService;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text($"Invalid Data"),
			new NavLink("Return", "Employees"),
			new Text()
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
				var e = service.RetrieveEmployeeAsync(App.Id).Result;

				pageContent.Add(new Text(
					$"\n       {"ID"}: {e.EmployeeId}" +
					$"\n      {"SSN"}: {e.Ssn}" +
					$"\n     {"Name"}: {e.Surname} {e.Name}" +
					$"\n   {"Salary"}: {e.Salary}" +
					$"\n     {"Role"}: {e.Role}" +
					$"\n   {"Salary"}: {e.Salary}" +
					$"\n    {"Tasks"}: {e.Tasks}" +
					$"\n{"Hire date"}: {e.DateHired}" +
					$"\n{"Quit date"}: {(e.DateQuit != null ? e.DateQuit : "--/--/----")}" +
					$"\n {"Employed"}: {e.IsEmployed}"
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
