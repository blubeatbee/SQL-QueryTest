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
			//var config = new ConfigurationBuilder()
			//	.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			//	.AddJsonFile("appsettings.json")
			//	.Build();

			//var options = optionsBuilder.UseSqlServer(config.GetConnectionString("GymnasiumConnection2"));

			using (var context = new Gymnasium2Context())
			{
				context.Initialise();
			}

			//var optionsBuilder = new DbContextOptionsBuilder<GymnasiumDbContext>();

			//using (var context = new GymnasiumDbContext())
			//{
			//	context.Initialise();
			//}
		}
	}
}
