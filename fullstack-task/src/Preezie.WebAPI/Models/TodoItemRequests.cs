namespace Preezie.WebAPI.Models;

public record CreateTodoItemRequest(string Title);
public record ToggleTodoItemCompletionRequest(int Id);
public record DeleteTodoItemRequest(int Id);
