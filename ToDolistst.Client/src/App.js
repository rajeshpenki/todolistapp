import React, { useState, useEffect } from 'react';
import TodoItem from './components/TodoItem';
import './styles/App.css';

function App() {
  const [todos, setTodos] = useState([]);
  const [inputValue, setInputValue] = useState('');
  const [filter, setFilter] = useState('all');

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const response = await fetch('https://localhost:7018/api/tasks');
      const data = await response.json();
      setTodos(data);
    } catch (error) {
      console.error('Error fetching tasks:', error);
    }
  };

  const addTodo = async () => {
    if (inputValue.trim()) {
      try {
        const response = await fetch('https://localhost:7018/api/tasks', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ name: inputValue, completed: false }),
        });
        const newTodo = await response.json();
        setTodos([...todos, newTodo]);
        setInputValue('');
        fetchTasks();

      } catch (error) {
        console.error('Error adding task:', error);
      }
    }
  };

  const toggleTodo = async (id) => {
    const todo = todos.find((todo) => todo.id === id);
    if (todo) {
      try {
        const response = await fetch(`https://localhost:7018/api/tasks/updatetask`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ ...todo, completed: !todo.completed }),
        });
        const updatedTodo = await response.json();
        setTodos(todos.map((todo) => (todo.id === id ? updatedTodo : todo)));
        fetchTasks();
      } catch (error) {
        console.error('Error updating task:', error);
      }
    }
  };

  const removeTodo = async (id) => {
    try {
      await fetch(`https://localhost:7018/api/tasks/${id}`, {
        method: 'DELETE',
      });
      setTodos(todos.filter((todo) => todo.id !== id));
      fetchTasks();
    } catch (error) {
      console.error('Error deleting task:', error);
    }
  };

  const updateTodo = async (id, newText) => {
    const todo = todos.find((todo) => todo.id === id);
    if (todo) {
      try {
        const response = await fetch(`https://localhost:7018/api/tasks/updatetask`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ ...todo, name: newText }),
        });
        const updatedTodo = await response.json();
        setTodos(todos.map((todo) => (todo.id === id ? updatedTodo : todo)));
        fetchTasks();
      } catch (error) {
        console.error('Error updating task:', error);
      }
    }
  };

  const handleFilterChange = (event) => {
    setFilter(event.target.value);
  };

  const filteredTodos = todos.filter((todo) => {
    if (filter === 'completed') return todo.completed;
    if (filter === 'incomplete') return !todo.completed;
    return true;
  });

  return (
    <div className="App">
      <div className='header'>        
        <h3>Todo List</h3>
        <select className='filter' onChange={handleFilterChange} value={filter}>
            <option value="all">All</option>
            <option value="completed">Completed</option>
            <option value="incomplete">Pending</option>
            </select>        
      </div>      
      <div className="add-todo">
        <input max={100} min={1}
            type="text"
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value)}
            placeholder="Add a new todo"
            style={{ width: '80%' }}
        />
        <button className="add-todo-button" onClick={addTodo}>  <i className="fas fa-plus"></i></button>
      </div>      
     
      <div>
      <ul className="todo-list">
      <div className="todo-item">
        <h4>Status</h4>
        <h4>Task Description</h4>
        <h4>Action</h4>
        </div>
        {filteredTodos.map((todo) => (
          <TodoItem
            key={todo.id}
            todo={todo}
            onToggle={() => toggleTodo(todo.id)}
            onRemove={() => removeTodo(todo.id)}
            onUpdate={updateTodo} // Pass the onUpdate prop
          />
        ))}
      </ul>
      </div>
    </div>
  );
}

export default App;