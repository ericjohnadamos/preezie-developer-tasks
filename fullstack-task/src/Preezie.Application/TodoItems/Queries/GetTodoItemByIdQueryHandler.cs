namespace Preezie.Application.TodoItems.Queries;

using FluentValidation;
using Mapster;
using Preezie.Application.Commons.Dtos;
using Preezie.Application.Interfaces;
using Preezie.Domain.Patterns.CQRS;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Query handler to get the todo item by Id.
/// </summary>
/// <param name="service">The readonly todo item service.</param>
public class GetTodoItemByIdQueryHandler(IReadOnlyTodoItemService service)
    : IQueryHandler<GetTodoItemByIdQuery, GetTodoItemByIdResult>
{
    private readonly GetTodoItemByIdQuery.Validator validator = new();

    public async Task<GetTodoItemByIdResult> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var todoItem = await service.GetTodoItemByIdAsync(request.Id);
        var todoItemDto = todoItem.Adapt<TodoItemDto>();
        return new GetTodoItemByIdResult(todoItemDto);
    }
}
