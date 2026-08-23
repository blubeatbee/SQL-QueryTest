using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Source.Data;
using Source.Menu;

namespace Source
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<GymnasiumDbContext>();
			
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;TrustServerCertificate=True;");

			using (var context = new GymnasiumDbContext(optionsBuilder.Options))
			{

				context.Initialise();

			}
		}
	}
}
