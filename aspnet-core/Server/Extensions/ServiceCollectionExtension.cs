using Book.Application.Contracts.Permissions;
using Book.Domain.Entities.Identity;
using Book.Infrastructure.Contexts;
using Book.Server.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Book.Server.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAndMigrateDb(this IServiceCollection services, string? connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        return services;
    }
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<BookUser, BookRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
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

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context => {
                    var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
                    context.Token = accessToken; 
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(options =>
        {
            // Here I stored necessary permissions/roles in a constant
            foreach (var prop in typeof(BookPermissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy =>
                    {
                        policy.RequireClaim("Permission", propertyValue.ToString());
                        //policy.AddRequirements(new CustomUserClaimRequirement(propertyValue.ToString()));
                    });
                }
            }
        });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API"

            });
            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme,
                    },
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                },
                new List<string>()
            }
            });
        });

        return services;
    }
}