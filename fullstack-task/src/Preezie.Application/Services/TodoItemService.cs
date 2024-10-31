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
        if (this.todoItems.Count == 0)
        {
            await this.CreateTodoItemAsync("Testing One");
            await this.CreateTodoItemAsync("Testing Two");
            await this.CreateTodoItemAsync("Testing Three");
            await this.CreateTodoItemAsync("Testing Four");
        }
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
    public async Task ToggleTodoItemCompletionAsync(int id)
    {
        var todoItem = await this.GetTodoItemByIdAsync(id);
        var toggledTodoItem = todoItem.WithToggledCompletion();

        if (!this.todoItems.TryUpdate(id, toggledTodoItem, todoItem))
            throw new InvalidOperationException($"There is an error when trying to toggle the completion of the todo item with an Id of '{id}'");
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

