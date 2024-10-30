namespace Preezie.WebAPI.Models;

public record CreateTodoItemRequest(string Title);
public record UpdateTodoItemRequest(int Id, string Title, bool IsCompleted);
