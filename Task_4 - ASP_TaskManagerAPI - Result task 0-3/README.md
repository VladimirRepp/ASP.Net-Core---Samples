# TaskManager API ✅

Репозиторий с решением задания **«Мини-проект: Task Manager API»** — финальный практикум, объединяющий всё изученное: Controllers, Services, DI, REST, CRUD, обработку ошибок и Swagger.

---

## 📋 Описание задания

**Уровень сложности:** простой

**Цель:** самостоятельно спроектировать небольшое REST API для управления задачами пользователя без использования базы данных (данные — в памяти).

**Предметная область:** задачи (`TaskItem`) с полями: `Id`, `Title`, `Description`, `IsCompleted`, `Priority`, `CreatedAt`.

---

## 📚 Термины

| Термин | Определение |
|---|---|
| **REST** | Архитектурный подход к построению Web API: ресурсы доступны через URI, операции — через HTTP-методы. |
| **Resource** | Сущность, с которой работает API (в нашем случае — `Task`). |
| **DTO (Data Transfer Object)** | Объект для передачи данных между клиентом и сервером. |
| **CRUD** | Четыре базовые операции: **C**reate, **R**ead, **U**pdate, **D**elete. |
| **Swagger** | Инструмент для документирования и тестирования API. |
| **OpenAPI** | Спецификация описания REST API (Swagger — её реализация). |

---

## 🛠️ Требования

