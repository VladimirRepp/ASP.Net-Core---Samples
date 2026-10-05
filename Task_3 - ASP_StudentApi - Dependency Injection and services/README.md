# StudentApi + Dependency Injection 💉

Репозиторий с решением задания **«Dependency Injection и сервисы»** — практикум по вынесению бизнес-логики из контроллера в сервисный слой и использованию встроенного DI-контейнера ASP.NET Core.

---

## 📋 Описание задания

**Уровень сложности:** простой

**Цель:** научиться отделять Controller от бизнес-логики с помощью Dependency Injection.

**Было (задание 3):**

```
Controller → List<Student>
```

**Стало (задание 4):**

```
Controller
     ↓
IStudentService
     ↓
StudentService
     ↓
List<Student>
```

**Предметная область:** тот же `StudentApi`, но с выделенным сервисным слоем.

---

## 📚 Термины

| Термин | Определение |
|---|---|
| **Business Logic** | Правила и логика работы приложения. |
| **Service** | Класс, содержащий определённую бизнес-логику. |
| **Dependency** | Объект, который необходим другому объекту для работы. |
| **Dependency Injection (DI)** | Механизм, при котором зависимости передаются объекту извне. |
| **DI Container** | Система ASP.NET Core, которая создаёт и предоставляет зарегистрированные зависимости. |
| **Interface** | Контракт, описывающий доступные методы объекта. |

### Lifetime сервисов

| Lifetime | Поведение | Когда использовать |
|---|---|---|
| **AddSingleton** | Один экземпляр на всё время работы приложения. | Stateless-сервисы, кэши, конфигурации. |
| **AddScoped** | Один экземпляр в пределах scope (в Web API — в пределах HTTP-запроса). | `DbContext`, репозитории, работа с БД. |
| **AddTransient** | Новый экземпляр при каждом запросе зависимости. | Лёгкие stateless-сервисы. |

---

## 🛠️ Требования

