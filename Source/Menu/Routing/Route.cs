using Source.Menu.Pages.Base;

namespace Source.Menu.Routing
{
	/// <summary>
	///		Represents a single navigable page in the console menu.
	/// </summary>
	/// <remarks>
	///		A <see cref="Route"/> consists of:
	///		<list type="bullet">
	///		<item>
	///			<term><see cref="Page"/></term>
	///			<description>The actual navigable menu page which is identified by <see cref="Path"/>.</description>
	///		</item>
	///		<item>
	///			<term><see cref="Path"/></term>
	///			<description>A <see langword="string"/> which serves to identify this <see cref="Route"/>. Max length is 16 chars.</description>
	///		</item>
	///		<item>
	///			<term><see cref="ChildRoutes"/></term>
	///			<description>
	///			<b>Optional.</b> A <see cref="Route"/> <see cref="ICollection{T}"/> which represents other pages this <see cref="Page"/> can link to.
	///			A <see cref="Route"/> can have many <b>Child Routes</b>, but can only have one <b>Parent Route</b>.
	///			</description>
	///		</item>
	///		<item>
	///			<term><see cref="ParentPath"/></term>
	///			<description>
	///			<b>Optional.</b> A <see langword="string"/> which is used to identify this <see cref="Route"/> as a child of another <see cref="Route"/>.
	///			When a <see cref="Route"/> is initialised via <see cref="ChildRoutes"/>, its <see cref="ParentPath"/> will automatically be set to its <b>parent's</b> <see cref="Path"/>.
	///			</description>
	///		</item>
	///		</list> 
	/// </remarks>
	public readonly struct Route
	{
		private const int max = 16;

		///	<inheritdoc cref="Route"/>
		/// <param name="path">serves to identify this <see cref="Route"/>. Max length is 16 chars.</param>
		/// <param name="page">The actual navigable menu page which is identified by <paramref name="path"/>.</param>
		public Route(string path, BasePage page)
		{
			Path = path.Length < max ? path : path.Trim().Substring(0, max);
			Page = page;
		}
		/// <inheritdoc cref="Route"/>
		/// <param name="path">serves to identify this <see cref="Route"/>.</param>
		/// <param name="page">The actual navigable menu page which is identified by <paramref name="path"/>.</param>
		/// <param name="childRoutes"><b>Optional.</b> A <see cref="Route"/> <see cref="ICollection{T}"/> which represents other pages this <paramref name="page"/> can link to.</param>
		public Route(string path, BasePage page, ICollection<Route> childRoutes)
		{
			path = path.Length < max ? path : path.Trim().Substring(0, max);

			ICollection<Route> newChildRoutes = [];

			foreach (var child in childRoutes)
			{
				// Uses deconstruction to populate the Route.Children list
				var (cPath, cPage, _, cChildren) = child;

				cPath = cPath.Length < max ? cPath : cPath.Trim().Substring(0, max);

				if (cChildren == null)
				{
					newChildRoutes.Add(new Route(path: cPath, parentPath: path, page: cPage));
				}
				else
				{
					newChildRoutes.Add(new Route(path: cPath, parentPath: path, page: cPage, childRoutes: cChildren));
				}
			}

			Path = path;
			Page = page;
			ChildRoutes = newChildRoutes;
		}
		private Route(string path, string parentPath, BasePage page)
		{
			Path = path;
			Page = page;
			ParentPath = parentPath;
		}
		private Route(string path, string parentPath, BasePage page, ICollection<Route> childRoutes)
		{
			Path = path;
			Page = page;
			ParentPath = parentPath;
			ChildRoutes = childRoutes;
		}

		public string Path { get; }
		public BasePage Page { get; }
		public string? ParentPath { get; }
		public ICollection<Route>? ChildRoutes { get; }

		/// <summary>
		///		Returns <see cref="Path"/>, <see cref="ParentPath"/> and each <see cref="ChildRoutes"/>.Path as string.
		/// </summary>
		/// <returns>A string.</returns>
		public override string ToString()
		{
			string? text = null;
			if (ChildRoutes != null)
			{
				foreach (var child in ChildRoutes)
				{
					text += $" {child.Path}";
				}
			}
			return string.Create(null, stackalloc char[256], $"[{nameof(Path)}: {Path,max}   {nameof(ParentPath)}:  {ParentPath ?? "---",max}   {nameof(ChildRoutes)}:{text ?? " ---"}]");
		}

		public void Deconstruct(
			out string path,
			out BasePage page,
			out string? parentPath,
			out ICollection<Route>? children)
		{
			path = Path;
			page = Page;
			parentPath = ParentPath;
			children = ChildRoutes;
		}
	}
}
