namespace Preezie.Application.TodoItems.Queries;

using Mapster;
using MediatR;
using Preezie.Application.Commons.Dtos;
using Preezie.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

public class GetTodoItemByIdQueryHandler(IReadOnlyTodoItemService service)
    : IRequestHandler<GetTodoItemByIdQuery, GetTodoItemByIdResult>
{
    public async Task<GetTodoItemByIdResult> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
    {
        var todoItem = await service.GetTodoItemByIdAsync(request.Id);
        var todoItemDto = todoItem.Adapt<TodoItemDto>();
        return new GetTodoItemByIdResult(todoItemDto);
    }
}
