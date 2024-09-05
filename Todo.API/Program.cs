using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo.API.Data;
using Todo.API.Mappings;
using Todo.API.Repositories;
using Todo.API.Repositories.Contracts;
using Todo.API.Services;
using Todo.API.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySQL(connectionString!);
});

builder.Services.AddControllers();

#region Repositories
builder.Services.AddScoped<ITodoItemsRepository, TodoItemsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
#endregion

#region Services
builder.Services.AddScoped<ITodoItemsService, TodoItemsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
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

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

var app = builder.Build();

app.MapControllers();

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

var context = app.Services.CreateScope().ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

await DataSeeder.Seed(context);

app.Run();
