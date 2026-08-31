using Source.DTO;
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
	public class CreateStudentFormPage(IService<int, StudentDto> service) : BasePage
	{
		private readonly IService<int, StudentDto> service = service;

		private IDictionary<string, string?> newStudentValues = new Dictionary<string, string?>()
		{
			{ "Ssn", string.Empty },
			{ "Surname", string.Empty },
			{ "Forname", string.Empty },
			{ "Midname", string.Empty },
			{ "ClassId", string.Empty },
			{ "CYearId", string.Empty },
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
				new Button($"First name:  {newStudentValues["Forname"]}", AddForname),
				new Button($"Middle name: {newStudentValues["Midname"]}", AddMidname),
				new Button($"Class:       {newStudentValues["CYearId"]+newStudentValues["ClassId"]}", AddClass),
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
		private void AddForname()
		{
			this.newStudentValues["Forname"] = Input.ToString<string>("Input student's first name.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddMidname()
		{
			this.newStudentValues["Midname"] = Input.ToString<string>("Input student's middle name.", 1, 50);
			_ = this.GetPageContent();
		}
		private void AddClass()
		{
			var classId = PageReader.Vertical([
				new Button("2024 EST", () => {}),
				new Button("2024 NAT", () => {}),
				new Button("2024 SAM", () => {}),
				new Button("2025 EST", () => {}),
				new Button("2025 NAT", () => {}),
				new Button("2025 SAM", () => {})
				]);
			var classArray = classId!.ToString().Split(" ");

			this.newStudentValues["CYearId"] = classArray[0];
			this.newStudentValues["ClassId"] = classArray[1];
			_ = this.GetPageContent();
		}

		private void CreateNewStudent()
		{
			try
			{
				this.service.Create(new StudentDto()
				{
					Ssn = this.newStudentValues["Ssn"]!,
					Surname = this.newStudentValues["Surname"]!,
					Forname = this.newStudentValues["Forname"]!,
					Midname = this.newStudentValues["Midname"],
					ClassId = this.newStudentValues["ClassId"]!,
					CyearId = this.newStudentValues["CYearId"]!,
					DateEnroll = DateOnly.FromDateTime(DateTime.Now),
					IsActive = true,
					DateQuit = null,
					IsGraduated = null
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
