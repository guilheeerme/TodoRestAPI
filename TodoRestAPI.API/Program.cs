using Microsoft.EntityFrameworkCore;
using TodoRestAPI.Application.Mapper;
using TodoRestAPI.Application.Services;
using TodoRestAPI.Domain.Abstractions.Repositories;
using TodoRestAPI.Infrastructure.Context;
using TodoRestAPI.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(TodoTaskMapperProfile));
builder.Services.AddScoped<TodoTaskAppService>();
builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("DataSource=app.db;Cache=Shared"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TodoTask}/{action=Index}/{id?}");

app.Run();
