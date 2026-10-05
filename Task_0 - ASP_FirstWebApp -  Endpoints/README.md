# First ASP.NET Core Application 🚀

Репозиторий с решением задания **«Первое ASP.NET Core приложение»** — практикум по основам ASP.NET Core: структура проекта, обработка HTTP-запросов, endpoints и middleware.

---

## 📋 Описание задания

**Уровень сложности:** очень простой

**Цель:** познакомиться со структурой ASP.NET Core-проекта, запустить первое веб-приложение и понять, как запрос пользователя превращается в ответ сервера.

---

## 📚 Термины

| Термин | Определение |
|---|---|
| **ASP.NET Core** | Кроссплатформенный фреймворк от Microsoft для создания веб-приложений, Web API и серверных приложений. |
| **Web-приложение** | Программа, которая работает на сервере и взаимодействует с клиентом через HTTP. |
| **HTTP-запрос** | Сообщение от клиента серверу. Например: `GET /hello` |
| **HTTP-ответ** | Сообщение сервера клиенту. |
| **Endpoint** | Конкретная точка приложения, которая отвечает на определённый HTTP-запрос. |
| **Middleware** | Компонент ASP.NET Core, участвующий в обработке HTTP-запроса. |
| **Program.cs** | Основной файл запуска и настройки приложения. |

---

## 🛠️ Требования

- [.NET SDK](https://dotnet.microsoft.com/download) (версия 6.0 или выше)
- Одна из сред разработки:
  - Visual Studio 2022+
  - JetBrains Rider
  - Visual Studio Code + C# расширение
  - Или просто командная строка

---

## 🚀 Запуск проекта

### Через командную строку

```bash
# Клонирование репозитория
git clone https://github.com/<username>/FirstAspNetApp.git
cd FirstAspNetApp

# Восстановление зависимостей
dotnet restore

# Запуск приложения
dotnet run
```

После запуска приложение будет доступно по адресу:
- 🔒 `https://localhost:7000`
- 🔓 `http://localhost:5000`

*(точный порт смотрите в файле `Properties/launchSettings.json` или в выводе консоли)*

### Через Visual Studio / Rider

Просто нажмите **F5** (Run) или **Ctrl+F5** (Run without debugging).

---

## 🌐 Реализованные endpoint'ы

| Метод | Маршрут | Ответ |
|---|---|---|
| `GET` | `/` | `Hello, ASP.NET Core!` |
| `GET` | `/about` | `Это страница About` |
| `GET` | `/contact` | `Это страница Contact` |
| `GET` | `/student` | `Это страница Student` |
| `GET` | `/hello/{name}` | `Hello, {name}!` |
| `GET` | `/calculator/add/{a}/{b}` | Сумма чисел `a + b` |

### Примеры запросов

```
GET /hello/Vladimir      →  Hello, Vladimir!
GET /calculator/add/10/20 →  30
```

---

## 📂 Структура проекта

```
FirstAspNetApp/
├── Program.cs              # Точка входа и настройка приложения
├── appsettings.json        # Конфигурация
├── FirstAspNetApp.csproj   # Файл проекта
├── Properties/
│   └── launchSettings.json # Настройки запуска (порты, профили)
└── README.md
```

## 🧠 Как это работает

1. **`WebApplication.CreateBuilder(args)`** — создаёт объект-построитель, который собирает конфигурацию, сервисы и логирование.
2. **`builder.Build()`** — собирает приложение на основе настроек.
3. **`app.MapGet(...)`** — регистрирует endpoint'ы: какой маршрут и какой HTTP-метод обрабатывается.
4. **`app.Run()`** — запускает веб-сервер (Kestrel) и начинает слушать входящие HTTP-запросы.

**Цепочка обработки запроса:**
```
Клиент → HTTP-запрос → Middleware → Endpoint → HTTP-ответ → Клиент
```

---

## ❓ Вопросы для самопроверки

<details>
<summary>Что такое HTTP?</summary>
Протокол передачи гипертекста — прикладной протокол для обмена сообщениями между клиентом и сервером в модели «запрос-ответ».
</details>

<details>
<summary>Чем GET отличается от POST?</summary>
<b>GET</b> —用于 получения данных, параметры передаются в URL, идемпотентен.<br>
<b>POST</b> —用于 отправки данных на сервер, параметры в теле запроса, не идемпотентен.
</details>

<details>
<summary>Что такое URL?</summary>
Uniform Resource Locator — адрес ресурса в сети: протокол + хост + порт + путь + параметры.
</details>

<details>
<summary>Что означает <code>/</code> в маршруте?</summary>
Корневой путь приложения — endpoint, обрабатывающий запрос к главной странице.
</details>

<details>
<summary>Что такое endpoint?</summary>
Точка приложения (маршрут + метод), которая обрабатывает определённый HTTP-запрос и возвращает ответ.
</details>

<details>
<summary>Что произойдёт, если открыть несуществующий адрес?</summary>
Сервер вернёт HTTP-статус <b>404 Not Found</b>, так как для этого маршрута нет зарегистрированного endpoint'а.
</details>

<details>
<summary>Что делает <code>app.Run()</code>?</summary>
Запускает веб-сервер и блокирует поток, начиная обработку входящих HTTP-запросов.
</details>

<details>
<summary>Чем <code>MapGet()</code> отличается от <code>MapPost()</code>?</summary>
<b>MapGet</b> регистрирует обработчик для HTTP-метода GET, <b>MapPost</b> — для POST.
</details>

<details>
<summary>Что такое route parameter?</summary>
Переменная в маршруте, заключённая в фигурные скобки (например, <code>{name}</code>), значение которой извлекается из URL.
</details>

<details>
<summary>Откуда ASP.NET Core получает значение <code>{name}</code>?</summary>
Из сегмента URL-адреса запроса в позиции, соответствующей параметру маршрута.
</details>

<details>
<summary>Что произойдёт, если написать <code>/hello/John/Smith</code>?</summary>
Маршрут не совпадёт с шаблоном <code>/hello/{name}</code> — сервер вернёт <b>404</b>.
</details>

<details>
<summary>Для чего нужен файл <code>Program.cs</code>?</summary>
Это точка входа: настройка сервисов, middleware, endpoint'ов и запуск приложения.
</details>

---

## ✅ Чек-лист выполнения задания

- [x] Создан ASP.NET Core Empty проект
- [x] Приложение успешно запускается
- [x] Реализован `GET /`
- [x] Реализован `GET /about`
- [x] Реализован `GET /contact`
- [x] Реализован `GET /student`
- [x] Реализован `GET /hello/{name}`
- [x] Реализован `GET /calculator/add/{a}/{b}` *(доп. задание)*
- [x] Разобрана структура `Program.cs`
- [x] Изучены вопросы самопроверки

---

## 📖 Полезные ссылки

- [Документация ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/)
- [Minimal APIs в ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/minimal-apis)
- [Маршрутизация в ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/routing)
- [HTTP — MDN](https://developer.mozilla.org/ru/docs/Web/HTTP)

---

## 📝 Лицензия

Учебный проект. Свободно для использования в образовательных целях.