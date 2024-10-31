namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using MediatR;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;

public class ToggleTodoItemCompletionCommandHandler(ITodoItemService todoItemService)
    : ICommandHandler<ToggleTodoItemCompletionCommand, Unit>
{
    private readonly ToggleTodoItemCompletionCommand.Validator validator = new();

    public async Task<Unit> Handle(ToggleTodoItemCompletionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await todoItemService.ToggleTodoItemCompletionAsync(request.Id);
        return await Task.FromResult(Unit.Value);
    }
}
