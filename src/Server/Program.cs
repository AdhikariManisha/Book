using Book.Application;
using Book.Application.Books;
using Book.Application.Connections;
using Book.Application.Contracts.Books;
using Book.Application.Contracts.Connections;
using Book.Application.Contracts.Emails;
using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Services;
using Book.Application.Contracts.Users;
using Book.Application.Emails;
using Book.Application.Users;
using Book.Authors;
using Book.Infrastructure.Repositories;
using Book.Infrastructure.Services;
using Book.Server.Extensions;
using Book.Server.Filters;
using Book.Shared.Options;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAutoMapper(typeof(BookApplicationAutoMapperProfile));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAndMigrateDb(connectionString);
builder.Services.AddIdentity();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddTransient<IDbConnection, DbConnection>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddControllers(option => option.Filters.Add<ApiExceptionFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins(@"http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
builder.Services.AddHangfireServer();
builder.Services.Configure<EmailOption>(builder.Configuration.GetSection(EmailOption.Email));
builder.Services.Configure<CacheOption>(builder.Configuration.GetSection(CacheOption.Cache));
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "localhost:6379";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.UseCors("corsapp");
}
app.UseHangfireDashboard();

//BackgroundJob.Schedule(() => Console.WriteLine("Hello"), TimeSpan.FromMinutes(1)) ;
//BackgroundJob.Enqueue(() => Console.WriteLine("Hello World!!!"));
////RecurringJob.AddOrUpdate("sayHello4", () => Console.WriteLine("Hello World!!!"), "50 */5 * * *");
//RecurringJob.RemoveIfExists("sayHello2");
//RecurringJob.TriggerJob("sayHello4");
//RecurringJob.RemoveIfExists("sayHello3");
//RecurringJob.AddOrUpdate("Test", () => Console.WriteLine("This is my first HangFireLab!!!"), "*/10 15-16 * * *");
//RecurringJob.AddOrUpdate("Test", () => Console.WriteLine("This is my first HangFireLab!!!"), "0 0 * * SUN");
//RecurringJob.AddOrUpdate("Test", () => Console.WriteLine("This is my first HangFireLab!!!"), "55,56,57 * * * * ");
//RecurringJob.AddOrUpdate("Test2", () => Console.WriteLine("This is my first HangFireLab!!!"), "*/2 * * * *");
//RecurringJob.AddOrUpdate("Test3", () => Console.WriteLine("This is my first HangFireLab!!!"), "1/2 * * * *");

//BackgroundJob.Enqueue<IEmailService>(s => s.SendTestEmailAsync());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

