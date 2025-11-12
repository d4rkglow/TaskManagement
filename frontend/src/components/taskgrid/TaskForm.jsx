import React, { useState } from 'react';
import { useTaskContext } from '../../context/TaskContext';

const STATUS_OPTIONS = [
    { value: 0, label: 'New' },
    { value: 1, label: 'Active' },
    { value: 2, label: 'Resolved' },
    { value: 3, label: 'Closed' },
];

const PRIORITY_OPTIONS = [
    { value: 0, label: 'Low' },
    { value: 1, label: 'Normal' },
    { value: 2, label: 'High' },
];

const TaskForm = ({ onClose, initialTask }) => {
    const isEditMode = !!initialTask;

    const { addTask, editTask, removeTask } = useTaskContext();

    const [formData, setFormData] = useState(
        initialTask || {
            status: STATUS_OPTIONS[0].value,
            priority: PRIORITY_OPTIONS[1].value,
        }
    );
    const [loading, setLoading] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: (name === 'status' || name === 'priority') ? parseInt(value) : value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!formData.title.trim()) {
            alert("Task title is required.");
            return;
        }

        try {
            setLoading(true);

            if (isEditMode) {
                await editTask(initialTask.id, formData);
            } else {
                await addTask(formData);
            }

            onClose();
        } catch (error) {
            alert(`Failed to ${isEditMode ? 'update' : 'save'} task.`);
            console.error(error);
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async () => {
        if (!window.confirm(`Are you sure you want to delete task "${initialTask.title}"?`)) {
            return;
        }
        setLoading(true);
        try {
            await removeTask(initialTask.id);
            onClose();
        } catch (error) {
            alert('Failed to delete task.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div style={{ marginBottom: '10px' }}>
                <label>Title:</label>
                <input
                    type="text"
                    name="title"
                    value={formData.title}
                    onChange={handleChange}
                    required
                    style={{ width: '100%', padding: '8px', boxSizing: 'border-box' }}
                />
            </div>
            <div style={{ marginBottom: '10px' }}>
                <label>Description:</label>
                <textarea
                    name="description"
                    value={formData.description}
                    onChange={handleChange}
                    style={{ width: '100%', padding: '8px', boxSizing: 'border-box' }}
                />
            </div>
            <div style={{ marginBottom: '20px', display: 'flex', gap: '20px' }}>
                <div>
                    <label>Status:</label>
                    <select name="status" value={formData.status} onChange={handleChange}>
                        {STATUS_OPTIONS.map(option => (
                            <option key={option.value} value={option.value}>
                                {option.label}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <label>Priority:</label>
                    <select name="priority" value={formData.priority} onChange={handleChange}>
                        {PRIORITY_OPTIONS.map(option => (
                            <option key={option.value} value={option.value}>
                                {option.label}
                            </option>
                        ))}
                    </select>
                </div>
            </div>
            <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'space-between' }}>
                <div>
                    <button type="submit" disabled={loading} style={{ padding: '8px 15px' }}>
                        {loading ? (isEditMode ? 'Updating...' : 'Saving...') : (isEditMode ? 'Update Task' : 'Save Task')}
                    </button>
                    <button type="button" onClick={onClose} style={{ marginLeft: '10px' }}>
                        Cancel
                    </button>
                </div>
                
                {isEditMode && (
                    <button
                        type="button"
                        onClick={handleDelete}
                        disabled={loading}
                        style={{ background: 'red', color: 'white', padding: '8px 15px' }}
                    >
                        {loading ? 'Deleting...' : 'Delete'}
                    </button>
                )}
            </div>
        </form>
    );
};

export default TaskForm;