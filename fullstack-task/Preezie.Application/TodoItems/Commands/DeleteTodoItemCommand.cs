namespace Preezie.Application.TodoItems.Commands;

using MediatR;

public record DeleteTodoItemCommand(int Id) : IRequest<Unit>;

