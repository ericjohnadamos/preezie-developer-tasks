import React from 'react';
import { TodoItem } from './TodoItem';

export function TodoList({ todos, onToggle, onDelete }) {
  return (
    <div className='todo-list'>
      {
        todos.map(todo => (
          <TodoItem key={todo.id} todo={todo} onToggle={onToggle} onDelete={onDelete} />
        ))
      }
    </div>
  );
}