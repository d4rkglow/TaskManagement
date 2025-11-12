using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Filters;
using TaskManagement.Interfaces;
using TaskManagement.Models;

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

        [HttpGet("GetList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpGet("Get{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpPost("New")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTask(WorkTask newTask)
        {
            if (newTask == null)
            {
                return BadRequest("Invalid task data provided.");
            }

            try
            {
                var createdTask = await _taskService.CreateTaskAsync(newTask);
                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new task.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new task.");
            }
        }

        [HttpPut("Edit{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditTask(Guid id, WorkTask task)
        {
            if (id != task.Id)
            {
                return BadRequest("Task ID mismatch.");
            }

            try
            {
                bool success = await _taskService.UpdateTaskAsync(task);

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

        [HttpPatch("EditStatus{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTaskStatus(Guid id, StatusType status)
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

        [HttpPatch("EditPriority{id:guid}/priority")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePriority(Guid id, PriorityType priority)
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

        [HttpDelete("Delete{id:guid}")]
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
