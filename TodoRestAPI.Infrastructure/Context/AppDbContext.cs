using Microsoft.EntityFrameworkCore;
using TodoRestAPI.Domain.Entities;

namespace TodoRestAPI.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TodoTask> TodoTasks { get; set; }
    }
}