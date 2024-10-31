import { useState, useCallback } from 'react';
import { todoService } from '../services/todoService';

export function useTodos() {
  const [ todos, setTodos ] = useState([]);
  const [ loading, setLoading ] = useState(false);
  const [ error, setError ] = useState(null);

  const fetchTodos = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await todoService.fetchTodos();
      setTodos(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }, []);

  const createTodoItem = async (title) => {
    try {
      setLoading(true);
      setError(null);
      await todoService.createTodoItem(title);
      await fetchTodos();
    } catch (err) {
      console.log(err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const toggleTodoItemCompletion = async (id) => {
    try {
      setError(null);
      await todoService.toggleTodoItemCompletion(id);
      setTodos(prev => prev.map(todo => {
        if (todo.id === id)
          return { ...todo, isCompleted: !todo.isCompleted }
        else
          return todo;
      }));
    } catch (err) {
      setError(err.message);
      await fetchTodos();
    }
  };

  const deleteTodoItem = async (id) => {
    try {
        setError(null);
        await todoService.deleteTodoItem(id);
        setTodos(prev => prev.filter(todo => todo.id !== id));
    } catch (err) {
      setError(err.message);
      await fetchTodos();
    }
  };

  return {
    todos,
    loading,
    error,
    fetchTodos,
    createTodoItem,
    toggleTodoItemCompletion,
    deleteTodoItem,
  };
}