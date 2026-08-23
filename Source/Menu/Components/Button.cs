using Source.Menu.Components.Base;

namespace Source.Menu.Components
{
	public class Button(string label, Action action) : BaseSelectable
	{
		protected sealed override string Label { get; } = label;
		protected Action Act { get; } = action;
		public override void OnSelect()
		{
			this.Act();
		}
	}
}
