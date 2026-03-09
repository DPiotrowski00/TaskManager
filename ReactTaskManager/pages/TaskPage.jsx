import { useState, useEffect } from 'react'

import useTasks from "../hooks/useTasks";

import FilterBar from "../components/FilterBar";
import TaskList from "../components/TaskList";
import TaskForm from "../components/TaskForm";

export default function TaskPage() {
    const { tasks, addTask, toggleTask, deleteTask, deleteCompleted } = useTasks();
    const [filter, setFilter] = useState("All");
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
            <TaskForm addTask={addTask} />
            <FilterBar filter={filter} setFilter={setFilter} unfinishedTaskCount={unfinishedTaskCount} deleteCompleted={deleteCompleted} />
            <TaskList tasks={filteredTasks} onCompletedClick={toggleTask} onDeleteClick={deleteTask} />
        </>
    );
}