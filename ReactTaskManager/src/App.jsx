import LogInPage from "../pages/LogInPage";
import TaskPage from "../pages/TaskPage";
import RegisterPage from "../pages/RegisterPage";

import { BrowserRouter, Routes, Route } from "react-router-dom";

import "./TaskStyle.css"

export default function App() {
    //const [loggedUser, setLoggedUser] = useState("");

    function onLogIn(username) {
        setLoggedUser(username);
    }

    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<LogInPage />} />
                <Route path="/tasks" element={<TaskPage />} />
                <Route path="/register" element={<RegisterPage />} />
            </Routes>
        </BrowserRouter>
    );
}