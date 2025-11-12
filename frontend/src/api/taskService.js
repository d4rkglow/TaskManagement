import axios from 'axios';
import config from './config';

const { API_BASE_URL, API_KEY } = config;

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'X-API-KEY': API_KEY,
    'Content-Type': 'application/json',
  },
});

export const getTasks = async () => {
  try {
    const response = await apiClient.get('Task/GetList'); 
    return response.data;
  } catch (error) {
    console.error("Error fetching tasks:", error);
    throw error;
  }
};

export const createTask = async (taskData) => {
  try {
    const response = await apiClient.post('Task', taskData); 
    return response.data;
  } catch (error) {
    console.error("Error creating task:", error);
    throw error;
  }
};

export const updateTask = async (taskId, taskData) => {
  try {
    const response = await apiClient.put(`/Task/${taskId}`, taskData); 
    return response.data; 
  } catch (error) {
    console.error(`Error updating task ${taskId}:`, error);
    throw error;
  }
};

export const updateTaskStatus = async (taskId, status) => {
  try {
    const response = await apiClient.patch(`/Task/${taskId}/status`, null, {
      params: {
        status: status
      }
    }); 
    return response.data; 
  } catch (error) {
    console.error(`Error updating task status ${taskId}:`, error.response?.data || error.message);
    throw error;
  }
};

export const deleteTask = async (taskId) => {
  try {
    await apiClient.delete(`/Task/${taskId}`);
    return true;
  } catch (error) {
    console.error(`Error deleting task ${taskId}:`, error);
    throw error;
  }
};