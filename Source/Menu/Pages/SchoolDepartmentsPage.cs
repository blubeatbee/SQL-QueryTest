using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	public class SchoolDepartmentsPage(IEmployeeService service) : BasePage
	{
		private readonly IEmployeeService service = service;

		private Dictionary<string, int> employeeAmount = new()
		{
			{ "Teacher", 0 },
			{ "Administrator", 0 },
			{ "Principal", 0 },
		};

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text("Number of currently employed Gymnasium personnel"),
			new Text(),
			new NavLink("Return", "Title")
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			this.Count();

			List<BaseComponent> pageContent = [
				this.PageContent[0],
				new Text($"Teachers:       {this.employeeAmount["Teacher"]}"),
				new Text($"Administrators: {this.employeeAmount["Administrator"]}"),
				new Text($"Principals:     {this.employeeAmount["Principal"]}"),
			];

			pageContent.AddRange(this.PageContent[1], this.PageContent[2]);

			return pageContent;
		}

		private void Count()
		{
			this.employeeAmount["Teacher"] = this.service.NumberOfActiveEmployees(1).Result;
			this.employeeAmount["Administrator"] = this.service.NumberOfActiveEmployees(2).Result;
			this.employeeAmount["Principal"] = this.service.NumberOfActiveEmployees(3).Result;
		}

	}
}
