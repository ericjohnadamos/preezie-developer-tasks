namespace Preezie.Application.TodoItems.Commands;

using MediatR;
using Preezie.Domain.Patterns.CQRS;

public record DeleteTodoItemCommand(int Id) : ICommand<Unit>;

