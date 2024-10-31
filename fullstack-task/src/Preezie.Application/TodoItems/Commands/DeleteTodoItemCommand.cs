namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using MediatR;
using Preezie.Domain.Patterns.CQRS;

/// <summary>
/// Command to soft delete the todo item.
/// </summary>
/// <param name="Id">The Id of the todo item.</param>
public record DeleteTodoItemCommand(int Id) : ICommand<Unit>
{
    public class Validator : AbstractValidator<DeleteTodoItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

