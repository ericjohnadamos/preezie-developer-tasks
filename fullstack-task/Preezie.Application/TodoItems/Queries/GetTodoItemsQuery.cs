namespace Preezie.Application.TodoItems.Queries;

using MediatR;
using Preezie.Application.Commons.Dtos;

public record GetTodoItemsQuery() : IRequest<GetTodoItemsResult>;

public record GetTodoItemsResult(IEnumerable<TodoItemDto> TodoItems);
