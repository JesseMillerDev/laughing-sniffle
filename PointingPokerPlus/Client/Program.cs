using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;
using Fluxor;

namespace PointingPokerPlus.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			var currentAssembly = typeof(Program).Assembly;

			builder.Services.AddFluxor(options => options.ScanAssemblies(currentAssembly));
			builder.RootComponents.Add<App>("app");

			await builder.Build().RunAsync();
		}
	}
}
