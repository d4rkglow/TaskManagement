import React, { useState } from 'react';
import { useTaskContext } from './context/TaskContext';
import Modal from './components/common/Modal';
import TaskForm from './components/taskgrid/TaskForm';
import TaskGrid from './components/taskgrid/TaskGrid';

function App() {
  const { loading, error } = useTaskContext();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingTask, setEditingTask] = useState(null);

  const handleOpenCreateModal = () => {
    setEditingTask(null);
    setIsModalOpen(true);
  };

  const handleOpenEditModal = (task) => {
    setEditingTask(task);
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setEditingTask(null);
  };

  if (loading) {
    return <h1>Loading tasks from API...</h1>;
  }

  if (error) {
    return (
      <div style={{ color: 'red', border: '1px solid red', padding: '10px' }}>
        <h2>Error!</h2>
        <p>{error}</p>
        <p>Ensure your Docker backend is running on http://localhost:8080.</p>
      </div>
    );
  }

  return (
    <div style={{ padding: '20px' }}>
      <h1>Task Manager Dashboard</h1>

      <button 
        onClick={handleOpenCreateModal} 
        style={{ marginBottom: '20px', padding: '10px 15px', cursor: 'pointer' }}
      >
        + Add New Task
      </button>

      <TaskGrid onEditTask={handleOpenEditModal} />

      <Modal
        isOpen={isModalOpen}
        onClose={handleCloseModal}
        title={editingTask ? "Edit Task" : "Create New Task"}
      >
        <TaskForm
          onClose={handleCloseModal}
          initialTask={editingTask}
        />
      </Modal>

    </div>
  );
}

export default App;