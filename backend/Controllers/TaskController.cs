using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Filters;
using TaskManagement.Interfaces;
using TaskManagement.Models;
using TaskManagementAPI.Models.DTOs;

namespace TaskManagement.Controllers
{
    [ApiController]
    [ApiKeyAuthFilter]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieves a list of all tasks.
        /// </summary>
        /// <returns>A list of tasks.</returns>
        [HttpGet("GetList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkTask>))]
        public async Task<IActionResult> GetTaskList()
        {
            try
            {
                var tasks = await _taskService.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task list.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        /// <summary>
        /// Retrieves a specific task by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>The requested task.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkTask))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTask(Guid id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="taskDto">The task data (title, description, status, priority).</param>
        /// <returns>The newly created task object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WorkTask))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskDto)
        {
            try
            {
                var createdTask = await _taskService.CreateTaskAsync(taskDto);

                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new task.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new task.");
            }
        }

        /// <summary>
        /// Updates all editable properties of an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="taskDto">The updated task data (title, description, status, priority).</param>
        /// <returns>A No Content response (204).</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditTask(Guid id, [FromBody] TaskUpdateDto taskDto)
        {
            try
            {
                if (id != taskDto.Id)
                {
                    return BadRequest("Task ID mismatch.");
                }

                bool success = await _taskService.UpdateTaskAsync(taskDto);

                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing task with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating task details.");
            }
        }

        /// <summary>
        /// Updates only the status of a specific task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="status">The new status value (0, 1, 2, or 3).</param>
        /// <returns>A No Content response (204).</returns>
        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTaskStatus(Guid id, [FromQuery] StatusType status)
        {
            try
            {
                bool success = await _taskService.UpdateTaskStatusAsync(id, status);

                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task status for ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating task status.");
            }
        }

        /// <summary>
        /// Updates only the priority of a specific task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="priority">The new priority value (e.g., 0, 1, or 2).</param>
        /// <returns>A No Content response (204).</returns>
        [HttpPatch("{id:guid}/priority")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePriority(Guid id, [FromQuery] PriorityType priority)
        {
            try
            {
                bool success = await _taskService.UpdatePriorityAsync(id, priority);

                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task priority for ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating task priority.");
            }
        }

        /// <summary>
        /// Deletes a specific task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>A No Content response (204).</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                bool success = await _taskService.DeleteTaskAsync(id);

                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting task.");
            }
        }
    }
}