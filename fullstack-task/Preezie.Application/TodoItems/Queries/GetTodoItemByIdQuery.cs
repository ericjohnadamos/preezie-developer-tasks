namespace Preezie.Application.TodoItems.Queries;

using MediatR;
using Preezie.Application.Commons.Dtos;

public record GetTodoItemByIdQuery(int Id) : IRequest<GetTodoItemByIdResult>;

public record GetTodoItemByIdResult(TodoItemDto TodoItem);
