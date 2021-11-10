using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RegistrationFormSyncer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Worker>();
                   services.AddModels(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("LocalDB"),
                        b => b.MigrationsAssembly("MigrationHandler")));
               });
    }
}
