using Source.Menu.Components.Base;

namespace Source.Menu.Components
{
	/// <summary>
	///		A special type of <see cref="NavLink"/> that should be used by lists of 
	///		Navigates to a new page and saves a number that will be used by <see cref="App.Id"/>
	///		in identifying a data row.
	/// </summary>
	/// <remarks>
	///		This
	/// </remarks>
	/// <param name="label">The data row's information that will be displayed on menu page.</param>
	/// <param name="id">The data row's associated id property.</param>
	/// <param name="toPath">Link to a menu page.</param>
	public class DataLink(string label, int id, string toPath) : BaseSelectable
	{
		protected sealed override string Label { get; } = label;
		protected int Id { get; } = id;
		protected string ToPath { get; set; } = toPath;

		public override void OnSelect()
		{
			App.Id = this.Id;
			App.UpdateActiveRoute(ToPath);
		}
	}
}
