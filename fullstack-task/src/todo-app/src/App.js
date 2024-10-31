import React, { useEffect } from 'react';
import { TodoList, TodoForm } from './components/todo';
import { useTodos } from './hooks/useTodos';
import './styles/styles.css';

function App() {
    const {
        todos,
        loading,
        error,
        fetchTodos,
        createTodoItem,
        toggleTodoItemCompletion,
        deleteTodoItem
    } = useTodos();

    useEffect(() => {
        fetchTodos();
    }, [fetchTodos]);

    return (
        <div>
            <h1>Todo List</h1>
            {
                error && (<div className='error-message'>{error}</div>)
            }
            <TodoForm onSubmit={createTodoItem} loading={loading}/>
            <TodoList todos={todos} onToggle={toggleTodoItemCompletion} onDelete={deleteTodoItem}/>
        </div>
    );
};

export default App;