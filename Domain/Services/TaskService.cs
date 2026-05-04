using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public List<TaskItem> GetAll()
    {
        return _taskRepository.GetAll();
    }

    public TaskItem? GetById(Guid id)
    {
        return _taskRepository.GetById(id);
    }

    public TaskItem Create(string title, DateTime deadline)
    {
        var task = new TaskItem
        {
            Title = title,
            Deadline = deadline,
            IsCompleted = false
        };

        return _taskRepository.Create(task);
    }

    public TaskItem? Update(Guid id, string title, DateTime deadline, bool isCompleted)
    {
        var task = new TaskItem
        {
            Id = id,
            Title = title,
            Deadline = deadline,
            IsCompleted = isCompleted
        };

        return _taskRepository.Update(task);
    }

    public TaskItem? Complete(Guid id)
    {
        var task = _taskRepository.GetById(id);

        if (task == null)
            return null;

        task.IsCompleted = true;

        return _taskRepository.Update(task);
    }

    public bool Delete(Guid id)
    {
        return _taskRepository.Delete(id);
    }
}