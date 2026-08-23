using Source.Menu.Components.Base;

namespace Source.Menu.UI
{
	/// <summary>
	///		Provides console GUIs for navigating menu pages.
	///		<list type="bullet">
	///			<item>
	///				<term>Properties</term>
	///				<description>Used for styling the menu. Changing a property affects <see cref="PageReader"/>.</description>
	///			</item>
	///			<item>
	///				<term>Methods</term>
	///				<description>Each method contains a different way to navigate a menu.</description>
	///			</item>
	///		</list>
	/// </summary>
	public static class PageReader
	{
		public static char Prefix { get; set; } = ' ';
		public static ConsoleColor Foreground { get; set; } = ConsoleColor.White;
		public static ConsoleColor Background { get; set; } = ConsoleColor.Black;
		public static char SelectedPrefix { get; set; } = '>';
		public static ConsoleColor SelectedForeground { get; set; } = ConsoleColor.Black;
		public static ConsoleColor SelectedBackground { get; set; } = ConsoleColor.White;

		public static BaseSelectable? Vertical(IList<BaseComponent> menuItems)
		{
			int i = 0;
			int j = 0;
			char selectMark;
			while (true)
			{
				Console.Clear();

				for (j = 0; j < menuItems.Count; j++)
				{
					while (menuItems[i] is not BaseSelectable)
					{
						i++;
					}
					selectMark = i == j ? SelectedPrefix : Prefix;
					Console.BackgroundColor = i == j ? SelectedBackground : Background;
					Console.ForegroundColor = i == j ? SelectedForeground : Foreground;
					if (menuItems[j] is BaseSelectable)
					{
						Console.WriteLine($"{selectMark}{menuItems[j]}");
					}
					else
					{
						Console.WriteLine($"{menuItems[j]}");
					}
					Console.BackgroundColor = SelectedForeground;
					Console.ForegroundColor = SelectedBackground;
				}

				ConsoleKey key = Console.ReadKey(true).Key;

				switch (key)
				{
					case ConsoleKey.DownArrow:
						do
						{
							i++;
							if (i > menuItems.Count - 1)
							{
								i = 0;
							}
						} while (menuItems[i] is not BaseSelectable);
						break;

					case ConsoleKey.UpArrow:
						do
						{
							i--;
							if (i < 0)
							{
								i = menuItems.Count - 1;
							}
						} while (menuItems[i] is not BaseSelectable);
						break;

					case ConsoleKey.Enter:
						return (BaseSelectable)menuItems[i];

					case ConsoleKey.Escape:
						return null;
				}
			}
		}
	}
}
