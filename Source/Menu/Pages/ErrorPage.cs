using Source.Menu.Components;
using Source.Menu.Components.Base;
using Source.Menu.Pages.Base;

namespace Source.Menu.Pages
{
	public sealed class ErrorPage : BasePage
	{
		private ErrorPage() { }
		public static ErrorPage Instance => Nested.instance;
		private static class Nested
		{
			internal static readonly ErrorPage instance = new();
		}

		protected sealed override IList<BaseComponent> PageContent { get; set; } = new List<BaseComponent>([
			new Text($"Error! The page you're trying to reach does not exist."),
			]);

		public override IList<BaseComponent> GetPageContent()
		{
			return PageContent;
		}

		public void UpdateErrorText(string newText)
		{
			PageContent.Prepend(new Text($"{newText}"));
			this.PageContent.ElementAt(1);
		}
	}
}
