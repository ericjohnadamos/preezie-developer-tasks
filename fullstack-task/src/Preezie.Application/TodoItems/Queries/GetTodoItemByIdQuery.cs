namespace Preezie.Application.TodoItems.Queries;

using Preezie.Application.Commons.Dtos;
using Preezie.Domain.Patterns.CQRS;

public record GetTodoItemByIdQuery(int Id) : IQuery<GetTodoItemByIdResult>;

public record GetTodoItemByIdResult(TodoItemDto TodoItem);
