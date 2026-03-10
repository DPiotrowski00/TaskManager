import LogInForm from "../components/LogInForm"
import { useNavigate } from "react-router-dom";

export default function LogInPage() {
    const navigate = useNavigate();

    function onLogInSucceeded(username) {
        navigate("/tasks", { state: { user: username } });
    }

    return (
        <LogInForm onLogInSucceeded={onLogInSucceeded} />
    );
}