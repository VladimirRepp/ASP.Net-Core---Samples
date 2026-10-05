namespace ASP_TaskManagerAPI.Models
{
    public class TaskItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
