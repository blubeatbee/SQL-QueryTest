using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Core;
using Source.Menu.Pages.Base;
using Source.Menu.UI;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Page for adding new Gradings.
	/// </summary>
	/// <param name="appService">The services access object which accesses the unitofwork pattern.</param>
	public class AddGradingPage(AppService appService) : BasePage
	{
		private readonly AppService service = appService;

		private Dictionary<string, string?> newGradingValues = new()
		{
			{ "Grade", "" },
			{ "CourseId", "" },
			{ "TeacherId", "" },
		};

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new NavLink("Return", "Student"),
			new Text("Add values:"),
			new Text(),
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			var pageContent = new List<BaseComponent>([
				this.PageContent[0],
				this.PageContent[1],
				new Button($"     Add Grade: {this.newGradingValues["Grade"]}", AddValueGrade),
				new Button($" Add Course Id: {this.newGradingValues["CourseId"]}", AddValueCourseId),
				new Button($"Add Teacher Id: {this.newGradingValues["TeacherId"]}", AddValueTeacherId),
				new Button($"Create New Grading with these Values", CreateNewGrading)
				]);


			return pageContent;
		}


		private void AddValueGrade()
		{
			var result = PageReader.Vertical([
				new Button($"0", () => {}),
				new Button($"1", () => {}),
				new Button($"2", () => {}),
				new Button($"3", () => {}),
				new Button($"4", () => {}),
				new Button($"5", () => {}),
				]);
			this.newGradingValues["Grade"] = result!.ToString();
			_ = this.GetPageContent();
		}

		private void AddValueCourseId()
		{
			var courses = this.service.CourseService.RetrieveCoursesForGradingAsync().Result;

			var buttonCourses = new List<BaseComponent>();

			foreach (var c in courses)
			{
				buttonCourses.Add(new Text(
					$"{c.Courseid,4} - {c.Title}, {c.Class}"
					));
			}

			foreach (var c in courses)
			{
				buttonCourses.Add(new Button(
					$"{c.Courseid}", () => { }
					));
			}

			var result = PageReader.Vertical(buttonCourses);
			this.newGradingValues["CourseId"] = result!.ToString();
			_ = this.GetPageContent();
		}

		private void AddValueTeacherId()
		{
			var teachers = this.service.EmployeeService.RetrieveActiveTeachersAsync().Result;

			var buttonsTeacher = new List<BaseComponent>();

			foreach (var t in teachers)
			{
				buttonsTeacher.Add(new Text(
					$"{t.EmployeeId,4} - {t.Surname} {t.Name}"
					));
			}

			foreach (var t in teachers)
			{
				buttonsTeacher.Add(new Button(
					$"{t.EmployeeId}", () => { }
					));
			}

			var result = PageReader.Vertical(buttonsTeacher);
			this.newGradingValues["TeacherId"] = result!.ToString();
			_ = this.GetPageContent();
		}

		private void CreateNewGrading()
		{
			_ = int.TryParse(this.newGradingValues["Grade"], out var newGrade);
			_ = int.TryParse(this.newGradingValues["TeacherId"], out var newTeacherId);
			_ = int.TryParse(this.newGradingValues["CourseId"], out var newCourseId);

			try
			{
				service.StudentService.AddGradingToStudent(new()
				{
					Grade = newGrade,
					DateSet = DateOnly.FromDateTime(DateTime.Now),
					StudentId = App.Id,
					TeacherId = newTeacherId,
					CourseId = newCourseId,
				});
			}
			catch
			{
				Console.WriteLine("Operation invalid. Grading could not be added. Press anywhere to continue.");
				_ = Console.ReadKey();
				_ = this.GetPageContent();
				return;
			}

			Console.WriteLine("Grading was successful. Press anywhere to continue.");
			_ = Console.ReadKey();
			_ = this.GetPageContent();
		}
	}
}
