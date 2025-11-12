import React from 'react';

const PRIORITY_LABELS = {
    0: 'Low',
    1: 'Normal',
    2: 'High',
};

const cardStyle = {
    backgroundColor: 'white',
    margin: '8px 0',
    padding: '10px',
    borderRadius: '4px',
    boxShadow: '0 1px 0 rgba(9,30,66,.25)',
    cursor: 'pointer',
};

const TaskCard = ({ task, onEdit }) => {
    const priorityText = PRIORITY_LABELS[task.priority] || 'N/A';

    let priorityColor = '#666';
    if (task.priority === 2) {
        priorityColor = '#f44336';
    } else if (task.priority === 1) {
        priorityColor = '#ff9800';
    } else if (task.priority === 0) {
        priorityColor = '#4caf50';
    }

    return (
        <div style={cardStyle} onClick={() => onEdit(task)}>
            <strong>{task.title}</strong>
            <p style={{
                margin: '5px 0 0',
                fontSize: '0.9em',
                color: priorityColor,
                fontWeight: 'bold'
            }}>
                Priority: {priorityText}
            </p>
        </div>
    );
};

export default TaskCard;