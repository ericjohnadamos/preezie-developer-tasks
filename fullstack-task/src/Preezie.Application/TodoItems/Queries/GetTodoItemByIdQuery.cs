﻿namespace Preezie.Application.TodoItems.Queries;

using FluentValidation;
using Preezie.Application.Commons.Dtos;
using Preezie.Domain.Patterns.CQRS;

/// <summary>
/// Query to get the todo item by Id.
/// </summary>
/// <param name="Id">The Id of the todo item.</param>
public record GetTodoItemByIdQuery(int Id) : IQuery<GetTodoItemByIdResult>
{
    public class Validator : AbstractValidator<GetTodoItemByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
};

public record GetTodoItemByIdResult(TodoItemDto TodoItem);
