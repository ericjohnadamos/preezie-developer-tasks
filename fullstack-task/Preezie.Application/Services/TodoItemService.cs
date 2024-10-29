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

    /// <inheritdoc />
    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
    {
        return await Task.FromResult(this.todoItems.Values);
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
        var todoItem = TodoItem.CreateWithTitle(title);
        todoItems.TryAdd(nextId++, todoItem);
        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> UpdateTodoItemAsync(int id, string title)
    {
        if (!todoItems.TryGetValue(id, out TodoItem todoItem))
            throw new KeyNotFoundException();

        todoItem.UpdateTitle(title);
        todoItems.TryUpdate(id, todoItem, todoItem);

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

