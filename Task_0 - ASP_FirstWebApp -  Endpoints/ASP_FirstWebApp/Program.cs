// Шаблон: ASP.Net Core - пустой

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// End point GET /:
app.MapGet("/", () =>
{
    return "Hello, ASP.NET Core!";
});

// End point GET /about:
app.MapGet("/about", () =>
{
    return "Сайт визитка: vladimir-repp.ru";
});

// End point GET /contacts:
app.MapGet("/contacts", () =>
{
    return "tg: @vladimir_repp";
});

// End point GET /students:
app.MapGet("/students", () =>
{
    return "Этот материал создан в обучающих целях";
});

// End point GET /user/{name} - c Route-параметрами (обязательми):
app.MapGet("/user/{name}", (string name) =>
{
    return $"Hello, {name}!";
});

// Additional task:
app.MapGet("/sum/{a}/{b}", (int a, int b) =>
{
    return $"{a} + {b} = {a + b}";
});

app.MapGet("/minus/{a}/{b}", (int a, int b) =>
{
    return $"{a} - {b} = {a - b}";
});

app.MapGet("/multiply/{a}/{b}", (int a, int b) =>
{
    return $"{a} * {b} = {a * b}";
});

app.MapGet("/divide/{a}/{b}", (int a, int b) =>
{
    return $"{a} / {b} = {a / b}";
});

app.Run();
