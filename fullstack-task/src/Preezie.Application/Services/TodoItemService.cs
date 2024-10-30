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
        var todoItemList = this.todoItems.Values.Where(i => !i.IsDeleted).ToList();
        return await Task.FromResult(todoItemList);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> GetTodoItemByIdAsync(int id)
    {
        if (!this.todoItems.TryGetValue(id, out TodoItem? todoItem))
            throw new KeyNotFoundException();
        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> CreateTodoItemAsync(string title)
    {
        var id = Interlocked.Increment(ref nextId) - 1;
        var todoItem = TodoItem.CreateWithIdAndTitle(id, title);

        if (!this.todoItems.TryAdd(id, todoItem))
            throw new InvalidOperationException();

        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> UpdateTodoItemAsync(int id, string title)
    {
        var todoItem = await this.GetTodoItemByIdAsync(id);
        var updatedTodoItem = todoItem.WithUpdatedTitle(title);
        this.todoItems.TryUpdate(id, updatedTodoItem, todoItem);

        var newTodoItem = await this.GetTodoItemByIdAsync(id);
        return await Task.FromResult(newTodoItem);
    }

    /// <inheritdoc/>
    public Task MarkTodoItemAsDeletedAsync(int id)
    {
        if (!this.todoItems.TryGetValue(id, out TodoItem? todoItem))
            throw new KeyNotFoundException();

        var todoItemWithDeletedStatus = todoItem.WithDeletedStatus();
        this.todoItems.TryUpdate(id, todoItemWithDeletedStatus, todoItem);

        return Task.CompletedTask;
    }
}

