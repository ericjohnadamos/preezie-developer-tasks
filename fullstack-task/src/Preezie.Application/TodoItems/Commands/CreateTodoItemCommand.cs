namespace Preezie.Application.TodoItems.Commands;

using Preezie.Domain.Patterns.CQRS;

public record CreateTodoItemCommand(string Title) : ICommand<CreateTodoItemResult>;

public record CreateTodoItemResult(int Id);

