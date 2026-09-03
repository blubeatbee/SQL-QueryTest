using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Menu.UI;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Page for creating a new student.
	/// </summary>
	/// <param name="service">The service pattern object that accesses the student table.</param>
	public class CreateStudentFormPage(IStudentService service) : BasePage
	{
		private readonly IStudentService service = service;

		private Dictionary<string, string?> newStudentValues = new Dictionary<string, string?>()
		{
			{ "Ssn", string.Empty },
			{ "Surname", string.Empty },
			{ "Name", string.Empty },
			{ "ClassId", string.Empty },
		};

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new NavLink("Cancel", "Students"),
			new Text()
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			List<BaseComponent> pageContent = [
				.. this.PageContent,
				new Button($"SSN:         {newStudentValues["Ssn"]}", AddSsn),
				new Button($"Surname:     {newStudentValues["Surname"]}", AddSurname),
				new Button($"Given name:  {newStudentValues["Name"]}", AddName),
				new Button($"Class:       {newStudentValues["ClassId"]}", AddClass),
				new Text(),
				new Button($"Create new student", CreateNewStudent),
				];
			return pageContent;
		}

		private void AddSsn()
		{
			this.newStudentValues["Ssn"] = Input.ToString<int>("Input student's social security number.", 12, 12);
			_ = this.GetPageContent();
		}
		private void AddSurname()
		{
			this.newStudentValues["Surname"] = Input.ToString<string>("Input student's surname.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddName()
		{
			this.newStudentValues["Name"] = Input.ToString<string>("Input student's given and middle names.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddClass()
		{
			var classId = PageReader.Vertical([
				new Button("2024 EST", () => {}),
				new Button("2024 NAT", () => {}),
				new Button("2025 EST", () => {}),
				new Button("2025 NAT", () => {}),
				]);
			this.newStudentValues["ClassId"] = classId!.ToString().Replace(" ", "");

			_ = this.GetPageContent();
		}

		private void CreateNewStudent()
		{
			try
			{
				this.service.Create(new()
				{
					Ssn = this.newStudentValues["Ssn"]!,
					Surname = this.newStudentValues["Surname"]!,
					Name = this.newStudentValues["Name"]!,
					ClassId = this.newStudentValues["Class"]!,
					DateEnrolled = DateOnly.FromDateTime(DateTime.Now),
				});
			}
			catch (InvalidOperationException)
			{
				Console.WriteLine("Operation invalid. Could not create new student. Press anywhere to continue.");
				_ = Console.ReadKey();
				_ = this.GetPageContent();
				return;
			}

			Console.WriteLine("New student created successfuly. Press anywhere to continue.");
			_ = Console.ReadKey();
			_ = this.GetPageContent();
		}
	}
}
