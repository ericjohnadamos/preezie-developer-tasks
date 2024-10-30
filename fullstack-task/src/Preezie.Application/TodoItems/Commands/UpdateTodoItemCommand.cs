namespace Preezie.Application.TodoItems.Commands;

using Preezie.Domain.Patterns.CQRS;

public record UpdateTodoItemCommand(int Id, string Title) : ICommand<UpdateTodoItemResult>;

public record UpdateTodoItemResult(int Id);

