# BookLibrary 📚

Репозиторий с решением задания **«Routing и HTTP-запросы»** — практикум по работе с маршрутами, HTTP-методами и REST-принципами в ASP.NET Core.

---

## 📋 Описание задания

**Уровень сложности:** очень простой → простой

**Цель:** научиться работать с маршрутами и различными HTTP-методами. Понять, что один и тот же ресурс может обрабатываться разными HTTP-методами.

**Предметная область:** библиотека книг (данные хранятся в памяти, без БД).

---

## 📚 Термины

### HTTP-методы

| Метод | Назначение |
|---|---|
| **GET** | Получение данных |
| **POST** | Создание / отправка данных |
| **PUT** | Полное изменение ресурса |
| **PATCH** | Частичное изменение ресурса |
| **DELETE** | Удаление ресурса |

### HTTP-статусы

| Код | Значение |
|---|---|
| **200 OK** | Запрос успешно обработан |
| **201 Created** | Ресурс создан |
| **400 Bad Request** | Некорректный запрос |
| **404 Not Found** | Ресурс не найден |
| **500 Internal Server Error** | Ошибка на сервере |

**Request** — HTTP-запрос. **Response** — HTTP-ответ.

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
git clone https://github.com/<username>/BookLibrary.git
cd BookLibrary

# Восстановление зависимостей
dotnet restore

# Запуск приложения
dotnet run
```

После запуска приложение будет доступно по адресу:
- 🔒 `https://localhost:7000`
- 🔓 `http://localhost:5000`

*(точный порт смотрите в `Properties/launchSettings.json` или в выводе консоли)*

### Через Visual Studio / Rider

Нажмите **F5** (Run) или **Ctrl+F5** (Run without debugging).

---

## 🌐 Реализованные endpoint'ы

| Метод | Маршрут | Назначение | Успех | Ошибка |
|---|---|---|---|---|
| `GET` | `/books` | Получить список книг | `200 OK` | — |
| `GET` | `/books/{id}` | Получить книгу по Id | `200 OK` | `404 Not Found` |
| `POST` | `/books` | Добавить новую книгу | `201 Created` | `400 Bad Request` |
| `DELETE` | `/books/{id}` | Удалить книгу | `204 No Content` | `404 Not Found` |
| `GET` | `/books?author=...` | Фильтр по автору *(доп.)* | `200 OK` | — |

### Примеры запросов

```http
GET /books
GET /books/2
POST /books
DELETE /books/2
GET /books?author=George%20Orwell
```

---

## 📂 Структура проекта

```
BookLibrary/
├── Program.cs              # Точка входа и настройка приложения
├── Book.cs                 # Модель книги
├── appsettings.json        # Конфигурация
├── BookLibrary.csproj      # Файл проекта
├── Properties/
│   └── launchSettings.json # Настройки запуска
└── README.md
```

---

## 🧪 Примеры тестирования

### curl

```bash
# Получить все книги
curl https://localhost:7000/books

# Получить книгу по Id
curl https://localhost:7000/books/2

# Фильтр по автору
curl "https://localhost:7000/books?author=George%20Orwell"

# Добавить книгу
curl -X POST https://localhost:7000/books \
  -H "Content-Type: application/json" \
  -d '{"title":"Dune","author":"Frank Herbert"}'

# Удалить книгу
curl -X DELETE https://localhost:7000/books/2
```

### REST Client (файл `requests.http`)

```http
### Получить все книги
GET https://localhost:7000/books

### Получить книгу по Id
GET https://localhost:7000/books/2

### Фильтр по автору
GET https://localhost:7000/books?author=George%20Orwell

### Добавить книгу
POST https://localhost:7000/books
Content-Type: application/json

{
  "title": "Dune",
  "author": "Frank Herbert"
}

### Удалить книгу
DELETE https://localhost:7000/books/2
```

---

## 🧠 Ключевые идеи

1. **Один ресурс — разные методы.** Путь `/books` может обрабатывать `GET` (получить список) и `POST` (создать книгу).
2. **HTTP-метод выражает намерение.** `GET` — чтение, `POST` — создание, `DELETE` — удаление.
3. **Status Code сообщает результат.** `200` — успех, `201` — создано, `404` — не найдено, `400` — некорректные данные.
4. **Route parameters** (`{id}`) извлекаются из URL и передаются в обработчик.
5. **Query parameters** (`?author=...`) используются для фильтрации.
6. **Body запроса** (JSON) автоматически десериализуется в объект модели.

