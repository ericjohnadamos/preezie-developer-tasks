namespace Preezie.Application.TodoItems.Commands;

using MediatR;

public record UpdateTodoItemCommand(int Id, string Title) : IRequest<UpdateTodoItemResult>;

public record UpdateTodoItemResult(int Id);

