using Book.Domain.Entities.Identity;
using Book.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Book.Server.Extensions
{
    public static class ServiceCollectionExtenion
    {
        public static IServiceCollection AddAndMigrateDb(this IServiceCollection servics, string? connectionString) {
            ArgumentNullException.ThrowIfNull(connectionString);

            servics.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return servics;
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
