// Шаблон: пустой проект ASP.NET Core Web API

using ASP_TaskManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<TaskService>();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/index.html"));

app.UseStaticFiles();
app.MapControllers(); // включает сам роутинг

app.Run();
