namespace Preezie.Application.UnitTests.Services;

using Preezie.Application.Commons.Exceptions;
using Preezie.Application.Interfaces;
using Preezie.Application.Services;
using Preezie.Domain.Entities;

public class TodoItemServiceDataManipulationTests
{
    private readonly ITodoItemService sut;

    public TodoItemServiceDataManipulationTests()
    {
        this.sut = new TodoItemService();
    }

    [Fact]
    public async Task CreateTodoItemAsync_WithValidTitle_CreatesTodoItem()
    {
        // Arrange
        const string title = "New Todo";

        // Act
        var result = await this.sut.CreateTodoItemWithIncompleteStatusAsync(title);

        // Assert
        Assert.NotEqual(0, result.Id);
        Assert.Equal(title, result.Title);

        // Verify item was actually stored
        var retrieved = await this.sut.GetTodoItemByIdAsync(result.Id);
        Assert.Equal(result.Id, retrieved.Id);
        Assert.Equal(title, retrieved.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public async Task CreateTodoItemAsync_WithInvalidTitle_ThrowsArgumentException(string invalidTitle)
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            this.sut.CreateTodoItemWithIncompleteStatusAsync(invalidTitle));
    }

    [Fact]
    public async Task CreateTodoItemAsync_MultipleItems_AssignsUniqueIds()
    {
        // Act
        var item1 = await this.sut.CreateTodoItemWithIncompleteStatusAsync("Item 1");
        var item2 = await this.sut.CreateTodoItemWithIncompleteStatusAsync("Item 2");

        // Assert
        Assert.NotEqual(item1.Id, item2.Id);
    }

    [Fact]
    public async Task ToggleTodoItemCompletionAsync_WithValidId_UpdatesTodoItem()
    {
        // Arrange
        var item = await this.sut.CreateTodoItemWithIncompleteStatusAsync("Item 1");
        var itemIsCompleted = item.IsCompleted;

        // Act
        await this.sut.ToggleTodoItemCompletionAsync(item.Id);

        // Assert
        Assert.NotEqual(!itemIsCompleted, itemIsCompleted);

        // Verify update was persisted
        var retrieved = await this.sut.GetTodoItemByIdAsync(item.Id);
        Assert.Equal(!itemIsCompleted, retrieved.IsCompleted);
    }

    [Fact]
    public async Task MarkTodoItemAsDeletedAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            this.sut.MarkTodoItemAsDeletedAsync(999));
    }

    [Fact]
    public async Task MarkTodoItemAsDeletedAsync_WithValidId_DoesNotThrow()
    {
        // Arrange
        var item = await this.sut.CreateTodoItemWithIncompleteStatusAsync("Test Item");

        // Act & Assert
        await this.sut.MarkTodoItemAsDeletedAsync(item.Id);
    }

    [Fact]
    public async Task Operations_UnderConcurrentAccess_MaintainsConsistency()
    {
        // Arrange
        var tasks = new List<Task<TodoItem>>();
        const int numberOfConcurrentOperations = 100;

        // Act
        for (int i = 0; i < numberOfConcurrentOperations; i++)
        {
            tasks.Add(this.sut.CreateTodoItemWithIncompleteStatusAsync($"Concurrent Item {i}"));
        }

        await Task.WhenAll(tasks);
        var allItems = await this.sut.GetTodoItemsAsync();

        // Assert
        Assert.Equal(numberOfConcurrentOperations, allItems.Count());
        Assert.Equal(numberOfConcurrentOperations, allItems.Select(i => i.Id).Distinct().Count());
    }
}
