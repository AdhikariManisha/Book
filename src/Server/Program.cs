using Book.Application;
using Book.Application.Books;
using Book.Application.Contracts.Books;
using Book.Application.Contracts.Repositories;
using Book.Infrastructure.Repositories;
using Book.Infrastructure.Seeders;
using Book.Server.Extensions;
using Book.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAndMigrateDb(connectionString);
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins(@"http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddAutoMapper(typeof(BookApplicationAutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("corsapp");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.Seed();

app.Run();

