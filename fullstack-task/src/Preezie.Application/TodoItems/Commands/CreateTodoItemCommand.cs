namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using Preezie.Domain.Patterns.CQRS;

public record CreateTodoItemCommand(string Title) : ICommand<CreateTodoItemResult>
{
    public class Validator : AbstractValidator<CreateTodoItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MinimumLength(1).WithMessage("Title must be at least 1 character long");
        }
    }
}

public record CreateTodoItemResult(int Id);

