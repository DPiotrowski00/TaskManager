import { useState } from 'react'
import useLogIn from "../hooks/useLogin"

export default function LogInForm({onLogInSucceeded}) {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const { validateLogIn } = useLogIn();

    async function handleButtonClick() {
        if (login !== "" && password !== "") {
            if (await validateLogIn(login, password)) {
                onLogInSucceeded(login);
            }
        }
        else {
            console.log("Nie wpisano loginu lub hasła!");
        }
    }

    return (
        <div>
            <input type="text" name="LogIn" value={login} onChange={(e) => setLogin(e.target.value)} />
            <input type="password" name="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <button onClick={handleButtonClick}>Zaloguj</button>
        </div>
    );
}