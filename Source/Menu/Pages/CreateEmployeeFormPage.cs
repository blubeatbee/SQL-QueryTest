using Source.DTO;
using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Menu.UI;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Page for creating a new employee.
	/// </summary>
	/// <param name="service">The service pattern object that accesses the employee table.</param>
	public class CreateEmployeeFormPage(ICudService<int, EmployeeDto> service) : BasePage
	{
		private readonly ICudService<int, EmployeeDto> service = service;

		private IDictionary<string, string?> newEmployeeValues = new Dictionary<string, string?>()
		{
			{ "Ssn", string.Empty },
			{ "Surname", string.Empty },
			{ "Forname", string.Empty },
			{ "Midname", string.Empty },
			{ "Role", string.Empty },
			{ "Salary", string.Empty },
		};

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new NavLink("Cancel", "Employees"),
			new Text()
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			List<BaseComponent> pageContent = [
				.. this.PageContent,
				new Button($"SSN:         {newEmployeeValues["Ssn"]}", AddSsn),
				new Button($"Surname:     {newEmployeeValues["Surname"]}", AddSurname),
				new Button($"First name:  {newEmployeeValues["Forname"]}", AddForname),
				new Button($"Middle name: {newEmployeeValues["Midname"]}", AddMidname),
				new Button($"Role:        {newEmployeeValues["Role"]}", AddRole),
				new Button($"Salary:      {newEmployeeValues["Salary"]}", AddSalary),
				new Text(),
				new Button($"Create new employee", CreateNewEmployee),
			];
			return pageContent;
		}

		private void AddSsn()
		{
			this.newEmployeeValues["Ssn"] = Input.ToString<int>("Input employee's social security number.", 12, 12);
			_ = this.GetPageContent();
		}
		private void AddSurname()
		{
			this.newEmployeeValues["Surname"] = Input.ToString<string>("Input employee's surname.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddForname()
		{
			this.newEmployeeValues["Forname"] = Input.ToString<string>("Input employee's first name.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddMidname()
		{
			this.newEmployeeValues["Midname"] = Input.ToString<string>("Input employee's middle name.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddRole()
		{
			var result = PageReader.Vertical([
				new Button("Teacher", () => { }),
				new Button("Administrator", () => { }),
				new Button("Principal", () => { })
				]);

			this.newEmployeeValues["Role"] = result!.ToString() ?? null;
			_ = this.GetPageContent();
		}
		private void AddSalary()
		{
			this.newEmployeeValues["Salary"] = Input.ToString<int>("Input employee's salary.", 1, 11);
			_ = this.GetPageContent();
		}

		private void CreateNewEmployee()
		{
			int salary;
			if (!int.TryParse(this.newEmployeeValues["Salary"], out salary))
			{
				salary = 0;
			}

			try
			{
				this.service.CreateOneEntry(new EmployeeDto()
				{
					Ssn = this.newEmployeeValues["Ssn"]!,
					Surname = this.newEmployeeValues["Surname"]!,
					Forname = this.newEmployeeValues["Forname"]!,
					Midname = this.newEmployeeValues["Midname"]!,
					Role = this.newEmployeeValues["Role"],
					Salary = salary,
					DateHired = DateOnly.FromDateTime(DateTime.Now)
				});
			}
			catch (InvalidOperationException)
			{
				Console.WriteLine("Operation invalid. Could not create new employee. Press anywhere to continue.");
				_ = Console.ReadKey();
				_ = this.GetPageContent();
				return;
			}

			Console.WriteLine("New employee created successfuly. Press anywhere to continue.");
			_ = Console.ReadKey();
			_ = this.GetPageContent();
		}
	}
}