---

## ❓ Вопросы для самопроверки

<details>
<summary>Почему получение книги выполняется через GET?</summary>
GET — идемпотентный метод для чтения данных, не изменяет состояние сервера. Подходит для безопасного получения ресурса.
</details>

<details>
<summary>Почему создание книги обычно выполняется через POST?</summary>
POST используется для создания новых ресурсов и/или отправки данных на сервер. Не идемпотентен — каждый вызов создаёт новый ресурс.
</details>

<details>
<summary>В чём отличие PUT от POST?</summary>
<b>PUT</b> — идемпотентный, полностью заменяет ресурс по известному URL.<br>
<b>POST</b> — не идемпотентен, создаёт новый ресурс, URL определяет сервер.
</details>

<details>
<summary>Чем PUT отличается от PATCH?</summary>
<b>PUT</b> — полная замена ресурса (нужно передать все поля).<br>
<b>PATCH</b> — частичное обновление (передаются только изменяемые поля).
</details>

<details>
<summary>Почему удаление выполняется через DELETE?</summary>
DELETE — семантически обозначает удаление ресурса и идемпотентен: повторный вызов не меняет результат.
</details>

<details>
<summary>Что означает HTTP-код 200?</summary>
<b>200 OK</b> — запрос успешно обработан, ответ содержит запрошенные данные.
</details>

<details>
<summary>Когда следует возвращать 201?</summary>
<b>201 Created</b> — при успешном создании нового ресурса (обычно в ответ на POST). В заголовке <code>Location</code> указывается URL нового ресурса.
</details>

<details>
<summary>Что означает 400?</summary>
<b>400 Bad Request</b> — клиент отправил некорректные данные (неверный формат, отсутствуют обязательные поля).
</details>

<details>
<summary>Что означает 404?</summary>
<b>404 Not Found</b> — запрошенный ресурс не существует на сервере.
</details>

<details>
<summary>Чем 404 Not Found отличается от 500 Internal Server Error?</summary>
<b>404</b> — проблема на стороне клиента (ресурса нет).<br>
<b>500</b> — проблема на стороне сервера (необработанное исключение, сбой логики).
</details>

<details>
<summary>Где находится информация о HTTP-методе запроса?</summary>
В первой строке HTTP-запроса (request line): <code>GET /books HTTP/1.1</code>.
</details>

<details>
<summary>Где находится тело (body) HTTP-запроса?</summary>
После заголовков, отделено пустой строкой. Используется в POST, PUT, PATCH.
</details>

<details>
<summary>Что такое JSON?</summary>
JavaScript Object Notation — текстовый формат обмена данными, легко читаемый человеком и машиной.
</details>

<details>
<summary>Почему JSON часто используется в Web API?</summary>
Компактный, независимый от языка, легко парсится, поддерживается всеми современными клиентами и фреймворками.
</details>

---

## ✅ Чек-лист выполнения задания

- [x] Создан проект `BookLibrary`
- [x] Создана модель `Book`
- [x] Данные хранятся в памяти (`List<Book>`)
- [x] `GET /books` возвращает список книг
- [x] `GET /books/{id}` возвращает книгу или `404`
- [x] `POST /books` добавляет книгу (с валидацией)
- [x] `DELETE /books/{id}` удаляет книгу или возвращает `404`
- [x] `GET /books?author=...` — фильтр по автору *(доп. задание)*
- [x] Изучены вопросы самопроверки

---

## 📖 Полезные ссылки

- [Routing в ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/routing)
- [Minimal APIs](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/minimal-apis)
- [HTTP-методы — MDN](https://developer.mozilla.org/ru/docs/Web/HTTP/Methods)
- [HTTP-статусы — MDN](https://developer.mozilla.org/ru/docs/Web/HTTP/Status)
- [REST API Best Practices](https://learn.microsoft.com/ru-ru/azure/architecture/best-practices/api-design)

---

## 📝 Лицензия

Учебный проект. Свободно для использования в образовательных целях.