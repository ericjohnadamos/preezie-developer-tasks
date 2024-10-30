namespace Preezie.WebAPI.Controllers;

using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Preezie.Application.TodoItems.Commands;
using Preezie.Application.TodoItems.Queries;
using Preezie.WebAPI.Models;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly IMediator mediator;

    public TodoController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(GetTodoItemsResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetTodoItemsResponse>> GetTodoItems()
    {
        var query = await this.mediator.Send(new GetTodoItemsQuery());
        var response = query.Adapt<GetTodoItemsResponse>();
        return this.Ok(response);
    }

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
        catch (Exception ex)
        {
            return this.NotFound(ex.Message);
        }
    }

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
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> UpdateTodo(int id, [FromBody] UpdateTodoItemRequest todoItemRequest)
    {
        if (id != todoItemRequest.Id)
            return this.BadRequest();

        try
        {
            var request = todoItemRequest.Adapt<UpdateTodoItemCommand>();
            var result = await this.mediator.Send(request);
            return this.Ok(result.Id);
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTodo(int id)
    {
        try
        {
            await this.mediator.Send(new DeleteTodoItemCommand(id));
            return this.NoContent();
        }
        catch (Exception ex)
        {
            return this.NotFound(ex.Message);
        }
    }
}
