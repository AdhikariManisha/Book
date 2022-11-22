using Book.Shared.Interfaces;

namespace Book.Server.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder Seed(this IApplicationBuilder builder) {
       using var scope = builder.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetService<IDataSeeder>();
        if (seeder != null)
        {
            var task = seeder.SeedAsync().Result;
        }

        return builder;
    }
}
