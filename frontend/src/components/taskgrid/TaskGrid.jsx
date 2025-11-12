import React, { useMemo } from 'react';
import { useTaskContext } from '../../context/TaskContext';
import { DragDropContext, Droppable, Draggable } from '@hello-pangea/dnd';
import TaskCard from './TaskCard';

const STATUSES = {
    0: { id: 0, title: 'New', color: '#607d8b' },
    1: { id: 1, title: 'Active', color: '#2196f3' },
    2: { id: 2, title: 'Resolved', color: '#4caf50' },
    3: { id: 3, title: 'Closed', color: '#f44336' },
};

const TaskGrid = ({ onEditTask }) => {
    const { tasks, editTaskStatus } = useTaskContext();

    const handleStatusChange = async (taskId, newStatusId) => {
        const status = parseInt(newStatusId);
        try {
            await editTaskStatus(taskId, status);
        } catch (error) {
            console.error("Failed to update status:", error);
            alert('Failed to update task status. Please try again.');
        }
    };

    const onDragEnd = (result) => {
        const { destination, source, draggableId } = result;

        if (!destination || destination.droppableId === source.droppableId) {
            return;
        }

        handleStatusChange(draggableId, destination.droppableId);
    };

    const groupedTasks = useMemo(() => {
        return tasks.reduce((acc, task) => {
            const statusId = task.status;
            if (!acc[statusId]) { acc[statusId] = []; }
            acc[statusId].push(task);
            return acc;
        }, {});
    }, [tasks]);

    return (
        <DragDropContext onDragEnd={onDragEnd}>
            <div style={{ 
                display: 'flex', 
                gap: '20px', 
                overflowX: 'auto', 
                padding: '10px 0',
                backgroundColor: '#eee',
                width: '100%',
                WebkitOverflowScrolling: 'touch',
            }}>
                {Object.keys(STATUSES).map(statusKey => {
                    const status = STATUSES[statusKey];
                    const columnTasks = groupedTasks[status.id] || [];

                    return (
                        <Droppable key={status.id} droppableId={String(status.id)}>
                            {(provided, snapshot) => (
                                <div 
                                    ref={provided.innerRef}
                                    {...provided.droppableProps}
                                    style={{
                                        flex: '0 0 320px', 
                                        maxWidth: '90vw', 
                                        minWidth: '280px',
                                        backgroundColor: snapshot.isDraggingOver ? '#e0e0e0' : '#f4f5f7',
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
                                        {columnTasks.map((task, index) => (
                                            <Draggable key={task.id} draggableId={String(task.id)} index={index}>
                                                {(dragProvided) => (
                                                    <div
                                                        ref={dragProvided.innerRef}
                                                        {...dragProvided.draggableProps}
                                                        {...dragProvided.dragHandleProps}
                                                    >
                                                        <TaskCard 
                                                            task={task} 
                                                            onEdit={onEditTask} 
                                                        />
                                                    </div>
                                                )}
                                            </Draggable>
                                        ))}
                                    </div>

                                    {provided.placeholder}
                                </div>
                            )}
                        </Droppable>
                    );
                })}
            </div>
        </DragDropContext>
    );
};

export default TaskGrid;