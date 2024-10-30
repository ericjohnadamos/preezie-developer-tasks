namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using MediatR;
using Preezie.Domain.Patterns.CQRS;

public record DeleteTodoItemCommand(int Id) : ICommand<Unit>
{
    public class Validator : AbstractValidator<DeleteTodoItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}

