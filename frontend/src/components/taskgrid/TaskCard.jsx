import React from 'react';

const cardStyle = {
    backgroundColor: 'white', 
    margin: '8px 0', 
    padding: '10px', 
    borderRadius: '4px',
    boxShadow: '0 1px 0 rgba(9,30,66,.25)',
    cursor: 'pointer', // Indicate it's clickable
};

const TaskCard = ({ task, onEdit }) => {
    return (
        <div style={cardStyle} onClick={() => onEdit(task)}>
            <strong>{task.title}</strong>
            <p style={{ margin: '5px 0 0', fontSize: '0.9em', color: '#666' }}>
                Priority: {task.priority}
            </p>
        </div>
    );
};

export default TaskCard;