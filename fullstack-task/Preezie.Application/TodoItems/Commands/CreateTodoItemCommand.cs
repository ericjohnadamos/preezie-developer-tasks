namespace Preezie.Application.TodoItems.Commands;

using MediatR;

public record CreateTodoItemCommand(string Title) : IRequest<CreateTodoItemResult>;

public record CreateTodoItemResult(int Id);
