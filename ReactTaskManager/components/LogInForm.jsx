import { useState } from 'react'

export default function LogInForm({onLogInSucceeded}) {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");

    async function getPassword(login) {
        const pass = await fetch(`https://localhost:7176/login/${login}`)[0];
    }

    function handleButtonClick() {
        if (login !== "" && password !== "") {
            onLogInSucceeded(login);
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