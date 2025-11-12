import React, { createContext, useState, useEffect, useContext } from 'react';
import { getTasks, createTask, updateTask, deleteTask } from '../api/taskService';

const TaskContext = createContext();

export const useTaskContext = () => {
    return useContext(TaskContext);
};

export const TaskProvider = ({ children }) => {
    const [tasks, setTasks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const refreshTasks = async () => {
        try {
            const data = await getTasks();
            setTasks(data);
            setError(null);
        } catch (err) {
            setError("Failed to refresh tasks.");
        }
    };

    const addTask = async (taskData) => {
        try {
            await createTask(taskData);

            await refreshTasks();

            return;
        } catch (err) {
            console.error("Failed to add task:", err);
            throw err;
        }
    };

    const editTask = async (taskId, taskData) => {
        try {
            await updateTask(taskId, taskData);

            await refreshTasks();

            return;
        } catch (err) {
            console.error("Failed to add task:", err);
            throw err;
        }
    };

    const removeTask = async (taskId) => {
        try {
            await deleteTask(taskId);

            await refreshTasks();

            return;
        } catch (err) {
            console.error("Failed to add task:", err);
            throw err;
        }
    };

    useEffect(() => {
        const fetchTasks = async () => {
            setLoading(true);
            await refreshTasks();
            setLoading(false);
        };
        fetchTasks();
    }, []);

    const contextValue = {
        tasks,
        loading,
        error,
        addTask,
        editTask,
        removeTask,
    };

    return (
        <TaskContext.Provider value={contextValue}>
            {children}
        </TaskContext.Provider>
    );
};