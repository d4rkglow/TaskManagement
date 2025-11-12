using TaskManagement.Models;
using TaskManagementAPI.Models.DTOs;

namespace TaskManagement.Interfaces
{
    public interface ITaskService
    {
        Task<List<WorkTask>> GetAllTasksAsync();
        Task<WorkTask?> GetTaskByIdAsync(Guid id);
        Task<WorkTask> CreateTaskAsync(TaskCreateDto newTask);
        Task<bool> UpdateTaskAsync(TaskUpdateDto task);
        Task<bool> UpdateTaskStatusAsync(Guid id, StatusType status);
        Task<bool> UpdatePriorityAsync(Guid id, PriorityType priority);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
