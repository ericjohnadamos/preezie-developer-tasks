namespace Preezie.Application.TodoItems.Queries;

using Preezie.Application.Commons.Dtos;
using Preezie.Domain.Patterns.CQRS;

/// <summary>
/// Query to get the todo items.
/// </summary>
public record GetTodoItemsQuery() : IQuery<GetTodoItemsResult>;

public record GetTodoItemsResult(IEnumerable<TodoItemDto> TodoItems);

