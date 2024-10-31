namespace Preezie.WebAPI.Controllers;

using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Preezie.Application.Commons.Exceptions;
using Preezie.Application.TodoItems.Commands;
using Preezie.Application.TodoItems.Queries;
using Preezie.WebAPI.Models;

[ApiController]
[Route("api/v1/todo")]
public class TodoController : ControllerBase
{
    private readonly IMediator mediator;

    public TodoController(IMediator mediator) => this.mediator = mediator;

    /// <summary>
    /// Gets the list of todo items.
    /// </summary>
    /// <returns>Returns the list of todo items.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetTodoItemsResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetTodoItemsResponse>> GetTodoItems()
    {
        var query = await this.mediator.Send(new GetTodoItemsQuery());
        var response = query.Adapt<GetTodoItemsResponse>();
        return this.Ok(response);
    }

    /// <summary>
    /// Gets the todo item by Id.
    /// </summary>
    /// <param name="id">The Id of the todo item to look for.</param>
    /// <returns>The specific todo item.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetTodoItemByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetTodoItemByIdResponse>> GetTodoItemById(int id)
    {
        try
        {
            var query = await this.mediator.Send(new GetTodoItemByIdQuery(id));
            var response = query.Adapt<GetTodoItemByIdResponse>();
            return this.Ok(response);
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a todo item.
    /// </summary>
    /// <param name="todoItemRequest">The todo item to add.</param>
    /// <returns>The Id of the created todo item.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateTodoItem([FromBody] CreateTodoItemRequest todoItemRequest)
    {
        try
        {
            var command = todoItemRequest.Adapt<CreateTodoItemCommand>();
            var result = await this.mediator.Send(command);
            return this.CreatedAtAction(nameof(GetTodoItemById), new { result.Id }, result.Id);
        }
        catch (InvalidOperationException ex)
        {
            return this.BadRequest(ex.Message);
        }
        catch (ValidationException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Toggles the todo item completion.
    /// </summary>
    /// <param name="todoItemRequest">The todo item to toggle the completion.</param>
    /// <returns>An await-able action result.</returns>
    [HttpPost("toggle-completion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ToggleTodoItemCompletion([FromBody] ToggleTodoItemCompletionRequest todoItemRequest)
    {
        try
        {
            var request = todoItemRequest.Adapt<ToggleTodoItemCompletionCommand>();
            await this.mediator.Send(request);
            return this.Ok();
        }
        catch (InvalidOperationException ex)
        {
            return this.BadRequest(ex.Message);
        }
        catch (ValidationException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes the todo item by Id.
    /// </summary>
    /// <param name="id">The Id of the todo item to delete.</param>
    /// <returns>An await-able action result.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTodoItem(int id)
    {
        try
        {
            await this.mediator.Send(new DeleteTodoItemCommand(id));
            return this.NoContent();
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return this.NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }
}
