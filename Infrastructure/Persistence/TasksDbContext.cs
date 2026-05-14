using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TasksDbContext : DbContext
{
    public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("tasks");
            entity.HasKey(task => task.Id);
            entity.Property(task => task.Id).HasColumnName("id");
            entity.Property(task => task.Title).HasColumnName("title").IsRequired().HasMaxLength(200);
            entity.Property(task => task.IsCompleted).HasColumnName("is_completed").IsRequired();
            entity.Property(task => task.Deadline).HasColumnName("deadline").HasColumnType("timestamp without time zone").IsRequired();
        });
    }
}