namespace Preezie.Application.TodoItems.Queries;

using Mapster;
using Preezie.Application.Commons.Dtos;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;

/// <summary>
/// Query handler to get the todo items.
/// </summary>
/// <param name="service">The readonly todo item service.</param>
public class GetTodoItemsQueryHandler(IReadOnlyTodoItemService service)
    : IQueryHandler<GetTodoItemsQuery, GetTodoItemsResult>
{
    public async Task<GetTodoItemsResult> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItems = await service.GetTodoItemsAsync();
        var todoItemDtos = todoItems.Adapt<IEnumerable<TodoItemDto>>();
        return new GetTodoItemsResult(todoItemDtos);
    }
}
