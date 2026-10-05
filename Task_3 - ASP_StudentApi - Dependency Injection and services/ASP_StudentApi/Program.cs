// Шаблон: пустой ASP.NET Core Web API проект

using ASP_StudentApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // регистрирует сервисы для контроллеров (валидация, биндинг).
builder.Services.AddSingleton<StudentsService>(); // регистрируем наш сервис как singleton, чтобы он жил на протяжении всего времени работы приложения.

// Singleton - один экземпляр на всё время жизни приложения.
// Scoped - один экземпляр в пределах определённой области действия. В Web API обычно — в пределах HTTP-запроса.
// Transient - новый экземпляр создаётся при каждом запросе зависимости.

var app = builder.Build();

// HTTP request ← клиент (браузер, Postman) отправил GET /students?name=Oleg
//      ↓
// Routing      ← ASP.NET Core смотрит на URL и решает, какой контроллер и экшен вызвать
//      ↓
// Controller   ← создаётся экземпляр StudentsController (через DI)
//      ↓
// Action       ← вызывается метод GetStudents("Oleg")
//      ↓
// Response     ← то, что вернул экшен, превращается в HTTP-ответ

// Важно понять: routing — это отдельный слой.
// Он не знает про код внутри методов, он только сопоставляет URL → (контроллер, экшен).

// Простой маршрут, не очень хорошо смешивать с контроллерами, но для теста сойдёт.
app.MapGet("/", () =>
{
    return """
        Доступные endpoint'ы:
        GET    /view                  - вывод простой страницы для инициализации POST, PUT, DELETE
        GET    /api/students          - все студенты
        GET    /api/students/{id}     - студент по ID
        POST   /api/students          - добавить студента (JSON в body)
        PUT    /api/students/{id}     - обновить студента (JSON в body)
        DELETE /api/students/{id}     - удалить студента
        ДОПОЛНИТЕЛЬНО:
        GET /api/students/older-than/{age}     - студенты старше указанного возраста
        """;
});

app.MapGet("/view", () => Results.Redirect("/index.html"));

app.UseStaticFiles();
app.MapControllers(); // включает сам роутинг

app.Run();
