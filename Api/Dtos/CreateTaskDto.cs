namespace Api.Dtos;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;

    public DateTime Deadline { get; set; }
}