using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;
using Source.Services.IServices;

namespace Source.Menu.Pages
{
	/// <summary>
	///		Menu page that displays a list of students.
	/// </summary>
	/// <param name="studentServices">The service pattern object that accesses the student table.</param>
	public class StudentsPage(IStudentService studentServices) : BasePage
	{
		private readonly IStudentService service = studentServices;

		private bool pageContentAscending = true;
		private string filterClass = string.Empty;

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text($"No Data Found"),
			new NavLink("Return", "Title"),
			new NavLink("Add new student", "NewStudent"),
			new Text(),
			new Text(
				$"{"ID",5} | {"SSN",-13} | {"Surname",-16} | {"Name",-32} | {"Class",-7} | " +
				$"{"Enrolled",-10} | {"Quit on",-10} | {"Active",-6} |"),
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
				new Button($"Show Class 2025EST Only", SetFilterByClass2025EST),
				new Button($"Show Class 2025NAT Only", SetFilterByClass2025NAT),
				this.PageContent[3],
				this.PageContent[4],
				this.PageContent[3]
			]);

			try
			{
				var studentList = this.service.RetrieveStudentsAsync(this.filterClass).Result.ToList();
				if (!this.pageContentAscending)
				{
					studentList.Reverse();
				}

				foreach (var e in studentList)
				{
					pageContent.Add(new DataLink(
						$"{e.StudentId,4} | {e.Ssn,-13} | {e.Surname,-16} | {e.Name,-32} | {e.ClassId,-7} |" +
						$"{e.DateEnrolled,-10} | {e.DateQuit,-10} | {e.IsActive,-6} |",
						e.StudentId,
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
			this.filterClass = string.Empty;
			this.GetPageContent();
		}
		private void SetFilterByClass2024EST()
		{
			this.filterClass = "2024EST";
			this.GetPageContent();
		}
		private void SetFilterByClass2024NAT()
		{
			this.filterClass = "2024NAT";
			this.GetPageContent();
		}
		private void SetFilterByClass2025EST()
		{
			this.filterClass = "2025EST";
			this.GetPageContent();
		}
		private void SetFilterByClass2025NAT()
		{
			this.filterClass = "2025NAT";
			this.GetPageContent();
		}
	}
}
