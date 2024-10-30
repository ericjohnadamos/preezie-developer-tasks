namespace Preezie.Application.Services;

using Preezie.Application.Commons.Exceptions;
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
            throw new NotFoundException($"Cannot find todo item with an Id of '{id}'");
        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> CreateTodoItemAsync(string title)
    {
        var id = Interlocked.Increment(ref nextId) - 1;
        var todoItem = TodoItem.CreateWithIdAndTitle(id, title);

        if (!this.todoItems.TryAdd(id, todoItem))
            throw new InvalidOperationException("There is an error when trying to add a todo item");

        return await Task.FromResult(todoItem);
    }

    /// <inheritdoc/>
    public async Task<TodoItem> UpdateTodoItemAsync(int id, string title)
    {
        var todoItem = await this.GetTodoItemByIdAsync(id);
        var updatedTodoItem = todoItem.WithUpdatedTitle(title);

        if (!this.todoItems.TryUpdate(id, updatedTodoItem, todoItem))
            throw new InvalidOperationException($"There is an error when trying to update a todo item with an Id of '{id}'");

        var newTodoItem = await this.GetTodoItemByIdAsync(id);
        return await Task.FromResult(newTodoItem);
    }

    /// <inheritdoc/>
    public async Task MarkTodoItemAsDeletedAsync(int id)
    {
        var todoItem = await this.GetTodoItemByIdAsync(id);
        var todoItemWithDeletedStatus = todoItem.WithDeletedStatus();

        if (!this.todoItems.TryUpdate(id, todoItemWithDeletedStatus, todoItem))
            throw new InvalidOperationException($"There is an error when trying to mark a todo item with an Id of '{id}' as deleted");
    }
}

