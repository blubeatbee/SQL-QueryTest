using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a list of employees.
	/// </summary>
	/// <param name="employeeService">The service pattern object that accesses the employee table.</param>
	public class EmployeesPage(EmployeeService employeeService) : BasePage
	{
		private readonly EmployeeService service = employeeService;

		private bool pageContentAscending = true;
		private short filterEmployee;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text($"No Data Found."),
			new NavLink("Return", "Title"),
			new NavLink("Add new employee", "NewEmployee"),
			new Text(),
			new Text(
				$"{"ID", 5} | {"SSN", -13} | {"Surname", -16} | {"Name", -16} | {"Middle name", -16} | " +
				$"{"Role", -16} | {"Salary",-12} | {"Hired on",-10} | {"Quit on",-10} |"),
			]);


		public sealed override IList<BaseComponent> GetPageContent()
		{
			var pageContent = new List<BaseComponent>();
			pageContent.AddRange(
				this.PageContent[1],
				this.PageContent[2],
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
				var employeeList = this.service.GetAllEmployees(this.filterEmployee).Result.ToList();
				if (!this.pageContentAscending)
				{
					employeeList.Reverse();
				}

				foreach (var i in employeeList)
				{
					pageContent.Add(new DataLink(
						$"{i.HumanId,4} | {i.Ssn.Insert(8, "-"),-13} | {i.Surname,-16} | {i.Forname,-16} | {i.Midname ?? null,-16} | " +
						$"{i.Role ?? null,-16} | {i.Salary,12} | {i.DateHired,-10} | {i.DateQuit,-10} |",
						i.HumanId,
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
			this.filterEmployee = 0;
			this.GetPageContent();
		}
		private void SetFilterToTeacherOnly()
		{
			this.filterEmployee = 1;
			this.GetPageContent();
		}
		private void SetFilterToAdminOnly()
		{
			this.filterEmployee = 2;
			this.GetPageContent();
		}
		private void SetFilterToPrincipalOnly()
		{
			this.filterEmployee = 3;
			this.GetPageContent();
		}
	}
}
