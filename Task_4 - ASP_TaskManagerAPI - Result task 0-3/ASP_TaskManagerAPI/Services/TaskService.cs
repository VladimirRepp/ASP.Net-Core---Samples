using ASP_TaskManagerAPI.Models;

namespace ASP_TaskManagerAPI.Services
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public TaskService()
        {
            // DB или иной загрузчик
            _tasks.Add(new TaskItem 
            { 
                ID = _nextId++, 
                Title = "Task 1", 
                Description = "Description 1", 
                IsCompleted = false, 
                Priority = 1, 
                CreatedAt = DateTime.UtcNow 
            });

            _tasks.Add(new TaskItem
            {
                ID = _nextId++,
                Title = "Task 2",
                Description = "Description 2",
                IsCompleted = true,
                Priority = 2,
                CreatedAt = DateTime.UtcNow
            });

            _tasks.Add(new TaskItem
            {
                ID = _nextId++,
                Title = "Task 3",
                Description = "Description 3",
                IsCompleted = false,
                Priority = 3,
                CreatedAt = DateTime.UtcNow
            });
        }

        #region === CRUD ===

        public List<TaskItem> GetAll()
        {
            return _tasks;
        }

        public bool TryGetById(int id, out TaskItem? task)
        {
            task = _tasks.FirstOrDefault(t => t.ID == id);
            return task != null;
        }

        public TaskItem Add(TaskItem task)
        {
            task.ID = _nextId++;
            task.CreatedAt = DateTime.UtcNow;
            _tasks.Add(task);
            return task;
        }

        public bool TryUpdate(int id, TaskItem updated)
        {
            if (!TryGetById(id, out var existing) || existing == null)
                return false;

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.IsCompleted = updated.IsCompleted;
            existing.Priority = updated.Priority;
            // ID и CreatedAt не меняем — это "паспорт" задачи

            return true;
        }

        public bool TryDelete(int id)
        {
            if (!TryGetById(id, out var task) || task == null)
                return false;

            _tasks.Remove(task);
            return true;
        }

        #endregion

        #region === Фильтры ===

        public List<TaskItem> GetByCompletion(bool isCompleted)
        {
            return _tasks.Where(t => t.IsCompleted == isCompleted).ToList();
        }

        public List<TaskItem> GetByPriority(int priority)
        {
            return _tasks.Where(t => t.Priority == priority).ToList();
        }

        #endregion
    }
}
