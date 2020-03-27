using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointingPokerPlus.Server.Data;

namespace PointingPokerPlus.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//BuildWebHost(args).Run();
			//1. Get the IWebHost which will host this application.
			var host = BuildWebHost(args);

			//2. Find the service layer within our scope.
			using (var scope = host.Services.CreateScope())
			{
				//3. Get the instance of BoardGamesDBContext in our services layer
				var services = scope.ServiceProvider;
				var context = services.GetRequiredService<PPPDBContext>();

				//4. Call the DataGenerator to create sample data
				DataGenerator.Initialize(services);
			}

			//Continue to run the application
			host.Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseConfiguration(new ConfigurationBuilder()
					.AddCommandLine(args)
					.Build())
				.UseStartup<Startup>()
				.Build();
	}
}
