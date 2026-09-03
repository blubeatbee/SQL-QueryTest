using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a list of employees.
	/// </summary>
	/// <param name="employeeService">The service pattern object that accesses the employee table.</param>
	public class EmployeesPage(IEmployeeService employeeService) : BasePage
	{
		private readonly IEmployeeService service = employeeService;

		private bool pageContentAscending = true;
		private short filterRole;
		private Dictionary<string, int> employeeAmount = new()
		{
			{ "Teacher", 0 },
			{ "Administrator", 0 },
			{ "Principal", 0 },
		};

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text($"No Data Found."),
			new NavLink("Return", "Title"),
			new NavLink("Add new employee", "NewEmployee"),
			new Text(),
			new Text(
				$"{"ID", 5} | {"SSN", -13} | {"Surname", -16} | {"Name", -32} | {"Role", -16} | " +
				$"{"Salary",-12} | {"Hired on",-10} | {"Quit on",-10} | {"Active",-6} |"),
			new Text(" Number of Currently Employed Gymnasium Personnel")
		]);


		public sealed override IList<BaseComponent> GetPageContent()
		{
			this.Count();

			var pageContent = new List<BaseComponent>();
			pageContent.AddRange(
				this.PageContent[1],
				this.PageContent[2],
				this.PageContent[3],
				this.PageContent[5],
				new Text($" \tTeachers:       {this.employeeAmount["Teacher"]}"),
				new Text($" \tAdministrators: {this.employeeAmount["Administrator"]}"),
				new Text($" \tPrincipals:     {this.employeeAmount["Principal"]}"),
				this.PageContent[3],
				new Button($"Sort by: {(this.pageContentAscending ? "Ascending" : "Descending")}", ToggleSort),
				new Button($"Show All Employees", SetFilterToAll),
				new Button($"Show Teachers Only", SetFilterToTeacherOnly),
				new Button($"Show Admins Only", SetFilterToAdminOnly),
				new Button($"Show Principals Only", SetFilterToPrincipalOnly),
				this.PageContent[3],
				this.PageContent[4],
				this.PageContent[3]
			);

			try
			{
				var employeeList = this.service.RetrieveEmployeesByRoleAsync(this.filterRole).Result.ToList();
				if (!this.pageContentAscending)
				{
					employeeList.Reverse();
				}

				foreach (var e in employeeList)
				{
					pageContent.Add(new DataLink(
						$"{e.EmployeeId,4} | {e.Ssn,-13} | {e.Surname,-16} | {e.Name,-32} | {e.Role,-12} |" +
						$"{e.Salary,12} | {e.DateHired,-10} | {e.DateQuit,-10} | {e.IsEmployed,-6} |",
						e.EmployeeId,
						"Employee"
					));
				}
			}
			catch
			{
				pageContent.Add(this.PageContent[0]);
			}

			return pageContent;
		}

		/// <summary> Used in toggling between a descended or ascended ordered list. </summary>
		private void ToggleSort()
		{
			this.pageContentAscending = !pageContentAscending;
			this.GetPageContent();
		}

		private void SetFilterToAll()
		{
			this.filterRole = 0;
			this.GetPageContent();
		}
		private void SetFilterToTeacherOnly()
		{
			this.filterRole = 1;
			this.GetPageContent();
		}
		private void SetFilterToAdminOnly()
		{
			this.filterRole = 2;
			this.GetPageContent();
		}
		private void SetFilterToPrincipalOnly()
		{
			this.filterRole = 3;
			this.GetPageContent();
		}

		private void Count()
		{
			this.employeeAmount["Teacher"] = this.service.NumberOfActiveEmployees(1).Result;
			this.employeeAmount["Administrator"] = this.service.NumberOfActiveEmployees(2).Result;
			this.employeeAmount["Principal"] = this.service.NumberOfActiveEmployees(3).Result;
		}
	}
}
