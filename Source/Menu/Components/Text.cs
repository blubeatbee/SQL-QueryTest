using Source.Menu.Components.Base;

namespace Source.Menu.Components
{
	/// <summary>
	///     Represents an unselectable block of text in the console menu.
	///     <para>
	///			Useful for displaying whitespaces, prompts, instructions, ASCII title cards, etc.
	///     </para>
	/// </summary>
	public sealed class Text() : BaseComponent
	{
		protected sealed override string Label { get; } = string.Empty;

		/// <summary>
		///     <inheritdoc cref="Text"/>
		/// </summary>
		/// <param name="text">
		///		Text to be displayed in menu.
		///		<para>
		///			Leave empty to display white space.
		///		</para>
		///	</param>
		public Text(string text) : this()
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				text = string.Empty;
			}
			this.Label = text;
		}
	}
}
