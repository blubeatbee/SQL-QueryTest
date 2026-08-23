namespace Source.Menu.Components.Base
{
	/// <summary>
	///		Base menu component which should be inherited by all components that the user can navigate to
	///		and select, such as buttons and navigation links.
	/// </summary>
	/// <remarks>
	///		This class inherits <see cref="BaseComponent"/>.
	/// </remarks>
	public abstract class BaseSelectable : BaseComponent
	{
		/// <summary>
		///		A function that will run when the user selects this <see cref="BaseSelectable"/>.
		/// </summary>
		public abstract void OnSelect();
	}
}
