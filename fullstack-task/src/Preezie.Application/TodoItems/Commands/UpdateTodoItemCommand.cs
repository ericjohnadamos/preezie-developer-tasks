namespace Preezie.Application.TodoItems.Commands;

using FluentValidation;
using Preezie.Domain.Patterns.CQRS;

public record UpdateTodoItemCommand(int Id, string Title) : ICommand<UpdateTodoItemResult>
{
    public class Validator : AbstractValidator<UpdateTodoItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MinimumLength(1).WithMessage("Title must be at least 1 character long");
        }
    }
};

public record UpdateTodoItemResult(int Id);

