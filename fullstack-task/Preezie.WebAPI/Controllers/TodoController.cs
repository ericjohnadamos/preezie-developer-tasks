namespace Preezie.WebAPI.Controllers;

using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Preezie.Application.Commons.Dtos;
using Preezie.Application.TodoItems.Commands;
using Preezie.Application.TodoItems.Queries;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly IMediator mediator;

    public TodoController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        var query = await this.mediator.Send(new GetTodoItemsQuery());
        var response = query.Adapt<IEnumerable<TodoItemDto>>();
        return this.Ok(response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemDto>> GetTodoItemById(int id)
    {
        try
        {
            var query = await this.mediator.Send(new GetTodoItemByIdQuery(id));
            return this.Ok(query.TodoItem);
        }
        catch (Exception ex)
        {
            return this.NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateTodoItem([FromBody] TodoItemDto item)
    {
        try
        {
            var result = await this.mediator.Send(new CreateTodoItemCommand(item.Title));
            return this.CreatedAtAction(nameof(GetTodoItemById), new { result.Id }, result.Id);
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateTodo(int id, [FromBody] string title)
    {
        // TODO: Update the from body to use a request model rather than a primitive string
        try
        {
            await this.mediator.Send(new UpdateTodoItemCommand(id, title));
            return this.NoContent();
        }
        catch (Exception ex)
        {
            // TODO: Need to handle not found exception and bad request properly
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
