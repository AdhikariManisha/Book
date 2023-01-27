using Book.Application;
using Book.Application.Books;
using Book.Application.Connections;
using Book.Application.Contracts.Books;
using Book.Application.Contracts.Connections;
using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Services;
using Book.Application.Contracts.Users;
using Book.Application.Users;
using Book.Authors;
using Book.Infrastructure.Repositories;
using Book.Infrastructure.Seeders;
using Book.Infrastructure.Services;
using Book.Server.Extensions;
using Book.Server.Filters;
using Book.Shared.Interfaces;
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
builder.Services.AddControllers(option => option.Filters.Add<ApiExceptionFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins(@"http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.UseCors("corsapp");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

