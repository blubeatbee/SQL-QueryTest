using Source.Menu.Components.Base;

namespace Source.Menu.Components
{
	public class QuitButton(string label = "Quit") : BaseSelectable
	{
		protected override string Label { get; } = label;

		public override void OnSelect()
		{
			Environment.Exit(0);
		}
	}
}
