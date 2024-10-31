const BASE_URL = "https://localhost:5000/api/v1/todo";

export const todoService = {
  async fetchTodos() {
    const response = await fetch(BASE_URL);
    if (!response.ok)
      throw new Error('Failed to fetch todos');
    const data = await response.json();
    return data.todoItems;
  },
  async createTodoItem(title) {
    const response = await fetch(BASE_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ title })
    });
    if (!response.ok) {
      if (response.status === 400) {
        const error = await response.text();
        throw new Error(error);
      }
      throw new Error('Failed to create a todo item');
    }
    return await response.json();
  },
  async toggleTodoItemCompletion(id) {
    const response = await fetch(`${BASE_URL}/toggle-completion`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ id })
    });
    if (!response.ok)
      throw new Error('Failed to toggle todo');
  },
  async deleteTodoItem(id) {
    const response = await fetch(`${BASE_URL}/${id}`, {
      method: 'DELETE',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ id })
    });
    if (!response.ok)
      throw new Error('Failed to delete todo');
  },
};