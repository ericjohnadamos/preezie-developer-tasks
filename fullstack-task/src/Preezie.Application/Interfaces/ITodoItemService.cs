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
    /// Toggles the todo item completion asynchronously.
    /// </summary>
    /// <param name="id">The Id of the todo item to toggle the completion.</param>
    /// <returns>An await-able task.</returns>
    Task ToggleTodoItemCompletionAsync(int id);

    /// <summary>
    /// Mark a todo item as deleted (soft delete).
    /// </summary>
    /// <param name="id">The id of the todo item to mark as deleted.</param>
    /// <returns>An await-able task.</returns>
    Task MarkTodoItemAsDeletedAsync(int id);
}
