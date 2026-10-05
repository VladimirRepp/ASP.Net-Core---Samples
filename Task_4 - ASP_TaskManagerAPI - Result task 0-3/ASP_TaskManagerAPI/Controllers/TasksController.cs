using ASP_TaskManagerAPI.Models;
using ASP_TaskManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_TaskManagerAPI.Controllers
{
    [ApiController]          
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        #region === Public API ===
        // GET /api/tasks
        // GET /api/tasks?completed=true
        // GET /api/tasks?completed=false
        // GET /api/tasks?priority=1
        
        [HttpGet]
        public IActionResult GetAll([FromQuery] bool? completed, [FromQuery] int? priority)
        {
            // Оба параметра опциональны. Если ни один не задан — вернуть все.
            if (completed.HasValue)
                return Ok(_taskService.GetByCompletion(completed.Value));

            if (priority.HasValue)
                return Ok(_taskService.GetByPriority(priority.Value));

            return Ok(_taskService.GetAll());
        }

        // GET /api/tasks/{id}
        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            if (!_taskService.TryGetById(id, out var task) || task == null)
                return NotFound();

            return task;
        }

        // POST /api/tasks
        [HttpPost]
        public ActionResult<TaskItem> Create([FromBody] TaskItem task)
        {
            var created = _taskService.Add(task);
            return CreatedAtAction(nameof(GetById), new { id = created.ID }, created);
        }

        // PUT /api/tasks/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskItem task)
        {
            if (!_taskService.TryUpdate(id, task))
                return NotFound();

            return NoContent();
        }

        // DELETE /api/tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_taskService.TryDelete(id))
                return NotFound();

            return NoContent();
        }

        #endregion
    }
}
