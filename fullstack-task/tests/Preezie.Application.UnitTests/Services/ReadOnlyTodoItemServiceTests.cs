﻿namespace Preezie.Application.UnitTests.Services;

using Preezie.Application.Interfaces;
using Preezie.Application.Services;
using System.Collections.Generic;

public class ReadOnlyTodoItemServiceTests
{
    private readonly IReadOnlyTodoItemService readOnlySut;
    private readonly ITodoItemService sut;

    public ReadOnlyTodoItemServiceTests()
    {
        this.sut = new TodoItemService();
        this.readOnlySut = this.sut;
    }

    [Fact]
    public async Task GetTodoItemsAsync_WhenEmpty_ReturnsEmptyList()
    {
        // Act
        var result = await this.readOnlySut.GetTodoItemsAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetTodoItemsAsync_WithItems_ReturnsAllItems()
    {
        // Arrange
        var item1 = await this.sut.CreateTodoItemAsync("Test 1");
        var item2 = await this.sut.CreateTodoItemAsync("Test 2");

        // Act
        var result = await this.readOnlySut.GetTodoItemsAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(item1, result);
        Assert.Contains(item2, result);
    }

    [Fact]
    public async Task GetTodoItemByIdAsync_WithValidId_ReturnsTodoItem()
    {
        // Arrange
        var createdItem = await this.sut.CreateTodoItemAsync("Test Item");

        // Act
        var result = await this.readOnlySut.GetTodoItemByIdAsync(createdItem.Id);

        // Assert
        Assert.Equal(createdItem.Id, result.Id);
        Assert.Equal(createdItem.Title, result.Title);
    }

    [Fact]
    public async Task GetTodoItemByIdAsync_WithInvalidId_ThrowsKeyNotFoundException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            this.readOnlySut.GetTodoItemByIdAsync(999));
    }
}