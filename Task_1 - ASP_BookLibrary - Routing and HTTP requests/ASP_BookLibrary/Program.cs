// Шаблон: ASP.Net Core - пустой

// static void Main(string[] args):
using ASP_BookLibrary.Controllers;
using ASP_BookLibrary.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

BookController books = new();

// Браузер сам по себе умеет делать только GET
// (когда ты вводишь URL в адресную строку или кликаешь по ссылке).
// POST и DELETE надо инициировать:
// 1. HTML-форма — умеет только GET и POST. DELETE через форму не сделать.
// 2. JavaScript(fetch / XMLHttpRequest) — умеет всё: GET, POST, PUT, DELETE, PATCH.
// 3. Инструменты вроде Postman / curl / Swagger — для тестирования без фронта.

// Сделаем POST/DELETE «с сайта» через fetch
// Создадим HTML-страницу с формой для добавления книги и кнопкой для удаления книги.
// wwwroot -> index.html

// Серверная часть обработки запросов:
app.MapGet("/", () => Results.Redirect("/index.html"));

// ДОПОЛНИТЕЛЬНОЕ ЗАДАНИЕ:
// Серверная часть: MapGet с query - параметром (не обязательный):
app.MapGet("/books", (string? author) =>
{
    // -> /books?author=Author 1
    // ? после идут параметры запроса
    // & можно сделать несколько параметров 

    if (string.IsNullOrEmpty(author))
        return Results.Ok(books.GetStringBooks()); // без фильтра — все книги

    var filtered = books.GetBooksByAuthor(author);
    return Results.Ok(filtered);
});

// Без параметра запроса
//app.MapGet("/books", () => books.GetStringBooks());

app.MapGet("/book/{id}", (int id) => 
{
    // Вместо return string, лучше возвращать объект IActionResult,
    // чтобы можно было вернуть разные коды состояния HTTP (например, 404 Not Found)

    // Так наприме, вернем return "404 Not Found",
    // браузер получит код состояния 200 OK,
    // а в теле ответа будет текст "404 Not Found".

    if (!books.TryGetBookByID(id, out var book))
    {
        return Results.NotFound("404 Not Found");
    }

    string result = $"Book ID: {book.Id}\nTitle: {book.Title}\nAuthor: {book.Author}";

    return Results.Ok(result);

});




app.MapPost("/book", (Book book) => 
{
    int id = books.AddBook(book);
    return Results.Ok($"Book added successfully with ID: {id}");
});

app.MapDelete("/book/{id}", (int id) =>
{
    if (!books.TryGetBookByID(id, out var book))
    {
        return Results.NotFound("404 Not Found");
    }

    // Remove the book from the collection
    bool isDeleted = books.RemoveBook(book);

    if (!isDeleted)
    {
        return Results.BadRequest("Error deleting the book.");
    }

    return Results.Ok($"Book with ID: {id} deleted successfully.");
});

// Обязательно подключаем статические файлы, чтобы браузер мог получить index.html
app.UseStaticFiles();

app.Run();
