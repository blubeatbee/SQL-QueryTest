using Source.Menu.Components.Base;

namespace Source.Menu.Components
{
	public class NavLink(string label, string toPath) : BaseSelectable
	{
		protected sealed override string Label { get; } = label;
		protected string ToPath { get; set; } = toPath;

		public sealed override void OnSelect()
		{
			App.UpdateActiveRoute(this.ToPath);
		}
	}
}
