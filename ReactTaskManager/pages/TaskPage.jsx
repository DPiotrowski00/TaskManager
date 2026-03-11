import { useState, useEffect } from 'react'

import useTasks from "../hooks/useTasks";
import { Navigate } from "react-router-dom";

import FilterBar from "../components/FilterBar";
import TaskList from "../components/TaskList";
import TaskForm from "../components/TaskForm";

export default function TaskPage() {

    const token = localStorage.getItem("token");
    if (!token) {
        return <Navigate to="/" />
    }

    const { tasks, addTask, toggleTask, deleteTask, deleteCompleted } = useTasks();
    const [filter, setFilter] = useState("All");
    const user = localStorage.getItem("user");

    let filteredTasks = tasks;
    if (filter === "Completed") {
        filteredTasks = filteredTasks.filter(t => t.completed);
    }
    if (filter === "Uncompleted") {
        filteredTasks = filteredTasks.filter(t => !t.completed);
    }

    filteredTasks = [...filteredTasks].sort((a, b) => b.id - a.id);
    const unfinishedTaskCount = tasks.filter(t => !t.completed).length;

    return (
        <>
            <p>Zalogowano jako: {user}</p>
            <TaskForm addTask={addTask} />
            <FilterBar filter={filter} setFilter={setFilter} unfinishedTaskCount={unfinishedTaskCount} deleteCompleted={deleteCompleted} />
            <TaskList tasks={filteredTasks} onCompletedClick={toggleTask} onDeleteClick={deleteTask} />
        </>
    );
}