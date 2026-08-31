using Source.Data;
using Source.Menu.Components;
using Source.Menu.Pages;
using Source.Menu.Routing;
using Source.Menu.UI;
using Source.Persistent;
using Source.Services;

namespace Source.Menu
{
	/// <summary>
	///		Serves as the entry point to the <see cref="Menu"/>.
	/// </summary>
	internal static class App
	{
		private static StudentService studentService = null!;
		private static EmployeeService employeeService = null!;

		/// <summary>The list of navigable routes. Navigating to a route displays its associated menu page.</summary>
		internal static Router routes = null!;

		/// <summary>The current route and menu page being displayed on console.</summary>
		private static Route activeRoute;

		/// <summary>
		///		A temporary id that should only be accessed via <see cref="DataLink"/> instances.
		///		This id is used to query a single data row from the database.
		/// </summary>
		internal static int Id { get; set; }

		/// <summary>
		///		Initialises the menu.
		/// </summary>
		/// <param name="dbContext">The database context. Cannot be decoupled from <see cref="GymnasiumDbContext"/>.</param>
		internal static void Initialise(this GymnasiumDbContext dbContext)
		{
			using (var unitOfWork = new UnitOfWork(dbContext))
			{
				App.studentService = new(unitOfWork);
				App.employeeService = new(unitOfWork);

				App.routes = new([
					new Route("Title", TitlePage.Instance),
					new Route("Students", new StudentsPage(studentService)),
					new Route("Student", new StudentPage(studentService)),
					new Route("NewStudent", new CreateStudentFormPage(studentService)),
					new Route("Employees", new EmployeesPage(employeeService)),
					new Route("Employee", new EmployeePage(employeeService)),
					new Route("NewEmployee", new CreateEmployeeFormPage(employeeService)),
					new Route("Departments", new SchoolDepartmentsPage(employeeService)),
					new Route("Error", ErrorPage.Instance)
					],
				"Error");

				App.activeRoute = routes.GetRoute("Title");

				Run();
			}

		}

		internal static void UpdateActiveRoute(string toPath)
		{
			try
			{
				activeRoute = routes.GetRoute(toPath);
			}
			catch (ArgumentNullException ex)
			{
				activeRoute = routes.GetErrorRoute();
			}
			catch (InvalidOperationException ex)
			{
				activeRoute = routes.GetErrorRoute();
			}
		}

		private static void Run()
		{
			while (true)
			{
				var selected = PageReader.Vertical(activeRoute.Page.GetPageContent());

				switch (selected)
				{
					case NavLink:
						selected.OnSelect();
						break;
					case DataLink:
						selected.OnSelect();
						break;
					case Button:
						selected.OnSelect();
						break;
					case QuitButton:
						selected.OnSelect();
						break;
					default:
						break;
				}
			}
		}
	}
}
