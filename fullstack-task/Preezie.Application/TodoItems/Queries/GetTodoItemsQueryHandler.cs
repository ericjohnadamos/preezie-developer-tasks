namespace Preezie.Application.TodoItems.Queries;

using Mapster;
using MediatR;
using Preezie.Application.Commons.Dtos;
using Preezie.Application.Interfaces;

public class GetTodoItemsQueryHandler(IReadOnlyTodoItemService service)
    : IRequestHandler<GetTodoItemsQuery, GetTodoItemsResult>
{
    public async Task<GetTodoItemsResult> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItems = await service.GetTodoItemsAsync();
        var todoItemDtos = todoItems.Adapt<IEnumerable<TodoItemDto>>();
        return new GetTodoItemsResult(todoItemDtos);
    }
}
