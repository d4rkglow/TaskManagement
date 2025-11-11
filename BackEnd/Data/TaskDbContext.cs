using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<WorkTask> Tasks { get; set; }
    }
}
