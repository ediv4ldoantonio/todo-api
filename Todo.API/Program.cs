using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo.API.DataContext;
using Todo.API.Repositories;
using Todo.API.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySQL(connectionString!);
});

#region Repositories
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Todo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DocumentTitle = "Todo API";
    });
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Todo API");

app.Run();
