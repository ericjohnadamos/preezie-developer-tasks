namespace Preezie.Application.Interfaces;

using Preezie.Domain.Entities;

public interface ITodoItemService : IReadOnlyTodoItemService
{
    /// <summary>
    /// Create a todo item asynchronously.
    /// </summary>
    /// <param name="title">The title of the todo item.</param>
    /// <returns>An await-able task of <see cref="TodoItem"/>.</returns>
    Task<TodoItem> CreateTodoItemAsync(string title);

    /// <summary>
    /// Update title of the todo item asynchronously.
    /// </summary>
    /// <param name="id">The id of the todo item to update.</param>
    /// <param name="title">The updated title of the todo item.</param>
    /// <returns>An await-able task of <see cref="TodoItem"/>.</returns>
    Task<TodoItem> UpdateTodoItemAsync(int id, string title);

    /// <summary>
    /// Mark a todo item as deleted (soft delete).
    /// </summary>
    /// <param name="id">The id of the todo item to mark as deleted.</param>
    /// <returns>An await-able task.</returns>
    Task MarkTodoItemAsDeletedAsync(int id);
}
