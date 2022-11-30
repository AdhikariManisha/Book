using Book.DbMigrator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Book.DbMigrator;
class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostBuilderContext, services) => {
            services.AddAndMigrateDb(hostBuilderContext.Configuration["ConnectionStrings:DefaultConnection"]);
            services.AddHostedService<DbMigratorHostedService>();
        });

}