import { todoService } from '../todoService';

globalThis.fetch = jest.fn();

describe('todoService', () => {
  // Clear all mocks before each test
  beforeEach(() => {
    fetch.mockClear();
  });

  describe('fetchTodos', () => {
    it('should fetch todo items successfully', async () => {
      // Arrange
      const mockTodos = {
        todoItems: [
          { id: 1, title: 'Todo Item 1', isCompleted: false },
          { id: 2, title: 'Todo Item 2', isCompleted: true }
        ]
      };

      fetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(mockTodos)
      });

      // Act
      const result = await todoService.fetchTodos();

      // Assert
      expect(fetch).toHaveBeenCalledWith('https://localhost:5000/api/todo');
      expect(result).toEqual(mockTodos.todoItems);
    });

    it('should handle fetch todo items error', async () => {
      // Arrange
      fetch.mockResolvedValueOnce({
        ok: false,
        status: 500,
        statusText: 'Internal Server Error'
      });

      // Act & Assert
      await expect(todoService.fetchTodos()).rejects.toThrow('Failed to fetch todos');
    });
  });

  describe('createTodoItem', () => {
    it('should create todo item successfully', async () => {
      // Arrange
      const newTodoId = 1;
      const title = 'New Todo';

      fetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(newTodoId)
      });

      // Act
      const result = await todoService.createTodoItem(title);

      // Assert
      expect(fetch).toHaveBeenCalledWith(
        'https://localhost:5000/api/todo',
        expect.objectContaining({
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ title })
        })
      );
      expect(result).toBe(newTodoId);
    });

    it('should handle validation error', async () => {
      // Arrange
      const errorMessage = 'Title is required';
      fetch.mockResolvedValueOnce({
        ok: false,
        status: 400,
        text: () => Promise.resolve(errorMessage)
      });

      // Act & Assert
      await expect(todoService.createTodoItem('')).rejects.toThrow(errorMessage);
    });
  });

  it('should toggle todo item completion successfully', async () => {
    // Arrange
    const todoId = 1;
    fetch.mockResolvedValueOnce({
      ok: true,
      status: 200
    });

    // Act
    await todoService.toggleTodoItemCompletion(todoId);

    // Assert
    expect(fetch).toHaveBeenCalledWith(
      'https://localhost:5000/api/todo/toggle-completion',
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: todoId })
      }
    );
  });

  it('should handle 404 not found error', async () => {
    // Arrange
    const todoId = 999;
    fetch.mockResolvedValueOnce({
      ok: false,
      status: 404,
      text: () => Promise.resolve('Todo item not found')
    });

    // Act & Assert
    await expect(
      todoService.toggleTodoItemCompletion(todoId)
    ).rejects.toThrow('Failed to toggle todo');
  });

  it('should handle 400 bad request error', async () => {
    // Arrange
    const todoId = 1;
    const errorMessage = 'Invalid todo item id';
    fetch.mockResolvedValueOnce({
      ok: false,
      status: 400,
      text: () => Promise.resolve(errorMessage)
    });

    // Act & Assert
    await expect(
      todoService.toggleTodoItemCompletion(todoId)
    ).rejects.toThrow('Failed to toggle todo');
  });

  it('should handle validation error', async () => {
    // Arrange
    const todoId = -1; // Invalid ID
    fetch.mockResolvedValueOnce({
      ok: false,
      status: 400,
      text: () => Promise.resolve('Invalid todo item id format')
    });

    // Act & Assert
    await expect(
      todoService.toggleTodoItemCompletion(todoId)
    ).rejects.toThrow('Failed to toggle todo');
  });

  describe('deleteTodoItem', () => {
    it('should delete todo item successfully', async () => {
      // Arrange
      const todoId = 1;
      fetch.mockResolvedValueOnce({
        ok: true
      });

      // Act
      await todoService.deleteTodoItem(todoId);

      // Assert
      expect(fetch).toHaveBeenCalledWith(
        `https://localhost:5000/api/todo/${todoId}`,
        expect.objectContaining({
          method: 'DELETE'
        })
      );
    });

    it('should handle not found error during deletion', async () => {
      // Arrange
      const todoItemId = 999;
      fetch.mockResolvedValueOnce({
        ok: false,
        status: 404,
        text: () => Promise.resolve('Failed to delete todo')
      });

      // Act & Assert
      await expect(todoService.deleteTodoItem(todoItemId)).rejects.toThrow('Failed to delete todo');
    });
  });
});
