using Book.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Book.DbMigrator
{
    public static class ServiceCollectionExtenion
    {
        public static IServiceCollection AddAndMigrateDb(this IServiceCollection servics, string? connectionString)
        {
            ArgumentNullException.ThrowIfNull(connectionString);

            servics.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            using var scope = servics.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (dbContext?.Database.GetMigrations().Count() > 0)
            {
                Console.WriteLine("Migrating Database");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrated Database successfully");
            }

            return servics;
        }
    }
}
