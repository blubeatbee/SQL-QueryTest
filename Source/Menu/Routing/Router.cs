namespace Source.Menu.Routing
{
	/// <summary>
	///		Class for containing a list of <see cref="Route"/> and methods to enumerate through them.
	///		This class cannot be inherited.
	/// </summary>
	/// <param name="routes">The Route</param>
	/// <param name="error">The path to a Route in <paramref name="routes"/>. This</param>
	public sealed class Router(IList<Route> routes, string error)
	{
		private readonly string error = error;

		public IList<Route> Routes { get; } = routes;

		public Route GetErrorRoute()
		{
			return Routes.FirstOrDefault(r => r.Path == this.error);
		}

		public Route GetRoute(string path)
		{
			string[] paths = [path];
			try
			{
				return GetRoute(paths);
			}
			catch (ArgumentNullException)
			{
				throw;
			}
			catch (InvalidOperationException)
			{
				throw;
			}
		}

		public Route GetRoute(string[] paths)
		{
			int maxDepths = paths.Length;
			Route? route = Routes.FirstOrDefault(r => r.Path == paths[0]);
			//Console.WriteLine(route!);

			for (int i = 1; i < maxDepths; i++)
			{
				if (!route.HasValue)
				{
					throw new InvalidOperationException($"{nameof(paths)} returned a valueless object from {nameof(this.Routes)}.");
				}
				if (route.Value.ChildRoutes == null || route.Value.ChildRoutes.Count <= 0)
				{
					throw new InvalidOperationException($"{nameof(paths)} returned object from {nameof(this.Routes)} that has an empty or nulled {nameof(Route.ChildRoutes)}.");
				}

				route = route.Value.ChildRoutes.FirstOrDefault(r => r.Path == paths[i]);
				//Console.WriteLine(route);
			}

			// For testing
			//_ = Console.ReadKey();

			return route ?? GetErrorRoute();
		}

	}
}
