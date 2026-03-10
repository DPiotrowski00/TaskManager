import { useState } from 'react'

export default function LogInForm({onLogInSucceeded}) {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");

    async function validateLogIn(login, password) {
        const response = await fetch("https://localhost:7176/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: login,
                password: password
            })
        });
        const result = await response.json();
        return result;
    }

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