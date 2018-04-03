using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddCommandLine(args).Build();
            
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseKestrel(options => options.AddServerHeader = false) // Do not add server information header
                .UseStartup<Startup>()
                .Build();
        }
    }
}
