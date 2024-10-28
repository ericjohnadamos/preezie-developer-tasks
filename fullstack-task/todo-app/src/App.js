import React, { useState, useEffect } from 'react';

function App() {
    const [todos, setTodos] = useState([]);
    const [title, setTitle] = useState('');

    function fetchTodos() {
        fetch('http://localhost:5000/api/todo')
            .then(response => response.json())
            .then(data => setTodos(data));
    }

    useEffect(() => {
        fetchTodos();
    }, []);

    function addTodo() {
        fetch('http://localhost:5000/api/todo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ title: title })
        }).then(() => {
            fetchTodos();
            setTitle('');
        });
    }

    function toggleTodo(id) {
        fetch(`http://localhost:5000/api/todo/${id}`, {
            method: 'PUT'
        }).then(() => {
            fetchTodos();
        });
    }

    function deleteTodo(id) {
        fetch('http://localhost:5000/api/todo', {
            method: 'DELETE',
            body: id
        }).then(() => {
            fetchTodos();
        });
    }

    return (
        <div>
            <h1>Todo List</h1>
            <input value={title} onChange={e => setTitle(e.target.value)} />
            <button onClick={addTodo}>Add</button>
            <ul>
                {todos.map(todo => (
                    <li key={todo.id}>
                        <span
                            style={{
                                textDecoration: todo.isCompleted ? 'line-through' : 'none'
                            }}
                        >
                            {todo.title}
                        </span>
                        <button onClick={() => toggleTodo(todo.id)}>Toggle</button>
                        <button onClick={() => deleteTodo(todo.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;