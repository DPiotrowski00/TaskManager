import { useState, useEffect } from 'react';

async function getTasks() {
    const response = await fetch("https://localhost:7176/task", {
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        }
    });

    if (!response.ok) {
        throw new Error("Failed to fetch tasks");
    }

    return response.json();
}

export default function useTasks() {
    const [tasks, setTasks] = useState([]);

    async function loadTasks() {
        try {
            const data = await getTasks();
            setTasks(data);
        }
        catch (err) {
            console.error(err)
        }
    }

    useEffect(() => {
        loadTasks();
    }, []);

    async function toggleTask(id) {
        const updateTask = tasks.filter(t => t.id === id)[0];
        updateTask.completed = !updateTask.completed;

        await fetch("https://localhost:7176/task", {
            method: "PATCH",
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token"),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(updateTask)
        });

        loadTasks();
    }

    async function addTask(passText) {
        if (passText && passText !== "") {

            const newTask = {
                text: passText,
                creator: localStorage.getItem("user"),
                completed: false,
            }

            await fetch("https://localhost:7176/task", {
                method: "POST",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem("token"),
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newTask)
            })

            loadTasks();
            console.log("Task created");
        }
    }

    async function deleteTask(id) {
        if (window.confirm("Na pewno usunąć?")) {
            await fetch(`https://localhost:7176/task/${id}`, {
                method: "DELETE",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem("token")
                }
            });
            loadTasks();
        }
    }

    async function deleteCompleted() {
        const tasksToDelete = tasks.filter(t => t.completed);
        for (let i = 0; i < tasksToDelete.length; i++) {
            await fetch(`https://localhost:7176/task/${tasksToDelete[i].id}`, {
                method: "DELETE",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem("token")
                }
            });
        }
        loadTasks();
    }

    return {tasks, addTask, toggleTask, deleteTask, deleteCompleted};
}