using ASP_StudentApi.Models;
using ASP_StudentApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_StudentApi.Controllers
{
    [ApiController]             // - > Атрибут, который сообщает ASP.NET Core, что этот класс является контроллером Web API.
    [Route("api/[controller]")] // - > Атрибут, который задаёт маршрут для контроллера. [controller] будет заменено на имя контроллера без суффикса "Controller" (в данном случае "students").
    public class StudentsController : ControllerBase // Для API наследуйся от ControllerBase (не от Controller). Controller нужен только если ты отдаёшь HTML-вьюхи. ControllerBase даёт тебе Ok(), NotFound(), Created() и т.д.
    {
        private readonly StudentsService _studentsService;

        //public StudentsController()
        //{
        //    // Так делать плохо, нужно использовать Dependency Injection
        //    _studentsService = new();


        // ASP.NET Core создаёт новый экземпляр контроллера на каждый запрос.
        // Это не случайность — это архитектурное решение фреймворка.
        // Поэтому не стоит хранить состояние в полях контроллера, лучше использовать сервисы,
        // Которые зарегистрованы в DI-контейнере (Dependency Injection) и живут дольше, чем один запрос.

        //}

        public StudentsController(StudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        #region === Public API ===

        // Возвращаемые типы: IActionResult vs ActionResult<T>:
        // public IActionResult GetById(int id) { ... }          - только статус + тело
        // public ActionResult<Book> GetById(int id) { ... }     - статус + типизированное тело
        // public Book GetById(int id) { ... }                   - только данные, статус всегда 200

        // Атрибуты привязки: [FromBody], [FromQuery], [FromRoute]
        // ASP.NET Core угадывает источник, но иногда угадывает неправильно. Можно указывать явно:
        //[HttpGet]
        //public IActionResult GetAll([FromQuery] string? author) { ... }

        //[HttpGet("{id}")]
        //public IActionResult GetById([FromRoute] int id) { ... }

        //[HttpPost]
        //public IActionResult Create([FromBody] Book book) { ... }\

        // Правила «угадывания» по умолчанию:
        // - Простой тип(int, string) → ищет в route, потом в query.
        // - Сложный тип(класс) → берёт из body.

        // Каждый атрибут говорит, на какой HTTP-метод реагирует экшен
        [HttpGet]
        public IActionResult GetAll() {
            return Ok(_studentsService.GetAllStudents());  // Ok() возвращает статус 200 + тело
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetById(int id) {
            if (!_studentsService.TryGetStudentByID(id, out var student))
                return NotFound();

            return student;  // неявно превращается в Ok(student)
        }

        [HttpGet("older-than/{age}")]
        public ActionResult<Student> OlderThan(int age)
        {
            var students = _studentsService.GetStudentsOlderThan(age);
            return Ok(students);
        }

        [HttpPost]
        public ActionResult<int> Create([FromBody] Student student) {
            int id = _studentsService.AddStudent(student);
            student.Id = id;

            return CreatedAtAction(nameof(GetById), new { id = id }, student);

            // CreatedAtAction — магия ASP.NET Core: он сам сгенерит URL /api/students/{id} на основе имени
            // экшена и поставит правильный заголовок Location.
            // Разберись, как это работает — это стандарт.
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student student)
        {
            // id в URL НЕ совпадает с id в теле запроса
            // Так как id в теле (student) не имет это значение

            if (!_studentsService.TryUpdateStudent(id, student))
                return NotFound();

            return NoContent(); // 204
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_studentsService.TryDeleteStudent(id))
                return NotFound();

            return NoContent(); // 204
        }
        #endregion

        #region === Private Methods ===


        #endregion
    }
}
