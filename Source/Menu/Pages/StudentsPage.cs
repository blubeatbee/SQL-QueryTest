using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a list of students.
	/// </summary>
	/// <param name="studentServices">The service pattern object that accesses the student table.</param>
	public class StudentsPage(StudentService studentServices) : BasePage
	{
		private readonly StudentService service = studentServices;

		private bool pageContentAscending = true;
		private string filterStudent = string.Empty;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text($"No Data Found"),
			new NavLink("Return", "Title"),
			new NavLink("Add new student", "NewStudent"),
			new Text(),
			new Text(
				$"{"ID", 5} | {"SSN", -13} | {"Surname", -16} | {"Name", -16} | {"Middle name", -16} | " +
				$"{"Class", -7} | {"Enrolled", -10} | {"Active?", -8} | {"Quit on", -10} | {"Graduated?", -8}"),
			]);

		public sealed override IList<BaseComponent> GetPageContent()
		{
			var pageContent = new List<BaseComponent>([
				this.PageContent[1],
				this.PageContent[2],
				this.PageContent[3],
				new Button($"Sort by {(this.pageContentAscending ? "Ascending" : "Descending")}", ToggleSort),
				new Button($"Show All Classes", SetFilterByNone),
				new Button($"Show Class 2024EST Only", SetFilterByClass2024EST),
				new Button($"Show Class 2024NAT Only", SetFilterByClass2024NAT),
				new Button($"Show Class 2024SAM Only", SetFilterByClass2024SAM),
				new Button($"Show Class 2025EST Only", SetFilterByClass2025EST),
				new Button($"Show Class 2025NAT Only", SetFilterByClass2025NAT),
				new Button($"Show Class 2025SAM Only", SetFilterByClass2025SAM),
				this.PageContent[3],
				this.PageContent[4],
				this.PageContent[3]
			]);

			try
			{
				var studentList = this.service.GetAllStudents(this.filterStudent).Result.ToList();
				if (!this.pageContentAscending)
				{
					studentList.Reverse();
				}

				foreach (var i in studentList)
				{
					pageContent.Add(new DataLink(
						$"{i.HumanId, 4} | {i.Ssn.Insert(8, "-"), -12} | {i.Surname, -16} | {i.Forname, -16} | {i.Midname ?? null, -16} | " +
						$"{i.CyearId, 4}{i.ClassId,3} | {i.DateEnroll, -10} | {i.IsActive, -8} | {i.DateQuit, -10} | {i.IsGraduated ?? false, -8}",
						i.HumanId,
						"Student"
					));
				}
			}
			catch
			{
				pageContent.Add(this.PageContent[0]);
			}

			return pageContent;
		}

		/// <summary>
		///		Used in toggling between a descended or ascended ordered list.
		/// </summary>
		private void ToggleSort()
		{
			this.pageContentAscending = !pageContentAscending;
			this.GetPageContent();
		}

		private void SetFilterByNone()
		{
			this.filterStudent = string.Empty;
			this.GetPageContent();
		}
		private void SetFilterByClass2024EST()
		{
			this.filterStudent = "2024EST";
			this.GetPageContent();
		}
		private void SetFilterByClass2024NAT()
		{
			this.filterStudent = "2024NAT";
			this.GetPageContent();
		}
		private void SetFilterByClass2024SAM()
		{
			this.filterStudent = "2024SAM";
			this.GetPageContent();
		}
		private void SetFilterByClass2025EST()
		{
			this.filterStudent = "2025EST";
			this.GetPageContent();
		}
		private void SetFilterByClass2025NAT()
		{
			this.filterStudent = "2025NAT";
			this.GetPageContent();
		}
		private void SetFilterByClass2025SAM()
		{
			this.filterStudent = "2025SAM";
			this.GetPageContent();
		}
	}
}
