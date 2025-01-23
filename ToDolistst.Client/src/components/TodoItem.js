import React, { useState } from 'react';

function TodoItem({ todo, onToggle, onRemove, onUpdate }) {
  const [isEditing, setIsEditing] = useState(false);
  const [editedText, setEditedText] = useState(todo.name);

  /*const handleTextClick = () => {
    setIsEditing(true);
  };
*/
  const handleInputChange = (e) => {
    setEditedText(e.target.value);
  };

  const handleInputBlur = () => {
    setIsEditing(false);
    onUpdate(todo.id, editedText);
  };

  const handleInputKeyPress = (e) => {
    if (e.key === 'Enter') {
      setIsEditing(false);
      onUpdate(todo.id, editedText);
    }
  };

  return (
    <div className="todo-item">
      <input 
        type="checkbox" 
        checked={todo.completed} 
        onChange={() => onToggle(todo.id)} 
      />
      {isEditing ? (
        <input
          type="text"
          value={editedText}
          onChange={handleInputChange}
          onBlur={handleInputBlur}
          onKeyPress={handleInputKeyPress}
          autoFocus
        />
      ) : (
        <span className="todo-text" //onClick={handleTextClick}
        >
          {todo.name}
        </span>
      )}
    
      <button onClick={() => onRemove(todo.id)} className="remove-button" tooltip="Delete">
        <i className="fas fa-trash"></i>
      </button>
    </div>
  );
}

export default TodoItem;

//<button onClick={() => setIsEditing(true)} className="edit-button" tooltip="Edit">
//<i className="fas fa-edit"></i>
//</button>