using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;

namespace Source.Menu.Pages
{
	public class TitlePage : BasePage
	{
		private TitlePage() { }
		public static TitlePage Instance => Nested.instance;
		private static class Nested
		{
			internal static readonly TitlePage instance = new();
		}

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text("Welcome to Gymnasium Database!\n" +
				"Press Arrow keys to navigate and Enter to select."),
			new Text(),
			new NavLink("View Students", "Students"),
			new NavLink("View Employees", "Employees"),
			new NavLink("View Courses", "Courses"),
			new QuitButton(),
			]
		);

		public override IList<BaseComponent> GetPageContent()
		{
			return PageContent;
		}
	}
}
