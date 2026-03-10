import { useState } from "react";
import { useNavigate } from "react-router-dom"
import useLogIn from "../hooks/useLogin";

export default function RegisterPage() {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const { tryRegister } = useLogIn();
    const navigate = useNavigate();

    async function handleClick() {
        if (login === "" || password === "") {
            console.log("Login i hasło muszą być uzupełnione.");
        }
        else {
            if (await tryRegister(login, password)) {
                navigate("/");
            }
        }
    }

    return (
        <>
            <div>
                <input type="text" value={login} onChange={ (e) => setLogin(e.target.value) } />
                <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                <button onClick={handleClick}>Zarejestruj</button>
            </div>
        </>
    );
}