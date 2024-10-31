namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Command handler to create a new todo item with incomplete status.
/// </summary>
/// <param name="service">The todo item service.</param>
public class CreateIncompleteTodoItemCommandHandler(ITodoItemService service)
    : ICommandHandler<CreateIncompleteTodoItemCommand, CreateTodoItemResult>
{
    private readonly CreateIncompleteTodoItemCommand.Validator validator = new();

    public async Task<CreateTodoItemResult> Handle(CreateIncompleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var todoItem = await service.CreateTodoItemWithIncompleteStatusAsync(request.Title);
        return new CreateTodoItemResult(todoItem.Id);
    }
}

