using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace TaskManagementAPI.Models.DTOs
{
    /// <summary>
    /// Model used for creating a new task.
    /// </summary>
    public class TaskCreateDto
    {
        /// <summary>
        /// The title of the task (required, max 100 chars).
        /// </summary>
        [Required(ErrorMessage = "The task title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// The description of the task (max 500 chars).
        /// </summary>
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// The task status (0 - New, 1 - Active, 2 - Resolved, 3 - Closed).
        /// </summary>
        [Range(0, 3, ErrorMessage = "Invalid status value.")]
        public StatusType Status { get; set; }

        /// <summary>
        /// The task priority (0 - Low, 1 - Normal, 2 - High).
        /// </summary>
        [Range(0, 2, ErrorMessage = "Invalid priority value.")]
        public PriorityType Priority { get; set; }
    }

    public class TaskUpdateDto
    {
        [Required(ErrorMessage = "The task id is required.")]
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the task (required, max 100 chars).
        /// </summary>
        [Required(ErrorMessage = "The task title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// The description of the task (max 500 chars).
        /// </summary>
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// The task status (0 - New, 1 - Active, 2 - Resolved, 3 - Closed).
        /// </summary>
        [Range(0, 3, ErrorMessage = "Invalid status value.")]
        public StatusType Status { get; set; }

        /// <summary>
        /// The task priority (0 - Low, 1 - Normal, 2 - High).
        /// </summary>
        [Range(0, 2, ErrorMessage = "Invalid priority value.")]
        public PriorityType Priority { get; set; }
    }


}
