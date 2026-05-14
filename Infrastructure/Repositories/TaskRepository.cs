using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TasksDbContext _dbContext;

    public TaskRepository(TasksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<TaskItem> GetAll()
    {
        return _dbContext.Tasks.AsNoTracking().ToList();
    }

    public TaskItem? GetById(Guid id)
    {
        return _dbContext.Tasks.AsNoTracking().FirstOrDefault(task => task.Id == id);
    }

    public TaskItem Create(TaskItem task)
    {
        task.Id = Guid.NewGuid();

        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();

        return task;
    }

    public TaskItem? Update(TaskItem updateTask)
    {
        var existingTask = _dbContext.Tasks.FirstOrDefault(task => task.Id == updateTask.Id);

        if (existingTask is null)
        {
            return null;
        }

        existingTask.Title = updateTask.Title;
        existingTask.IsCompleted = updateTask.IsCompleted;
        existingTask.Deadline = updateTask.Deadline;

        _dbContext.SaveChanges();

        return existingTask;
    }

    public bool Delete(Guid id)
    {
        var task = _dbContext.Tasks.FirstOrDefault(task => task.Id == id);

        if (task is null)
            return false;

        _dbContext.Tasks.Remove(task);
        _dbContext.SaveChanges();

        return true;
    }
}