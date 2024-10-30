namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;
using System.Threading;
using System.Threading.Tasks;

public class UpdateTodoItemCommandHandler(ITodoItemService service)
    : ICommandHandler<UpdateTodoItemCommand, UpdateTodoItemResult>
{
    private readonly UpdateTodoItemCommand.Validator validator = new();

    public async Task<UpdateTodoItemResult> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var todoItem = await service.UpdateTodoItemAsync(request.Id, request.Title);
        return new UpdateTodoItemResult(todoItem.Id);
    }
}
