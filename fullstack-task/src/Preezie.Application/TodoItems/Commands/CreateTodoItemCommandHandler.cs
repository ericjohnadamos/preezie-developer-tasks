namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;
using System.Threading;
using System.Threading.Tasks;

public class CreateTodoItemCommandHandler(ITodoItemService service)
    : ICommandHandler<CreateTodoItemCommand, CreateTodoItemResult>
{
    private readonly CreateTodoItemCommand.Validator validator = new();

    public async Task<CreateTodoItemResult> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var todoItem = await service.CreateTodoItemAsync(request.Title);
        return new CreateTodoItemResult(todoItem.Id);
    }
}

