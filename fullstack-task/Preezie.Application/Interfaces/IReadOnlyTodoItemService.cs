namespace Preezie.Application.Interfaces;

using Preezie.Domain.Entities;

public interface IReadOnlyTodoItemService
{
    /// <summary>
    /// Get todo items asynchronously.
    /// </summary>
    /// <returns>An enumerable await-able task of <see cref="TodoItem"/>.</returns>
    Task<IEnumerable<TodoItem>> GetTodoItemsAsync();

    /// <summary>
    /// Get todo item by id asynchronously.
    /// </summary>
    /// <param name="id">The id of the todo item.</param>
    /// <returns>An await-able task of <see cref="TodoItem"/>.</returns>
    Task<TodoItem> GetTodoItemByIdAsync(int id);
}

