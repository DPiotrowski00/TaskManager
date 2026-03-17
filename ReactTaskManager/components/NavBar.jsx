import { Link } from "react-router-dom";
import "../styles/NavBarStyle.css";

export default function NavBar() {
    return (
        <nav className="nav">
            <Link className="nav" to="/tasks">Zadania</Link>
            <Link className="nav" to="/">Logowanie</Link>
            <Link className="nav" to="/register">Rejestracja</Link>
        </nav>
    );
}