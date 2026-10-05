# StudentApi 🎓

Репозиторий с решением задания **«ASP.NET Core Web API + Controller»** — практикум по переходу от Minimal API к классической архитектуре Web API с контроллерами.

---

## 📋 Описание задания

**Уровень сложности:** простой

**Цель:** понять классическую архитектуру ASP.NET Core Web API:

```
HTTP request
      ↓
   Routing
      ↓
  Controller
      ↓
    Action
      ↓
  Response
```

**Предметная область:** API для работы со студентами (данные хранятся в памяти).

---

## 📚 Термины

| Термин | Определение |
|---|---|
| **Controller** | Класс, содержащий обработчики HTTP-запросов. |
| **Action** | Метод контроллера, отвечающий на HTTP-запрос (например, `[HttpGet] public IActionResult GetBooks()`). |
| **Attribute** | Конструкция C#, добавляющая метаданные к классу или методу (например, `[ApiController]`). |
| **Model** | Класс, описывающий данные предметной области. |
| **Serialization** | Преобразование объекта C# → JSON. |
| **Deserialization** | Преобразование JSON → объект C#. |

---

## 🛠️ Требования

- [.NET SDK](https://dotnet.microsoft.com/download) (версия 6.0 или выше)
- Одна из сред разработки:
  - Visual Studio 2022+
  - JetBrains Rider
  - Visual Studio Code + C# расширение
- Инструмент для тестирования API:
  - [Swagger](https://swagger.io/) (встроен)
  - [Postman](https://www.postman.com/)
  - [Bruno](https://www.usebruno.com/)
  - [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
  - `curl`

---

## 🚀 Запуск проекта

### Через командную строку

```bash
# Клонирование репозитория
git clone https://github.com/<username>/StudentApi.git
cd StudentApi

# Восстановление зависимостей
dotnet restore

# Запуск приложения
dotnet run
```

После запуска приложение будет доступно по адресу:
- 🔒 `https://localhost:7000`
- 🔓 `http://localhost:5000`

**Swagger UI** (если включён): `https://localhost:7000/swagger`

### Через Visual Studio / Rider

Нажмите **F5** (Run) или **Ctrl+F5** (Run without debugging).

---

## 🌐 Реализованные endpoint'ы

| Метод | Маршрут | Назначение | Успех | Ошибка |
|---|---|---|---|---|
| `GET` | `/api/students` | Получить всех студентов | `200 OK` | — |
| `GET` | `/api/students/{id}` | Получить студента по Id | `200 OK` | `404 Not Found` |
| `POST` | `/api/students` | Создать студента | `201 Created` | `400 Bad Request` |
| `PUT` | `/api/students/{id}` | Изменить студента | `200 OK` / `204` | `404 Not Found` |
| `DELETE` | `/api/students/{id}` | Удалить студента | `204 No Content` | `404 Not Found` |
| `GET` | `/api/students/older-than/{age}` | Студенты старше указанного возраста *(доп.)* | `200 OK` | — |

### Примеры запросов

```http
GET    /api/students
GET    /api/students/1
POST   /api/students
PUT    /api/students/1
DELETE /api/students/1
GET    /api/students/older-than/18
```

---

## 📂 Структура проекта

```
StudentApi/
│
├── Controllers/
│   └── StudentsController.cs   # Обработчики HTTP-запросов
│
├── Models/
│   └── Student.cs              # Модель предметной области
│
├── Properties/
│   └── launchSettings.json     # Настройки запуска
│
├── appsettings.json            # Конфигурация
├── Program.cs                  # Точка входа и регистрация сервисов
├── StudentApi.csproj           # Файл проекта
└── README.md
```

---

## 🧪 Примеры тестирования

### curl

```bash
# Все студенты
curl https://localhost:7000/api/students

# Один студент
curl https://localhost:7000/api/students/1

# Студенты старше 18
curl https://localhost:7000/api/students/older-than/18

# Создать студента
curl -X POST https://localhost:7000/api/students \
  -H "Content-Type: application/json" \
  -d '{"name":"Alex","age":21}'

# Изменить студента
curl -X PUT https://localhost:7000/api/students/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"Alex","age":22}'

# Удалить студента
curl -X DELETE https://localhost:7000/api/students/1
```

### REST Client (файл `requests.http`)

```http
### Получить всех студентов
GET https://localhost:7000/api/students

### Получить студента по Id
GET https://localhost:7000/api/students/1

### Студенты старше 18
GET https://localhost:7000/api/students/older-than/18

### Создать студента
POST https://localhost:7000/api/students
Content-Type: application/json

{
  "name": "Alex",
  "age": 21
}

### Изменить студента
PUT https://localhost:7000/api/students/1
Content-Type: application/json

{
  "name": "Alex",
  "age": 22
}

### Удалить студента
DELETE https://localhost:7000/api/students/1
```

---

## 🧠 Как это работает

### Атрибуты контроллера

| Атрибут | Назначение |
|---|---|
| `[ApiController]` | Включает поведение, специфичное для API: автоматическую валидацию модели, автоматические ответы 400, привязку параметров из тела/маршрута. |
| `[Route("api/[controller]")]` | Задаёт базовый маршрут. `[controller]` автоматически подставляется как имя контроллера без слова `Controller` → `students`. |
| `[HttpGet]` / `[HttpPost]` / `[HttpPut]` / `[HttpDelete]` | Привязывают метод к соответствующему HTTP-методу. |
| `[FromBody]` | Указывает, что параметр нужно десериализовать из тела запроса (JSON → объект). |

### Цепочка обработки запроса

1. **HTTP request** — клиент отправляет запрос, например `GET /api/students/1`.
2. **Routing** — ASP.NET Core сопоставляет URL с маршрутом контроллера.
3. **Controller** — выбирается `StudentsController`.
4. **Action** — вызывается метод `GetById(1)`.
5. **Response** — `IActionResult` (`Ok`, `NotFound` и др.) превращается в HTTP-ответ.

### Сериализация / десериализация

- **POST/PUT** — JSON из тела запроса **десериализуется** в объект `Student` (благодаря `[FromBody]` и `System.Text.Json`).
- **GET/POST-ответ** — объект `Student` **сериализуется** обратно в JSON.

---

## ❓ Вопросы для самопроверки

<details>
<summary>Зачем нужны Controllers?</summary>
Для разделения логики обработки запросов по группам (ресурсам), что делает код структурированным, читаемым и поддерживаемым.
</details>

<details>
<summary>Почему не стоит размещать всю логику приложения в Program.cs?</summary>
Файл разрастётся, станет сложным в поддержке и тестировании; нарушается принцип единственной ответственности; сложно масштабировать и командная работа затруднена.
</details>

<details>
<summary>Что такое Controller?</summary>
Класс, содержащий action'ы — методы, обрабатывающие HTTP-запросы к определённому ресурсу.
</details>

<details>
<summary>Что такое Action?</summary>
Метод контроллера, привязанный к HTTP-методу и маршруту, который обрабатывает запрос и возвращает результат.
</details>

<details>
<summary>Что делает <code>[ApiController]</code>?</summary>
Включает API-специфичное поведение: авто-валидацию модели, автоматическую привязку параметров, автоматический ответ 400 при ошибках модели.
</details>

<details>
<summary>Что делает <code>[Route]</code>?</summary>
Задаёт шаблон URL, по которому маршрутизируются запросы к контроллеру или action'у.
</details>

<details>
<summary>Что делает <code>[HttpGet]</code>?</summary>
Привязывает action к HTTP-методу GET.
</details>

<details>
<summary>Что делает <code>[HttpPost]</code>?</summary>
Привязывает action к HTTP-методу POST.
</details>

<details>
<summary>Откуда ASP.NET Core получает JSON POST-запроса?</summary>
Из тела HTTP-запроса (body). Параметр помечается атрибутом <code>[FromBody]</code> или выводится автоматически при <code>[ApiController]</code>.
</details>

<details>
<summary>Как JSON превращается в объект Student?</summary>
Через <b>deserialization</b> — встроенный сериализатор <code>System.Text.Json</code> сопоставляет поля JSON со свойствами класса.
</details>

<details>
<summary>Как объект Student превращается обратно в JSON?</summary>
Через <b>serialization</b> — при формировании ответа <code>Ok(student)</code> фреймворк сериализует объект в JSON автоматически.
</details>

<details>
<summary>Что такое serialization?</summary>
Преобразование объекта в формат, пригодный для передачи/хранения (например, JSON или XML).
</details>

<details>
<summary>Что такое deserialization?</summary>
Обратный процесс — преобразование данных (JSON) в объект C#.
</details>

<details>
<summary>Для чего используется IActionResult?</summary>
Возвращаемый тип action'а, позволяющий вернуть различные HTTP-ответы: <code>Ok()</code>, <code>NotFound()</code>, <code>BadRequest()</code>, <code>Created()</code> и др.
</details>

<details>
<summary>Чем <code>Ok()</code> отличается от <code>NotFound()</code>?</summary>
<code>Ok()</code> возвращает статус <b>200</b> с телом ответа; <code>NotFound()</code> — статус <b>404</b> без тела.
</details>

<details>
<summary>Зачем разделять Models и Controllers?</summary>
Для разделения ответственности: Models описывают данные, Controllers — логику обработки запросов. Это упрощает поддержку, тестирование и повторное использование.
</details>

---

## ✅ Чек-лист выполнения задания

- [x] Создан проект `StudentApi`
- [x] Создана модель `Student` в папке `Models`
- [x] Создан `StudentsController` в папке `Controllers`
- [x] `GET /api/students` — список студентов
- [x] `GET /api/students/{id}` — один студент или `404`
- [x] `POST /api/students` — создание студента
- [x] `PUT /api/students/{id}` — изменение студента
- [x] `DELETE /api/students/{id}` — удаление студента
- [x] `GET /api/students/older-than/{age}` — фильтр *(доп. задание)*
- [x] Настроен Swagger / инструмент для тестирования
- [x] Изучены вопросы самопроверки

---

## 📖 Полезные ссылки

- [ASP.NET Core Web API — обзор](https://learn.microsoft.com/ru-ru/aspnet/core/web-api/)
- [Контроллеры в ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/web-api/action-return-types)
- [Атрибуты маршрутизации](https://learn.microsoft.com/ru-ru/aspnet/core/mvc/controllers/routing)
- [Model Binding](https://learn.microsoft.com/ru-ru/aspnet/core/mvc/models/model-binding)
- [Swagger / OpenAPI](https://learn.microsoft.com/ru-ru/aspnet/core/tutorials/web-api-help-pages-using-swagger)
- [Serialization в System.Text.Json](https://learn.microsoft.com/ru-ru/dotnet/standard/serialization/system-text-json/how-to)

---

## 📝 Лицензия

Учебный проект. Свободно для использования в образовательных целях.