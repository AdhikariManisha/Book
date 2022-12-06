using Book.Domain.Entities.Identity;
using Book.Infrastructure.Contexts;
using Book.Infrastructure.Seeders;
using Book.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Book.DbMigrator
{
    public static class ServiceCollectionExtenion
    {
        public static IServiceCollection AddAndMigrateDb(this IServiceCollection services, string? connectionString)
        {
            ArgumentNullException.ThrowIfNull(connectionString);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (dbContext?.Database.GetMigrations().Count() > 0)
            {
                Console.WriteLine("Migrating Database");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrated Database successfully");
            }

            return services;
        }
        public static IServiceCollection AddDataSeeders(this IServiceCollection services)
        {
            var dataSeederServices = Assembly.GetAssembly(typeof(ApplicationDbContext)).GetTypes()
                .Where(s => s.GetInterfaces().Contains(typeof(IDataSeeder)));

            foreach (var service in dataSeederServices)
            {
                services.AddTransient(typeof(IDataSeeder), service);
            }

            return services;
        }
        public static IServiceCollection AddIdentity(this IServiceCollection servics)
        {
            servics.AddIdentity<BookUser, BookRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            return servics;
        }
    }
}
