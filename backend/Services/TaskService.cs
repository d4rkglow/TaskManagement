using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Interfaces;
using TaskManagement.Models;
using TaskManagementAPI.Models.DTOs;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly TaskDbContext _context;

        public TaskService(ILogger<TaskService> logger, TaskDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<WorkTask>> GetAllTasksAsync()
        {
            try
            {
                return await _context.Tasks.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all tasks from the database.");
                return new List<WorkTask>();
            }
        }

        public async Task<WorkTask?> GetTaskByIdAsync(Guid id)
        {
            try
            {
                return await _context.Tasks.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve task with ID {Id}.", id);
                return null;
            }
        }

        public async Task<WorkTask> CreateTaskAsync(TaskCreateDto taskDto)
        {
            try
            {
                var newTask = new WorkTask()
                {
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    Status = taskDto.Status,
                    Priority = taskDto.Priority,
                    Date = DateTime.UtcNow
                };

                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();
                return newTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create new task.");
                throw;
            }
        }

        public async Task<bool> UpdateTaskAsync(TaskUpdateDto task)
        {
            try
            {
                var editTask = await _context.Tasks.FindAsync(task.Id);

                if (editTask != null)
                {
                    editTask.Title = task.Title;
                    editTask.Description = task.Description;
                    editTask.Status = task.Status;
                    editTask.Priority = task.Priority;

                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update details for task ID {Id}.", task.Id);
                return false;
            }
        }

        public async Task<bool> UpdateTaskStatusAsync(Guid id, StatusType status)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task != null)
                {
                    task.Status = status;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update status for task ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> UpdatePriorityAsync(Guid id, PriorityType priority)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task != null)
                {
                    task.Priority = priority;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update priority for task ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task != null)
                {
                    _context.Tasks.Remove(task);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete task with ID {Id}.", id);
                return false;
            }
        }
    }
}