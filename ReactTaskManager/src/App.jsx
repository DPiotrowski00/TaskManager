import LogInPage from "../pages/LogInPage";
import TaskPage from "../pages/TaskPage";
import RegisterPage from "../pages/RegisterPage";

import NavBar from "../components/NavBar";

import { BrowserRouter, Routes, Route } from "react-router-dom";

import "../styles/DefaultPageStyle.css";

export default function App() {
    return (
        <>
            <BrowserRouter>
                <NavBar />
                <Routes>
                    <Route path="/" element={<LogInPage />} />
                    <Route path="/tasks" element={<TaskPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                </Routes>
            </BrowserRouter>
        </>
    );
}