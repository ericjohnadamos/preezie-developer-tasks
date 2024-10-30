namespace Preezie.Application.Services;

using Preezie.Application.Interfaces;
using Preezie.Domain.Entities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TodoItemService : ITodoItemService
{
    private readonly ConcurrentDictionary<int, TodoItem> todoItems = new();
    private int nextId = 1;

    /// <inheritdoc/>
    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
    {
        var todoItemList = todoItems.Values.ToList();
        return await Task.FromResult(todoItemList);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> GetTodoItemByIdAsync(int id)
    {
        if (!todoItems.TryGetValue(id, out TodoItem todoItem))
            throw new KeyNotFoundException();
        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> CreateTodoItemAsync(string title)
    {
        var id = Interlocked.Increment(ref nextId) - 1;
        var todoItem = TodoItem.CreateWithIdAndTitle(id, title);

        if (!todoItems.TryAdd(id, todoItem))
            throw new InvalidOperationException();

        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> UpdateTodoItemAsync(int id, string title)
    {
        if (!todoItems.TryGetValue(id, out TodoItem todoItem))
            throw new KeyNotFoundException();

        var updatedTodoItem = todoItem.WithUpdatedTitle(title);
        todoItems.TryUpdate(id, updatedTodoItem, todoItem);

        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public Task MarkTodoItemAsDeletedAsync(int id)
    {
        // TODO: Implement mark as deletion
        if (!todoItems.TryGetValue(id, out TodoItem todoItem))
            throw new KeyNotFoundException();
        return Task.CompletedTask;
    }
}

