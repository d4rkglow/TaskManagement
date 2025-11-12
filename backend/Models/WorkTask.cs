namespace TaskManagement.Models
{
    public class WorkTask
    {
        public WorkTask(string title) { 
            Title = title;
            Date = DateTime.Now;
            Status = StatusType.New;
            Priority = PriorityType.Normal;
        }

        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }
    }
}
