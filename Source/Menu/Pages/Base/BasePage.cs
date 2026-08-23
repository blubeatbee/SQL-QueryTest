using Source.Menu.Components.Base;

namespace Source.Menu.Pages.Base
{
	/// <summary>
	///		The base page which all other menu pages inherits from.
	/// </summary>
	public abstract class BasePage
	{
		/// <summary>
		///		Contains the contents of the menu page, such as text, buttons, navigation, etc.
		/// </summary>
		protected abstract IList<BaseComponent> PageContent { get; set; }

		/// <summary>
		///		Returns the page content, which should be displayed on console using <see cref="UI.PageReader"/>.
		/// </summary>
		/// <returns>
		///		<see cref="PageContent"/>.
		/// </returns>
		public abstract IList<BaseComponent> GetPageContent();
	}
}
