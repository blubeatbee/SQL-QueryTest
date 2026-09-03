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
	public class CreateEmployeeFormPage(IEmployeeService service) : BasePage
	{
		private readonly IEmployeeService service = service;

		private Dictionary<string, string?> newEmployeeValues = new()
		{
			{ "Ssn", string.Empty },
			{ "Surname", string.Empty },
			{ "Name", string.Empty },
			{ "Role", string.Empty },
			{ "Salary", string.Empty },
			{ "Tasks", string.Empty},
		};

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new NavLink("Cancel", "Employees"),
			new Text()
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			List<BaseComponent> pageContent = [
				.. this.PageContent,
				new Button($"SSN:        {newEmployeeValues["Ssn"]}", AddSsn),
				new Button($"Surname:    {newEmployeeValues["Surname"]}", AddSurname),
				new Button($"Given name: {newEmployeeValues["Name"]}", AddName),
				new Button($"Role:       {newEmployeeValues["Role"]}", AddRole),
				new Button($"Salary:     {newEmployeeValues["Salary"]}", AddSalary),
				new Button($"Tasks:      {newEmployeeValues["Tasks"]}", AddTasks),
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
		private void AddName()
		{
			this.newEmployeeValues["Name"] = Input.ToString<string>("Input employee's given and middle names.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddRole()
		{
			var result = PageReader.Vertical([
				new Button("Teacher", () => { }),
				new Button("Administrator", () => { }),
				new Button("Principal", () => { })
				]);

			this.newEmployeeValues["Role"] = result!.ToString() ?? "0";
			_ = this.GetPageContent();
		}
		private void AddSalary()
		{
			this.newEmployeeValues["Salary"] = Input.ToString<decimal>("Input employee's salary.", 1, 11);
			_ = this.GetPageContent();
		}
		private void AddTasks()
		{
			this.newEmployeeValues["Tasks"] = Input.ToString<string>("Input employee tasks.", 1, 500);
			_ = this.GetPageContent();
		}

		private void CreateNewEmployee()
		{
			if (!decimal.TryParse(this.newEmployeeValues["Salary"], out var salary))
			{
				salary = 0;
			}

			var employeeRole = this.newEmployeeValues["Role"] switch
			{
				"Teacher" => 1,
				"Administrator" => 2,
				"Principal" => 3,
				_ => 1
			};

			try
			{
				this.service.Create(new()
				{
					Ssn = this.newEmployeeValues["Ssn"]!,
					Surname = this.newEmployeeValues["Surname"]!,
					Name = this.newEmployeeValues["Name"]!,
					RoleId = employeeRole,
					Salary = salary,
					Tasks = this.newEmployeeValues["Tasks"],
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
