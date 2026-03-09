import LogInPage from "../pages/LogInPage"
import TaskPage from "../pages/TaskPage"

import { useState } from 'react'

import "./TaskStyle.css"

export default function App() {
    const [loggedUser, setLoggedUser] = useState("");

    function onLogIn(username) {
        setLoggedUser(username);
    }

    if (loggedUser === "") {
        return (
            <LogInPage onLogIn={onLogIn} />
        );
    }
    else {
        return (
            <>
                <p>Zalogowano jako: {loggedUser}</p>
                <TaskPage />
            </>
        );
    }
}