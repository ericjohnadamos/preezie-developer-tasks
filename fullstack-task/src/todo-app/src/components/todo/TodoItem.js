import React from 'react';

export function TodoItem({ todo, onToggle, onDelete }) {
  return (
    <div className='todo-item'>
      <span className={todo.isCompleted ? 'completed' : ''}>
        {todo.title}
      </span>
      <span className='todo-actions'>
        <button onClick={() => onToggle(todo.id)}>Toggle</button>
        <button onClick={() => onDelete(todo.id)}>Delete</button>
      </span>
    </div>
  );
}