- [.NET SDK](https://dotnet.microsoft.com/download) (версия 6.0 или выше)
- Одна из сред разработки:
  - Visual Studio 2022+
  - JetBrains Rider
  - Visual Studio Code + C# расширение
- Инструмент для тестирования API:
  - Swagger UI (встроен)
  - Postman, Bruno, REST Client или `curl`

---

## 🚀 Запуск проекта

```bash
# Клонирование репозитория
git clone https://github.com/<username>/TaskManager.git
cd TaskManager

# Восстановление зависимостей
dotnet restore

# Запуск приложения
dotnet run
```

После запуска приложение доступно по адресу:
- 🔒 `https://localhost:7000`
- 🔓 `http://localhost:5000`
- 📄 **Swagger UI:** `https://localhost:7000/swagger`

---

## 🌐 Реализованные endpoint'ы

| Метод | Маршрут | Назначение | Успех | Ошибка |
|---|---|---|---|---|
| `GET` | `/api/tasks` | Список задач (с фильтрами) | `200 OK` | — |
| `GET` | `/api/tasks/{id}` | Задача по Id | `200 OK` | `404 Not Found` |
| `POST` | `/api/tasks` | Создать задачу | `201 Created` | `400 Bad Request` |
| `PUT` | `/api/tasks/{id}` | Обновить задачу | `200 OK` | `400` / `404` |
| `DELETE` | `/api/tasks/{id}` | Удалить задачу | `204 No Content` | `404 Not Found` |

### Фильтрация

```http
GET /api/tasks?completed=true         # только выполненные
GET /api/tasks?completed=false        # только невыполненные
GET /api/tasks?priority=1             # задачи с priority = 1
GET /api/tasks?completed=true&priority=2   # комбинированный фильтр
```

---

## 📂 Структура проекта

```
TaskManager/
│
├── Controllers/
│   └── TasksController.cs       # Тонкий контроллер — только маршрутизация
│
├── Models/
│   └── TaskItem.cs              # Модель предметной области
│
├── Services/
│   ├── IDataService.cs          # Контракт сервиса
│   └── TaskService.cs           # Бизнес-логика + in-memory хранилище
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── Program.cs                   # Регистрация сервисов + middleware
├── TaskManager.csproj
└── README.md
```

### Архитектура

```
                HTTP
                 ↓
              Client
                 ↓
             Controller        ← тонкий слой (маршрутизация)
                 ↓
              Service          ← бизнес-логика
                 ↓
           In-Memory Data      ← List<TaskItem>
```

---

## 🧪 Примеры тестирования

### curl

```bash
# Список всех задач
curl https://localhost:7000/api/tasks

# Только выполненные
curl "https://localhost:7000/api/tasks?completed=true"

# Только невыполненные с приоритетом 2
curl "https://localhost:7000/api/tasks?completed=false&priority=2"

# Задача по Id
curl https://localhost:7000/api/tasks/1

# Создать задачу
curl -X POST https://localhost:7000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Написать README",
    "description": "Оформить документацию",
    "isCompleted": false,
    "priority": 1
  }'

# Обновить задачу
curl -X PUT https://localhost:7000/api/tasks/1 \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Изучить ASP.NET Core",
    "description": "Контроллеры, DI, Swagger",
    "isCompleted": true,
    "priority": 2
  }'

# Удалить задачу
curl -X DELETE https://localhost:7000/api/tasks/1
```

### REST Client (файл `requests.http`)

```http
@baseUrl = https://localhost:7000

### Получить все задачи
GET {{baseUrl}}/api/tasks

### Только выполненные
GET {{baseUrl}}/api/tasks?completed=true

### Только с приоритетом 1
GET {{baseUrl}}/api/tasks?priority=1

### Задача по Id
GET {{baseUrl}}/api/tasks/1

### Создать задачу
POST {{baseUrl}}/api/tasks
Content-Type: application/json

{
  "title": "Написать README",
  "description": "Оформить документацию",
  "isCompleted": false,
  "priority": 1
}

### Обновить задачу
PUT {{baseUrl}}/api/tasks/1
Content-Type: application/json

{
  "title": "Изучить ASP.NET Core",
  "description": "Контроллеры, DI, Swagger",
  "isCompleted": true,
  "priority": 2
}

### Удалить задачу
DELETE {{baseUrl}}/api/tasks/1

### Ошибка 404
GET {{baseUrl}}/api/tasks/999

### Ошибка 400 (пустой Title)
POST {{baseUrl}}/api/tasks
Content-Type: application/json

{
  "title": "",
  "priority": -100
}
```

---

## 🧠 Ключевые идеи проекта

### REST-дизайн

| Ресурс | URI | HTTP-метод | Операция |
|---|---|---|---|
| Коллекция задач | `/api/tasks` | `GET` | Прочитать все |
| Коллекция задач | `/api/tasks` | `POST` | Создать новую |
| Конкретная задача | `/api/tasks/{id}` | `GET` | Прочитать одну |
| Конкретная задача | `/api/tasks/{id}` | `PUT` | Обновить |
| Конкретная задача | `/api/tasks/{id}` | `DELETE` | Удалить |

### Разделение ответственности

- **Controller** — принимает HTTP-запрос, валидирует входные данные, возвращает HTTP-ответ.
- **Service** — бизнес-логика: фильтрация, работа с коллекцией.
- **Model** — структура данных.
- **DI** — связывает всё вместе, управляет временем жизни.

### Обработка ошибок

| Ситуация | Ответ |
|---|---|
| Задача не найдена | `404 Not Found` |
| Пустой `Title` | `400 Bad Request` |
| `Priority` вне диапазона 1–3 | `400 Bad Request` |
| Успешное создание | `201 Created` |
| Успешное удаление | `204 No Content` |

### Swagger

Swagger UI доступен по адресу `/swagger` и позволяет:
- Просмотреть все endpoint'ы и их параметры.
- Отправить тестовые запросы прямо из браузера.
- Посмотреть схемы моделей (JSON-структуры).
- Проверить HTTP-статусы ответов.

---

## ❓ Вопросы для самопроверки

<details>
<summary>Что такое REST API?</summary>
Архитектурный стиль для построения веб-сервисов: ресурсы идентифицируются URI, действия выражаются HTTP-методами, взаимодействие stateless.
</details>

<details>
<summary>Что такое ресурс в REST?</summary>
Сущность предметной области, доступная по URI. В нашем случае — задача (<code>TaskItem</code>).
</details>

<details>
<summary>Почему <code>/api/tasks/5</code> является ресурсом?</summary>
URI идентифицирует конкретный экземпляр ресурса — задачу с Id = 5.
</details>

<details>
<summary>Почему для получения задачи используется GET?</summary>
GET — безопасный идемпотентный метод для чтения данных; не изменяет состояние сервера.
</details>

<details>
<summary>Почему для создания задачи используется POST?</summary>
POST не идемпотентен, используется для создания новых ресурсов; сервер сам присваивает Id.
</details>

<details>
<summary>Чем PUT отличается от PATCH?</summary>
<b>PUT</b> — полное обновление ресурса (передаются все поля).<br>
<b>PATCH</b> — частичное обновление (только изменяемые поля).
</details>

<details>
<summary>Что такое CRUD?</summary>
Create, Read, Update, Delete — четыре базовые операции над ресурсом, реализуемые через POST, GET, PUT/PATCH, DELETE.
</details>

<details>
<summary>Что такое DTO?</summary>
Data Transfer Object — объект для передачи данных между слоями или по сети, часто без бизнес-логики.
</details>

<details>
<summary>Чем Model отличается от DTO?</summary>
<b>Model</b> описывает предметную область и может содержать логику/связи.<br>
<b>DTO</b> — только данные для передачи, часто с ограниченным набором полей, чтобы не раскрывать лишнего.
</details>

<details>
<summary>Что такое сериализация?</summary>
Преобразование объекта C# в JSON (или другой формат) для передачи по сети.
</details>

<details>
<summary>Что такое HTTP Status Code?</summary>
Числовой код в ответе, описывающий результат обработки запроса (200, 201, 204, 400, 404, 500 и др.).
</details>

<details>
<summary>Когда следует использовать 200?</summary>
При успешной обработке запроса с телом ответа (например, GET-запрос вернул данные).
</details>

<details>
<summary>Когда следует использовать 201?</summary>
При успешном создании нового ресурса (в ответ на POST). В заголовке <code>Location</code> указывается URL нового ресурса.
</details>

<details>
<summary>Когда использовать 204?</summary>
При успешной обработке запроса без тела ответа (например, DELETE).
</details>

<details>
<summary>Когда использовать 400?</summary>
Когда клиент отправил некорректные данные: пустые обязательные поля, неверные типы, нарушение диапазонов.
</details>

<details>
<summary>Когда использовать 404?</summary>
Когда запрошенный ресурс не существует на сервере.
</details>

<details>
<summary>Что такое Dependency Injection?</summary>
Механизм передачи зависимостей объекту извне (обычно через конструктор), управляемый DI-контейнером.
</details>

<details>
<summary>Зачем нужен Service Layer?</summary>
Для отделения бизнес-логики от HTTP-слоя. Улучшает тестируемость, переиспользование и поддерживаемость.
</details>

<details>
<summary>Почему Controller не должен содержать всю бизнес-логику?</summary>
Иначе он превращается в «God Object», плохо тестируется, дублирует логику и нарушает единственную ответственность.
</details>

<details>
<summary>Что такое Swagger?</summary>
Набор инструментов для проектирования, документирования и тестирования REST API на основе спецификации OpenAPI.
</details>

<details>
<summary>Что такое OpenAPI?</summary>
Стандартизированная спецификация описания REST API: пути, параметры, схемы моделей, ответы.
</details>

<details>
<summary>Для чего API нужен Swagger UI?</summary>
Для наглядной документации и интерактивного тестирования API прямо из браузера.
</details>

<details>
<summary>Что изменится в приложении, если вместо List&lt;TaskItem&gt; подключить SQL-базу данных?</summary>
Появится слой работы с БД (репозиторий / <code>DbContext</code>). Service продолжит отвечать за бизнес-логику, но данные будет получать из БД. Controller останется неизменным.
</details>

<details>
<summary>Какой слой должен отвечать за работу с базой данных?</summary>
Отдельный слой доступа к данным (репозиторий или <code>DbContext</code> в Entity Framework Core), вызываемый из Service.
</details>

<details>
<summary>Какие части приложения придётся изменить при переходе с хранения в памяти на EF Core?</summary>
<b>Service</b> — заменить <code>List&lt;TaskItem&gt;</code> на <code>DbContext</code>.<br>
<b>Program.cs</b> — зарегистрировать <code>DbContext</code> через <code>AddDbContext</code> (lifetime = Scoped).<br>
<b>Модель</b> — добавить навигационные свойства и Data Annotations при необходимости.<br>
<b>Controller</b> — не меняется, работает через интерфейс сервиса.
</details>

---

## ✅ Чек-лист требований задания

Проект должен продемонстрировать:

- [x] **Model** — `TaskItem` с полями `Id`, `Title`, `Description`, `IsCompleted`, `Priority`, `CreatedAt`
- [x] **Controller** — `TasksController`
- [x] **Actions** — 5 методов для CRUD
- [x] **Routing** — атрибуты `[Route]`, `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`
- [x] **HTTP methods** — GET, POST, PUT, DELETE
- [x] **HTTP status codes** — 200, 201, 204, 400, 404
- [x] **Service** — `TaskService`
- [x] **Interface** — `ITaskService`
- [x] **Dependency Injection** — регистрация через `AddSingleton`
- [x] **CRUD** — все четыре операции
- [x] **JSON** — сериализация/десериализация через `System.Text.Json`
- [x] **Обработка ошибок** — валидация входных данных, 400 и 404
- [x] **Swagger / OpenAPI** — подключены `AddSwaggerGen` и `UseSwaggerUI`
- [x] **Фильтрация** — по `completed` и `priority` *(доп. из шагов 3–4)*

---

## 🎯 Что демонстрирует проект

| Концепция | Где смотреть |
|---|---|
| Minimal setup ASP.NET Core | `Program.cs` |
| Controllers + Actions | `Controllers/TasksController.cs` |
| Routing (attribute-based) | `[Route("api/[controller]")]` |
| Model Binding (`[FromBody]`, `[FromQuery]`) | параметры action'ов |
| DI + Service Layer | `Services/` + регистрация в `Program.cs` |
| REST + HTTP-статусы | коды ответов в контроллере |
| Swagger | `/swagger` в браузере |
| Сериализация JSON | автоматическая через `Ok(...)` |
| Валидация | приватный метод `IsValid` в контроллере |

---

## 📖 Полезные ссылки

- [Создание Web API в ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/tutorials/first-web-api)
- [REST API Best Practices](https://learn.microsoft.com/ru-ru/azure/architecture/best-practices/api-design)
- [Controllers в Web API](https://learn.microsoft.com/ru-ru/aspnet/core/web-api/)
- [Dependency Injection](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/dependency-injection)
- [Swagger / OpenAPI](https://learn.microsoft.com/ru-ru/aspnet/core/tutorials/web-api-help-pages-using-swagger)
- [HTTP-статусы — MDN](https://developer.mozilla.org/ru/docs/Web/HTTP/Status)
- [Entity Framework Core](https://learn.microsoft.com/ru-ru/ef/core/)

---

## 📝 Лицензия

Учебный проект. Свободно для использования в образовательных целях.