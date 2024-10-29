namespace Preezie.Application.TodoItems.Commands;

using MediatR;
using Preezie.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

public class UpdateTodoItemCommandHandler(ITodoItemService service)
    : IRequestHandler<UpdateTodoItemCommand, UpdateTodoItemResult>
{
    public async Task<UpdateTodoItemResult> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await service.UpdateTodoItemAsync(request.Id, request.Title);
        return new UpdateTodoItemResult(todoItem.Id);
    }
}
