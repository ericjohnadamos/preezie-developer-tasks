namespace Preezie.Application.Commons.Dtos;

using System.ComponentModel.DataAnnotations;

public class TodoItemDto
{
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
