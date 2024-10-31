namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using MediatR;
using Preezie.Domain.Patterns.CQRS;

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

