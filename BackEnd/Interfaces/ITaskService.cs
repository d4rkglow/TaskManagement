using TaskManagement.Models;

namespace TaskManagement.Interfaces
{
    public interface ITaskService
    {
        Task<List<WorkTask>> GetAllTasksAsync();
        Task<WorkTask?> GetTaskByIdAsync(Guid id);
        Task<WorkTask> CreateTaskAsync(WorkTask newTask);
        Task<bool> UpdateTaskDetailsAsync(WorkTask task);
        Task<bool> UpdateTaskStatusAsync(Guid id, StatusType status);
        Task<bool> UpdatePriorityAsync(Guid id, PriorityType priority);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
