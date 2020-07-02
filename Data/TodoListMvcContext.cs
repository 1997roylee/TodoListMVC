using Microsoft.EntityFrameworkCore;
using TodoListMvc.Models;

namespace TodoListMvc.Data
{
    public class TodoListMvcContext : DbContext
    {
        public TodoListMvcContext(DbContextOptions<TodoListMvcContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<TodoItem>().Property(todo => todo.Id).ValueGeneratedOnAdd();

        public DbSet<TodoItem> TodoItem { get; set; }
    }
}