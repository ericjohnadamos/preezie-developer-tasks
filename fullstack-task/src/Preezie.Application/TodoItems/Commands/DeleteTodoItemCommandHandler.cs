namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using MediatR;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;
using System.Threading;
using System.Threading.Tasks;

public class DeleteTodoItemCommandHandler(ITodoItemService service)
    : ICommandHandler<DeleteTodoItemCommand, Unit>
{
    private readonly DeleteTodoItemCommand.Validator validator = new();

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await service.MarkTodoItemAsDeletedAsync(request.Id);
        return await Task.FromResult(Unit.Value);
    }
}
