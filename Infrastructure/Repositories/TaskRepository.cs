using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(task => task.Id == id);
    }

    public TaskItem Create(TaskItem task)
    {
        task.Id = Guid.NewGuid();
        _tasks.Add(task);

        return task;
    }

    public TaskItem? Update(TaskItem updateTask)
    {
        var existingTask = GetById(updateTask.Id);

        if (existingTask is null)
        {
            return null;
        }

        existingTask.Title = updateTask.Title;
        existingTask.IsCompleted = updateTask.IsCompleted;
        existingTask.Deadline = updateTask.Deadline;

        return existingTask;
    }

    public bool Delete(Guid id)
    {
        var task = GetById(id);

        if (task is null)
            return false;

        _tasks.Remove(task);

        return true;
    }
}