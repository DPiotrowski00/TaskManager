export default function useLogIn() {
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
        const result = await response.text();
        if (result === "") {
            return false;
        }
        else {
            localStorage.setItem("token", result);
            localStorage.setItem("user", login);
            return true;
        }
    }

    async function tryRegister(login, password) {
        const response = await fetch("https://localhost:7176/login/register", {
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

    return { validateLogIn, tryRegister };
}