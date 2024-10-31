namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using Preezie.Domain.Patterns.CQRS;

/// <summary>
/// Command to create a new todo item.
/// </summary>
/// <param name="Title">The title of the todo item.</param>
public record CreateIncompleteTodoItemCommand(string Title) : ICommand<CreateTodoItemResult>
{
    public class Validator : AbstractValidator<CreateIncompleteTodoItemCommand>
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

