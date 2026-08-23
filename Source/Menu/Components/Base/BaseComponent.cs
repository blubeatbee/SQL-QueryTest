namespace Source.Menu.Components.Base
{
	/// <summary>
	///		The base menu component which should be inherited by all menu components.
	/// </summary>
	/// <remarks>
	///		These components includes:
	///		<list type="bullet">
	///			<item>
	///				<term>Unselectable components</term>
	///				<description>e.g. empty spaces, text prompt.</description>
	///			</item>
	///			<item>
	///				<term>Selectable components</term>
	///				<description>e.g. buttons, navigation.</description>
	///			</item>
	///		</list>
	/// </remarks>
	public abstract class BaseComponent
	{
		/// <summary>
		///		The text that will be displayed in the menu page.
		/// </summary>
		protected abstract string Label { get; }

		/// <summary>
		///		Returns the label text instead of the whole <see cref="BaseComponent"/> as a string.
		/// </summary>
		/// <returns><see cref="Label"/></returns>
		public sealed override string ToString()
		{
			return this.Label;
		}
	}
}
