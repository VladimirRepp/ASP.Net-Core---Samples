using ASP_StudentApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_StudentApi.Controllers
{
    [ApiController]             // - > Атрибут, который сообщает ASP.NET Core, что этот класс является контроллером Web API.
    [Route("api/[controller]")] // - > Атрибут, который задаёт маршрут для контроллера. [controller] будет заменено на имя контроллера без суффикса "Controller" (в данном случае "students").
    public class StudentsController : ControllerBase // Для API наследуйся от ControllerBase (не от Controller). Controller нужен только если ты отдаёшь HTML-вьюхи. ControllerBase даёт тебе Ok(), NotFound(), Created() и т.д.
    {
        private List<Student> _students;
        private int _nextId = 0;

        public StudentsController()
        {
            // Инициализация данных студентов
            _students = new List<Student>
            {
                new Student { Id = 1, Name = "Oleg", Age = 20 },
                new Student { Id = 2, Name = "Anna", Age = 22 },
                new Student { Id = 3, Name = "Ivan", Age = 21 }
            };
            _nextId = _students.Max(s => s.Id) + 1;
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
            return Ok(GetAllStudents());  // Ok() возвращает статус 200 + тело
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetById(int id) {
            if (!TryGetStudentByID(id, out var student))
                return NotFound();

            return student;  // неявно превращается в Ok(student)
        }

        [HttpGet("older-than/{age}")]
        public ActionResult<Student> OlderThan(int age)
        {
            var students = GetStudentsOlderThan(age);
            return Ok(students);
        }

        [HttpPost]
        public ActionResult<int> Create([FromBody] Student student) {
            int id = AddStudent(student);
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

            if (!TryUpdateStudent(id, student))
                return NotFound();

            return NoContent(); // 204
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!TryDeleteStudent(id))
                return NotFound();

            return NoContent(); // 204
        }
        #endregion

        #region === Private Methods ===

        private List<Student> GetAllStudents()
        {
            return _students;
        }

        private bool TryGetStudentByID(int id, out Student student)
        {
            student = _students.FirstOrDefault(s => s.Id == id);
            return student != null;
        }

        private int AddStudent(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);

            return student.Id;
        }

        private bool TryUpdateStudent(int id, Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null)
                return false;

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;

            return true;
        }

        private bool TryDeleteStudent(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return false;

            _students.Remove(student);

            return true;
        }

        private List<Student> GetStudentsOlderThan(int age)
        {
            return _students.Where(s => s.Age > age).ToList();
        }

        #endregion
    }
}
