// Шаблон: пустой ASP.NET Core Web API проект


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // регистрирует сервисы для контроллеров (валидация, биндинг).

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

// Работает ли POST, PUT, DELETE?
// ПОЧЕМУ? 

// Какой жизненнный цикл у контроллера?
// - Каждый раз, когда приходит HTTP-запрос, создаётся новый экземпляр контроллера.
// Следовательно, если ты что-то хранишь в полях класса контроллера, то при следующем запросе эти данные будут потеряны.
// - Если тебе нужно хранить данные между запросами, то используй Singleton или Database.
// - Или воспользуйся Dependency Injection и сервисами с нужным жизненным циклом (Scoped, Singleton, Transient).

// Dependency Injection и сервисы - тема следующей задачи

app.UseStaticFiles();
app.MapControllers(); // включает сам роутинг через контроллеры. Без этого вызовы к /api/students не будут работать.

app.Run();
