using Book.Domain.Entities.Identity;
using Book.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Book.Server.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAndMigrateDb(this IServiceCollection service, string? connectionString) {
            ArgumentNullException.ThrowIfNull(connectionString);

            service.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return service;
        }
        public static IServiceCollection AddIdentity(this IServiceCollection service)
        {
            service.AddIdentity<BookUser, BookRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return service;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            return service;
        }
    }
}
