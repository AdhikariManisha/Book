using Book.Infrastructure.Contexts;
using Book.Infrastructure.Seeders;
using Book.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Book.DbMigrator;

public class DbMigratorHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                Console.WriteLine("Seeded Started");
                try
                {
                    var _dataSeeders = _serviceProvider.GetServices<IDataSeeder>();
                    if (_dataSeeders != null)
                    {
                        foreach (var seeder in _dataSeeders) {
                            await seeder.SeedAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Seeded Successfully");
                    _hostApplicationLifetime.StopApplication();
                }
            });
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
