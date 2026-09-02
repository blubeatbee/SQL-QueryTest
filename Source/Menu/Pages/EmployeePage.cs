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
		private readonly EmployeeService service = employeeService;

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
				var employee = service.GetEmployee(App.Id).Result;

				pageContent.Add(new Text(
					$"\n          {"ID"}: {employee.HumanId}" +
					$"\n         {"SSN"}: {employee.Ssn.Insert(8, "-")}" +
					$"\n        {"Name"}: {employee.Surname}{employee.Forname}{employee.Midname ?? null}" +
					$"\n      {"Salary"}: {employee.Salary}" +
					$"\n        {"Role"}: {employee.Role}" +
					$"\n       {"Tasks"}: {null}" +
					$"\n   {"Hire date"}: {employee.DateHired}" +
					$"\n {"Is Employed"}: {employee.IsEmployed}" +
					$"\n   {"Quit date"}: {(employee.DateQuit != null ? employee.DateQuit : "--/--/----")}"
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
