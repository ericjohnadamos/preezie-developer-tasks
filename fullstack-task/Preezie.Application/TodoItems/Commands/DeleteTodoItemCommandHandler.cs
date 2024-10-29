namespace Preezie.Application.TodoItems.Commands;

using MediatR;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;
using System.Threading;
using System.Threading.Tasks;

public class DeleteTodoItemCommandHandler(ITodoItemService service)
    : ICommandHandler<DeleteTodoItemCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        await service.MarkTodoItemAsDeletedAsync(request.Id);
        return await Task.FromResult(Unit.Value);
    }
}
