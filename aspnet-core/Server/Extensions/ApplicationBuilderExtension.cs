using Book.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Book.Server.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder Seed(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetService<IDataSeeder>();
        if (seeder != null)
        {
            var task = seeder.SeedAsync().Result;
        }

        return builder;
    }

    public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder builder)
    {
        return builder.Use(async(ctx, next) => {
            if (ctx.User.Identity?.IsAuthenticated != true)
            {
                var authResult = await ctx.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                if (authResult?.Principal != null)
                {
                    ctx.User = authResult.Principal;
                }
            }
            await next();
        });
    }
}
