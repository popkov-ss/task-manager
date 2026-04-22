namespace TaskManager.Core.Entities
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public Guid TaskTypeId { get; set; }
        public JobType? TaskType { get; set; }
    }
}
