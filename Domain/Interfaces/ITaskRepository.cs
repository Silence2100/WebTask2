using Domain.Models;

namespace Domain.Interfaces;

public interface ITaskRepository
{
    List<TaskItem> GetAll();

    TaskItem? GetById(Guid id);

    TaskItem Create(TaskItem task);

    TaskItem? Update(TaskItem task);

    bool Delete(Guid id);
}