import React from 'react';
import { useTaskContext } from '../../context/TaskContext';
import TaskCard from './TaskCard';

const STATUSES = {
    0: { id: 0, title: 'New', color: '#607d8b' },
    1: { id: 1, title: 'Active', color: '#2196f3' },
    2: { id: 2, title: 'Resolved', color: '#4caf50' },
    3: { id: 3, title: 'Closed', color: '#f44336' },
};

const TaskGrid = ({ onEditTask }) => {
    const { tasks } = useTaskContext();

    const groupedTasks = tasks.reduce((acc, task) => {
        const statusId = task.status;
        
        if (!acc[statusId]) {
            acc[statusId] = [];
        }
        acc[statusId].push(task);
        return acc;
    }, {});

    return (
        <div style={{ 
            display: 'flex', 
            gap: '20px', 
            overflowX: 'auto', 
            padding: '10px 0',
            backgroundColor: '#eee'
        }}>
            {Object.keys(STATUSES).map(statusKey => {
                const status = STATUSES[statusKey];
                const columnTasks = groupedTasks[status.id] || [];

                return (
                    <div 
                        key={status.id}
                        style={{ 
                            flex: '0 0 320px',
                            backgroundColor: '#f4f5f7',
                            borderRadius: '5px',
                            padding: '10px',
                            minHeight: '400px',
                            boxShadow: '0 2px 4px rgba(0,0,0,0.1)'
                        }}
                    >
                        <h3 style={{ 
                            borderBottom: `3px solid ${status.color}`, 
                            paddingBottom: '5px', 
                            color: status.color,
                            margin: '0 0 15px 0'
                        }}>
                            {status.title} ({columnTasks.length})
                        </h3>
                        
                        <div style={{ minHeight: '300px' }}>
                            {columnTasks.map(task => (
                                <TaskCard 
                                    key={task.id} 
                                    task={task} 
                                    onEdit={onEditTask}
                                />
                            ))}
                        </div>
                    </div>
                );
            })}
        </div>
    );
};

export default TaskGrid;