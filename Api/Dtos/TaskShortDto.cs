namespace Api.Dtos;

public class TaskShortDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime Deadline { get; set; }
}