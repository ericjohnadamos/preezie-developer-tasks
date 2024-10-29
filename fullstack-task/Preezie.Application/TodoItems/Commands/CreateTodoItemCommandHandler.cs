namespace Preezie.Application.TodoItems.Commands;

using MediatR;
using Preezie.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

public class CreateTodoItemCommandHandler(ITodoItemService service)
    : IRequestHandler<CreateTodoItemCommand, CreateTodoItemResult>
{
    public async Task<CreateTodoItemResult> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await service.CreateTodoItemAsync(request.Title);
        return new CreateTodoItemResult(todoItem.Id);
    }
}

