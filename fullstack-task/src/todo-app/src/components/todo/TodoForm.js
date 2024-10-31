import React, { useState } from 'react';

export function TodoForm({ onSubmit, loading }) {
  const [ title, setTitle ] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!title.trim())
      return;

    try {
      await onSubmit(title);
      setTitle('');
    } catch (err) {
      // Nothing to do
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type='text'
        value={title}
        onChange={e => setTitle(e.target.value)}
        placeholder="Add"
        disabled={loading}
      />
      <button type='submit' disabled={ loading || !title.trim() } className='todo-add-btn'>
        { loading ? 'Create a new todo item...' : 'Add' }
      </button>
    </form>
  );
};