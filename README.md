# ASP.NET Core Practice 🎯

Учебный репозиторий с решениями практических заданий по **ASP.NET Core** — от первого «Hello, World!» до полноценного REST API с контроллерами, Dependency Injection и Swagger.

Здесь собраны пошаговые примеры, которые помогут разобраться со структурой ASP.NET Core-приложения, маршрутизацией, HTTP-методами, сервисным слоем и построением Web API.

---

## 📚 Содержание

| № | Задание | Уровень | Ключевые темы |
|---|---|---|---|
| 1 | Task_0 - ASP_FirstWebApp -  Endpoints | 🟢 очень простой | Program.cs, Minimal API, endpoints, middleware |
| 2 | Task_1 - ASP_BookLibrary - Routing and HTTP requests | 🟢 очень простой → простой | HTTP-методы, статусы, route/query параметры |
| 3 | Task_2 - ASP_StudentApi - Web API + Controller | 🟡 простой | Controllers, Actions, атрибуты, IActionResult |
| 4 | Task_3 - ASP_StudentApi - Dependency Injection and services | 🟡 простой | Service Layer, интерфейсы, DI, lifetime |
| 5 | Task_4 - ASP_TaskManagerAPI - (мини-проект) | 🟡 простой | REST, CRUD, DTO, обработка ошибок, Swagger |

> 📁 Каждое задание лежит в отдельной папке со своим `README.md`, исходным кодом и примерами запросов.

---

## 🛠️ Общие требования

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- Одна из сред разработки:
  - **Visual Studio 2022+** — Windows / macOS
  - **JetBrains Rider** — Windows / macOS / Linux
  - **Visual Studio Code** + расширение C#
- Инструмент для тестирования API:
  - **Swagger UI** (встроен в Web API проекты)
  - [Postman](https://www.postman.com/)
  - [Bruno](https://www.usebruno.com/)
  - [REST Client (VS Code)](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
  - `curl`

---

## 🚀 Как запустить любое задание

Каждое задание — самостоятельный проект. Перейдите в нужную папку и запустите:

```bash
cd 0X-название-задания/ИмяПроекта

# Восстановить зависимости
dotnet restore

# Запустить приложение
dotnet run
```

По умолчанию приложение будет доступно:
- 🔒 `https://localhost:7000`
- 🔓 `http://localhost:5000`

Точные порты смотрите в `Properties/launchSettings.json` или в выводе консоли.

Для проектов с Web API — Swagger UI открывается по адресу:
```
https://localhost:7000/swagger
```

---

## 🧭 Прогресс обучения

```
[✔] 1. Первое приложение         →  Program.cs, endpoints
[✔] 2. Routing и HTTP            →  методы, статусы, параметры
[✔] 3. Web API + Controller      →  контроллеры, атрибуты, actions
[✔] 4. DI и сервисы              →  интерфейсы, lifetime, Service Layer
[✔] 5. Мини-проект Task Manager  →  REST, CRUD, Swagger, ошибки
```

---

## 🧠 Чему учит этот репозиторий

- **Архитектура ASP.NET Core** — от `Program.cs` до многослойного приложения.
- **REST-дизайн** — ресурсы, HTTP-методы, статусы.
- **Dependency Injection** — регистрация сервисов, время жизни, инверсия зависимостей.
- **Обработка ошибок** — валидация входных данных, корректные коды ответов.
- **Документирование API** — Swagger / OpenAPI.
- **Разделение ответственности** — Model / Controller / Service.

---

## 🧩 Стек

| Компонент | Что используется |
|---|---|
| Платформа | .NET 6+ |
| Фреймворк | ASP.NET Core (Minimal API + Controllers) |
| Язык | C# |
| Формат данных | JSON (System.Text.Json) |
| Документация | Swagger / OpenAPI |
| Хранение данных | In-Memory (List) |
| DI | Встроенный DI-контейнер ASP.NET Core |

---

## ➕ Дополнительные примеры (в планах)

В папке `examples/` планируется добавлять примеры по темам:

- [ ] Работа с **Entity Framework Core** (SQLite / PostgreSQL)
- [ ] **DTO** и **AutoMapper**
- [ ] **FluentValidation** для валидации
- [ ] **Middleware** собственной разработки
- [ ] **Аутентификация и авторизация** (JWT)
- [ ] **Юнит-тесты** с xUnit + Moq
- [ ] **Docker** для ASP.NET Core приложения
- [ ] **Background Services** (`IHostedService`)
- [ ] **SignalR** — реальное время
- [ ] **gRPC** — альтернатива REST

Если хотите добавить свой пример — создайте папку в `examples/` по шаблону и оформите собственный `README.md`.

---

## 📖 Полезные материалы

### Официальная документация
- [ASP.NET Core — обзор](https://learn.microsoft.com/ru-ru/aspnet/core/introduction-to-aspnet-core)
- [Minimal APIs](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/minimal-apis)
- [Controllers в Web API](https://learn.microsoft.com/ru-ru/aspnet/core/web-api/)
- [Dependency Injection](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/dependency-injection)
- [Swagger / OpenAPI](https://learn.microsoft.com/ru-ru/aspnet/core/tutorials/web-api-help-pages-using-swagger)

### Основы HTTP и REST
- [HTTP — MDN](https://developer.mozilla.org/ru/docs/Web/HTTP)
- [HTTP-методы — MDN](https://developer.mozilla.org/ru/docs/Web/HTTP/Methods)
- [HTTP-статусы — MDN](https://developer.mozilla.org/ru/docs/Web/HTTP/Status)
- [REST API — Best Practices](https://learn.microsoft.com/ru-ru/azure/architecture/best-practices/api-design)

### Инструменты
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Postman](https://www.postman.com/downloads/)
- [Bruno](https://www.usebruno.com/)
- [Swagger Editor](https://editor.swagger.io/)

---

## 🎓 Как работать с этим репозиторием

1. **Начинайте с первого задания.** Не переходите к следующему, пока не разобрались с текущим.
2. **Запускайте каждый проект локально.** Читать код недостаточно — нужно видеть, как он работает.
3. **Экспериментируйте.** Меняйте маршруты, добавляйте поля, ломайте и починяйте.
4. **Отвечайте на вопросы самопроверки** в README каждого задания — они для закрепления.
5. **Делайте дополнительные задания** — они помечены в конце каждого README.
6. **Сравнивайте свой код с эталонным.** Но лучше — пишите сначала сами, потом сверяйтесь.

---

## 🤝 Как добавить своё задание

1. Создайте папку в корне репозитория: `06-название-задания/`.
2. Внутри — проект(ы) .NET и `README.md` по образцу существующих.
3. Обновите таблицу «Содержание» и «Структура репозитория» в этом файле.
4. Добавьте запись в раздел «Прогресс обучения».
5. Оформите Pull Request / коммит.

---

## 📝 Лицензия

Учебный проект. Свободно для использования в образовательных целях.

---

## ⭐ Если пригодилось

Поставьте звезду репозиторию — это мотивирует добавлять новые примеры и задания.