- [.NET SDK](https://dotnet.microsoft.com/download) (версия 6.0 или выше)
- Одна из сред разработки:
  - Visual Studio 2022+
  - JetBrains Rider
  - Visual Studio Code + C# расширение
- Инструмент для тестирования API:
  - Swagger, Postman, Bruno, REST Client или `curl`

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

После запуска приложение доступно по адресу:
- 🔒 `https://localhost:7000`
- 🔓 `http://localhost:5000`
- 📄 Swagger UI: `https://localhost:7000/swagger`

### Через Visual Studio / Rider

Нажмите **F5** (Run) или **Ctrl+F5** (Run without debugging).

---

## 🌐 Реализованные endpoint'ы

| Метод | Маршрут | Назначение |
|---|---|---|
| `GET` | `/api/students` | Получить всех студентов |
| `GET` | `/api/students/{id}` | Получить студента по Id |
| `POST` | `/api/students` | Создать студента |
| `PUT` | `/api/students/{id}` | Изменить студента |
| `DELETE` | `/api/students/{id}` | Удалить студента |
| `GET` | `/api/students/older-than/{age}` | Студенты старше указанного возраста *(доп.)* |

---

## 📂 Структура проекта

```
StudentApi/
│
├── Controllers/
│   └── StudentsController.cs    # Тонкий контроллер — только маршрутизация
│
├── Models/
│   └── Student.cs               # Модель предметной области
│
├── Services/
│   ├── IDataService.cs       # Интерфейс сервиса (контракт)
│   └── StudentService.cs        # Реализация бизнес-логики
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── Program.cs                   # Регистрация сервисов в DI
├── StudentApi.csproj
└── README.md
```
---

## 🧪 Примеры тестирования

### curl

```bash
# Все студенты
curl https://localhost:7000/api/students

# Студент по Id
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

### Что даёт DI

1. **Controller не создаёт сервис сам.** Он лишь объявляет, что ему нужен `IStudentService`. ASP.NET Core сам создаёт и передаёт экземпляр.
2. **Слабая связанность.** Controller зависит от интерфейса, а не от конкретной реализации. Легко подменить `StudentService` на другую реализацию (например, работающую с БД).
3. **Тестируемость.** В unit-тестах легко подставить мок `IStudentService`.
4. **Управление жизненным циклом.** Lifetime сервисов контролируется DI-контейнером.

### Как работает DI-контейнер

```
1. Запрос → Controller
2. Контейнер видит: StudentsController требует IStudentService
3. Контейнер смотрит регистрации: IStudentService → StudentService
4. Контейнер создаёт экземпляр StudentService (с учётом lifetime)
5. Экземпляр передаётся в конструктор контроллера
```

### Выбор Lifetime

| Сервис | Рекомендуемый lifetime | Причина |
|---|---|---|
| `StudentService` (в памяти) | `Singleton` | Данные должны сохраняться между запросами |
| Сервис с `DbContext` | `Scoped` | `DbContext` не потокобезопасен |
| Лёгкий stateless-сервис | `Transient` | Не хранит состояние |

> ⚠️ **Важно:** нельзя «потреблять» `Scoped`-сервис внутри `Singleton`-сервиса — это приведёт к ошибке при валидации scope.

---

## ❓ Вопросы для самопроверки

<details>
<summary>Что такое Dependency Injection?</summary>
Механизм, при котором объект получает свои зависимости извне (обычно через конструктор), а не создаёт их самостоятельно.
</details>

<details>
<summary>Какую проблему решает DI?</summary>
Убирает жёсткую связанность между классами, упрощает тестирование, повторное использование и замену реализаций, централизует управление временем жизни объектов.
</details>

<details>
<summary>Почему Controller не должен самостоятельно создавать Service?</summary>
Это создаёт жёсткую связанность (<code>new StudentService()</code>), усложняет тестирование и замену реализации, а также лишает возможности управлять lifetime через DI-контейнер.
</details>

<details>
<summary>Что такое Dependency?</summary>
Объект, который нужен другому объекту для выполнения его работы. Например, для <code>StudentsController</code> зависимость — это <code>IStudentService</code>.
</details>

<details>
<summary>Зачем нужен интерфейс IStudentService?</summary>
Он задаёт контракт: что умеет сервис, не раскрывая как именно. Позволяет подменять реализацию, упрощает тестирование и соблюдает принцип инверсии зависимостей (DIP).
</details>

<details>
<summary>Что произойдёт, если не зарегистрировать StudentService в DI?</summary>
При попытке создать контроллер ASP.NET Core выбросит исключение <code>InvalidOperationException: Unable to resolve service for type 'IStudentService'</code>.
</details>

<details>
<summary>В чём отличие Singleton от Scoped?</summary>
<b>Singleton</b> — один экземпляр на всё приложение.<br>
<b>Scoped</b> — один экземпляр в рамках scope (в Web API — на каждый HTTP-запрос).
</details>

<details>
<summary>В чём отличие Scoped от Transient?</summary>
<b>Scoped</b> — один экземпляр на запрос.<br>
<b>Transient</b> — новый экземпляр при каждом запросе зависимости (даже в рамках одного HTTP-запроса).
</details>

<details>
<summary>Какой lifetime обычно используется для сервисов, работающих с DbContext?</summary>
<b>Scoped</b> — потому что <code>DbContext</code> не потокобезопасен и должен жить в пределах одного запроса.
</details>

<details>
<summary>Почему бизнес-логику полезно выносить из Controller?</summary>
Контроллер остаётся тонким — только маршрутизация и формирование ответа. Логику можно переиспользовать (например, из фоновых задач), тестировать отдельно и менять независимо.
</details>

<details>
<summary>Как DI упрощает тестирование?</summary>
Позволяет в тестах подставлять мок-реализации интерфейсов вместо реальных сервисов, не изменяя код контроллера.
</details>

<details>
<summary>Что произойдёт, если заменить StudentService другой реализацией IStudentService?</summary>
Ничего в контроллере менять не нужно — достаточно зарегистрировать новую реализацию в <code>Program.cs</code>. Controller продолжит работать через интерфейс.
</details>

---

## ✅ Чек-лист выполнения задания

- [x] Создан интерфейс `IStudentService`
- [x] Создана реализация `StudentService`
- [x] Работа с коллекцией перенесена из Controller в Service
- [x] Сервис зарегистрирован в `Program.cs` через `AddSingleton`
- [x] Controller получает зависимость через конструктор
- [x] Убраны `new StudentService()` из контроллера
- [x] Изучены lifetime: `Singleton`, `Scoped`, `Transient`
- [x] Добавлен метод `GetStudentsByAge(int minAge)` *(доп. задание)*
- [x] Изучены вопросы самопроверки

---

## 📖 Полезные ссылки

- [Dependency Injection в ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/dependency-injection)
- [DI в .NET](https://learn.microsoft.com/ru-ru/dotnet/core/extensions/dependency-injection)
- [Service Lifetimes](https://learn.microsoft.com/ru-ru/dotnet/core/extensions/dependency-injection#service-lifetimes)
- [SOLID: Dependency Inversion Principle](https://learn.microsoft.com/ru-ru/dotnet/architecture/modern-web-apps-azure/architectural-principles)

---

## 📝 Лицензия

Учебный проект. Свободно для использования в образовательных целях.