namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using MediatR;
using Preezie.Domain.Patterns.CQRS;

/// <summary>
/// Command to toggle the todo item completion status.
/// </summary>
/// <param name="Id">The Id of the todo item.</param>
public record ToggleTodoItemCompletionCommand(int Id) : ICommand<Unit>
{
    public class Validator : AbstractValidator<ToggleTodoItemCompletionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

